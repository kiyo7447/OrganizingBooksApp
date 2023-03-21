/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const int StartPage = 1;
			const int EndPage = 8859;
			const string UrlTemplate = "https://manga-zip.is/post/page/{0}";

			var httpClient = new HttpClient();
			var titles = new SortedDictionary<string, string>();

			for (int page = StartPage; page <= EndPage; page++)
			{
				Console.WriteLine($"Crawling page {page}...");

				var url = string.Format(UrlTemplate, page);
				var html = await httpClient.GetStringAsync(url);
				var doc = new HtmlDocument();
				doc.LoadHtml(html);

				var links = doc.DocumentNode.SelectNodes("//a[@rel='bookmark']");

				if (links == null)
				{
					Console.WriteLine($"No bookmarks found on page {page}.");
					continue;
				}

				foreach (var link in links)
				{
					var text = link.InnerText.Trim();
					var match = Regex.Match(text, @"(.+)\[(.+)\]");

					if (match.Success)
					{
						var key = ParseData(match.Groups[2].Value," vol");
						var value = Regex.Replace(match.Groups[1].Value, @"第\d{2}.*", string.Empty); 
						titles[key] = value;
						Console.WriteLine($"Key={key}, Value={value}");
					}
					else
					{
						Console.WriteLine($"Error={text}");
					}
				}
			}

			using var writer = new StreamWriter("Title.txt");

			foreach (var title in titles)
			{
				Console.WriteLine($"{title.Key}, {title.Value}");
				await writer.WriteLineAsync($"{title.Key}\t{title.Value}");
			}
		}
		private static string ParseData(string rawValue, string keyWord)
		{
			var key = rawValue;
			var index = rawValue.IndexOf(keyWord, StringComparison.OrdinalIgnoreCase);
			if (index >= 0)
			{
				key = key.Substring(0, index);
			}

			return key;
		}


	}
}
*/
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace WebCrawler
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var httpClient = new HttpClient();
			var parser = new HtmlParser();
			var outputFile = "Title.txt";
			var baseUrl = "https://manga-zip.is/post/page/";

			using var streamWriter = File.CreateText(outputFile);

			for (int i = 1; i <= 8859; i++)
			{
				var url = $"{baseUrl}{i}";
				Console.WriteLine($"Processing page {i}");

				try
				{
					var response = await httpClient.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var document = await parser.ParseDocumentAsync(content);

						var titles = document.QuerySelectorAll("a[rel='bookmark']")
							.Select(element => element.TextContent);

						foreach (var title in titles)
						{
							await streamWriter.WriteLineAsync(title);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error processing page {i}: {ex.Message}");
				}
			}

			Console.WriteLine("Web crawling complete!");
		}
	}
}

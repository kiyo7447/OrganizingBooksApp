using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KeyValueExtractor
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputFile = "Title.txt";
			string outputFile = "TitleKeyValue.txt";

			var keyValuePairs = new SortedDictionary<string, string>();

			// ファイルを一行ずつ読み込みます
			using (var reader = new StreamReader(inputFile))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					// 正規表現で[]の中の文字列を抽出します
					var keyMatch = Regex.Match(line, @"\[(.*?)\]");
					if (keyMatch.Success)
					{
						string key = keyMatch.Groups[1].Value;
						string value = Regex.Replace(line, @"\[.*?\]", "").Trim();

						// KeyとValueに対して、指定された処理を行います
						key = CutAfterKey(key);
						value = CutAfterValue(value);

						// 重複したKeyの場合、上書きします
						if (keyValuePairs.ContainsKey(key))
						{
							keyValuePairs[key] = value;
						}
						else
						{
							keyValuePairs.Add(key, value);
						}
					}
				}
			}

			// ソートされたKey-Valueペアをファイルに書き込みます
			using (var writer = new StreamWriter(outputFile))
			{
				foreach (var pair in keyValuePairs)
				{
					writer.WriteLine($"{pair.Key},{pair.Value}");
				}
			}
		}

		// 「スペース+vol」以降の文字をカットするメソッドです
		static string CutAfterKey(string key)
		{
			int index = key.IndexOf(" vol", StringComparison.OrdinalIgnoreCase);
			return index >= 0 ? key.Substring(0, index) : key;
		}

		// 「スペース＋第」以降の文字をカットするメソッドです
		static string CutAfterValue(string value)
		{
			int index = value.IndexOf(" 第", StringComparison.OrdinalIgnoreCase);
			if (index >= 0)
			{
				return value.Substring(0, index);
			}
			// 「第01」や「第02」などの文字以降をカットします
			var match = Regex.Match(value, @"第\d{2}");
			if (match.Success)
			{
				index = value.IndexOf(match.Value, StringComparison.OrdinalIgnoreCase);
				return value.Substring(0, index);
			}
			return value;
		}
	}
}

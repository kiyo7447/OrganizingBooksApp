using System;
using System.IO;
using System.Text.RegularExpressions;

class FileNameConverter
{
	static void Main()
	{
		// 変換対象のディレクトリパス
		string targetDirectory = @"C:\Users\kiyot\OneDrive\Books\SourceDirectory";

		// 関数を呼び出してファイル名の変換を実行
		RemoveDigitsFromFileName(targetDirectory);

		Console.WriteLine("Filename conversion completed.");
	}

	static void RemoveDigitsFromFileName(string targetDirectory)
	{
		// ディレクトリ内のすべてのファイルを取得
		string[] filePaths = Directory.GetFiles(targetDirectory);

		// 正規表現パターンを作成
		string pattern = @"\(\d{13,16}\)";
		Regex regex = new Regex(pattern);

		foreach (string filePath in filePaths)
		{
			// ファイル名の取得
			string fileName = Path.GetFileName(filePath);

			// 正規表現で末尾の16桁の数字を探す
			Match match = regex.Match(fileName);

			if (match.Success)
			{
				// 新しいファイル名を作成
				string newFileName = regex.Replace(fileName, "");

				// ファイル名を変更
				string newFilePath = Path.Combine(targetDirectory, newFileName);
				File.Move(filePath, newFilePath);
				Console.WriteLine($"File renamed: {fileName} -> {newFileName}");
			}
		}
	}
}

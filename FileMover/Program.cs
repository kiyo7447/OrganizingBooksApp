using System;
using System.IO;

namespace FileMover
{
	class Program
	{
		static void Main(string[] args)
		{
			string sourceDirectory = @"C:\Users\kiyot\OneDrive\Books\Working"; // ここにソースフォルダのパスを入力してください
			string targetDirectory = @"C:\Users\kiyot\OneDrive\Books\SourceDirectory"; // ここにターゲットフォルダのパスを入力してください

			try
			{
				// ソースフォルダ内のすべてのファイルを移動します
				MoveFilesRecursively(sourceDirectory, targetDirectory);
				Console.WriteLine("ファイルの移動が完了しました。");
			}
			catch (Exception e)
			{
				Console.WriteLine($"エラーが発生しました: {e.Message}");
			}
		}
		static void MoveFilesRecursively(string source, string target)
		{
			// ソースディレクトリが存在しない場合、処理を終了します
			if (!Directory.Exists(source))
			{
				Console.WriteLine($"ソースディレクトリが存在しません: {source}");
				return;
			}

			// ターゲットディレクトリが存在しない場合、作成します
			if (!Directory.Exists(target))
			{
				Directory.CreateDirectory(target);
			}

			// ソースディレクトリ内のすべてのファイルを処理します
			foreach (string filePath in Directory.GetFiles(source))
			{
				string fileName = Path.GetFileName(filePath);
				string targetFilePath = Path.Combine(target, fileName);

				// ファイルが既に存在する場合、コンソールにメッセージを表示します
				if (File.Exists(targetFilePath))
				{
					Console.WriteLine($"ファイルが上書きされました: {targetFilePath}");
				}

				// ファイルを移動し、必要に応じて上書きします
				File.Move(filePath, targetFilePath, true);
			}

			// サブディレクトリを処理します
			foreach (string subDirectory in Directory.GetDirectories(source))
			{
				// サブディレクトリ内のファイルを再帰的に移動します
				MoveFilesRecursively(subDirectory, target);
			}
		}
	}
}

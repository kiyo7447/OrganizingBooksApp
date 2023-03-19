using System;
using System.IO;
using System.Text.RegularExpressions;
using Serilog;

namespace BookFileOrganizer
{
	class Program
	{
		static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			//string defaultInputDirectory = @"C:\Users\kiyot\OneDrive\Books\SourceDirectory";
			string defaultInputDirectory = @"C:\Users\kiyot\OneDrive\Books\Upload\26";
			string outputDirectory = @"C:\Users\kiyot\OneDrive\Books\まんが";

			string inputDirectory = args.Length > 0 ? args[0] : defaultInputDirectory;

			OrganizeBookFiles(inputDirectory, outputDirectory);

			Log.CloseAndFlush();
		}

		static void OrganizeBookFiles(string inputDirectory, string outputDirectory)
		{
			string[] filePaths = null;

			try
			{
				filePaths = Directory.GetFiles(inputDirectory);
			}
			catch (Exception ex)
			{
				Log.Error($"Error reading files from the input directory: {ex.Message}");
				return;
			}

			//Regex regex = new Regex(@"^(.+?)(_|\s)v?(\d+([\-sb]\d*)?(-\d{2,})?).*\.(zip|avif|rar)$", RegexOptions.IgnoreCase);
			//Regex regex = new Regex(@"^(.+?)(_|\s)v?(\d+([\-sb]\d*)?(-\d{2,})?|ch\d+).*\.(zip|avif|rar)$", RegexOptions.IgnoreCase);

			Regex regex = new Regex(@"^(.+?)(_|\s)(v|ch|Vol.)?(\d{2}(s|b)?)(\-\d{2})?(s|b|s_fix|fix|s_b|sf|e|_fix|AVIF|s fix| fix|\+a|_avif)*\.(zip|avif|rar)$", RegexOptions.IgnoreCase);
			foreach (string filePath in filePaths)
			{
				string fileName = Path.GetFileName(filePath);
				Match match = regex.Match(fileName);

				if (match.Success)
				{
					string bookTitle = match.Groups[1].Value;
					string bookFolder = FindOrCreateOutputFolder(outputDirectory, bookTitle);

					try
					{
						string destinationPath = Path.Combine(bookFolder, fileName);

						File.Move(filePath, destinationPath, true); // Pass 'true' to overwrite the existing file
						Log.Information($"Moved {fileName} to {bookFolder}");
					}
					catch (Exception ex)
					{
						Log.Error($"Error moving {fileName}: {ex.Message}");
					}
				}
			}
		}

		static string FindOrCreateOutputFolder(string outputDirectory, string bookTitle)
		{
			string bookFolderWithSpaces = Path.Combine(outputDirectory, bookTitle.Replace('_', ' '));
			string bookFolderWithUnderscores = Path.Combine(outputDirectory, bookTitle.Replace(' ', '_'));

			if (Directory.Exists(bookFolderWithSpaces))
			{
				return bookFolderWithSpaces;
			}
			else if (Directory.Exists(bookFolderWithUnderscores))
			{
				return bookFolderWithUnderscores;
			}
			else
			{
				// If neither folder exists, create one with underscores
				Directory.CreateDirectory(bookFolderWithUnderscores);
				//return bookFolderWithUnderscores;
				return bookFolderWithUnderscores;
			}
		}
	}
}

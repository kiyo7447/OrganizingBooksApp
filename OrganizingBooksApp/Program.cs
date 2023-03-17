using System;
using System.IO;
using System.Text.RegularExpressions;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookFileOrganizer
{
	class Program
	{
		static void Main(string[] args)
		{
			//string workingDirectory = @"C:\YourWorkingDirectoryPath";
			string workingDirectory = @"C:\Users\kiyot\OneDrive\Books\携帯の未振り分け１";
			//string workingDirectory = @"C:\Users\kiyot\OneDrive\Books\WorkingDirectoryPath";
			string outputDirectory = @"C:\Users\kiyot\OneDrive\Books\まんが集";
			OrganizeBookFiles(workingDirectory, outputDirectory);
		}

		static void OrganizeBookFiles(string directoryPath, string outputDirectory)
		{
			string[] filePaths = Directory.GetFiles(directoryPath);
		  //Regex regex = new Regex(@"^(.+?)_v(\d+([\-sb]\d*)?).*\.(zip|avif)$", RegexOptions.IgnoreCase);
		  //Regex regex = new Regex(@"^(.+?)(_|\s)v(\d+([\-sb]\d*)?).*\.(zip|avif)$", RegexOptions.IgnoreCase);
		  //Regex regex = new Regex(@"^(.+?)(_|\s)v(\d+([\-sb]\d*)?(\-\d+)?).*\.(zip|avif|rar)$", RegexOptions.IgnoreCase);
			Regex regex = new Regex(@"^(.+?)(_|_v|\s|\sv|_ch)(\d+([\-sb]\d*)?(-\d{2,})?).*\.(zip|avif|rar)$", RegexOptions.IgnoreCase);



			foreach (string filePath in filePaths)
			{
				string fileName = Path.GetFileName(filePath);
				Match match = regex.Match(fileName);

				if (match.Success)
				{
					string bookTitle = match.Groups[1].Value;
					string bookFolder = Path.Combine(outputDirectory, bookTitle);

					if (!Directory.Exists(bookFolder))
					{
						Directory.CreateDirectory(bookFolder);
					}

					string destinationPath = Path.Combine(bookFolder, fileName);
					File.Move(filePath, destinationPath, true);

					Console.WriteLine($"Moved {fileName} to {bookFolder}");
				}
			}
		}
	}
}

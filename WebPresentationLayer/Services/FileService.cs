using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebPresentationLayer.Services;

public class FileService
	// Помощен клас, който служи за създаването и изтриването на изображения, използвани 
	// за профилни снимки на потребителите и снимки на домашните животни
{
	private readonly IWebHostEnvironment _environment;

	public FileService(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	public void SaveMemoryStreamToFile(MemoryStream memoryStream, string folder, string fileName)
		// Метод, който запазва подадена снимка(под формата на MemmoryStream) като файл
		// с специфично име и директория
	{
	
		var path = Path.Combine(_environment.WebRootPath, folder);
		path = Path.Combine(path, fileName); // образува пътя на файла

		using (var fileStream = new FileStream(path, FileMode.Create))
		using (var streamWriter = new StreamWriter(fileStream))
		{
			memoryStream.WriteTo(fileStream); // запазва файла, там където е посочил пътя
		}
	}

	public void DeleteFile(string filePathAndName) // низът е под тази форма: "/pet/{Guid}.jpg"
												   // или "/account/{Guid}.jpg"
     // Метод, който изтрива файл чрез подаден параметър за пълният път към него
    {
		var pathArray = filePathAndName.Split('/', StringSplitOptions.RemoveEmptyEntries);
		var path = Path.Combine(_environment.WebRootPath, filePathAndName);
		path = Path.Combine(path, pathArray[1]);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
}

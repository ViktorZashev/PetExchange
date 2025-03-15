using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebPresentationLayer.Services;

public class FileService
{
	private readonly IWebHostEnvironment _environment;

	public FileService(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	public void SaveMemoryStreamToFile(MemoryStream memoryStream, string folder, string fileName)
	{
		var path = Path.Combine(_environment.WebRootPath, folder);
		path = Path.Combine(path, fileName);

		using (var fileStream = new FileStream(path, FileMode.Create))
		using (var streamWriter = new StreamWriter(fileStream))
		{
			memoryStream.WriteTo(fileStream);
			// Alternatively, you can write the content of the MemoryStream to a text file:
			// var content = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
			// streamWriter.WriteLine(content);
		}
	}

	public void DeleteFile(string filePathAndName)
	{
		var pathArray = filePathAndName.Split('/', StringSplitOptions.RemoveEmptyEntries);
		var path = Path.Combine(_environment.WebRootPath, pathArray[0]);
		path = Path.Combine(path, pathArray[1]);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
}

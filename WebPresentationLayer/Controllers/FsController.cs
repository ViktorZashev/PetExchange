using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;
using WebPresentationLayer.Services;

namespace WebPresentationLayer.Controllers
{
	public class FsController : Controller
	{
		private readonly FileService _fileSrv;
		public FsController(FileService fileService)
		{
			_fileSrv = fileService;
		}


		[HttpGet("/fs/{*path}")]
		public async Task<IActionResult> GetFile([FromRoute] string path)
		{
			return View();
		}


	}
}

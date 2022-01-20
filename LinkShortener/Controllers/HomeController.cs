
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Services.LinkShortener.Services.Helper;

namespace LinkShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UpdateDbService _updateDbService;

        public HomeController(IWebHostEnvironment hostingEnvironment, UpdateDbService updateDbService)
        {
            _hostingEnvironment = hostingEnvironment;
            _updateDbService = updateDbService;
        }

        public async Task<IActionResult> Index()
        {
            //await _updateDbService.UpdateTitleAndDescription();
            return View();
        }

        public IActionResult App()
        {
            return View();
        }

        public IActionResult Download()
        {
            var net = new System.Net.WebClient();
            var filePath = "App/LS1.apk";
            var basePath = _hostingEnvironment.WebRootPath;
            var link = basePath + "/" + filePath;
            var data = net.DownloadData(link);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            var fileName = "LS1.apk";
            return File(content, contentType, fileName);
        }

        public IActionResult Error(string code)
        {
            return View();
        }
    }
}

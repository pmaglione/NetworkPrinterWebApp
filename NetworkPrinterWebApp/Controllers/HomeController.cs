using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetworkPrinterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkPrinterWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PrintTest(string content = null, string ip = null, string port = null) 
        {
            var ipAddress = string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;
            int portNumber = string.IsNullOrEmpty(port) ? 9100 : int.Parse(port); //ie: 9100

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            client.Connect(ipAddress, portNumber);
            StreamWriter writer = new StreamWriter(client.GetStream());
            if (!string.IsNullOrEmpty(content))
                writer.WriteLine(content);
            writer.WriteLine("Hello World!");
            writer.Flush();
            writer.Close();
            client.Close();

            return View("Index");
        }
    }
}

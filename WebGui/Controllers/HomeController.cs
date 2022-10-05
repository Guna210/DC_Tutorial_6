using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGui.Models;

namespace WebGui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/getvalues");
            RestResponse response = client.Get(request);
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(response.Content);

            return View(customers);
        }
    }
}

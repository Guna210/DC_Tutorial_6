using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGui.Models;

namespace WebGui.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Customer";
            return View();
        }

        [HttpGet]
        [Route("/customer/go/{index}")]
        public IActionResult Go(int index)
        {
            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/getvalues/{index}", Method.Get);
            request.AddUrlSegment("index", index);
            RestResponse response = client.Execute(request);

            return Ok(response.Content);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Customer customer)
        {
            RestClient restClient = new RestClient("http://localhost:61741/");
            RestRequest restRequest = new RestRequest("api/students", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(customer));
            RestResponse restResponse = restClient.Execute(restRequest);

            Customer returnStudent = JsonConvert.DeserializeObject<Customer>(restResponse.Content);
            if (returnStudent != null)
            {
                return Ok(returnStudent);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/getvalues/{index}", Method.Delete);
            request.AddUrlSegment("index", index);
            RestResponse response = client.Execute(request);

            return Ok(response.Content);
        }

        [HttpPut]
        public IActionResult Update(int index, [FromBody]Customer customer)
        {
            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
            request.AddJsonBody(JsonConvert.SerializeObject(customer));
            RestResponse response = client.Put(request);

            return Ok(response.Content);
        }

        [HttpGet]
        public IActionResult search([FromBody]Customer customer)
        {
            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/getvalues");
            RestResponse response = client.Get(request);
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(response.Content);

            foreach(Customer cust in customers)
            {
                if(cust.lastname.Equals(customer.lastname))
                {
                    return Ok(JsonConvert.SerializeObject(cust));
                }
            }

            return BadRequest();
        }

        [HttpGet]
        public void generate()
        {
            RestClient client = new RestClient("http://localhost:61741/");
            RestRequest request = new RestRequest("api/generate");
            RestResponse response = client.Get(request);
        }
    }
}

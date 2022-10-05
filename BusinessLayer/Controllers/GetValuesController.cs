using BizGui;
using DataLayer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLayer.Controllers
{
    public class GetValuesController : ApiController
    {
        private RestClient client = new RestClient("http://localhost:61064/");
        

        public List<User> GetAll()
        {
            RestRequest request = new RestRequest("api/users");
            RestResponse response = client.Get(request);
            List<User> result = new List<User>();
            result = JsonConvert.DeserializeObject<List<User>>(response.Content);
            return result;
        }

        public User GetWithID(int id)
        {
            string req = "api/users/" + id.ToString();
            RestRequest request = new RestRequest(req);
            RestResponse response = client.Get(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        public User PostInsert([FromBody]User data)
        {
            RestRequest request = new RestRequest("api/users/", Method.Post);

            request.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse response = client.Execute(request);
            User user2 = JsonConvert.DeserializeObject<User>(response.Content);

            return user2;
        }

        public void PutUpdate(int id, [FromBody]User data)
        {
            string req = "api/users/" + id.ToString();
            RestRequest request = new RestRequest(req);

            request.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse response = client.Put(request);
        }

        public User DeleteWithID(int id)
        {
            string req = "api/users/" + id.ToString();
            RestRequest request = new RestRequest(req);
            RestResponse response = client.Delete(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);
            DataInterMed data = new DataInterMed();

            return user;
        }

        public void DeleteAll()
        {
            RestRequest request = new RestRequest("api/users");
            RestResponse response = client.Get(request);
            List<User> result = new List<User>();
            result = JsonConvert.DeserializeObject<List<User>>(response.Content);

            for (int i = 1; i <= result.Count; i++)
            {
                DeleteWithID(i);
            }
        }
    }
}

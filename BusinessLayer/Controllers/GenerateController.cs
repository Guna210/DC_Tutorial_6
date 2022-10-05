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
    public class GenerateController : ApiController
    {
        private Random rand = new Random();
        private string[] vowels = { "a", "e", "i", "o", "u" };
        private string[] letters = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

        public void GetGenerate()
        {
            GetValuesController getValuesController = new GetValuesController();
            List<User> users = getValuesController.GetAll();
            for (int i = users.Count+1; i <= users.Count+100; i++)
            {
                User user = new User();
                user.Index = i;
                user.acctNo = (int)AcctNo();
                user.pin = (int)PIN();
                user.balance = Balance();
                user.firstname = Firstname();
                user.lastname = Lastname();

                
                getValuesController.PostInsert(user);
            }
        }

        private string Firstname()
        {
            string FirstName = "";
            for (int i = 0; i < 3; i++)
            {
                string v = vowels[rand.Next(0, 5)];
                string l = letters[rand.Next(0, 21)];

                FirstName = String.Concat(FirstName, v, l);
            }

            return FirstName;
        }

        private string Lastname()
        {
            string LastName = "";
            for (int i = 0; i < 5; i++)
            {
                string v = vowels[rand.Next(0, 5)];
                string l = letters[rand.Next(0, 21)];

                LastName = String.Concat(LastName, v, l);
            }

            return LastName;
        }

        private uint PIN()
        {
            uint PIN = (uint)rand.Next(0, 9999);
            return PIN;
        }

        private uint AcctNo()
        {
            uint AcctNo = (uint)rand.Next(100000, 999999);
            return AcctNo;
        }

        private int Balance()
        {
            int Balance = rand.Next(-999999, 999999999);
            return Balance;
        }
    }
}

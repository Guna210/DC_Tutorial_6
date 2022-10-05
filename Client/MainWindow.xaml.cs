using BizGui;
using DataLayer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestClient restClient = new RestClient("http://localhost:61741/");
        private int total;
        private string searchValue;

        public MainWindow()
        {
            InitializeComponent();

            RestRequest request = new RestRequest("api/getvalues");
            RestResponse response = restClient.Get(request);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            total = users.Count;
            TotalNum.Text = "Total Users : "+total.ToString();
        }

        private void gobutton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            try
            {
                index = Int32.Parse(indexBox.Text);
                RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
                RestResponse response = restClient.Get(request);
                User data = JsonConvert.DeserializeObject<User>(response.Content);
                if ((index > total)||(index <= 0))
                {
                    TotalNum.Text = "No Record For Index Found!";
                }
                else
                {
                    firstnameBox.Text = data.firstname;
                    lastnameBox.Text = data.lastname;
                    balanceBox.Text = data.balance.ToString();
                    acctnoBox.Text = data.acctNo.ToString();
                    pinBox.Text = data.pin.ToString();
                }
            }
            catch (FormatException ex)
            {
                TotalNum.Text = "Invalid Input Format!";
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            searchValue = searchText.Text;
            RestRequest request = new RestRequest("api/getvalues");
            RestResponse response = restClient.Get(request);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            User user = new User();

            foreach(User u in users)
            {
                if(u.lastname == searchValue)
                {
                    user = u;
                    break;
                }
                else
                {
                    user.Index = -1;
                }
            }

            if(user.Index != -1)
            {
                indexBox.Text = user.Index.ToString();
                firstnameBox.Text = user.firstname;
                lastnameBox.Text = user.lastname;
                balanceBox.Text = user.balance.ToString();
                acctnoBox.Text = user.acctNo.ToString();
                pinBox.Text = user.pin.ToString();
            }
            else
            {
                TotalNum.Text = "No Such Record Found!";
            }
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            User data = new User();
            try
            {
                int index = Int32.Parse(indexBox.Text);
                if(index <= total)
                {
                    TotalNum.Text = "A Record For Index ALready Exists!";
                }
                else
                {
                    data.Index = index;
                    data.acctNo = Int32.Parse(acctnoBox.Text);
                    data.pin = Int32.Parse(pinBox.Text);
                    data.balance = Int32.Parse(balanceBox.Text);
                    data.firstname = firstnameBox.Text;
                    data.lastname = lastnameBox.Text;

                    RestRequest request = new RestRequest("api/getvalues/");
                    request.AddJsonBody(JsonConvert.SerializeObject(data));
                    RestResponse response = restClient.Post(request);
                    total = total + 1;
                    TotalNum.Text = "Total Users : " + total.ToString();
                }
            }
            catch (FormatException ex)
            {
                TotalNum.Text = "Invalid Input Format!";
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = Int32.Parse(indexBox.Text);
                if((index <= 0)||(index > total))
                {
                    TotalNum.Text = "No Record For Index To Delete!";
                }
                else
                {
                    RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
                    RestResponse response = restClient.Delete(request);
                    User data = JsonConvert.DeserializeObject<User>(response.Content);
                    total = total - 1;
                    TotalNum.Text = "Total Users : " + total.ToString();
                }
            }
            catch (FormatException ex)
            {
                TotalNum.Text = "Invalid Input Format!";
            }
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            RestRequest request = new RestRequest("api/generate");
            RestResponse response = restClient.Get(request);

            total = total + 100;
            TotalNum.Text = "Total Users : " + total.ToString();
        }

        private void deleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            RestRequest request = new RestRequest("api/getvalues");
            RestResponse response = restClient.Delete(request);

            total = 0;
            TotalNum.Text = "Total Users : " + total.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            User user = new User();
            try
            {
                index = Int32.Parse(indexBox.Text);
                if((index > total)||(index <= 0))
                {
                    TotalNum.Text = "Invalid Index Number!";
                }
                else
                {
                    user.Index = index;
                    user.acctNo = Int32.Parse(acctnoBox.Text);
                    user.pin = Int32.Parse(pinBox.Text);
                    user.balance = Int32.Parse(balanceBox.Text);
                    user.firstname = firstnameBox.Text;
                    user.lastname = lastnameBox.Text;

                    RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
                    request.AddJsonBody(JsonConvert.SerializeObject(user));
                    RestResponse response = restClient.Put(request);

                    TotalNum.Text = "Successfully Updated!";
                }
            }
            catch (FormatException ex)
            {
                TotalNum.Text = "Invalid Input Format!";
            }
        }

        // Make the generate method and search method
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CoffeeManagementClient.Controllers
{
    public class BaseController : Controller
    {
        protected static HttpClient client;

        public BaseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7250/");
            //Dung media type la json
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}

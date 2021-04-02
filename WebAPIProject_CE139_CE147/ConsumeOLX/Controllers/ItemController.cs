using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using OLX.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeOLX.Controllers
{
    public class ItemController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44389/api");
        HttpClient client;
        public ItemController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<ItemViewModel> modelList = new List<ItemViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/item").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList= JsonConvert.DeserializeObject<List<ItemViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(ItemViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/item", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Update(int id)
        {
            ItemViewModel modelList = new ItemViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/item" +id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<ItemViewModel>(data);
            }
            return View("AddItem", modelList);
        }

        [HttpPost]
        public ActionResult Update(ItemViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/item/"+model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("AddItem", model);
        }

        public ActionResult Buy(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/item/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
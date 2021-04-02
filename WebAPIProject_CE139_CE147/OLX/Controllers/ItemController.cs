using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OLX.Models;





namespace OLX.Controllers
{
    public class ItemController : ApiController
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<Item> GetItems()
        {
            return db.Items.ToList();
        }

        public Item GetItems(int Id)
        {
            return db.Items.Find(Id);
        }

        [HttpPost]
        public HttpResponseMessage AddItem(Item model)
        {
            try
            {
                db.Items.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch(Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateItem(int id, Item model)
        {
            try
            {
                if(id==model.Id)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
               
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        public HttpResponseMessage DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if(item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}

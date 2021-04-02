using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace OLX.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;


        }
        public DbSet<Item> Items { get; set; }
    }
}
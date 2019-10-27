using DapperDemos.CustomQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.Entities
{
    public class Customer
    {
        [DbColumn("IdTest")]
        public int Id { get; set; }

        [DbColumn("FirstName")]
        public string FirstName { get; set; }

        [DbColumn("LastName")]
        public string LastName { get; set; }

        [DbColumn("City")]
        public string City { get; set; }

        [DbColumn("Country")]
        public string Country { get; set; }

        [DbColumn("Phone")]
        public string Phone { get; set; }

        [DbColumn("CustomerTypeId")]
        public int ?CustomerTypeId { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}", Id, FirstName, LastName, City, Country, Phone, CustomerTypeId);
        }
    }
}
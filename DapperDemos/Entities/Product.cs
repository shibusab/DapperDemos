using DapperDemos.CustomQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.Entities
{
    public class Product
    {
        [DbColumn("Id")]
        public int Id { get; set; }

        [DbColumn("ProductName")]
        public string ProductName { get; set; }

        [DbColumn("SupplierId")]
        public int SupplierId { get; set; }

        [DbColumn("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [DbColumn("Package")]
        public string Package { get; set; }

        [DbColumn("IsDiscontinued")]
        public bool IsDiscontinued { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", Id, ProductName, SupplierId, UnitPrice, Package, IsDiscontinued);
        }
    }
}

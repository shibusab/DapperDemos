using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.CustomQuery
{
    public class ColumnPositionAttribute:Attribute
    {
        public Int16 ColumnPosition { get; set; }
    }
}

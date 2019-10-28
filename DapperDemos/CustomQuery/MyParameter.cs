using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.CustomQuery
{
    public enum DataType { String, DateTime, Int,Decimal, Char };
    public enum QueryType { Procedure,Text};
    public class MyParameter
    {
        public string ParameterName { get; set; }
        public DataType DataType { get; set; }
        public string Value { get; set; }

        public MyParameter(string parameterName, DataType dataType, string value)
        {
            this.ParameterName = parameterName;
            this.DataType = dataType;
            this.Value = value;
        }
    }
}

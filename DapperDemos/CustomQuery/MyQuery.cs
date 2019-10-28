using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.CustomQuery
{
    public class MyQuery
    {
        private SqlConnection connection;
        public MyQuery(IDbConnection connection)
        {
            this.connection = connection as SqlConnection;
        }

        public List<T> Query<T>(string procedureName, List<MyParameter> parameters, QueryType queryType) where T : new()
        {
            List<T> results = new List<T>();
            using (SqlCommand command = new SqlCommand(procedureName, this.connection))
            {
                if( null != parameters && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var sqlParameter = new SqlParameter(parameter.ParameterName , TransformToSqlParameter(parameter.DataType));
                        sqlParameter.Value= parameter.Value;
                        command.Parameters.Add(sqlParameter);
                    }
                }
                if (queryType.Equals(QueryType.Procedure))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // result = reader.MapToList<T>();
                    results = reader.MapToList1<T>();
                }
            }
            return results;
        }

        private SqlDbType TransformToSqlParameter(DataType dataType)
        {
            var retval = SqlDbType.Int;
            switch(dataType)
            {
                case DataType.String: return SqlDbType.VarChar;
                case DataType.Char:return SqlDbType.Char;
                case DataType.DateTime: return SqlDbType.DateTime;
                case DataType.Int: return SqlDbType.Int;
                case DataType.Decimal: return SqlDbType.Decimal;
            }
            return retval;
        }

        public List<T> Query<T>(string query) where T : new()
        {
            List<T> results = new List<T>();
            using (SqlCommand command = new SqlCommand(query, this.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // result = reader.MapToList<T>();
                    results = reader.MapToList1<T>();
                }
            }
            return results;
        }
        public T QueryOne<T>(string query) where T : new()
        {
            T result = new T();
            using (SqlCommand command = new SqlCommand(query, this.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // result = reader.MapToSingle<T>();
                    result = reader.MapToSingle1<T>();
                    
                }
            }
            return result;
        }
    }
}

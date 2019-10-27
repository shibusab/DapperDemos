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

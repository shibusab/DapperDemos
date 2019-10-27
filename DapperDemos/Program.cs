using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=MyTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //with Dapper
            //ReadAllCustomers(connectionString);
            //ReadAllProducts(connectionString);
            //ReadSomeCustomersWithParameters(connectionString);
            //ReadSomeCustomersWithDynamicParameters(connectionString);

            //with custom Query
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = @"SELECT Id as IdTest, FirstName, LastName,City,Country,Phone,CustomerTypeID From Customer WHERE Country = 'USA'";
                var results = new CustomQuery.MyQuery(connection).Query<Entities.Customer>(query);
                var results2 = new CustomQuery.MyQuery(connection).QueryOne<Entities.Customer>(query);

            }
            Console.WriteLine("Hello World! Enter to Close");
            Console.ReadKey();

        }

      
        private static void ReadSomeCustomersWithDynamicParameters(string connectionString)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Country", "USA");

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"SELECT Id, FirstName, LastName,City,Country,Phone,CustomerTypeID From Customer WHERE Country = @Country";
                var results = db.Query<Entities.Customer>(query, queryParameters).ToList();

                foreach (var result in results)
                    Console.WriteLine(result.ToString());
            }
        }

        private static void ReadSomeCustomersWithParameters(string connectionString)
        {
            //var queryParameters = new DynamicParameters();
            //queryParameters.Add("@country", "USA");
            //queryParameters.Add("@parameter2", valueOfparameter2);

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"SELECT Id, FirstName, LastName,City,Country,Phone,CustomerTypeID From Customer WHERE Country =@country";
                var results = db.Query<Entities.Customer>(query , new { country="USA" }).ToList();

                foreach (var result in results)
                    Console.WriteLine(result.ToString());
            }
        }

        private static void ReadAllCustomers(string connectionString)
        {
           
            using ( IDbConnection db = new SqlConnection(connectionString) )
            {
                var results = db.Query<Entities.Customer>(@"SELECT Id, FirstName, LastName,City,Country,Phone,CustomerTypeID From Customer").ToList();

                foreach (var result in results)
                Console.WriteLine(result.ToString());
            }
        }

        private static void ReadAllProducts(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var results = db.Query<Entities.Product>("sp_GetAllProducts",  commandType: CommandType.StoredProcedure).ToList();

                foreach (var result in results)
                    Console.WriteLine(result.ToString());
            }

        }
    }
}
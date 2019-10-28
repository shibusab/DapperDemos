using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemos.CustomQuery
{
    public static class DataReaderExtensions
    {
        public static List<T> MapToList<T>(this DbDataReader dr) where T : new()
        {
            List<T> RetVal = null;
            var Entity = typeof(T);
            var PropDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    RetVal = new List<T>();
                    var Props = Entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropDict = Props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (dr.Read())
                    {
                        T newObject = new T();
                        for (int Index = 0; Index < dr.FieldCount; Index++)
                        {
                            if (PropDict.ContainsKey(dr.GetName(Index).ToUpper()))
                            {
                                var Info = PropDict[dr.GetName(Index).ToUpper()];
                                if ((Info != null) && Info.CanWrite)
                                {
                                    var Val = dr.GetValue(Index);
                                    Info.SetValue(newObject, (Val == DBNull.Value) ? null : Val, null);
                                }
                            }
                        }
                        RetVal.Add(newObject);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dr.Close();
            }
            return RetVal;
        }
    
        public static T MapToSingle<T>(this DbDataReader dr) where T : new()
        {
            T RetVal = new T();
            var Entity = typeof(T);
            var PropDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    var Props = Entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    PropDict = Props.ToDictionary(p => p.Name.ToUpper(),  p => p);

                    dr.Read();
                    for (int Index = 0; Index < dr.FieldCount; Index++)
                    {
                        if (PropDict.ContainsKey(dr.GetName(Index).ToUpper()))
                        {
                            var Info = PropDict[dr.GetName(Index).ToUpper()];
                            if ((Info != null) && Info.CanWrite)
                            {
                                var Val = dr.GetValue(Index);
                                Info.SetValue(RetVal, (Val == DBNull.Value) ? null : Val, null);
                            }
                        } 
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dr.Close();
            }
            return RetVal;
        }

        public static T MapToSingle1<T>(this DbDataReader dr) where T : new()
        {
            T retVal = new T();
            var mappings = WriteColumnMappings<T>(retVal);
            var theType = retVal.GetType();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    for (int Index = 0; Index < dr.FieldCount; Index++)
                    {
                        if(mappings.ContainsKey(dr.GetName(Index).ToUpper()))
                        {
                            var info =   theType.GetProperty( mappings[dr.GetName(Index).ToUpper()]);
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(Index);
                                info.SetValue(retVal, (val == DBNull.Value) ? null : val, null);
                            }
                        } 
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dr.Close();
            }
            return retVal;
        }
        public static List<T> MapToList1<T>(this DbDataReader dr) where T : new()
        {
            List<T> retVal = new List<T>();
            var mappings = WriteColumnMappings<T>(new T());
            var theType = typeof(T); ;
            try
            {
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        T newObject = new T();
                        for (int Index = 0; Index < dr.FieldCount; Index++)
                        {
                            if (mappings.ContainsKey(dr.GetName(Index).ToUpper()))
                            {
                                var info = theType.GetProperty(mappings[dr.GetName(Index).ToUpper()]);
                                if ((info != null) && info.CanWrite)
                                {
                                    var val = dr.GetValue(Index);
                                    info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                                }
                            }
                        }
                        retVal.Add(newObject);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dr.Close();
            }
            return retVal;
        }

        public static Dictionary<string,string> WriteColumnMappings<T>(T item) where T : new()
        {
            var retVal = new Dictionary<string, string>();

            // Get the PropertyInfo object:
            var properties = item.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                //string msg = "{0} property maps to {1} db column";
                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                if (columnMapping != null)
                {
                    var mapsto = columnMapping as DbColumnAttribute;
                    retVal.Add(mapsto.Name.ToUpper(), property.Name );
                    //Console.WriteLine(msg, property.Name, mapsto.Name);
                }
            }
            
            return retVal;
        }
    }
}

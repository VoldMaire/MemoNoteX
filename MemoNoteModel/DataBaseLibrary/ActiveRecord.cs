using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace MemoNoteModel
{
    public class ActiveRecord<T> where T: ActiveRecord<T>
    {
        [DataBaseProperty("Id")]
        public Guid Id { get; set; }

        public static Dictionary<Guid, T> Objects = new Dictionary<Guid, T>();

        public static ClassInfo Classinfo = null;

        private static SqlConnectionStringBuilder connBuilder = null;

        public ActiveRecord()
        {
            Id = Guid.NewGuid();
            Objects.Add(Id, (T)this);
            if(Classinfo == null)
                FillClassInfo();
        }

        public void FillClassInfo()
        {
            if (Classinfo == null)
            {
                Classinfo = new ClassInfo();
                Classinfo.TypeClass = this.GetType();
                DataBaseClassAttribute ClassAttribute = Attribute.GetCustomAttribute(Classinfo.TypeClass, typeof(DataBaseClassAttribute)) as DataBaseClassAttribute;
                Classinfo.NameTable = ClassAttribute.Name;
                foreach (PropertyInfo prop in typeof(T).GetProperties().ToList())
                {
                    System.Reflection.PropertyAttributes prat = prop.Attributes;
                    foreach (Attribute attr in Attribute.GetCustomAttributes(prop))
                    {
                        if (attr.GetType() == typeof(DataBasePropertyAttribute))
                        {
                            Classinfo.NameProperties.Add(((DataBasePropertyAttribute)attr).Name);
                            Classinfo.TypePropereties.Add(prop.PropertyType);
                            string propprog = prop.ToString();
                            propprog = propprog.Split(' ').ElementAt(1);
                            Classinfo.Properties.Add(propprog);
                            break;
                        }
                    }
                }
            }
        }

        public void Save()
        {
            string dbConnString = GetConnectionStr();
            SqlConnection dbConn = new SqlConnection(dbConnString);
            dbConn.Open();
            try
            {
                object found = Activator.CreateInstance(Classinfo.TypeClass);
                T res = (T)found;
                SqlCommand dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "SELECT * FROM [" + Classinfo.NameTable + "] WHERE (Id=@Id)";
                dbCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                dbCommand.Parameters["@Id"].Value = this.Id;
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                dbReader.Read();
                try
                {
                    for (int i = 0; i < dbReader.FieldCount; i++)
                    {
                        PropertyInfo propinfo = ((T)res).GetType().GetProperty(Classinfo.Properties.ElementAt(i));
                        if (dbReader["Id"] == DBNull.Value)
                        {
                            if (dbReader[Classinfo.NameProperties.ElementAt(i)].GetType() == typeof(string))
                                propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)].ToString().Trim());
                            else
                                propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)]);
                        }
                        else
                            propinfo.SetValue(found, null);
                    }                   
                }
                catch (Exception)
                {
                    res = null;
                }
                finally
                {
                    dbReader.Close();
                    dbConn.Close();
                }
                if (res == null)
                {
                    SqlCommand dbSaveCommand = dbConn.CreateCommand();
                    dbSaveCommand.CommandText = "INSERT INTO " + "[" + Classinfo.NameTable + "]" + " ( ";
                    foreach (string prop in Classinfo.NameProperties)
                    {
                        dbSaveCommand.CommandText += prop;
                        if (Classinfo.NameProperties.IndexOf(prop) != Classinfo.NameProperties.Count - 1)
                        {
                            dbSaveCommand.CommandText += ", ";
                        }
                    }
                    dbSaveCommand.CommandText += " ) VALUES (";
                    for (int i = 0; i < Classinfo.NameProperties.Count; i++)
                    {
                        string propprog = Classinfo.Properties.ElementAt(i);
                        string proptable = Classinfo.NameProperties.ElementAt(i);
                        dbSaveCommand.CommandText += "@" + proptable;
                        if (i != Classinfo.NameProperties.Count - 1)
                        {
                            dbSaveCommand.CommandText += ", ";
                        }
                        dbSaveCommand.Parameters.Add("@" + proptable, GetDBType(Classinfo.TypePropereties.ElementAt(i)));
                        if (Classinfo.TypeClass.GetProperty(propprog).GetValue(this) != null)
                            dbSaveCommand.Parameters["@" + proptable].Value = Classinfo.TypeClass.GetProperty(propprog).GetValue(this);
                        else
                            dbSaveCommand.Parameters["@" + proptable].Value = DBNull.Value;
                    }
                    dbSaveCommand.CommandText += ")";
                    dbConn.Open();
                    dbSaveCommand.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand dbSaveCommand = dbConn.CreateCommand();
                    dbSaveCommand.CommandText = "Update [" + Classinfo.NameTable + "] SET ";
                    foreach (string proptable in Classinfo.NameProperties)
                    {
                        if (proptable != "Id")
                        {
                            Type proptype = Classinfo.TypePropereties.ElementAt(Classinfo.NameProperties.IndexOf(proptable));
                            string propprog = Classinfo.Properties.ElementAt(Classinfo.NameProperties.IndexOf(proptable));
                            dbSaveCommand.CommandText += proptable + "=@" + proptable;
                            dbSaveCommand.Parameters.Add("@" + proptable, GetDBType(proptype));
                            if (Classinfo.TypeClass.GetProperty(propprog).GetValue(this) != null)
                                dbSaveCommand.Parameters["@" + proptable].Value = Classinfo.TypeClass.GetProperty(propprog).GetValue(this);
                            else
                                dbSaveCommand.Parameters["@" + proptable].Value = DBNull.Value;
                            if (Classinfo.NameProperties.IndexOf(proptable) != Classinfo.NameProperties.Count - 2)
                            {
                                dbSaveCommand.CommandText += ", ";
                            }
                        }
                    }
                    dbSaveCommand.CommandText += " WHERE Id=@Id";
                    dbSaveCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                    dbSaveCommand.Parameters["@Id"].Value = this.Id;
                    dbConn.Open();
                    dbSaveCommand.ExecuteNonQuery();
                }

            }

            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void Delete()
        {
            string dbConnString = GetConnectionStr();
            SqlConnection dbConn = new SqlConnection(dbConnString);
            try
            {                
                SqlCommand dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "DELETE FROM " + "[" + Classinfo.NameTable + "] WHERE Id = @Id";
                dbCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                dbCommand.Parameters["@Id"].Value = Classinfo.TypeClass.GetProperty("Id").GetValue(this);
                dbConn.Open();
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                dbConn.Close();
            }
        }

        public static T Find(string field, object value)
        {
            string dbConnString = GetConnectionStr();
            SqlConnection dbConn = new SqlConnection(dbConnString);
            dbConn.Open();
            try
            {
                SqlCommand dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "SELECT * FROM [" + Classinfo.NameTable + "] WHERE (" + field + "=@" + field + ")";
                dbCommand.Parameters.Add("@" + field, GetDBType(Classinfo.TypePropereties.ElementAt(Classinfo.NameProperties.IndexOf(field))));
                dbCommand.Parameters["@" + field].Value = value;
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                dbReader.Read();
                object found = Activator.CreateInstance(Classinfo.TypeClass);
                T res = (T)found;
                for (int i = 0; i < dbReader.FieldCount; i++)
                {
                    PropertyInfo propinfo = ((T)res).GetType().GetProperty(Classinfo.Properties.ElementAt(i));
                    if (dbReader[Classinfo.NameProperties.ElementAt(i)] != DBNull.Value)
                    {
                        if (dbReader[Classinfo.NameProperties.ElementAt(i)].GetType() == typeof(string))
                            propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)].ToString().Trim());
                        else
                            propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)]);
                    }
                    else
                        propinfo.SetValue(found, null);
                }
                return res;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public static T Find(string thisfieldname, string tablename, string field, object value)
        {
            string dbConnString = GetConnectionStr();
            SqlConnection dbConn = new SqlConnection(dbConnString);
            dbConn.Open();
            try
            {
                SqlCommand dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "SELECT * FROM [" + Classinfo.NameTable + "] WHERE "  + thisfieldname + " IN (SELECT " + field + " FROM " + "[" + tablename + "]" + " WHERE " + field + "=@" + field + ")";
                dbCommand.Parameters.Add("@" + field, GetDBType(Classinfo.TypePropereties.ElementAt(Classinfo.NameProperties.IndexOf(field))));
                dbCommand.Parameters["@" + field].Value = value;
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                dbReader.Read();
                object found = Activator.CreateInstance(Classinfo.TypeClass);
                T res = (T)found;
                for (int i = 0; i < dbReader.FieldCount; i++)
                {
                    PropertyInfo propinfo = ((T)res).GetType().GetProperty(Classinfo.Properties.ElementAt(i));
                    if (dbReader[Classinfo.NameProperties.ElementAt(i)] != DBNull.Value)
                    {
                        if (dbReader[Classinfo.NameProperties.ElementAt(i)].GetType() == typeof(string))
                            propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)].ToString().Trim());
                        else
                            propinfo.SetValue(found, dbReader[Classinfo.NameProperties.ElementAt(i)]);
                    }
                    else
                        propinfo.SetValue(found, null);
                }
                return res;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                dbConn.Close();
            }
        }

        private  static SqlDbType GetDBType(Type prop)
        {
            string type = prop.ToString().Substring(7);
            switch (type)
            {
                case "Int" : return SqlDbType.Int;
                case "Guid" : return SqlDbType.UniqueIdentifier;
                case "String" : return SqlDbType.NChar;
                case "DateTime" : return SqlDbType.DateTime;
                case "Double" : return SqlDbType.Float;
                default: return SqlDbType.Binary;
            }
        }

        protected static string GetConnectionStr()
        {
            connBuilder = new SqlConnectionStringBuilder();
            connBuilder.DataSource = @"(local)\SQLEXPRESS";
            connBuilder.InitialCatalog = "MemoNoteDB";
            connBuilder.IntegratedSecurity = true;
            connBuilder.Pooling = true;
            return connBuilder.ConnectionString;
        }
    }
}

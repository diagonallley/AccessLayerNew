using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace framework.dal

    {
    public class QueryMethods

        {

        #region searchandreplace
        public static string parameterString = "@";
        public static string querySearchString = "?";
        public static string ReplaceFirst(string query, string search, string replace)
            {
            int pos = query.IndexOf(search);
            if (pos < 0)
                {
                return query;
                }
            return query.Substring(0, pos) + replace + query.Substring(pos + search.Length);
            } //replaceMethod(string,"?",replaceWith)
        #endregion

        #region queryExtractMethods

        public static List<string> getQuery(List<string> queries, List<List<QueryParams>> list)
            {
            List<string> queryList = new List<string>();
            for (int i = 0; i < queries.Count; i++)
                {
                queries[i].Trim();
                string search = querySearchString;

                for (int j = 0; j < list[i].Count; j++)
                    {
                    string query = ReplaceFirst(queries[i], search, parameterString + list[i][j].fieldName.ToString()); //returns the changed string


                    Console.WriteLine(queries[i]);
                    queries[i] = query;


                    }
                queryList.Add(queries[i]);
                }

            return queryList;

            } //GetQuery New Implementation for multiple queries

        public static string getQuery(string query, List<List<QueryParams>> list)
            {
            query = query.Trim();
            string search = querySearchString;
            for (int i = 0; i < list.Count; i++)

                {
                for (int j = 0; j < list[i].Count; j++)
                    {
                    query = ReplaceFirst(query, search, parameterString + list[i][j].fieldName);


                    }
                }

            return query;

            } //GetQuery New Implementation for a single query


        #endregion

        #region execution
        public static void executeQueries(List<string> queries, List<List<QueryParams>> list)  //new implementation for list of queries with parameters
            {
            List<string> queryList = getQuery(queries, list);

            GenericQuery(queryList, list);
            }


        public static dynamic executeQueries(string query, List<List<QueryParams>> list)  //new implementation for single string
            {
            string queryEx = getQuery(query, list);


            //genericQuery(queryEx, list);
            return GenericQuery(queryEx, list);

            }

        public static dynamic executeQueries(string query)
            {

            //genericQuery(query);
            return GenericQuery(query);

            } //single query method without any parameters

        #endregion

        #region QueryMethods
        public static dynamic GenericQuery(string query)
            {
            using (IDbConnection connection = GenericDatabase.GetConnection())
                {
                connection.Open();
                IDbCommand command = GenericDatabase.GetCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;





                command.Connection = connection;
                string[] queryStartsWith = query.Split(" ");
                string queryStarts = queryStartsWith[0].ToLower();

                if (query.ToLower().Contains("select") && query.ToLower().Contains("count"))
                    {
                    Int32 data = Convert.ToInt32(command.ExecuteScalar());
                    return data;
                    }
                else if (queryStarts == "select")
                    {
                    List<DataRow> SelectList = new List<DataRow>();
                    DataTable dt = new DataTable();
                    IDataReader reader = command.ExecuteReader();
                    dt.Load(reader);

                   

                    foreach (DataRow row in dt.Rows)
                        {
                        SelectList.Add(row);

                        }

                    return SelectList;


                    }
                else if (queryStarts == "update" || queryStarts == "delete" || queryStarts == "create" || queryStarts == "insert")
                    {
                    command.ExecuteNonQuery();
                    return true;
                    }


                };
            return false;
            } //generic query method that takes no parameters


        public static dynamic GenericQuery(List<string> list, List<List<QueryParams>> paramlist)
        {
            using (IDbConnection connection = GenericDatabase.GetConnection())
            {

                connection.Open();
                DbTransaction transaction = (DbTransaction)connection.BeginTransaction();




                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!list[i].ToLower().Contains("select"))
                        {
                            IDbCommand cmd = GenericDatabase.GetCommand();

                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = list[i];



                            for (int j = 0; j < paramlist[i].Count; j++)
                            {
                                DbParameter prm = GenericDatabase.GetParameter();



                                prm.ParameterName = parameterString + (string)paramlist[i][j].fieldName;
                                prm.Value = (string)paramlist[i][j].value;
                                if (paramlist[i][j].dataType == dataType.String)
                                {
                                    prm.DbType = DbType.String;

                                }
                                else if (paramlist[i][j].dataType == dataType.Integer)
                                {
                                    prm.DbType = DbType.Int32;
                                }
                                cmd.Parameters.Add(prm);
                                Console.WriteLine(cmd.Parameters.Count);

                            }




                            cmd.ExecuteNonQuery();

                        }


                    }
                    transaction.Commit();
                    return true;



                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                    return false;
                }
                finally { connection.Close(); }

            }



        } //multiple queries, multiple params



        public static dynamic GenericQuery(string query, List<List<QueryParams>> paramlist)
            {
            using (IDbConnection connection = GenericDatabase.GetConnection())
                {
                using (IDbCommand cmd = GenericDatabase.GetCommand())
                    {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    try
                        {
                        for (int i = 0; i < paramlist.Count; i++)
                            {
                            for (int j = 0; j < paramlist[i].Count; j++)
                                {
                                DbParameter prm = GenericDatabase.GetParameter();


                                prm.ParameterName = parameterString + (string)paramlist[i][j].fieldName;
                                prm.Value = (string)paramlist[i][j].value;
                                if (paramlist[i][j].dataType == dataType.String)
                                    {
                                    prm.DbType = DbType.String;

                                    }
                                else if (paramlist[i][j].dataType == dataType.Integer)
                                    {
                                    prm.DbType = DbType.Int32;
                                    }
                                cmd.Parameters.Add(prm);


                                }

                            }

                        query = query.Trim();
                        string[] queryStartsWith = query.Split(" ");

                        string select = queryStartsWith[0];
                        Console.WriteLine(select);

                        if (queryStartsWith[0] == "select" && query.Contains("count"))
                            {
                            Int32 data = Convert.ToInt32(cmd.ExecuteScalar());
                            return data;



                            }
                        else if (queryStartsWith[0] == "select")
                            {
                            connection.Open();
                            cmd.ExecuteNonQuery();

                            IDataReader reader = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            
                            List<DataRow> selectlist = new List<DataRow>() { };

                            //cmd.ExecuteReader();


                            while (reader.FieldCount >= 1)
                                {
                                dt.Load(reader);
                                foreach (DataRow row in dt.Rows)
                                    {
                                    selectlist.Add(row);
                                    }
                                return selectlist;
                                }
                            if (reader.FieldCount == 0)
                                {
                                return false;
                                }




                            }
                        else if (queryStartsWith[0].ToLower() == "create" || queryStartsWith[0].ToLower() == "update" || queryStartsWith[0].ToLower() == "delete" || queryStartsWith[0].ToLower() == "insert")
                            {
                            cmd.ExecuteNonQuery();
                            return true;

                            }



                        //Console.WriteLine("Query Executed");

                        return false;

                        }
                    catch (SqlException e) { Console.WriteLine(e); return false; }
                    finally { connection.Close(); }
                    }
                }
            } //Single query, multiple params

        }





    #endregion




    }








    







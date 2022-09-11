using System.Data;

namespace framework.dal
    {
    public class Execution
        {

        #region params
        public static List<List<QueryParams>> listoflists = new List<List<QueryParams>>() {

            { new List<QueryParams>()
                {
                    new QueryParams("name","Joy",dataType.String,100),
                    new QueryParams("id","12345",dataType.Integer,4),
                 

                    //new QueryParams("@email","gkabos5@cmu.edu","string",50),
                    //new QueryParams("@join_date","2022-01-17","string",20),

                }
            }, { new List<QueryParams>()
                {

                    new QueryParams("id","2334",dataType.Integer,4),
                    new QueryParams("name","Flin",dataType.String,4),



                }
            },

        };

        public static List<List<QueryParams>> listofparams = new List<List<QueryParams>>(){ new List<QueryParams>()
                {
                    new QueryParams("name","Evelyn",dataType.String,100),


                }
            };

        public static List<string> listofqueries = new List<string>() {
                {"update student set name=? where id=?" },{ "update student set id=? where name=?"}/*{ "select * from student;"}*/
            };

        public static List<List<QueryParams>> listoflist = new List<List<QueryParams>>() { new List<QueryParams>()
                {
                    new QueryParams("name","Evelynn",dataType.String,100),


                }
            };
        #endregion

        public static void Main(string[] args)
            {

          /*  dynamic row = (QueryMethods.executeQueries("select * from student;"));

            foreach (DataRow r in row)
                {
                Console.WriteLine(r[1].ToString());
                }
            Console.WriteLine("__________||__________");
            dynamic value = QueryMethods.executeQueries("select count(id) from student;");
            Console.WriteLine(value);*/

            dynamic rows = QueryMethods.executeQueries("select * from student where name=?", listoflist);
            //dynamic rows = QueryMethods.executeQueries("update student set name='Evelynn' where name=?",listoflist);
            Console.WriteLine(rows[0][0]);
            

            }
        }

    }


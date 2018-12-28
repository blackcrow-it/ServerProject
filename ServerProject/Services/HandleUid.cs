using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Services
{
    public class HandleUid
    {
        public static string UidRollNumberStudent()
        {
            var roll = "";
            string queryString = "SELECT TOP 1 * FROM Students ORDER BY RollNumber DESC";
            string connectionString =
                "Server=tcp:serverproject.database.windows.net,1433;Initial Catalog=ServerProject;Persist Security Info=False;User ID=adminserver;Password=123a@123A;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var rl = reader.GetString(0);
                            var rls = reader.GetString(0);
                            Console.WriteLine(rl);
                            var ln = rls.Length;
                            Console.WriteLine(ln);
                            var stxt = rl.Substring(0, 3);
                            Console.WriteLine(stxt);
                            string snum = rls.Substring(3, ln);
                            var n = Int64.Parse(snum);
                            n += 1;
                            snum = n.ToString();
                            roll = stxt + snum;
                        }
                        Debug.WriteLine("Cos");
                    }
                    else
                    {
                        Debug.WriteLine("Khoong cos");
                        roll = "std1000";
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return roll;
        }
    }
}

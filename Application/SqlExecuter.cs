using ServiceStation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public static class SqlExecuter
    {
        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\программы\\ServiceStation\\domain1\\ServiceStation\\Database.mdf;Integrated Security=True;Current Language=Russian";
        private static SqlConnection Connection = new SqlConnection(connectionString);

        public static DataTable Select(string tableName)
        {
            var query = "SELECT * FROM " + tableName;

            Connection.Close();
            Connection.Open();

            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();
            SqlDataAdapter dataAdp = new SqlDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdp.Fill(dt);

            Connection.Close();
            return dt;
        }

        public static void Insert(IItem item)
        {
            var sb = new StringBuilder();
            sb.Append("INSERT INTO " + item.tableName);
            sb.Append(" (");
            foreach (var e in item.GetType().GetFields())
            {
                sb.Append(e.Name + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(") VALUES (");
            foreach (var e in item.GetType().GetFields())
            {
                sb.Append(" '" + e.GetValue(item) + "',");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            var command = new SqlCommand(sb.ToString(), Connection);
            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public static void Clear()
        {
            var query = "TRUNCATE TABLE Clients";
            Connection.Open();
            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public static DataTable Execute(string query, string tableName)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();
            SqlDataAdapter dataAdp = new SqlDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdp.Fill(dt);

            Connection.Close();
            return dt;
        }

        public static void Execute(string query)
        {
            Connection.Open();
            SqlCommand command = new SqlCommand(query, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}

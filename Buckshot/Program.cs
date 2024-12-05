using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buckshot
{
    internal class DB
    {
        SqlConnection SqlConnection = new SqlConnection(@"Data Source=510-10;Initial Catalog=Shotgun;Integrated Security=True");

        public void OpenConection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Closed)
            {
                SqlConnection.Open();
            }
        }
        public void CloseConection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Open)
            {
                SqlConnection.Close();
            }
        }
        public SqlConnection GetConection()
        {
            return SqlConnection;
        }
        
        static class Program
        {

            /// <summary>
            /// Главная точка входа для приложения.
            /// </summary>
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Auth());
            }
        }
    }

}

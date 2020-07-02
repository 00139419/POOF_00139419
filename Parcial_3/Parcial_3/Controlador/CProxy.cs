using System;
using System.Data;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;
using Npgsql;


namespace Parcial_3.Controlador
{
    public class CProxy
    {
        public interface IProxy 
        {
            DataTable query(string query);
            void nonQuery(string Password, String nonQuery);
        }

        public class Proxy : IProxy
        {
            public DataTable query(string query)
            {
               return ConnectionDb.ExecuteQuery(query);
            }

            public void nonQuery(string Password, string Nonquery)
            {
                try
                {
                    if (Password.Equals("123"))
                    {
                        ConnectionDb.ExecuteNonQuery(Nonquery);
                        MessageBox.Show("La Accion se realizo correctamente!");
                    }
                    else
                    {
                        throw new AccessException("La contraseña que ingreso no es correcta!");
                    }
                }
                catch (AccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private class ConnectionDb
        {
            
            private static readonly string connectionString =   "Server=127.0.0.1;" +
                                                                "Port=5432;" +
                                                                "User Id=postgres;" +
                                                                "Password=Daniel0356#;" +
                                                                "Database=Parcial_3;";

            public static DataTable ExecuteQuery(string query)
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                DataSet ds = new DataSet();

                conn.Open();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                da.Fill(ds);

                conn.Close();

                return ds.Tables[0];
            }

            public static void ExecuteNonQuery(string query)
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);

                conn.Open();

                NpgsqlCommand comm = new NpgsqlCommand(query, conn);
                comm.ExecuteNonQuery();

                conn.Close();
            }
          
        }
        
    }
}
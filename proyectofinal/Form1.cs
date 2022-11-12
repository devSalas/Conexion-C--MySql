using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace proyectofinal
{
    public partial class Form1 : Form
    {
        MySqlConnection myCon;

        public Form1()
        {
            InitializeComponent();
            conectar();
            llenarTabla();
        }
        private void conectar()
        {

            try
            {
                /*string server = "railway";
                string database = "containers-us-west-104.railway.app";
                string user = "root";
                string password = "aqkqJ4pUuwU3zTXtG56C";
                int port = 6125;
                string cadenaConecxion = "server=" + server + ";database=" + database + ";Uid=" + user + ";pwd=" + password+"port="+port;
                myCon= new MySqlConnection(cadenaConecxion);
                myCon.Open();*/
                string server = "localhost";
                string database = "mysql-prueba";
                string user = "root";
                string password = "";
               
                string cadenaConecxion = "server=" + server + ";database=" + database + ";Uid=" + user + ";pwd=" + password;
                myCon = new MySqlConnection(cadenaConecxion);
                myCon.Open();

            }
            catch (Exception ex)
            {

                MessageBox.Show("algo malo añ conectar");
            }

        }
        public void llenarTabla()
        {
            string query = "select id,nombre,edad,dev from usuario";
            Console.WriteLine(query);
            MySqlCommand comandoDB = new MySqlCommand(query, myCon);
            comandoDB.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                reader = comandoDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int n = dbTabla.Rows.Add();
                        dbTabla.Rows[n].Cells[0].Value = reader.GetString(0);
                        dbTabla.Rows[n].Cells[1].Value = reader.GetString(1);
                        dbTabla.Rows[n].Cells[2].Value = reader.GetString(2);
                        dbTabla.Rows[n].Cells[3].Value = reader.GetString(3);
                    }
                }
                else
                {
                    Console.Write("no hay trabajadores");
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine("algo malo al llamar");
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            string query = "";
            string mensajeError = "";
            if (txtNombre.Text == "" || txtEdad.Text == "" || txtProfesion.Text == "")
            {
                mensajeError = "hay un campo vacio, por favor llenar todos";
            }

            if (mensajeError == "")
            {
                query = "insert into usuario " +
                    "(nombre,edad,dev) values " +
                    "('" + txtNombre.Text + "' , '" + txtEdad.Text + "' , '" + txtProfesion.Text + "')";
                MySqlCommand comando = new MySqlCommand(query, myCon);
                comando.CommandTimeout = 60;
                MySqlDataReader reader;

                try
                {
                    reader = comando.ExecuteReader();
                    reader.Close();

                    dbTabla.Rows.Clear();
                    dbTabla.Refresh();
                    llenarTabla();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                MessageBox.Show(mensajeError);
            }
        }
    }
}

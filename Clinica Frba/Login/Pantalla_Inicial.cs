using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Clinica_Frba.Login
{
    public partial class Pantalla_Inicial : Form1
    {
        public Pantalla_Inicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    string usuario = textBox1.Text;
                    string pass = SHA256Encrypt(textBox2.Text);
                    string respuesta = "";

                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.login", conexion))
                    {
                        conexion.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = usuario;
                        cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass;
                        cmd.Parameters.Add("@respuesta", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        respuesta = (cmd.Parameters["@respuesta"].Value.ToString());
                        if (String.Equals(respuesta, "abrir sesion"))
                        {                                                           
                            new Pantalla_Seleccion_Rol(usuario).Show();
                        }
                        else
                        {
                            new Dialogo("" + respuesta, "Aceptar").ShowDialog();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        public string SHA256Encrypt(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

       
    }
}

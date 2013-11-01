using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Compra_de_Bono
{
    public partial class Compra : Form1
    {
        String usuario;
        int id_Afiliado;
        int id_Plan;

        public Compra(String unUsuario, String unRol)
        {
            InitializeComponent();
            usuario = unUsuario;

            if (String.Equals(unRol, "Afiliado"))
            {
                traer_Info_Afiliado(unUsuario);
                textBox1.Enabled = false;
                button2.Enabled = false;
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nroAfiliado = Convert.ToInt32(textBox1.Text);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand buscarUsuario = new SqlCommand("USE GD2C2013 SELECT u.Username FROM YOU_SHALL_NOT_CRASH.Afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) WHERE (Cast (Nro_Afiliado as varchar) + Cast(Digito_Familiar as varchar)) = '" +nroAfiliado+"'", conexion);
                    string usuarioBuscado = (string)buscarUsuario.ExecuteScalar();

                    traer_Info_Afiliado(usuarioBuscado);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        
        public void traer_Info_Afiliado(String unUsuario)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Traer_Info_Afiliado", conexion))
                    {
                        conexion.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = unUsuario;
                        cmd.Parameters.Add("@nroAfiliado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@precioConsulta", SqlDbType.Real).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@precioFarmacia", SqlDbType.Real).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@idPlan", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int nroAfiliado = Convert.ToInt32(cmd.Parameters["@nroAfiliado"].Value);
                        decimal precioConsulta = Convert.ToDecimal(cmd.Parameters["@precioConsulta"].Value);
                        decimal precioFarmacia = Convert.ToDecimal(cmd.Parameters["@precioFarmacia"].Value);
                        id_Afiliado = Convert.ToInt32(cmd.Parameters["@idAfiliado"].Value);
                        id_Plan = Convert.ToInt32(cmd.Parameters["@idPlan"].Value);
                        
                        textBox1.Text = nroAfiliado.ToString();                  
                        label2.Text = "Precio: " + precioConsulta.ToString();
                        label5.Text = "Precio: " + precioFarmacia.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

        }

        //finalizar compra
        private void button1_Click(object sender, EventArgs e)
        {
            decimal monto;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Comprar_Bonos", conexion))
                    {
                        conexion.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Value = id_Afiliado;
                        cmd.Parameters.Add("@idPlan", SqlDbType.Int).Value = id_Plan;
                        cmd.Parameters.Add("@cantBonosConsulta", SqlDbType.Int).Value = numericUpDown1.Value;
                        cmd.Parameters.Add("@cantBonosFarmacia", SqlDbType.Int).Value = numericUpDown2.Value;

                        cmd.ExecuteNonQuery();

                        new Dialogo("todo ok", "Aceptar").Show();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

        }

        
    }
}

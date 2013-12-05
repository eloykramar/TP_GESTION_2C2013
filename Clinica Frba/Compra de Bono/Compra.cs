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
        decimal precioBonoConsulta;
        decimal precioBonoFarmacia;


        public Compra(String unUsuario, String unRol)
        {
            InitializeComponent();
            usuario = unUsuario;

            if (String.Equals(unRol, "Afiliado"))
            {
                traer_Info_Afiliado(unUsuario);
                textBox1.Enabled = false;
                button2.Enabled = false;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                button1.Enabled = true;
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.Equals(textBox1.Text, "")) return;
            int nroAfiliado = Convert.ToInt32(textBox1.Text);
            string usuarioBuscado = "";


            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    
                    int existeAfiliado = getIdAfiliadoxNro(nroAfiliado.ToString());
                    if (existeAfiliado == 0)
                    {
                        numericUpDown1.Enabled = false;
                        numericUpDown2.Enabled = false;
                        button1.Enabled = false;
                        throw new Exception("No existe un afiliado con el numero buscado");
                    }

                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                    button1.Enabled = true;
                    
                    conexion.Open();
                    SqlCommand buscarUsuario = new SqlCommand("USE GD2C2013 SELECT u.Username FROM YOU_SHALL_NOT_CRASH.Afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) WHERE Nro_Afiliado = " +nroAfiliado, conexion);                    
                    usuarioBuscado = (string)buscarUsuario.ExecuteScalar();                            

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
                        precioBonoConsulta = Convert.ToDecimal(cmd.Parameters["@precioConsulta"].Value);
                        precioBonoFarmacia = Convert.ToDecimal(cmd.Parameters["@precioFarmacia"].Value);
                        id_Afiliado = Convert.ToInt32(cmd.Parameters["@idAfiliado"].Value);
                        id_Plan = Convert.ToInt32(cmd.Parameters["@idPlan"].Value);
                        
                        textBox1.Text = nroAfiliado.ToString();                  
                        label6.Text = precioBonoConsulta.ToString();
                        label7.Text = precioBonoFarmacia.ToString();
                        label8.Text = "Usuario afiliado: " + unUsuario;
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
            int existeAfiliado = getIdAfiliadoxNro(textBox1.Text);
            if (existeAfiliado == 0)
            {
                MessageBox.Show("No existe un afiliado con el numero buscado");
                return;
            }
            if (Convert.ToInt32(numericUpDown1.Value) == 0 && Convert.ToInt32(numericUpDown2.Value) == 0) return;
            int cantBonosConsulta = Convert.ToInt32(numericUpDown1.Value);
            int cantBonosFarmacia = Convert.ToInt32(numericUpDown2.Value);
            decimal monto = ((precioBonoConsulta * cantBonosConsulta) + (precioBonoFarmacia * cantBonosFarmacia));
            int idCompra;

            try
            {                  
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Registrar_Compra", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Value = id_Afiliado;
                        cmd.Parameters.Add("@cantBonosConsulta", SqlDbType.Int).Value = cantBonosConsulta;
                        cmd.Parameters.Add("@cantBonosFarmacia", SqlDbType.Int).Value = cantBonosFarmacia;
                        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;
                        cmd.Parameters.Add("@idCompra", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        idCompra = Convert.ToInt32(cmd.Parameters["@idCompra"].Value);
                    }
                    
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Comprar_Bonos", conexion))
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@fechaActual", SqlDbType.DateTime).Value = getFechaActual();
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Value = id_Afiliado;
                        cmd.Parameters.Add("@idPlan", SqlDbType.Int).Value = id_Plan;
                        cmd.Parameters.Add("@cantBonosConsulta", SqlDbType.Int).Value = cantBonosConsulta;
                        cmd.Parameters.Add("@cantBonosFarmacia", SqlDbType.Int).Value = cantBonosFarmacia;
                        cmd.Parameters.Add("@idCompra", SqlDbType.Int).Value = idCompra;

                        cmd.ExecuteNonQuery();                        
                    }                
                     
                    new Dialogo("Compra finalizada exitosamente ;Cantidad bonos consulta: " + cantBonosConsulta + ", precio unitario: " + precioBonoConsulta + ";Cantidad bonos farmacia: " + cantBonosFarmacia + ", precio unitario: " + precioBonoFarmacia + ";Monto total: " + monto+ ";idCompra: " +idCompra, "Aceptar").Show();                    
                }
            }
            catch (Exception ex)
            {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(textBox1.Text, ""))
            {
                string Str = textBox1.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros");
                    textBox1.Text = "";
                }
            }
        } 
    }
}

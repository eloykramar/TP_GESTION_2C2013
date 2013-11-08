﻿using System;
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
                    SqlCommand buscarUsuario = new SqlCommand("USE GD2C2013 SELECT u.Username FROM YOU_SHALL_NOT_CRASH.Afiliado a join YOU_SHALL_NOT_CRASH.Usuario u on (a.DNI = u.DNI_Usuario) WHERE Nro_Afiliado = '" +nroAfiliado+"'", conexion);
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
            int cantBonosConsulta = Convert.ToInt32(numericUpDown1.Value);
            int cantBonosFarmacia = Convert.ToInt32(numericUpDown2.Value);
            decimal monto = ((precioBonoConsulta * cantBonosConsulta) + (precioBonoFarmacia * cantBonosFarmacia));

            try
            {                  
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Comprar_Bonos", conexion))
                    {
                        conexion.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@fechaActual", SqlDbType.DateTime).Value = getFechaActual();
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Value = id_Afiliado;
                        cmd.Parameters.Add("@idPlan", SqlDbType.Int).Value = id_Plan;
                        cmd.Parameters.Add("@cantBonosConsulta", SqlDbType.Int).Value = cantBonosConsulta;
                        cmd.Parameters.Add("@cantBonosFarmacia", SqlDbType.Int).Value = cantBonosFarmacia;

                        cmd.ExecuteNonQuery();                        
                    }
                    
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Registrar_Compra", conexion))
                    {                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idAfiliado", SqlDbType.Int).Value = id_Afiliado;                        
                        cmd.Parameters.Add("@cantBonosConsulta", SqlDbType.Int).Value = cantBonosConsulta;
                        cmd.Parameters.Add("@cantBonosFarmacia", SqlDbType.Int).Value = cantBonosFarmacia;
                        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;

                        cmd.ExecuteNonQuery();        
                    }
                     
                    new Dialogo("Compra finalizada exitosamente ;Cantidad bonos consulta: " + cantBonosConsulta + ", precio unitario: " + precioBonoConsulta + ";Cantidad bonos farmacia: " + cantBonosFarmacia + ", precio unitario: " + precioBonoFarmacia + ";Monto total: " + monto, "Aceptar").Show();                    
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

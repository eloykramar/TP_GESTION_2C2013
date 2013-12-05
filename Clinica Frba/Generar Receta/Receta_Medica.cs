using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Generar_Receta
{
    public partial class Receta_Medica : Form1
    {   
        int idConsulta;
        int idReceta;
        int idAfiliado;
        public Receta_Medica(int idC, int idA, int idR)
        {
            idConsulta=idC;
            idReceta=idR;
            idAfiliado=idA;
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                //lleno con los bonos farmacia
                SqlCommand cmd = new SqlCommand("USE GD2C2013 SELECT ID_BONO_FARMACIA FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_RECETA_MEDICA= " + idReceta, conexion);
                SqlDataReader bonos = cmd.ExecuteReader();
                if (bonos.HasRows)
                {
                    while (bonos.Read())
                    {
                        listBox1.Items.Add(Convert.ToInt32(bonos["ID_BONO_FARMACIA"]));
                    }
                }
                bonos.Dispose();
                cmd.Dispose();
                conexion.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                int idB = Convert.ToInt32(listBox1.Items[listBox1.SelectedIndex]);
                new CompletarBono(idReceta, idAfiliado, idB).ShowDialog();
            }
            else MessageBox.Show("Seleccione un bono de la lista.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bono = textBox1.Text;
            if (bono.Length == 0) bono = "0";
            using (SqlConnection conexion = this.obtenerConexion())
            {
              conexion.Open();
              //verifico si el bono corresponde a ese afiliado
              SqlCommand cmd = new SqlCommand(string.Format("SELECT ID_AFILIADO FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_Bono_Farmacia = {0}", bono), conexion);
              int idAfiBono = ExecuteScalarOrZero(cmd);
              int nroAfiBono = getNroxIdAfiliado(idAfiBono.ToString());
              cmd.Dispose();
              
                if (idAfiBono > 0)
                {
                    cmd = new SqlCommand(string.Format("SELECT ID_RECETA_MEDICA FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_Bono_Farmacia = {0}", bono), conexion);
                    int idRecetaEnUso = ExecuteScalarOrZero(cmd);
                    cmd.Dispose();
                    if (idRecetaEnUso == 0)
                    {
                        if (getRaizAfi(nroAfiBono.ToString()) == getRaizAfi(getNroxIdAfiliado(idAfiliado.ToString()).ToString()))   //Si el bono corresponde al grupo familiar, entonces sigo
                        {
                            cmd = new SqlCommand(string.Format(
                                "SELECT ID_PLAN FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_Bono_Farmacia ={0}", bono), conexion);
                            int planBono = ExecuteScalarOrZero(cmd);
                            cmd.Dispose();
                            cmd = new SqlCommand(string.Format(
                                "SELECT ID_PLAN FROM YOU_SHALL_NOT_CRASH.AFILIADO WHERE ID_AFILIADO ={0}", idAfiliado), conexion);
                            int planAfi = ExecuteScalarOrZero(cmd);
                            cmd.Dispose();
                            if (planAfi != planBono)
                            {
                                MessageBox.Show("El Plan del Afiliado no coincide con el del bono.");
                                conexion.Close();
                                return;
                            }//si el plan coincide, sigo...


                            cmd = new SqlCommand(string.Format("UPDATE YOU_SHALL_NOT_CRASH.BONO_FARMACIA SET ID_RECETA_MEDICA={0}, FECHA_PRESCRIPCION_MEDICA='{1}' WHERE ID_BONO_FARMACIA={2}", idReceta, Convert.ToString(fechaActual), bono), conexion);
                            cmd.ExecuteNonQuery();
                            listBox1.Items.Add(Convert.ToInt32(bono));
                        }
                        else MessageBox.Show("El bono farmacia no corresponde a este afiliado.");
                    }
                    else MessageBox.Show("El bono ingresado ya fué utilizado.");
                }
                else MessageBox.Show("El bono ingresado es incorrecto.");
                conexion.Close();

            }
        }
    }
}
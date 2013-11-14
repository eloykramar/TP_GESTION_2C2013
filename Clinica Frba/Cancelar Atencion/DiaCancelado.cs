using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class DiaCancelado : Form1
    {
        int idProfesional;

        public DiaCancelado(int idP)
        {
            InitializeComponent();
            idProfesional = idP;
        }

        private void butCanc_Click(object sender, EventArgs e)
        {
            String hora_inicio = dateTimePickerHoraIni.Value.TimeOfDay.ToString();
            String hora_fin = dateTimePickerHoraFin.Value.TimeOfDay.ToString();
            String dia_inicio = dateTimePickerDiaIni.Value.ToString("d");
            String dia_fin = dateTimePickerDiaFin.Value.ToString("d");
            DateTime dia_hora_inicio;
            DateTime dia_hora_fin;

            dia_hora_inicio = Convert.ToDateTime(dia_inicio + " " + hora_inicio);
            dia_hora_fin = Convert.ToDateTime(dia_fin + " " + hora_fin); 

           
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(string.Format(
                        "SELECT ID_CANCELACION_DIA FROM YOU_SHALL_NOT_CRASH.CANCELACION_DIA WHERE ID_PROFESIONAL = {0} AND DiaHora_inicio = '{1}' AND DiaHora_Fin = '{2}'", idProfesional, dia_hora_inicio, dia_hora_fin), conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    cmd.Dispose();
                    reader.Close();

                    cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Cancelar_dia_rango", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_profesional", SqlDbType.Int).Value = idProfesional;
                    cmd.Parameters.Add("@DiaHora_inicio", SqlDbType.DateTime).Value = dia_hora_inicio;
                    cmd.Parameters.Add("@DiaHora_Fin", SqlDbType.DateTime).Value = dia_hora_fin;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    MessageBox.Show("Su turno ha sido cancelado satisfactoriamente");
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Este rango ya ha sido cancelado previamente");
                }
                    

                
                conexion.Close();

 
            }
           


            

        }
        
        
        private void butSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

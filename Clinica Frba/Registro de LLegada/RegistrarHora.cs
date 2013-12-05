using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Registro_de_LLegada
{
    public partial class RegistrarHora : Form1
    {
        int id_turno;

        public RegistrarHora(int idTurno)
        {
            InitializeComponent();
            id_turno = idTurno;
            dateTimePickerFecha.Value = this.fechaActual;

        }

        private void buttAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                String fecha = dateTimePickerFecha.Value.ToString("d");
                String hora = dateTimePickerHora.Value.TimeOfDay.ToString();
                DateTime fecha_llegada = Convert.ToDateTime(fecha + " " + hora);
                DateTime fecha_turno;
                DateTime horario_minimo_de_llegada = Convert.ToDateTime("01/01/2001 00:00:00.000");
                int hh_turno, mm_turno;
                String dia_turno;

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(string.Format(
                        "SELECT FECHA, ID_AFILIADO FROM YOU_SHALL_NOT_CRASH.TURNO WHERE ID_TURNO = {0}", id_turno), conexion);
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {
                        fecha_turno = Convert.ToDateTime(reader.GetSqlDateTime(0).Value);
                        dia_turno = fecha_turno.ToString("d");
                        hh_turno = Convert.ToInt32(fecha_turno.Hour);
                        mm_turno = Convert.ToInt32(fecha_turno.Minute);

                        if (mm_turno == 0)
                        {
                            hh_turno = hh_turno - 1;
                            mm_turno = 45;
                        }
                        else
                        {
                            mm_turno = 15;
                        }

                        horario_minimo_de_llegada = Convert.ToDateTime(dia_turno + " " + hh_turno.ToString() + ":" + mm_turno.ToString() + ":00.000");

                        int idAfiliado = Convert.ToInt32(reader.GetSqlInt32(1).Value);

                        cmd.Dispose();
                        reader.Close();

                        if (dia_turno != fecha)
                            throw new Exception("La fecha de registro de llegada debe ser la misma que la del turno");

                        if (horario_minimo_de_llegada >= fecha_llegada) //Si la hora de llegada es menor o igual a la hora estipulada(15 minutos antes del turno) se registra
                        {

                            new IngresarBonoConsulta(idAfiliado, fecha_llegada, id_turno).ShowDialog();
                            Close();

                        }
                        else
                        {
                            MessageBox.Show("La hora de registro debe ser como minimo 15 minitos anterior a la hora del turno");
                        }
                    } //Fin del read

                    cmd.Dispose();
                    reader.Close();
                    

                }//Fin using
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }


        }// Fin boton aceptar

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

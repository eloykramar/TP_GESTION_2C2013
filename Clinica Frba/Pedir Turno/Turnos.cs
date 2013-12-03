using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Pedir_Turno
{
    public partial class Turnos : Form1
    {
        int idAfiliado;
        int idProfesional;


        public Turnos(int idP, int idA)
        {
            InitializeComponent();
            idAfiliado = idA;
            idProfesional = idP;
            List<String> Lista_dias = new List<String>();

            try
            {

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("select distinct(ia.fecha) from YOU_SHALL_NOT_CRASH.agenda a join YOU_SHALL_NOT_CRASH.item_agenda ia on (a.id_agenda = ia.id_agenda) where id_profesional = " + idProfesional + " and ia.ID_TURNO is NULL order by 1", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox2.DisplayMember = "Fecha";
                    comboBox2.DataSource = tablaDeNombres;


                }//FIN USING
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();

            }


        }//FIN TURNOS

        private void button1_Click(object sender, EventArgs e)
        {
          

            DateTime dia_reservacion = Convert.ToDateTime(comboBox2.Text);
            String horario = (Convert.ToDateTime(comboBox1.Text)).ToShortTimeString();

            String[] horarioSpliteado = horario.Split(':');
            double hora = Convert.ToDouble(horarioSpliteado[0]);
            double minutos = Convert.ToDouble(horarioSpliteado[1]);

            DateTime fecha_completa = dia_reservacion.AddHours(Convert.ToDouble(hora)).AddMinutes(Convert.ToDouble(minutos));
            MessageBox.Show("dia: " + dia_reservacion.ToString() + ", hora: "+horario + " fecha completa: " +fecha_completa.ToString());

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();    

                SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_turno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fechaCompleta", SqlDbType.DateTime).Value = fecha_completa;
                cmd.Parameters.Add("@profesional", SqlDbType.Int).Value = idProfesional;
                cmd.Parameters.Add("@afiliado", SqlDbType.Int).Value = idAfiliado;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dia_reservacion;
                cmd.Parameters.Add("@horaInicio", SqlDbType.Time).Value = horario;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Su turno ha sido ingresado correctamente");
            }            
        }

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTime dia_reservacion = Convert.ToDateTime(comboBox2.Text);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();                
                SqlCommand cmd = new SqlCommand("select distinct(ia.hora_inicio) from YOU_SHALL_NOT_CRASH.agenda a join YOU_SHALL_NOT_CRASH.item_agenda ia on (a.id_agenda = ia.id_agenda) where id_profesional = " + idProfesional + " and ia.ID_TURNO is NULL and ia.fecha = '" + dia_reservacion + "' order by 1", conexion);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tablaDeNombres = new DataTable();

                adapter.Fill(tablaDeNombres);

                comboBox1.DisplayMember = "hora_inicio";
                comboBox1.DataSource = tablaDeNombres;
            }

        }//FIN CLASE TURNOS

        public class Turno_por_profesional
        {
            public String profesional { get; set; }
            public String fecha { get; set; }
            public String dia { get; set; }

            public void turno_por_profesional() { }

            public void turno_por_profesional(String pProfesional, String pFecha, String pDia)
            {
                this.profesional = pProfesional;
                this.fecha = pFecha;
                this.dia = pDia;
            }

        }
    }
}
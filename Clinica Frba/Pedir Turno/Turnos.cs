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
        String nombreCompletoP;
        
        //PARA CANCELACION DE DIA PROFESIONAL
        public Turnos(int idP, String unNombreCompletoP)
        {
            InitializeComponent();
            nombreCompletoP = unNombreCompletoP;
            idProfesional = idP;
            label1.Left = 40;
            comboBox2.Left = 230;
            label1.Text = "Fechas en la agenda con turnos:";
            label2.Visible = false;
            comboBox1.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            label3.Visible = true;
            label4.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            DateTime fechaActual = getFechaActual();

            try
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("select distinct(ia.fecha) from YOU_SHALL_NOT_CRASH.agenda a join YOU_SHALL_NOT_CRASH.item_agenda ia on (a.id_agenda = ia.id_agenda) where id_profesional = " + idProfesional + " and ia.fecha >= '" + fechaActual.AddDays(1) + "' and ID_Turno is not NULL order by 1", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox2.DisplayMember = "Fecha";
                    comboBox2.DataSource = tablaDeNombres;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        //PARA OTORGAR TURNOS
        public Turnos(int idP, int idA, String unNombreCompletoP)
        {
            InitializeComponent();
            idAfiliado = idA;
            idProfesional = idP;
            nombreCompletoP = unNombreCompletoP;
            button3.Visible = false;
            DateTime fechaActual = getFechaActual();            

            try
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("select distinct(ia.fecha) from YOU_SHALL_NOT_CRASH.agenda a join YOU_SHALL_NOT_CRASH.item_agenda ia on (a.id_agenda = ia.id_agenda) where id_profesional = " + idProfesional + " and ia.ID_TURNO is NULL and ia.fecha >= '" + fechaActual + "' order by 1", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox2.DisplayMember = "Fecha";
                    comboBox2.DataSource = tablaDeNombres;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        //reservar turno
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                validar();
                validarFecha();
                DateTime dia_reservacion = Convert.ToDateTime(comboBox2.Text);
                String horario = (Convert.ToDateTime(comboBox1.Text)).ToShortTimeString();

                String[] horarioSpliteado = horario.Split(':');
                double hora = Convert.ToDouble(horarioSpliteado[0]);
                double minutos = Convert.ToDouble(horarioSpliteado[1]);

                DateTime fecha_completa = dia_reservacion.AddHours(Convert.ToDouble(hora)).AddMinutes(Convert.ToDouble(minutos));

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

                    SqlCommand traerNombreCompletoA = new SqlCommand("SELECT Nombre+' '+Apellido FROM YOU_SHALL_NOT_CRASH.AFILIADO WHERE ID_AFILIADO = " + idAfiliado, conexion);
                    String nombreCompletoA = traerNombreCompletoA.ExecuteScalar().ToString();

                    new Dialogo("Turno otorgado;Fecha: " + fecha_completa + ";Profesional: " + nombreCompletoP + ";Afiliado: " + nombreCompletoA, "Aceptar").ShowDialog();
                }
                buscarHorarios();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                MessageBox.Show("ERROR - " + ex.Message, "Aceptar");
            }
        }

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //buscar horarios
        private void button2_Click_1(object sender, EventArgs e)
        {
            buscarHorarios();
        }

        //cancelar dia
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {                
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                    throw new Exception("La hora desde debe ser menor a la hora hasta");

                validarFecha();
                int horaInicio = dateTimePicker1.Value.Hour;
                int minutosInicio = dateTimePicker1.Value.Minute;
                int horaFin = dateTimePicker2.Value.Hour;
                int minutosFin = dateTimePicker2.Value.Minute;

                DateTime diaHora_inicio = Convert.ToDateTime(comboBox2.Text).AddHours(horaInicio).AddMinutes(minutosInicio);
                DateTime diaHora_Fin = Convert.ToDateTime(comboBox2.Text).AddHours(horaFin).AddMinutes(minutosFin);

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();   

                    SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Cancelar_dia_rango", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_profesional", SqlDbType.Int).Value = idProfesional;
                    cmd.Parameters.Add("@DiaHora_inicio", SqlDbType.DateTime).Value = diaHora_inicio.AddMinutes(-1);
                    cmd.Parameters.Add("@DiaHora_Fin", SqlDbType.DateTime).Value = diaHora_Fin.AddMinutes(+1);
                    cmd.Parameters.Add("@fechaActual", SqlDbType.DateTime).Value = getFechaActual();
                    cmd.Parameters.Add("@motivo", SqlDbType.NVarChar).Value = textBox1.Text;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    new Dialogo("Se ha cancelado el rango: " + diaHora_inicio + ", " + diaHora_Fin + ";Para el profesional: " + nombreCompletoP, "Aceptar").ShowDialog();
                    
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }

        }

        private void buscarHorarios()
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
        }        

        private void validar()
        {
            if (String.Equals(comboBox1.Text, ""))
                throw new Exception("Horario de turno no seleccionado");
        }

        private void validarFecha()
        {
            if (String.Equals(comboBox2.Text, ""))
                throw new Exception("Fecha no seleccionada");
        }
        //FIN CLASE TURNOS

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
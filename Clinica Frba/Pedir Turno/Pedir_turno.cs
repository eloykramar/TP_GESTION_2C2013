using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Clinica_Frba.Pedir_Turno;


namespace Clinica_Frba.Pedir_Turno
{
    public partial class Pedir_Turnos : Form1
    {
        String profesional;
        String especialidad;

        public Pedir_Turnos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            profesional = textBox1.Text;
            especialidad = textBox2.Text;
            dataGridView1.DataSource = BuscarProfesional(profesional, especialidad);
        }



        public List<Profesional> BuscarProfesional(String pNombre, String pEspecialidad)
        {
            List<Profesional> Lista = new List<Profesional>();

            try
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(string.Format(
                        "SELECT (p.NOMBRE + ' ' + p.APELLIDO) as nombre, s.DESCRIPCION as especialidad FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p, YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL e,YOU_SHALL_NOT_CRASH.ESPECIALIDAD s WHERE p.ID_PROFESIONAL = e.ID_PROFESIONAL AND e.CODIGO_ESPECIALIDAD=s.CODIGO_ESPECIALIDAD AND (s.DESCRIPCION like '%{0}%' AND NOMBRE like '%{1}%')", pEspecialidad, pNombre), conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Profesional pProfesional = new Profesional();
                        pProfesional.nombre = reader.GetString(0);
                        pProfesional.especialidad = reader.GetString(1);
                        Lista.Add(pProfesional);
                    }
                    conexion.Close();
                }   //FIN using
            }//Fin try
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                
            }
            return Lista;
        }  //FIN BuscarProfesional

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                profesional = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                Turnos Turno = new Turnos(profesional);
                Turno.Show();
            }
            else
            {
                MessageBox.Show("Error, no hay filas seleccionadas");
            }
        } 
    } //Fin class Pedir_Turnos



    public class Profesional 
    {
        public String nombre {get; set;}
        public String especialidad {get; set;}

        public void profesional() { }


        public void profesional(String pNombre, String pEspecialidad)
        {
            this.nombre = pNombre;
            this.especialidad = pEspecialidad;
        }
    }



}

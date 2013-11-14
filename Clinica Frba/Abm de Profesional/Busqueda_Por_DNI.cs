using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Clinica_Frba.Abm_de_Profesional
{
    public partial class Busqueda_Por_DNI : Form1
    {


        public Busqueda_Por_DNI()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //BUSCAR
        private void button2_Click(object sender, EventArgs e)
        {
            
                if (textBox2.Text != "")
                {
                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        try
                        {

                            conexion.Open();
                            DataTable tabla = new DataTable();

                            cargarATablaParaDataGripView("USE GD2C2013 SELECT DISTINCT e.DESCRIPCION FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD E join YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD te on e.CODIGO_TIPO_ESPECIALIDAD=te.CODIGO_TIPO_ESPECIALIDAD WHERE e.DESCRIPCION like '%"+textBox2.Text+"%'", ref tabla, conexion);

                            dataGridView1.Columns.Clear();
                            dataGridView1.DataSource = tabla;

                            dataGridView1.Columns[0].ReadOnly = true;

                            textBox2.Text = "";

                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                            (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                        }
                    }
                }
            }
        

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            dataGridView1.Columns.Clear();
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }




        public void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) 
        { 
        //Form2 es el form que se abrira con la informacion del dataGribView en los textBox 
       
 
        }
       
    }
}

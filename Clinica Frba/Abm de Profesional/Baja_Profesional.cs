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
    public partial class Baja_Profesional : Form1
    {

        public Baja_Profesional()
        {

            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    

                    SqlDataAdapter adapter = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres = new DataTable();


                    adapter.Fill(tablaDeNombres);
                    comboBox2.DisplayMember = "Descripcion";
                    comboBox2.DataSource = tablaDeNombres;
                    comboBox2.SelectedItem = null;

                    SqlDataAdapter adapter2 = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres2 = new DataTable();


                    adapter2.Fill(tablaDeNombres2);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.DataSource = tablaDeNombres2;
                    comboBox1.SelectedItem = null;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            (new Busqueda_Por_DNI()).Show();
        }


        private void cargarTabla(ref DataTable tabla) 
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = tabla;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            DataGridViewButtonColumn botonBaja = this.crearBotones("Seleccionar", "Dar de baja");
            dataGridView1.Columns.Add(botonBaja);

            comboBox1.SelectedItem = null;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox2.SelectedItem = null;
        }

        //Buscar
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {

                    string nom = " AND P.APELLIDO like '%" + textBox1.Text + "%'";
                    string dni = " AND P.DNI=" + textBox2.Text;
                    string esp = " AND e.descripcion='" + comboBox2.Text+"'";
                    string tipo_esp = " AND te.descripcion='" + comboBox1.Text + "'";

                    string where = "where p.ACTIVO=1";
                    if (!String.Equals(textBox1.Text, "")) where += nom;
                    if (!String.Equals(textBox2.Text, "")) where += dni;
                    if (!String.Equals(comboBox1.Text.ToString(), "")) where += tipo_esp;
                    if (!String.Equals(comboBox2.Text.ToString(), "")) where += esp;
                
                        conexion.Open();
                        DataTable tabla = new DataTable();


                        cargarATablaParaDataGripView("USE GD2C2013 select distinct p.DNI, p.APELLIDO,p.NOMBRE,p.ACTIVO FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep join YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on e.CODIGO_ESPECIALIDAD=ep.CODIGO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD te on te.CODIGO_TIPO_ESPECIALIDAD=e.CODIGO_TIPO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.PROFESIONAL p on p.ID_PROFESIONAL=ep.ID_PROFESIONAL"+" "+ where, ref tabla, conexion);

                        cargarTabla(ref tabla);


                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String prof = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();


                using (SqlConnection conexion = this.obtenerConexion())
                {

                    if (e.ColumnIndex == 4)
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Baja_Logica_Profesional", conexion))
                            {
                                conexion.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@dni", SqlDbType.NVarChar).Value = prof;
                                cmd.ExecuteNonQuery();

                                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                                MessageBox.Show("Profesional inhabilitado. D.N.I.: "+prof, "Aceptar");

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

        //LIMPIAR
        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = null;
            dataGridView1.Columns.Clear();


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {

            this.Close();
        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }




        

    }
}

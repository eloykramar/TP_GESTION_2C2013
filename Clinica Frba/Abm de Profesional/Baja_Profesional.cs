using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Clinica_Frba.ABM_de_Profesional
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

                    SqlDataAdapter adapter = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres = new DataTable();


                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.DataSource = tablaDeNombres;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            textBox3.Enabled = false;

            
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
            DataGridViewButtonColumn botonBaja = this.crearBotones("Seleccionar", "Dar de baja");
            dataGridView1.Columns.Add(botonBaja);

            comboBox1.SelectedItem = null;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        //Buscar
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    if (textBox2.Text != "")
                    {
                        conexion.Open();
                        DataTable tabla = new DataTable();


                        cargarATablaParaDataGripView("USE GD2C2013 select p.DNI, p.APELLIDO,p.NOMBRE FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p WHERE p.DNI=" + textBox2.Text.ToString() + " and p.ACTIVO=1", ref tabla, conexion);

                        cargarTabla(ref tabla);
                        

                    }
                    else
                    {
                        if (comboBox1.Text != "" && textBox1.Text != "")
                        {

                            conexion.Open();
                            DataTable tabla = new DataTable();


                            cargarATablaParaDataGripView("USE GD2C2013 select p.DNI, p.APELLIDO,p.NOMBRE FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep join YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on e.CODIGO_ESPECIALIDAD=ep.CODIGO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD te on te.CODIGO_TIPO_ESPECIALIDAD=e.CODIGO_TIPO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.PROFESIONAL p on p.ID_PROFESIONAL=ep.ID_PROFESIONAL WHERE te.DESCRIPCION='" + comboBox1.Text.ToString() + "' and p.APELLIDO like '%" + textBox1.Text.ToString() + "%' and p.ACTIVO=1", ref tabla, conexion);

                            cargarTabla(ref tabla);
                        }
                        else
                        {
                            if (textBox1.Text != "")
                            {
                                conexion.Open();
                                DataTable tabla = new DataTable();


                                cargarATablaParaDataGripView("USE GD2C2013 select p.DNI, p.APELLIDO,p.NOMBRE FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p WHERE p.APELLIDO like '%" + textBox1.Text.ToString() + "%' and p.ACTIVO=1", ref tabla, conexion);

                                cargarTabla(ref tabla);
                            }
                            else
                            {
                                conexion.Open();
                                DataTable tabla = new DataTable();


                                cargarATablaParaDataGripView("USE GD2C2013 select p.DNI, p.APELLIDO,p.NOMBRE FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep join YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on e.CODIGO_ESPECIALIDAD=ep.CODIGO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.TIPO_ESPECIALIDAD te on te.CODIGO_TIPO_ESPECIALIDAD=e.CODIGO_TIPO_ESPECIALIDAD join YOU_SHALL_NOT_CRASH.PROFESIONAL p on p.ID_PROFESIONAL=ep.ID_PROFESIONAL WHERE te.DESCRIPCION='" + comboBox1.Text.ToString() +"' and p.ACTIVO=1", ref tabla, conexion);

                                cargarTabla(ref tabla);
                            }
                        }
                    }

                   

                   

                }catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String profesional = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();


                using (SqlConnection conexion = this.obtenerConexion())
                {
                    if (this.dataGridView1.Columns[e.ColumnIndex].Name.Equals("Dar de baja"))
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Baja_Logica_Profesional", conexion))
                            {
                                conexion.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@dni", SqlDbType.NVarChar).Value = profesional;
                                cmd.ExecuteNonQuery();

                                new Dialogo(profesional + " inhabilitado \n", "Aceptar").ShowDialog();
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String prof = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();


                using (SqlConnection conexion = this.obtenerConexion())
                {

                    if (e.ColumnIndex == 3)
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
                                new Dialogo(prof + " inhabilitado \n", "Aceptar").ShowDialog();

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


        

    }
}

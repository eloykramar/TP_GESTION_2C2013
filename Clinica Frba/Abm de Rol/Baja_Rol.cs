using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Abm_de_Rol
{
    public partial class Baja_Rol : Form1
    {
        public Baja_Rol()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    //cargar comboBox
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.F_Roles () ORDER BY RN", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox1.DisplayMember = "NombreRol";
                    comboBox1.DataSource = tablaDeNombres;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }
            
        //buscar
        private void button2_Click(object sender, EventArgs e)
        {
            string varFiltro1 = "";
            string varFiltro2 = "";

            string textoFiltro1;
            string textoFiltro2;

            textoFiltro1 = comboBox1.Text;
            textoFiltro2 = textBox1.Text;


            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    DataTable tabla = new DataTable();

                    if (!(String.Equals(textoFiltro1, "No seleccionado")))
                    {
                        varFiltro1 = "WHERE r.Descripcion = '" + textoFiltro1 + "'";

                        if (textoFiltro2.Length > 0)
                        {
                            varFiltro2 = "and f.Descripcion LIKE '%" + textoFiltro2 + "%'";
                        }
                    }
                    else
                    {
                        if (textoFiltro2.Length > 0)
                        {
                            varFiltro2 = "WHERE f.Descripcion LIKE '%" + textoFiltro2 + "%'";
                        }
                    }

                    cargarATablaParaDataGripView("USE GD2C2013 SELECT DISTINCT(r.Descripcion), r.Activo FROM YOU_SHALL_NOT_CRASH.ROL r join YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD rf on (r.ID_Rol = rf.ID_Rol) join YOU_SHALL_NOT_CRASH.FUNCIONALIDAD f on (rf.ID_Funcionalidad = f.ID_Funcionalidad) " + varFiltro1 + varFiltro2, ref tabla, conexion);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    DataGridViewButtonColumn botonFuncionalidades = this.crearBotones("Funcionalidades", "Mostrar Funciondalidades");
                    dataGridView1.Columns.Add(botonFuncionalidades);
                    DataGridViewButtonColumn botonInhabilitar = this.crearBotones("Inhabilitacion Logica", "Inhabilitar Rol");
                    dataGridView1.Columns.Add(botonInhabilitar);
                }

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        //limpiar
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "No seleccionado";
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String nombreRol = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();


                using (SqlConnection conexion = this.obtenerConexion())
                {
                    if (e.ColumnIndex == 2)
                    {
                        conexion.Open();
                        DataTable tabla = new DataTable();

                        cargarATablaParaDataGripView("USE GD2C2013 SELECT f.Descripcion FROM YOU_SHALL_NOT_CRASH.ROL r join YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD rf on (r.ID_Rol = rf.ID_Rol) join YOU_SHALL_NOT_CRASH.FUNCIONALIDAD f on (rf.ID_Funcionalidad = f.ID_Funcionalidad) where r.Descripcion = '" + nombreRol + "'", ref tabla, conexion);

                        dataGridView2.DataSource = tabla;
                        dataGridView2.Columns[0].ReadOnly = true;
                    }

                    if (e.ColumnIndex == 3)
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Baja_Logica_Rol", conexion))
                            {
                                conexion.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@nombreRol", SqlDbType.NVarChar).Value = nombreRol;
                                cmd.ExecuteNonQuery();
                             
                                new Dialogo(nombreRol + " inhabilitado \n", "Aceptar").ShowDialog();
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

    }
}

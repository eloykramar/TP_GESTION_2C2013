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
    public partial class Modificar_Rol : Form1
    {
        int indexRowRol;

        public Modificar_Rol()
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
                        DataGridViewButtonColumn botonModificar = this.crearBotones("", "Modificar Rol");
                        dataGridView1.Columns.Add(botonModificar);
                    }

                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String nombreRol = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                indexRowRol = e.RowIndex;

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    if (e.ColumnIndex == 2)
                    {
                        conexion.Open();
                        DataTable tabla = new DataTable();

                        cargarATablaParaDataGripView("USE GD2C2013 SELECT f.Descripcion FROM YOU_SHALL_NOT_CRASH.ROL r join YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD rf on (r.ID_Rol = rf.ID_Rol) join YOU_SHALL_NOT_CRASH.FUNCIONALIDAD f on (rf.ID_Funcionalidad = f.ID_Funcionalidad) where r.Descripcion = '" + nombreRol + "'", ref tabla, conexion);
                        dataGridView2.Columns.Clear();
                        dataGridView2.DataSource = tabla;
                        dataGridView2.Columns[0].ReadOnly = true;

                        dataGridView2.Columns[0].ReadOnly = true;
                        DataGridViewButtonColumn botonModificar = this.crearBotones("", "Modificar / Eliminar / Agregar Funcionalidad");
                        dataGridView2.Columns.Add(botonModificar);
                    }

                      if (e.ColumnIndex == 3)//boton modificar rol
                      {
                          (new Modificar_Rol_Particular(nombreRol)).Show();
                      }
                    
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    String nombreRolActual = dataGridView1.Rows[indexRowRol].Cells[0].Value.ToString();
                    String nombreFuncionalidadActual = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (e.ColumnIndex == 1) //boton modificar/agregar/elimar func
                    {
                        (new Modificar_Rol_Particular(nombreRolActual, nombreFuncionalidadActual)).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        //limpiar
        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "No seleccionado";
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
        }
    }
}
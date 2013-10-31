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
    public partial class Alta_Rol : Form1
    {
        string nombreRol;
        string funcionalidad;
        
        public Alta_Rol()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.FUNCIONALIDAD", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.DataSource = tablaDeNombres;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        //agregar funcionalidad
        private void button2_Click(object sender, EventArgs e)
        {
            funcionalidad = comboBox1.Text;

            if (listBox1.Items.Contains(funcionalidad))
            {
                (new Dialogo("Ya existe la funcionalidad", "Aceptar")).ShowDialog();
            }
            else
            {
                listBox1.Items.Add(funcionalidad);
                comboBox1.Text = "";
            }  
        }

        //borrar funcionalidad
        private void button3_Click(object sender, EventArgs e)
        {
            funcionalidad = listBox1.Text;

            if (listBox1.Items.Contains(funcionalidad))
            {
                listBox1.Items.Remove(funcionalidad);
                comboBox1.Text = "";
            }
            else
            {
                (new Dialogo("No existe la funcionalidad", "Aceptar")).ShowDialog();
            } 
        }

        //limpiar
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            listBox1.Items.Clear();
        }

        
        //buscar
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_Rol", conexion))
                    {
                        if (textBox1.Text != "")
                        {
                            conexion.Open();                     
                      
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@nombreRol", SqlDbType.NVarChar).Value = textBox1.Text;
                            cmd.Parameters.Add("@respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();                        
                            int respuesta = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);

                            if (respuesta == -1)
                            {
                                (new Dialogo("Ya existe el rol", "Aceptar")).ShowDialog();
                            }
                            else
                            {
                                foreach (String nombreFunc in listBox1.Items)
                                {
                                    SqlCommand insertarFuncs = new SqlCommand("USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD VALUES (" + respuesta + ", (SELECT ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE Descripcion = '" + nombreFunc + "'))", conexion);
                                    insertarFuncs.ExecuteNonQuery();
                                }

                                int filasAfectadasTotales = 1 + listBox1.Items.Count;
                                new Dialogo(nombreRol + " agregado \n" + filasAfectadasTotales + " filas afectadas", "Aceptar").ShowDialog();
                                }
                            }
                            else
                            {
                                new Dialogo("Debe Completar el nombre del rol que quiere dar de alta", "Aceptar").ShowDialog();
                            }

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

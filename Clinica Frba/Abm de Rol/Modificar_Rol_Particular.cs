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
    public partial class Modificar_Rol_Particular : Form1
    {
        string nombreRol;
        string nombreFunc;

        public Modificar_Rol_Particular(String unRol)
        {
            InitializeComponent();

            comboBox1.Text = unRol;
            nombreRol = unRol;

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        public Modificar_Rol_Particular(String unRol, String unaFuncionalidad)
        {
            InitializeComponent();

            comboBox1.Text = unRol;
            comboBox2.Text = unaFuncionalidad;

            nombreRol = unRol;
            nombreFunc = unaFuncionalidad;

            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;      
            comboBox2.Enabled = false;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                //cargar comboBox3 todas las funcionalidades
                try
                {

                    conexion.Open();

                    SqlCommand funcs = new SqlCommand("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(funcs);
                    DataTable tablaDeFuncionalidades = new DataTable();

                    adapter.Fill(tablaDeFuncionalidades);

                    comboBox3.DisplayMember = "Descripcion";
                    comboBox3.DataSource = tablaDeFuncionalidades;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

        }

        //modificar nombre rol
        private void button1_Click(object sender, EventArgs e)
        {            
            string nuevoNombreRol = textBox1.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand modRol = new SqlCommand("USE GD2C2013 UPDATE YOU_SHALL_NOT_CRASH.ROL SET Descripcion='" + nuevoNombreRol + "' where Descripcion='" + nombreRol + "'", conexion);
                    modRol.ExecuteNonQuery();
                    comboBox1.Text = nuevoNombreRol;
                    new Dialogo("Rol " + nombreRol + " modificado a " + nuevoNombreRol + ";1 fila afectada", "Aceptar").ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        //habilitar rol
        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand validacion = new SqlCommand("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.ROL where Descripcion = '" + nombreRol + "' and Activo=0", conexion);
                    int cantidadDeFilas = (int)validacion.ExecuteScalar();

                    if (cantidadDeFilas != 0)
                    {
                        SqlCommand habilitarRol = new SqlCommand("USE GD2C2013 UPDATE YOU_SHALL_NOT_CRASH.ROL SET Activo = 1 where Descripcion = '" + nombreRol + "'", conexion);
                        habilitarRol.ExecuteNonQuery();
                        (new Dialogo("El rol se ha habilitado", "Aceptar")).ShowDialog();
                    }

                    else
                    {
                        (new Dialogo("El rol ya esta habilitado", "Aceptar")).ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("El rol ya esta habilitado", "Aceptar")).ShowDialog();
                }
            }
        }

        //modificar nombre funcionalidad
        private void button2_Click(object sender, EventArgs e)
        {
            string nuevoNombreFunc = textBox2.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand modFunc = new SqlCommand("USE GD2C2013 UPDATE YOU_SHALL_NOT_CRASH.FUNCIONALIDAD SET Descripcion='" + nuevoNombreFunc + "' where Descripcion='" + nombreFunc + "'", conexion);
                    modFunc.ExecuteNonQuery();
                    comboBox2.Text = nuevoNombreFunc;                    
                    new Dialogo("Funcionalidad " + nombreFunc + " modificada a " + nuevoNombreFunc + ";1 fila afectada", "Aceptar").ShowDialog();
                    nombreFunc = nuevoNombreFunc;
                    textBox2.Text = "";
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        //eliminar funcionalidad
        private void button3_Click(object sender, EventArgs e)
        {

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand delRolFunc = new SqlCommand("USE GD2C2013 DELETE FROM YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD WHERE ID_Rol= (SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion = '" + nombreRol + "') and ID_Funcionalidad = (SELECT ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE Descripcion = '" + nombreFunc + "')", conexion);
                    delRolFunc.ExecuteNonQuery();

                    comboBox2.Update();

                    new Dialogo("La funcionalidad " + nombreFunc + ", fue eliminada de " + nombreRol + "\n 1 fila afectada", "Aceptar").ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        //agregar funcionalidad
        private void button4_Click(object sender, EventArgs e)
        {
            string nombreFuncParaAgregar = comboBox3.Text;
            
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD WHERE ID_Rol= (SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion = '" + nombreRol + "') and ID_Funcionalidad = (SELECT ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE Descripcion = '" + nombreFuncParaAgregar + "')", conexion);
                    int cantidadDeFilas = (int)cmd.ExecuteScalar();

                    if (cantidadDeFilas != 0)
                    {
                        (new Dialogo("El rol " + nombreRol + " ya posee la funcionalidad " + nombreFuncParaAgregar, "Aceptar")).ShowDialog();
                    }
                    else
                    {
                        SqlCommand addRolFunc = new SqlCommand("USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD VALUES ((SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion = '" + nombreRol + "'), (SELECT ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE Descripcion = '" + nombreFuncParaAgregar + "'))", conexion);
                        addRolFunc.ExecuteNonQuery();
                        new Dialogo("La funcionalidad " + nombreFuncParaAgregar + ", fue agregada a " + nombreRol + "\n 1 fila afectada", "Aceptar").ShowDialog();
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

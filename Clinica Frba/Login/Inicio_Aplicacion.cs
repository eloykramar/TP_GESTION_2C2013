using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Login
{
    public partial class Inicio_Aplicacion : Form1
    {
        public Inicio_Aplicacion(String unUsuario, String unRol)
        {
            InitializeComponent();

            label1.Text = "Sesión inciada como: " + unUsuario;
            label2.Text = "Rol: " + unRol;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select f.Descripcion from YOU_SHALL_NOT_CRASH.ROL r join YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD rf on (r.ID_Rol = rf.ID_Rol) join YOU_SHALL_NOT_CRASH.FUNCIONALIDAD f on (rf.ID_Funcionalidad = f.ID_Funcionalidad) where r.Descripcion = '" + unRol + "'", conexion);

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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

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
    public partial class Pantalla_Seleccion_Rol : Form1
    {
        String usuario;
        
        public Pantalla_Seleccion_Rol(String unUsuario)
        {
            InitializeComponent();

            usuario = unUsuario;

            label1.Text = "Hola, " + unUsuario;
            
            if (unUsuario.Length > 7)
            {
            label1.Left = 35;
            }
            

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.USUARIO u join YOU_SHALL_NOT_CRASH.ROL_USUARIO ru on (u.DNI_Usuario = ru.DNI_Usuario) join YOU_SHALL_NOT_CRASH.ROL r on (ru.ID_Rol = r.ID_Rol) where u.Username='"+unUsuario+"'", conexion);

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

        private void button1_Click(object sender, EventArgs e)
        {            
            String rol = comboBox1.Text;
            Close();
            new Inicio_Aplicacion(usuario, rol).Show();
        }
    }
}

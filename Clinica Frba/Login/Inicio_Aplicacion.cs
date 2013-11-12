using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Clinica_Frba.Abm_de_Rol;
using Clinica_Frba.ABM_de_Afiliado;
using Clinica_Frba.Compra_de_Bono;
using Clinica_Frba.Registrar_Agenda;
using Clinica_Frba.ABM_de_Profesional;
using Clinica_Frba.Pedir_Turno;
using Clinica_Frba.Registro_de_LLegada;
using Clinica_Frba.Registro_Resultado_Atencion;
using Clinica_Frba.Cancelar_Atencion;

namespace Clinica_Frba.Login
{
    public partial class Inicio_Aplicacion : Form1
    {
        string usuario;
        string rol;

        public Inicio_Aplicacion(String unUsuario, String unRol)
        {
            InitializeComponent();

            usuario = unUsuario;
            rol = unRol;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string funcionalidad = comboBox1.Text;

            if (String.Equals(funcionalidad, "ABM Rol"))
            {
                (new ABM_Rol()).Show();
            }

            if (String.Equals(funcionalidad, "ABM Afiliado"))
            {
                (new ABM_Afiliado()).Show();
            }

            if (String.Equals(funcionalidad, "Registrar Agenda del medico"))
            {
                (new Seleccionar_Profesional()).Show();
            }

            if (String.Equals(funcionalidad, "Compra de bonos"))
            {
                (new Compra(usuario,rol)).Show();
            }            
            
            if (String.Equals(funcionalidad, "ABM Profesional"))
            {
                (new ABM_Profesional()).Show();
            }

            if (String.Equals(funcionalidad, "Pedir turno"))
            {
                (new Pedir_Turnos(getNroxUser(usuario))).Show();
            }

            if (String.Equals(funcionalidad, "Registro de llegada para atencion medica"))
            {
                (new BuscarProf(0)).Show();
            }

            if (String.Equals(funcionalidad, "Cancelacion de turno"))
            {
                (new Menu_Cancelar(rol, usuario)).menu_handler();
            }

            if (String.Equals(funcionalidad, "Registro de resultado de atencion medica"))
            {
                (new Seleccion_Turno_Result( getIdPxUser(usuario),0)).ShowDialog();
            }

        }
    }
}

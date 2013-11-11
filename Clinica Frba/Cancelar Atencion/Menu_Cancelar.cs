using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Menu_Cancelar : Form1
    {
        string rol;
        string user;

        public Menu_Cancelar(string P_rol, string P_user)
        {
            rol = P_rol;
            user = P_user;
            InitializeComponent();
        }

        private void btnCancProf_Click(object sender, EventArgs e)
        {
            cancelar_profesional("");
        }

        private void btnCancAfi_Click(object sender, EventArgs e)
        {
            cancelar_afiliado("");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void menu_handler()
        {
            switch (rol)
            {
                //si es administrativo
                case "Administrativo": this.ShowDialog();
                    break;
                //si es un profesinoal
                case "Profesional": cancelar_profesional(user);
                    break;
                //si es un afiliado
                case "Afiliado": cancelar_afiliado(user);
                    break;
            }
        }
        public void cancelar_profesional(string user)
        {
            int id=0;
            if (String.Equals(user, ""))
            {
                (new Buscar_Prof_Canc_Prof(0)).ShowDialog();
            }
            else
            {
                id = getIdPxUser(user);
                MessageBox.Show("doy de baja un dia");
            }
        }

        public void cancelar_afiliado(string user)
        {

            (new Buscar_Prof_Canc_Afi(getNroxUser(user))).ShowDialog();
            
        }

        public int getNroxUser(string user)
        {
            int nro = 0;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select Nro_Afiliado from YOU_SHALL_NOT_CRASH.AFILIADO A join YOU_SHALL_NOT_CRASH.USUARIo U ON A.DNI=U.DNI_USUARIO WHERE A.Fecha_Baja IS NULL and U.USERNAME='" + user + "'", conexion);
                try
                {
                    conexion.Open();
                    nro = (int)cmd2.ExecuteScalar();
                    return nro;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

        }

        public int getIdPxUser(string user)
        {
            int id = 0;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL P join YOU_SHALL_NOT_CRASH.USUARIo U ON P.DNI=U.DNI_USUARIO WHERE P.ACTIVO=1 and U.USERNAME='" + user + "'", conexion);
                try
                {
                    conexion.Open();
                    id = (int)cmd2.ExecuteScalar();
                    return id;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

        }

    }
}

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
                (new Buscar_Prof_Canc_Prof(id)).ShowDialog();
            }
        }

        public void cancelar_afiliado(string user)
        {

            (new Buscar_Prof_Canc_Afi(getNroxUser(user))).ShowDialog();
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.DetalleAfiliado
{
    public partial class Modif  : DetalleAfiliado
    {
        public Modif(string id)
        {
         //txtId.Text= id.ToString();
         txtNombre.Text = "Nombre prueba";
         txtApellido.Text = "Apellido prueba";
         txtMail.Text = "prueba@hotmail.com";
         txtDNI.Text = "12345678";
         txtDir.Text = "su casa de prueba";
         txtNroAf.Text = "123434-1";
         txtUser.Text = "prueba";
         cmbCivil.SelectedIndex = 1;
         cmbSexo.SelectedIndex = 1;
         cmbPlan.SelectedIndex = 3;
        }

        public override void guardar()
        {
            MessageBox.Show("uuuuuuuuuuhhhhhhhhhh.......");
        }

    
    }
}

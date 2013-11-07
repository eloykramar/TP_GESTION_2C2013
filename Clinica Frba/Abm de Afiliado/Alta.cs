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
    public partial class Alta : DetalleAfiliado
    {
        public override void guardar()
        {
            string telefono = "NULL";
            if (!String.Equals(txtTel.Text, ""))  telefono = txtTel.Text;
            
            string Sql = "";
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    Sql = "USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO ( Username, Pass, DNI_Usuario, Intentos_Fallidos)"
                    + " VALUES ('" + txtUser.Text + "', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', '" + txtDNI.Text + "', 0); "
                    + "INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO"
                    + " (Nombre, Apellido, Direccion, DNI, Telefono, ID_Estado_Civil, Cantidad_Consultas, Mail, Sexo, Familiares_a_Cargo, ID_Plan, Fecha_Nac)"
                    + " VALUES ('" + txtNombre.Text + "', '"
                    + txtApellido.Text + "', '"
                    + txtDir.Text + "', "
                    + txtDNI.Text + ", "
                    + telefono + ", "
                    + cmbCivil.SelectedValue.ToString() + ", "
                    + numConsultas.Value.ToString() + ", '"
                    + txtMail.Text + "', '"
                    + cmbSexo.SelectedValue.ToString() + "', "
                    + numFACargo.Value.ToString() + ", "
                    + cmbPlan.SelectedValue.ToString() + ", '"
                    + dtpNac.Value.ToShortDateString() + "')";
                    conexion.Open();
                    SqlCommand newAfi = new SqlCommand(Sql, conexion);
                    newAfi.ExecuteNonQuery();

                    MessageBox.Show("El Afiliado ha sido cargado satisfactoriamente", "Alta Completa");
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }
        private void Alta_Load(object sender, EventArgs e)
        {

        }
    }
}

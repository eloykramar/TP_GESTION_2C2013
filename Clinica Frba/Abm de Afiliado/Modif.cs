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
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    txtId.Text = id;
                    conexion.Open();
                    SqlCommand info = new SqlCommand("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.USUARIO JOIN YOU_SHALL_NOT_CRASH.AFILIADO ON DNI=DNI_Usuario WHERE ID_Afiliado=" + txtId.Text, conexion);
                    SqlDataReader afi = info.ExecuteReader();
                    afi.Read();
                    txtUser.ReadOnly = true;
                    txtDNI.ReadOnly = true;
                    
                    txtNombre.Text = Convert.ToString(afi["Nombre"]);
                    txtApellido.Text = Convert.ToString(afi["Apellido"]);
                    txtMail.Text = Convert.ToString(afi["Mail"]);
                    txtDNI.Text = Convert.ToString(afi["DNI"]);
                    txtTel.Text = Convert.ToString(afi["Telefono"]);
                    txtDir.Text = Convert.ToString(afi["Direccion"]);
                    txtNroAf.Text = Convert.ToString(afi["Nro_Afiliado"]);
                    txtUser.Text = Convert.ToString(afi["Username"]);
                    numFACargo.Value = Convert.ToInt32(afi["Familiares_A_Cargo"]);
                    numConsultas.Value = Convert.ToInt32(afi["Cantidad_Consultas"]);
                    dtpNac.Value = Convert.ToDateTime(afi["Fecha_Nac"]);
                    cmbCivil.SelectedValue = Convert.ToInt32(afi["ID_Estado_Civil"]);
                    cmbPlan.SelectedValue = Convert.ToInt32(afi["ID_Plan"]);
                    cmbSexo.SelectedValue = Convert.ToChar(afi["Sexo"]);





                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
                
            }

        }
    

        public override void guardar()
        {
            string telefono = "NULL";
            if (!String.Equals(txtTel.Text, "")) telefono = txtTel.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand modAfi = new SqlCommand(
                    "USE GD2C2013 UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Nombre='"+ txtNombre.Text
                    + "', Apellido='" + txtApellido.Text
                    + "', Direccion='" + txtDir.Text
                    + "', Telefono=" + telefono
                    + ", ID_Estado_Civil=" + cmbCivil.SelectedValue.ToString()
                    + ", Cantidad_Consultas=" + numConsultas.Value.ToString()
                    + ", Mail='" + txtMail.Text
                    + "', Sexo='" + cmbSexo.SelectedValue.ToString()
                    + "', Familiares_a_Cargo=" + numFACargo.Value.ToString()
                    + ", ID_Plan=" + cmbPlan.SelectedValue.ToString() 
                    + ", Fecha_Nac='" + dtpNac.Value.ToShortDateString() 
                    + "' WHERE ID_Afiliado=" + txtId.Text, conexion);
                    modAfi.ExecuteNonQuery();
                    MessageBox.Show("Los datos del afiliado se han modificado satisfactoriamente", "Modificado");
                    this.Close();
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

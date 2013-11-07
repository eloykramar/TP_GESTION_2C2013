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
        public ABM_de_Afiliado.ABM_Afiliado preAlta;
        int digito;

        public Alta( ABM_de_Afiliado.ABM_Afiliado P_prealta, int P_digito)//ref List<string> P_users, ref List<string> P_dnis, ref List<string> P_values, int P_digito)
        {
            preAlta = P_prealta;
            digito = P_digito;
        }

        public override void guardar()
        {
            string telefono = "NULL";
            if (!String.Equals(txtTel.Text, ""))  telefono = txtTel.Text;
            
            string Sql = "";
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    Int32 cant_users = 0;
                    SqlCommand cmd = new SqlCommand("USE GD2C2013 SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username='" + txtUser.Text + "'", conexion);
                    cant_users = (Int32)cmd.ExecuteScalar();
                    Int32 cant_dnis = 0;
                    cmd = new SqlCommand("USE GD2C2013 SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE DNI_Usuario='" + txtDNI.Text + "'", conexion);
                    cant_dnis = (Int32)cmd.ExecuteScalar();

                    if (cant_users == 0 && !preAlta.users.Contains(txtUser.Text))
                    {
                        if (cant_dnis == 0 && !preAlta.dnis.Contains(txtDNI.Text))
                        {
                            //guardo los valores
                            preAlta.values.Add("('" + txtNombre.Text + "', '"
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
                            + dtpNac.Value.ToShortDateString() + "', "
                            + digito + ")");
                            //hasta aca

                            preAlta.dnis.Add(txtDNI.Text);
                            preAlta.users.Add(txtUser.Text);

                            if (digito == 1)
                            {
                                if (cmbCivil.SelectedValue.Equals(2) || cmbCivil.SelectedValue.Equals(4))
                                 if (MessageBox.Show("Desea agregar un conyugue?", "Alta familiar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                 {
                                  (new Alta(preAlta, 2)).ShowDialog();
                                 }

                                digito=3;
                                while ((digito-3) < numFACargo.Value)
                                {
                                    if (MessageBox.Show("Desea agregar un hijo?", "Alta familiar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        (new Alta(preAlta, digito)).ShowDialog();
                                        digito++;
                                    }
                                    else break;
                                }


                                Sql = "USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO ( Username, Pass, DNI_Usuario, Intentos_Fallidos)"
                                + " VALUES ";

                                for (int i = 0; i < preAlta.users.Count(); i++)
                                {
                                    Sql += "('" + preAlta.users[i] + "', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', '" + preAlta.dnis[i] + "', 0), ";
                                }
                                Sql = Sql.Substring(0, Sql.Length - 2);

                                Sql+="; INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO"
                                + " (Nombre, Apellido, Direccion, DNI, Telefono, ID_Estado_Civil, Cantidad_Consultas, Mail, Sexo, Familiares_a_Cargo, ID_Plan, Fecha_Nac, Nro_Afiliado)"
                                + " VALUES ";

                                for (int i = 0; i < preAlta.values.Count(); i++)
                                {
                                    Sql += preAlta.values[i] + ", ";
                                }
                                Sql = Sql.Substring(0, Sql.Length - 2);

                                SqlCommand newAfi = new SqlCommand(Sql, conexion);
                                newAfi.ExecuteNonQuery();

                                MessageBox.Show("El Afiliado ha sido cargado satisfactoriamente", "Alta Completa");
                            }
                            this.Close();
                        }
                        else MessageBox.Show("Este DNI ya corresponde a un usuario.", "Error");
                    }   
                    else MessageBox.Show("Nombre de usuario en uso.\n Por favor, ingrese uno nuevo.", "Error");

                    

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

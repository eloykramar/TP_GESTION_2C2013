using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Registro_de_LLegada
{
    public partial class IngresarBonoConsulta : Form1
    {
        int idAfiliado;
        DateTime fecha_llegada;
        int id_turno;

        public IngresarBonoConsulta(int idAfi,DateTime fe_lle,int idTur )
        {
            InitializeComponent();
            idAfiliado = idAfi;
            fecha_llegada = fe_lle;
            id_turno = idTur;
        }

        private void buttAceptar_Click(object sender, EventArgs e)
        {
            int id_bono_ingresado = Convert.ToInt32(textBono.Text);
            int numero_de_consulta_a_ingresar = 0;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                //verifico si el bono corresponde a ese afiliado
                SqlCommand cmd = new SqlCommand(string.Format("SELECT ID_AFILIADO FROM YOU_SHALL_NOT_CRASH.BONO_CONSULTA WHERE ID_Bono_Consulta = {0}", id_bono_ingresado), conexion);
                int idAfiBono = ExecuteScalarOrZero(cmd);
                int nroAfiBono = getNroxIdAfiliado(idAfiBono.ToString());
                cmd.Dispose();

                if (idAfiBono>0)
                {
                    if (getRaizAfi(nroAfiBono.ToString()) == getRaizAfi(getNroxIdAfiliado(idAfiliado.ToString()).ToString()))  //Si el bono corresponde al grupo familiar, entonces sigo
                    {
                        cmd = new SqlCommand(string.Format(
                            "SELECT ID_PLAN FROM YOU_SHALL_NOT_CRASH.BONO_CONSULTA WHERE ID_Bono_Consulta ={0}", id_bono_ingresado), conexion);
                        int planBono = ExecuteScalarOrZero(cmd);
                        cmd.Dispose(); 
                        cmd = new SqlCommand(string.Format(
                            "SELECT ID_PLAN FROM YOU_SHALL_NOT_CRASH.AFILIADO WHERE ID_AFILIADO ={0}", idAfiliado), conexion);
                        int planAfi = ExecuteScalarOrZero(cmd);
                        cmd.Dispose();
                        if (planAfi != planBono)
                        {
                            MessageBox.Show("El Plan del Afiliado no coincide con el del bono.");
                            return;
                        }//si el plan coincide, sigo...

                        cmd = new SqlCommand(string.Format(
                            "SELECT Numero_Consulta_Afiliado FROM YOU_SHALL_NOT_CRASH.BONO_CONSULTA WHERE ID_Bono_Consulta ={0}", id_bono_ingresado), conexion);
                        int nroConsultaBono = ExecuteScalarOrZero(cmd);
                        cmd.Dispose();
                        if (nroConsultaBono==0)   //Si es 0 quiere decir que nunca se uso en una consulta
                            {
                                //Obtengo el ultimo numero de consulta del afiliado segun los bonos usados 
                                cmd = new SqlCommand(string.Format(
                                    "select MAX(b.Numero_Consulta_Afiliado) from YOU_SHALL_NOT_CRASH.AFILIADO a join YOU_SHALL_NOT_CRASH.BONO_CONSULTA b on a.ID_Afiliado = b.ID_Afiliado WHERE a.ID_Afiliado={0} group by a.ID_Afiliado", idAfiliado), conexion);
                                numero_de_consulta_a_ingresar = ExecuteScalarOrZero(cmd) + 1;
                                cmd.Dispose();
                                
                                //Registro la llegada en turno + registro el numero de consulta asociada al bono_consulta
                                cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Ingresar_bono_y_llegada", conexion);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@id_bono_ingresado", SqlDbType.Int).Value = id_bono_ingresado;
                                cmd.Parameters.Add("@numero_de_consulta_a_ingresar", SqlDbType.Int).Value = numero_de_consulta_a_ingresar;
                                cmd.Parameters.Add("@id_turno", SqlDbType.Int).Value = id_turno;
                                cmd.Parameters.Add("@fecha_llegada", SqlDbType.DateTime).Value = fecha_llegada;
                                cmd.Parameters.Add("@idafiliado", SqlDbType.Int).Value = idAfiliado;
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Registro ingresado correctamente");
                                Close();
                            }
                            else MessageBox.Show("El bono ya ha sido utilizado");
                    }
                    else MessageBox.Show("El numero de bono no corresponde al numero de afiliado ingresado");
            }

                else MessageBox.Show("Numero de bono incorrecto");
                conexion.Close();
            }//Fin using

        }

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBono_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(textBono.Text, ""))
            {
                string Str = textBono.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros", "Error");
                    textBono.Text = "";
                }
            }
        }//Fin boton aceptar
    }
}

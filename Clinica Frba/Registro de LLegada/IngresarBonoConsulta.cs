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
            int id_ultima_consulta_bono;
            int numero_de_consulta_a_ingresar = 0;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                //verifico si el bono corresponde a ese afiliado
                SqlCommand cmd = new SqlCommand(string.Format(
                    "SELECT ID_AFILIADO FROM YOU_SHALL_NOT_CRASH.BONO_CONSULTA WHERE ID_Bono_Consulta = {0}", id_bono_ingresado), conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    if (Convert.ToInt32(reader.GetSqlInt32(0).Value) == idAfiliado)  //Si el idAfiliado del bono es = al id del afiliado ingresado, ese bono pertenece al afiliado, entonces sigo
                    {

                        cmd.Dispose();
                        reader.Close();

                        cmd = new SqlCommand(string.Format(
                            "SELECT Numero_Consulta_Afiliado FROM YOU_SHALL_NOT_CRASH.BONO_CONSULTA WHERE ID_Bono_Consulta ={0}", id_bono_ingresado), conexion);
                        reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader.IsDBNull(0))   //Si es null quiere decir que nunca se uso en una consulta
                            {
                                cmd.Dispose();
                                reader.Close();

                                //Obtengo el ultimo numero de consulta del afiliado segun los bonos usados 

                                cmd = new SqlCommand(string.Format(
                                    "select MAX(b.Numero_Consulta_Afiliado) from YOU_SHALL_NOT_CRASH.AFILIADO a join YOU_SHALL_NOT_CRASH.BONO_CONSULTA b on a.ID_Afiliado = b.ID_Afiliado WHERE a.ID_Afiliado={0} group by a.ID_Afiliado", idAfiliado), conexion);
                                reader = cmd.ExecuteReader();

                                if (reader.Read())
                                {
                                    if (reader.IsDBNull(0))//Si el maximo numero de consulta es null,es porque nunca se atendio
                                    {
                                        id_ultima_consulta_bono = 0;
                                    }
                                    else 
                                    {
                                        id_ultima_consulta_bono = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                                    }        
                                    
                                    numero_de_consulta_a_ingresar = id_ultima_consulta_bono + 1;
                                }

                                cmd.Dispose();
                                reader.Close();


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
                            }
                            else MessageBox.Show("El bono ya ha sido utilizado");
                        }
                    }
                    else MessageBox.Show("El numero de bono no corresponde al numero de afiliado ingresado");
                }//Fin primer reader
                else MessageBox.Show("Numero de bono incorrecto");
                cmd.Dispose();
                reader.Close();
                conexion.Close();
            }//Fin using

        }

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }//Fin boton aceptar
    }
}

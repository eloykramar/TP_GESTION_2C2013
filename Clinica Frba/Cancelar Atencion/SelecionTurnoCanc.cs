using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Registro_de_LLegada;
using System.Data.SqlClient;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Seleccion_Turno_Canc : Seleccion_Turno
    {
        public Seleccion_Turno_Canc(int idP, int idA)
            : base(idP, idA)
        {
            //ERRORRRRR
            //EN LA BUSQUEDA DE TURNOS A CANCELAR NO TIENEN QUE APARECER LOS DE LA FECHA ACTUAL 
            InitializeComponent();
        }

        public override void mainTurnos()
        {
            int cancelado = 0;

            try
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();
                    int id_turno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_TURNO"].Value);   //Levanto el id del turno en el datagridview
                    DateTime fechaActual = getFechaActual();

                    SqlCommand cmd = new SqlCommand(string.Format(
                        "SELECT Cancelado FROM YOU_SHALL_NOT_CRASH.TURNO WHERE ID_TURNO = {0}", id_turno), conexion);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cancelado = Convert.ToInt32(reader.GetSqlBoolean(0).Value);
                    }
                    cmd.Dispose();
                    reader.Close();

                    if (String.Equals(textBox1.Text,""))
                        throw new Exception("Debe especificar un motivo de la cancelacion");

                    if (cancelado == 0)
                    {
                        cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Cancelar_turno_afiliado", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_turno", SqlDbType.Int).Value = id_turno;
                        cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaActual;
                        cmd.Parameters.Add("@motivo", SqlDbType.NVarChar).Value = textBox1.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Su turno ha sido cancelado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El turno ya se encuentra cancelado");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

    }
}


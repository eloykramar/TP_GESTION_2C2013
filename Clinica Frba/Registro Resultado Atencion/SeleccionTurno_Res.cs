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


namespace Clinica_Frba.Registro_Resultado_Atencion
{
    public partial class Seleccion_Turno_Result : Seleccion_Turno
    {
        public Seleccion_Turno_Result(long idP, int idA)
            : base(idP, idA)
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string dia = Convert.ToString(fechaActual);
                    //lleno el datagrid
                    string busquedaDeAfiliado = "";
                    if (idA > 0) busquedaDeAfiliado = " AND ID_AFILIADO=" + idA;
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_TURNO, NUMERO, FECHA, FECHA_LLEGADA FROM YOU_SHALL_NOT_CRASH.TURNO where ID_TURNO IN (SELECT ID_TURNO FROM YOU_SHALL_NOT_CRASH.CONSULTA) AND ID_PROFESIONAL=" + idP + " AND FECHA>='" + dia + "'" + busquedaDeAfiliado + " AND CANCELADO = 0", conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_TURNO"].Visible = false;
                    dataGridView1.ReadOnly = true;



                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        public override void mainTurnos()
        {
            //cargo resultado de consulta.
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_TURNO"].Value);
                new Registro_Consulta(idTurno).ShowDialog();
                Close();

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }

    }
}


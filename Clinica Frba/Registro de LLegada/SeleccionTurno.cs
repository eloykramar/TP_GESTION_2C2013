using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.Registro_de_LLegada
{
    public partial class Seleccion_Turno : Form1
    {
        int idTurno;

        public Seleccion_Turno()
        {
            InitializeComponent();

        }

        public Seleccion_Turno(int idP, int idA)
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string dia="12/11/2013";//FALTA LEERLO DEL ARCHIVO DE CONFIG.
                    //lleno el datagrid
                    string busquedaDeAfiliado = "";
                    if (idA > 0) busquedaDeAfiliado = " AND ID_AFILIADO=" + idA;
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_TURNO, NUMERO, FECHA, FECHA_LLEGADA, CANCELADO FROM YOU_SHALL_NOT_CRASH.TURNO where ID_PROFESIONAL=" + idP + busquedaDeAfiliado + " AND FECHA>='" + dia + "'" + " AND CANCELADO = 0", conexion);

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


        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            mainTurnos();   
        }

        public virtual void mainTurnos()
        {
            //Registro llegada turno.
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (Convert.ToByte((dataGridView1.CurrentRow.Cells["CANCELADO"].Value)) == 0)   //Si el turno no esta cancelado trato de registrarlo
                {
                    idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_TURNO"].Value);
                    new RegistrarHora(idTurno).ShowDialog();
                }
                else //Si esta cancelado muestro el mensaje
                {
                    MessageBox.Show("El turno que ingresó esta cancelado");
                }
            }
            else MessageBox.Show("No hay filas seleccionadas");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

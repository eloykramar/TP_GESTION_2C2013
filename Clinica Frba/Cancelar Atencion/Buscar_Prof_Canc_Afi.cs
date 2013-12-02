using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;
using System.Data.SqlClient;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Buscar_Prof_Canc_Afi : Pedir_Turnos
    {
        public Buscar_Prof_Canc_Afi(int x) : base(x)
        {
            InitializeComponent();

            String where = "";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    //lleno el datagrid
                    if (textBox2.Text != "")
                    {
                        String idAfiliado = buscarIdAfiliado(textBox2.Text);
                        where = " AND a.ID_Afiliado = " + idAfiliado;
                    }
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT p.ID_Profesional, (p.Nombre+' '+p.Apellido) Nombre, p.DNI from YOU_SHALL_NOT_CRASH.AFILIADO a LEFT JOIN YOU_SHALL_NOT_CRASH.TURNO t ON t.ID_AFILIADO = a.ID_Afiliado JOIN YOU_SHALL_NOT_CRASH.PROFESIONAL p on p.ID_PROFESIONAL = t.ID_PROFESIONAL where ACTIVO = 1 AND t.Cancelado = 0" + where, conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_Profesional"].Visible = false;
                    dataGridView1.ReadOnly = true;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
                conexion.Close();
            }
        }
        public override void turnos()
        {
            // abro ventana para reservar
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            int idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            string afi = textBox2.Text;
            int idA = getIdAfiliadoxNro(afi);
            if (idA != 0)
            {
                //abro ventana para dar baja
                (new Seleccion_Turno_Canc(idP, idA)).ShowDialog();
            }
            else
            {
                MessageBox.Show("El nro de afiliado incorrecto", "Error");
            }
        }

        public override void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                if (textBox2.Text != "")
                {
                    string afi = " AND t.ID_AFILIADO =" + buscarIdAfiliado(textBox2.Text);
                    string nom = " AND (P.Nombre+' '+P.Apellido) like '%" + textBox1.Text + "%'";
                    string esp = " AND E.Descripcion like '%" + textBox3.Text + "%'";
                    string fecha = " AND t.FECHA >= '" + getFechaActual() + "'";

                    string where = "where P.ACTIVO=1 AND t.Cancelado = 0";
                    where += fecha;
                    if (!String.Equals(textBox1.Text, "")) where += nom;
                    if (!String.Equals(textBox3.Text, "")) where += esp;
                    if (!String.Equals(textBox2.Text, "")) where += afi;


                    //lleno el datagrid

                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT P.ID_Profesional, (Nombre+' '+Apellido) Nombre, DNI from (YOU_SHALL_NOT_CRASH.PROFESIONAL P JOIN YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL EP ON EP.ID_PROFESIONAL=P.ID_PROFESIONAL) join YOU_SHALL_NOT_CRASH.ESPECIALIDAD E ON E.CODIGO_ESPECIALIDAD=EP.CODIGO_ESPECIALIDAD JOIN YOU_SHALL_NOT_CRASH.TURNO t ON t.ID_PROFESIONAL = p.ID_PROFESIONAL " + where, conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_Profesional"].Visible = false;
                    dataGridView1.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Ingrese numero de afiliado");
                }
                conexion.Close();
            }

        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {

        }


    }
}

using System;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;
using Clinica_Frba.Registro_de_LLegada;
using System.Data.SqlClient;
using System.Data;


namespace Clinica_Frba.Generar_Receta
{
    public partial class BuscarProfesional : Pedir_Turnos
    {
        public BuscarProfesional(int x)
            : base(x)
        {
            InitializeComponent();
        }
        public override void turnos()
        {
            //abro ventana para confirmar
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            int idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            string afi = textBox2.Text;
            int idA = getIdAfiliadoxNro(afi);
            if (idA != 0)
            {
                (new SeleccionarTurnoParaAtencion(idP, idA)).ShowDialog();
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


    }
}

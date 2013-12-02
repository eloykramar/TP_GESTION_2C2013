using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Clinica_Frba.Pedir_Turno
{
    public partial class Pedir_Turnos : Form1
    {
        public Pedir_Turnos()
        {
            arrancar(0);
        }
        public Pedir_Turnos(int Afiliado)
        {
            arrancar(Afiliado);
        }

        public void arrancar(int Afiliado)
        {
            InitializeComponent();


            if (Afiliado != 0)
            {
                textBox2.Text = Afiliado.ToString();
                textBox2.ReadOnly = true;
            }
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    //lleno el datagrid

                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT p.ID_Profesional, (p.Nombre+' '+p.Apellido) Nombre, p.DNI from YOU_SHALL_NOT_CRASH.PROFESIONAL p  where ACTIVO = 1", conexion);

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
        private void Pedir_Turno_Load(object sender, EventArgs e)
        {

        }

        public virtual void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {

                    string nom = " AND (P.Nombre+' '+P.Apellido) like '%" + textBox1.Text + "%'";
                    string esp = " AND E.Descripcion like '%" + textBox3.Text + "%'";

                    string where = "where P.ACTIVO=1";
                    if (!String.Equals(textBox1.Text, "")) where += nom;
                    if (!String.Equals(textBox3.Text, "")) where += esp;
                    



                    //lleno el datagrid

                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT P.ID_Profesional, (Nombre+' '+Apellido) Nombre, DNI from (YOU_SHALL_NOT_CRASH.PROFESIONAL P JOIN YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL EP ON EP.ID_PROFESIONAL=P.ID_PROFESIONAL) join YOU_SHALL_NOT_CRASH.ESPECIALIDAD E ON E.CODIGO_ESPECIALIDAD=EP.CODIGO_ESPECIALIDAD " + where, conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_Profesional"].Visible = false;
                    dataGridView1.ReadOnly = true;

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(textBox2.Text, ""))
            {
                string Str = textBox2.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros");
                    textBox2.Text = "";
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (!textBox2.ReadOnly) textBox2.Text = "";
            textBox3.Text = "";
            dataGridView1.DataSource = "";
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            
            this.turnos();
        }

        public virtual void turnos()
        {   // abro ventana para reservar
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            int idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            string afi = textBox2.Text;
            int idA = getIdAfiliadoxNro(afi);
            if (idA != 0)
            {
                (new Turnos(idP, idA)).ShowDialog();
            }else
            {
                MessageBox.Show("El nro de afiliado incorrecto", "Error");
            }
        }

        public String buscarIdAfiliado(String nroAfiliado) 
        {
            String idAf = "";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd1 = new SqlCommand("USE GD2C2013 SELECT a.ID_AFILIADO from YOU_SHALL_NOT_CRASH.AFILIADO a where a.Nro_Afiliado = " + textBox2.Text, conexion);
                idAf = cmd1.ExecuteScalar().ToString();
                cmd1.Dispose();
                conexion.Close();
            }
            return idAf;   
        }   //Fin buscarIdAfiliado
    }
}

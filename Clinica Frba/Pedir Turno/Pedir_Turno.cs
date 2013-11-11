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

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select CODIGO_ESPECIALIDAD, Descripcion from YOU_SHALL_NOT_CRASH.ESPECIALIDAD", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();
                    tablaDeNombres.Rows.Add();
                    tablaDeNombres.AcceptChanges();
                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.ValueMember = "CODIGO_ESPECIALIDAD";
                    comboBox1.DataSource = tablaDeNombres;

                    //lleno el datagrid
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT ID_Profesional, (Nombre+' '+Apellido) Nombre, DNI from YOU_SHALL_NOT_CRASH.PROFESIONAL where ACTIVO = 1", conexion);

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
            }
        }
        private void Pedir_Turno_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                string nom = " AND (P.Nombre+' '+P.Apellido) like '%" + textBox1.Text + "%'";
                string esp = " AND EP.CODIGO_ESPECIALIDAD=" + comboBox1.SelectedValue;

                string where = "where P.ACTIVO=1";
                if (!String.Equals(textBox1.Text, "")) where += nom;
                if (!String.Equals(comboBox1.SelectedValue.ToString(), "")) where += esp;



                //lleno el datagrid

                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select DISTINCT P.ID_Profesional, (Nombre+' '+Apellido) Nombre, DNI from YOU_SHALL_NOT_CRASH.PROFESIONAL P JOIN YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL EP ON EP.ID_PROFESIONAL=P.ID_PROFESIONAL " + where, conexion);

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
            comboBox1.SelectedIndex = 0;
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            this.turnos();
        }

        private void Busqueda_Enter(object sender, EventArgs e)
        {

        }
        public virtual void turnos()
        {
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            int idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            string afi = textBox2.Text;
            int idA = 0;//falta validar q el afiliado exista
            (new Turnos(idP, idA)).ShowDialog();
        }
    }
}

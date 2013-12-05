using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Clinica_Frba.Generar_Receta;

namespace Clinica_Frba.Registro_Resultado_Atencion
{
    public partial class Registro_Consulta : Form1
    {
        int idC = 0;
        int idTurno=0;
        public Registro_Consulta(int idT)
        {
            idTurno = idT;
            InitializeComponent();
            listSin.DisplayMember = "Text";
            listEnf.DisplayMember = "Text";
            ListViewItem myItem;
            SqlCommand cmdC = new SqlCommand("USE GD2C2013 SELECT ID_CONSULTA FROM YOU_SHALL_NOT_CRASH.CONSULTA WHERE ID_TURNO="+idT, this.obtenerConexion());
            idC = ExecuteScalarOrZero(cmdC);
            cmdC.Dispose();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                //lleno sintomas cargados
                SqlCommand cmd = new SqlCommand("USE GD2C2013 SELECT ID_SINTOMA, DESCRIPCION FROM YOU_SHALL_NOT_CRASH.SINTOMA WHERE ID_SINTOMA IN (SELECT ID_SINTOMA FROM YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA WHERE ID_CONSULTA=" + idC + ")", conexion);
                SqlDataReader cons = cmd.ExecuteReader();
                if (cons.HasRows)
                {
                    while (cons.Read())
                    {
                        myItem = new ListViewItem();
                        myItem.Text = Convert.ToString(cons["DESCRIPCION"]);
                        myItem.Tag = Convert.ToString(cons["ID_SINTOMA"]);
                        listSin.Items.Add(myItem);
                    }
                }
                cons.Dispose();
                cmd.Dispose();

                //lleno enfermedades cargadas
                cmd = new SqlCommand("USE GD2C2013 SELECT ID_ENFERMEDAD, DESCRIPCION FROM YOU_SHALL_NOT_CRASH.ENFERMEDAD WHERE ID_ENFERMEDAD IN (SELECT ID_ENFERMEDAD FROM YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA WHERE ID_CONSULTA=" + idC + ")", conexion);
                cons = cmd.ExecuteReader();
                if (cons.HasRows)
                {
                    while (cons.Read())
                    {
                        myItem = new ListViewItem();
                        myItem.Text = Convert.ToString(cons["DESCRIPCION"]);
                        myItem.Tag = Convert.ToString(cons["ID_ENFERMEDAD"]);
                        listEnf.Items.Add(myItem);
                    }
                }
                cons.Dispose();
                cmd.Dispose();
                conexion.Close();
            }
            llenarEnfermedades();
            llenarSintomas();
            
        }

        public void llenarEnfermedades()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmdE = new SqlCommand("USE GD2C2013 SELECT ID_ENFERMEDAD, Descripcion FROM YOU_SHALL_NOT_CRASH.ENFERMEDAD", conexion);

                SqlDataAdapter adapterE = new SqlDataAdapter(cmdE);
                DataTable tableE = new DataTable();
                tableE.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapterE.Fill(tableE);
                dgvEnf.DataSource = tableE;
                dgvEnf.Columns["ID_ENFERMEDAD"].Visible = false;
                dgvEnf.ReadOnly = true;
                cmdE.Dispose();
                conexion.Close();
            }
        }

        public void llenarSintomas()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmdS = new SqlCommand("USE GD2C2013 SELECT ID_SINTOMA, Descripcion FROM YOU_SHALL_NOT_CRASH.SINTOMA", conexion);

                SqlDataAdapter adapterS = new SqlDataAdapter(cmdS);
                DataTable tableS = new DataTable();
                tableS.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapterS.Fill(tableS);
                dgvSin.DataSource = tableS;
                dgvSin.Columns["ID_SINTOMA"].Visible = false;
                dgvSin.ReadOnly = true;
                cmdS.Dispose();
                conexion.Close();
            }
        }

        private void btnAddSin_Click(object sender, EventArgs e)
        {
            if (dgvSin.SelectedRows.Count != 0)
            {
                ListViewItem myItem = new ListViewItem();
                myItem.Text = Convert.ToString(dgvSin.CurrentRow.Cells["Descripcion"].Value);
                myItem.Tag = Convert.ToString(dgvSin.CurrentRow.Cells["ID_SINTOMA"].Value);
                listSin.Items.Add(myItem);

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }



        private void btnDelSin_Click(object sender, EventArgs e)
        {
            if (listSin.SelectedIndex >= 0)
            {
                ListViewItem i = (ListViewItem)listSin.SelectedItem;
                listSin.Items.Remove(i);
            }
            else
            {
                MessageBox.Show("No hay ningun item seleccionado");
            }
        }

        private void btnAddEnf_Click(object sender, EventArgs e)
        {
            if (dgvEnf.SelectedRows.Count != 0)
            {
                ListViewItem myItem = new ListViewItem();
                myItem.Text = Convert.ToString(dgvEnf.CurrentRow.Cells["Descripcion"].Value);
                myItem.Tag = Convert.ToString(dgvEnf.CurrentRow.Cells["ID_ENFERMEDAD"].Value);
                listEnf.Items.Add(myItem);

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }

        private void btnDelEnf_Click(object sender, EventArgs e)
        {
            if (listEnf.SelectedIndex >= 0)
            {
                ListViewItem i = (ListViewItem)listEnf.SelectedItem;
                listEnf.Items.Remove(i);
            }
            else
            {
                MessageBox.Show("No hay ningun item seleccionado");
            }
        }

        private void Registro_Consulta_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)//GUARDAR
        {
            ListViewItem item;
            int i = 0;
            string sql = "USE GD2C2013 " +
                    "BEGIN TRANSACTION;" +
                    "DELETE FROM YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA WHERE ID_CONSULTA=" + idC + ";" +
                    "DELETE FROM YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA WHERE ID_CONSULTA=" + idC + ";";

           
            if (listSin.Items.Count > 0)
            {
                sql += "INSERT INTO YOU_SHALL_NOT_CRASH.SINTOMA_CONSULTA (ID_SINTOMA, ID_CONSULTA) VALUES";
                for ( i = 0; i < listSin.Items.Count; i++)
                {
                    item = (ListViewItem)listSin.Items[i];
                    sql += " ( " + item.Tag + " , " + idC + " ),";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ";";
            }
            if (listEnf.Items.Count > 0)
            {
                sql += "INSERT INTO YOU_SHALL_NOT_CRASH.ENFERMEDAD_CONSULTA (ID_ENFERMEDAD, ID_CONSULTA) VALUES";
                for ( i = 0; i < listEnf.Items.Count; i++)
                {
                    item = (ListViewItem)listEnf.Items[i];
                    sql += " ( " + item.Tag + " , " + idC + " ),";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ";";
            }
            sql += "COMMIT;";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conexion.Close();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(string.Format("SELECT ID_AFILIADO FROM YOU_SHALL_NOT_CRASH.TURNO WHERE ID_TURNO = {0}", idTurno), conexion);
                int idAfi = ExecuteScalarOrZero(cmd);
                cmd.Dispose();
                cmd = new SqlCommand(string.Format("SELECT ID_RECETA FROM YOU_SHALL_NOT_CRASH.RECETA WHERE ID_CONSULTA = {0}", idC), conexion);
                int idRec = ExecuteScalarOrZero(cmd);
                cmd.Dispose();
                if (idRec == 0)
                {

                    //creo una receta
                    cmd = new SqlCommand(string.Format("INSERT INTO YOU_SHALL_NOT_CRASH.RECETA (ID_CONSULTA) VALUES ({0})", idC), conexion);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    //leo el id de la nueva receta
                    cmd = new SqlCommand(string.Format("SELECT ID_RECETA FROM YOU_SHALL_NOT_CRASH.RECETA WHERE ID_CONSULTA = {0}", idC), conexion);
                    idRec = ExecuteScalarOrZero(cmd);
                    cmd.Dispose();
                }
                conexion.Close();
                new Receta_Medica(idC, idAfi, idRec).ShowDialog();
            }
        }
    }
}

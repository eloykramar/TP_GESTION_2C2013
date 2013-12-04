using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Registro_Resultado_Atencion
{
    public partial class Registro_Consulta : Form1
    {
        List<Cod_Desc> sintomas = new List<Cod_Desc>();
        List<Cod_Desc> enfermedades = new List<Cod_Desc>();
        

        public Registro_Consulta(int idT)
        {
            InitializeComponent();

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
                SqlCommand cmdS = new SqlCommand("USE GD2C2013 SELECT ID_SINTOMA, Descripcion FROM YOU_SHALL_NOT_CRASH.TOMASIN", conexion);

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

        private void btnEnfermedad_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddSin_Click(object sender, EventArgs e)
        {
            if (dgvSin.SelectedRows.Count != 0)
            {
                ListViewItem myItem = new ListViewItem();
                myItem.Text = Convert.ToString(dgvSin.CurrentRow.Cells["Descripcion"].Value);
                myItem.Tag = Convert.ToString(dgvSin.CurrentRow.Cells["ID_SINTOMA"].Value);
                listSin.Items.Add(myItem);
                Close();

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }

        private void btnAddEnf_Click(object sender, EventArgs e)
        {
            if (dgvEnf.SelectedRows.Count != 0)
            {
                Cod_Desc enf = new Cod_Desc();
                enf.cod = Convert.ToInt32(dgvEnf.CurrentRow.Cells["ID_ENFERMEDAD"].Value);
                enf.des = Convert.ToString(dgvEnf.CurrentRow.Cells["Descripcion"].Value);
                enfermedades.Add(enf);
                
                Close();

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.ABM_de_Afiliado
{
    public partial class ABM_Afiliado : Buscar_Afiliado
    {
        public ABM_Afiliado()
        {
            InitializeComponent();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            this.users = new List<string>();
            this.dnis = new List<string>();
            this.values = new List<string>();
            (new DetalleAfiliado.Alta(this, 1)).ShowDialog();
        }


        private void btnModif_Click(object sender, EventArgs e)
        {
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            string id = dataGridView1.CurrentRow.Cells["ID_Afiliado"].Value.ToString();//dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].FormattedValue(int)
            (new DetalleAfiliado.Modif(id)).ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            string id = dataGridView1.CurrentRow.Cells["ID_Afiliado"].Value.ToString();
            string nom = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();

            if (MessageBox.Show("¿Esta seguro que desea eliminar al afiliado " + nom + "?", "Confirme", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }

            using (SqlConnection conexion = this.obtenerConexion())
            {

                //lleno el datagrid

                SqlCommand del = new SqlCommand("USE GD2C2013 UPDATE YOU_SHALL_NOT_CRASH.AFILIADO SET Fecha_Baja=getDate() WHERE ID_Afiliado=" + id, conexion);
                conexion.Open();
                del.ExecuteNonQuery();

                btnClean.PerformClick();
                btnBuscar.PerformClick();
            }

        }

        
    }
}

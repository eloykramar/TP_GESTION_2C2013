using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.ABM_de_Afiliado;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Buscar_Afi_Canc : Buscar_Afiliado
    {
        public Buscar_Afi_Canc()
        {
            InitializeComponent();
        }

        private void btnBuscarProf_Click(object sender, EventArgs e)
        {
            int c = dataGridView1.SelectedRows.Count;
            if (c < 1) return;
            int idA = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Afiliado"].Value);
            (new Buscar_Prof_Canc_Afi(idA)).ShowDialog();
        }
    }
}
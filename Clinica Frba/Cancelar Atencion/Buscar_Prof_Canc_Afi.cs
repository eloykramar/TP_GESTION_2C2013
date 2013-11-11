using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Buscar_Prof_Canc_Afi : Pedir_Turnos
    {
        public Buscar_Prof_Canc_Afi(int x) : base(x)
        {
            InitializeComponent();
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
                MessageBox.Show("doy de baja un turno");
                //(new Turnos(idP, idA)).ShowDialog();
            }
            else
            {
                MessageBox.Show("El nro de afiliado incorrecto", "Error");
            }
        }


    }
}

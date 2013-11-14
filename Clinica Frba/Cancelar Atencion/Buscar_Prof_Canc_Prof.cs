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
    public partial class Buscar_Prof_Canc_Prof : Pedir_Turnos
    {
        public Buscar_Prof_Canc_Prof(int x)
            : base(x)
        {
            InitializeComponent();
        }
        public override void turnos()
        {
            //abro ventana para dar baja
            int idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            new DiaCancelado(idP).ShowDialog();
        }


    }
}

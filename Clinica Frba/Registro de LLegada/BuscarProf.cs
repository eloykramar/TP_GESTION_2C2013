using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;


namespace Clinica_Frba.Registro_de_LLegada
{
    public partial class BuscarProf : Pedir_Turnos
    {
        int IdAfiliado = 0;
        public BuscarProf(int idA)
            : base(idA)
        {
            InitializeComponent();
            IdAfiliado = idA;
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
                (new Seleccion_Turno(idP, idA)).ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("El nro de afiliado incorrecto", "Error");
            }

        }


    }
}

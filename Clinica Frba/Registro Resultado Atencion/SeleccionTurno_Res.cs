using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Registro_de_LLegada;


namespace Clinica_Frba.Registro_Resultado_Atencion
{
    public partial class Seleccion_Turno_Result : Seleccion_Turno
    {
        public Seleccion_Turno_Result(long idP, int idA)
            : base(idP, idA)
        {
            InitializeComponent();
        }

        public override void mainTurnos()
        {
            //cargo resultado de consulta.
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_TURNO"].Value);
                new Registro_Consulta(idTurno).ShowDialog();
                Close();

            }
            else MessageBox.Show("No hay filas seleccionadas");
        }

    }
}


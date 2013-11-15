using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Registro_de_LLegada;
using Clinica_Frba.Generar_receta;

namespace Clinica_Frba.Generar_Receta
{
    public partial class SeleccionarTurnoParaAtencion : Seleccion_Turno
    {
        int idProf, idAfi;
        int idTurno;

        public SeleccionarTurnoParaAtencion(int idP, int idA)
            : base(idP, idA)
        {
            InitializeComponent();
            idProf = idP;
            idAfi = idA;
        }

        public override void mainTurnos()
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (Convert.ToByte((dataGridView1.CurrentRow.Cells["CANCELADO"].Value)) == 0)   //Si el turno no esta cancelado trato de registrarlo
                {
                    idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_TURNO"].Value);
                    new Receta(idProf, idAfi, idTurno).ShowDialog();
                }
                else //Si esta cancelado muestro el mensaje
                {
                    MessageBox.Show("El turno que ingresó esta cancelado");
                }
            }
            else MessageBox.Show("No hay filas seleccionadas");
            
        }
    }
}

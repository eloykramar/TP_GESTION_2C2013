using System;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;
using Clinica_Frba.Registro_de_LLegada;

namespace Clinica_Frba.Generar_Receta
{
    public partial class BuscarProfesional : Pedir_Turnos
    {
        public BuscarProfesional(int x)
            : base(x)
        {
            InitializeComponent();
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
                (new SeleccionarTurnoParaAtencion(idP, idA)).ShowDialog();
            }
            else
            {
                MessageBox.Show("El nro de afiliado incorrecto", "Error");
            }

        }


    }
}

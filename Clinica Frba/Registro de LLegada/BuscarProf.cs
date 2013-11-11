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
        public BuscarProf(int x)  : base(x)
        {

        }
        public override void turnos()
        {
            MessageBox.Show("hola");
        }
    }
}

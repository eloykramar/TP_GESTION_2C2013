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
    public partial class Buscar_Prof_Canc : Pedir_Turnos
    {
        public Buscar_Prof_Canc(int x) : base(x)
        {
            InitializeComponent();
        }
        public override void turnos()
        {
            //abro ventana para dar baja
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Registro_de_LLegada;


namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Seleccion_Turno_Canc : Seleccion_Turno
    {
        public Seleccion_Turno_Canc(int x, int y)
            : base(x, y)
        {
            InitializeComponent();
        }

        public override void mainTurnos()
        {   
            //Cancelo turno.
            MessageBox.Show("doy de baja un turno");
        }

    }
}


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
        public Seleccion_Turno_Result(int idP, int idA)
            : base(idP, idA)
        {
            InitializeComponent();
        }

        public override void mainTurnos()
        {
            //Cancelo turno.
            MessageBox.Show("registro resultado");
        }

    }
}


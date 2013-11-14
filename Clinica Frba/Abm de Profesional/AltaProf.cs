using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.Abm_de_Profesional
{
    public partial class AltaProf : DetalleProf
    {
        public AltaProf()
        {
            InitializeComponent();
        }

        public override void guardarProfesional()
        {
            //das el alta
            MessageBox.Show("hola, doy alta");
        }
    }
}


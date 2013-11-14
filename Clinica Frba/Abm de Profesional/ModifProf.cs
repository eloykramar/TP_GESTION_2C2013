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
    public partial class ModifProf : DetalleProf
    {
        public ModifProf(string dni) : base(dni)
        {
            InitializeComponent();
        }

        private void ModifProf_Load(object sender, EventArgs e)
        {

        }

        public override void guardarProfesional()
        {
            //guardarlo
            MessageBox.Show("hola, modifico");
        }
    }
}

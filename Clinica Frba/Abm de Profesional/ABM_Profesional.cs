using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.ABM_de_Profesional
{
    public partial class ABM_Profesional : Form1
    {
        public ABM_Profesional()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new Alta_Profesional()).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Modificacion_Profesional()).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new Baja_Profesional()).Show();
        }

        
    }
}

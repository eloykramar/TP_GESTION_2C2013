using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.Abm_de_Rol
{
    public partial class ABM_Rol : Form1
    {
            public ABM_Rol()
            {
                InitializeComponent();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            (new Alta_Rol()).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new Baja_Rol()).Show();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new Modificar_Rol()).Show();    
        }

        private void ABM_Rol_Load(object sender, EventArgs e)
        {

        }

        private void ABM_Rol_Load_1(object sender, EventArgs e)
        {

        }
    }
}

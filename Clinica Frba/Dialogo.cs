using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba
{
    public partial class Dialogo : Form
    {
        public Dialogo(String mensaje, String botonLeyenda)
        {
            InitializeComponent();
            String[] mensajes = mensaje.Split(';');
            listBox1.Items.AddRange(mensajes);
            listBox1.Enabled = false;
            button1.Text = botonLeyenda;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

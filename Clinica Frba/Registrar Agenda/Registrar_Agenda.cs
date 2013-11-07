using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.Registrar_Agenda
{
    public partial class Registrar_Agenda : Form1
    {
        int idProfesional;
        
        public Registrar_Agenda(String unID, String unNombre, String unApellido)
        {
            InitializeComponent();
            label1.Text = "Registrando agenda para: " + unApellido + ", " + unNombre;
            idProfesional = Convert.ToInt32(unID);
            comboBox1.Items.Add("00");
            comboBox1.Items.Add("30");
            comboBox2.Items.Add("00");
            comboBox2.Items.Add("30");

        }
    }
}

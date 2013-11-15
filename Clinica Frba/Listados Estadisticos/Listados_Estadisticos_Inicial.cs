using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.Listados_Estadisticos
{
    public partial class Listados_Estadisticos_Inicial : Form1
    {
        int semestre;
        String año;
        
        public Listados_Estadisticos_Inicial()
        {
            InitializeComponent();
            textBox1.Text = "Formato AAAA";
        }

        private void sePuedeGenerarListado()
        {     
            if (textBox1.Text.Equals("Formato AAAA") || textBox1.Text.Length != 4)
            {
                throw new Exception("Año no ingresado o en formato erroneo;");
            }

            if (Convert.ToInt64(textBox1.Text) < 2013)
            {
                throw new Exception("Los listados Estadisticos son a partir de 2013");
            }

            if (!(checkBox1.Checked) && !(checkBox2.Checked))
            {
                throw new Exception("Semestre no seleccionado;");
            }

            if ((checkBox1.Checked) && (checkBox2.Checked))
            {
                throw new Exception("Mas de un semestre seleccionado;");
            }
        }

        private void validar()
        {
            this.sePuedeGenerarListado();
            año = textBox1.Text;

            if (checkBox1.Checked)
            {
                semestre = 1;
            }
            else
            {
                semestre = 2;
            }
        }

        private void mandar_pedido(int unListado)
        {
            try
            {
                validar();
                new Listado_Particular(unListado, año, semestre).ShowDialog();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }


        /*codigos de listado
        1 Especialidades con mas cancelaciones
        2 Bonos farmacia vencidos
        3 Especialidades con mas bonos farmacia recetados
        4 Bonos no usados por quien los compro*/

        private void button1_Click(object sender, EventArgs e)
        {
            mandar_pedido(1);
        }  

        private void button2_Click(object sender, EventArgs e)
        {
            mandar_pedido(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mandar_pedido(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mandar_pedido(4);
        }

    }
}

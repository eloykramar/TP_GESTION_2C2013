using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Listados_Estadisticos
{
    public partial class Listado_Particular : Form1
    {
        public Listado_Particular(int unListado, String unAño, int unSemestre)
        {
            InitializeComponent();
            DataTable tabla = new DataTable();
            int mesFinal;
            int mesInicial;

            if (unSemestre == 1)
            {
                mesInicial = 1;
                mesFinal = 6;
            }
            else
            {
                mesInicial = 7;
                mesFinal = 12;
            }
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                if (unListado == 1)
                {
                    label1.Text = "TOP 5 de especialidades con mas cancelaciones";
                    cargarATablaParaDataGripView("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.Top5_Especialidades_Mas_Canceladas_En('" + unAño + "'," + mesInicial + "," + mesFinal + ")", ref tabla, conexion);                        
                }

                if (unListado == 2)
                {
                    DateTime fechaActual = getFechaActual();                    
                    label1.Text = "TOP 5 afiliados con bonos farmacia vencidos";
                    cargarATablaParaDataGripView("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.Top5_Bonos_Farmacia_Vencidos_En('" + unAño + "'," + mesInicial + "," + mesFinal + ",'" +fechaActual+ "')", ref tabla, conexion);                        
                }


                if (unListado == 3)
                {
                    label1.Text = "TOP 5 de especialidades con mas bonos farmacia recetados";
                    cargarATablaParaDataGripView("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.Top5_Especialidades_Que_Mas_Recetaron_En('" + unAño + "'," + mesInicial + "," + mesFinal + ")", ref tabla, conexion);
                }

                if (unListado == 4)
                {
                    label1.Text = "TOP 10 de afiliados que usaron bonos de otro";
                    cargarATablaParaDataGripView("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.Top10_Afiliado_Que_Uso_Bonos_De_Otro_En('" + unAño + "'," + mesInicial + "," + mesFinal + ")", ref tabla, conexion);
                }

               
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

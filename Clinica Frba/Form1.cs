using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Clinica_Frba
{
    public partial class Form1 : Form
    {
        private String stringDeConexion;
        private DateTime fechaActual;

        public Form1()
        {
            InitializeComponent();
            this.configurarse();
            Console.Out.WriteLine("Conexion: " + this.stringDeConexion);
            Console.Out.WriteLine("fecha: " + this.fechaActual.ToString("yyyy/MM/dd"));

            /*ejemplo de como usar la conexion que agarra desde el txt. es solo para que lo vean despues se borra.
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {                    
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.FUNCIONALIDAD", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.DataSource = tablaDeNombres;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }   
            }
             */
        }

        private void configurarse()
        {
            String linea;

            String ruta = Application.StartupPath + @"\CLINICAFRBA.txt";

            StreamReader archivo = new StreamReader(ruta, Encoding.ASCII);

            while ((linea = archivo.ReadLine()) != null)
            {
                String[] renglon = linea.Split(':');
                String primerPalabra = renglon[0];

                if (primerPalabra.Equals("conexion"))
                    this.stringDeConexion = renglon[1];

                if (primerPalabra.Equals("fecha"))
                {
                    String[] fecha = renglon[1].Split('/');
                    this.fechaActual = new DateTime(Convert.ToInt32(fecha[0]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[2]));
                }
            }

            archivo.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public DateTime getFechaActual()
        {
            return this.fechaActual;
        }

        public void setFechaActual(String fecha)
        {
            this.fechaActual = Convert.ToDateTime(fecha);
        }

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(this.stringDeConexion);
        }
      
    }
}

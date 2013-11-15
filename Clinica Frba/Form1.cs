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

        public void cargarATablaParaDataGripView(string unaConsulta, ref DataTable unaTabla, SqlConnection unaConexion)
        {
            SqlCommand cmd = new SqlCommand(unaConsulta, unaConexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);            
            adapter.Fill(unaTabla);
        }

        public DataGridViewButtonColumn crearBotones(String nombreColumna, String leyendaBoton)
        {
            DataGridViewButtonColumn botones = new DataGridViewButtonColumn();
            botones.HeaderText = nombreColumna;
            botones.Text = leyendaBoton;
            botones.UseColumnTextForButtonValue = true;
            botones.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            return botones;
        }

        public int getIdAfiliadoxNro(string nro)
        {
            int id = 0;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_Afiliado from YOU_SHALL_NOT_CRASH.AFILIADO WHERE Fecha_Baja IS NULL and Nro_Afiliado=" + nro, conexion);
                try
                {
                    conexion.Open();
                    id = (Int32)cmd2.ExecuteScalar();
                    return id;
                }
                catch (Exception ex)
                {
                    if (String.Equals(ex.Message,"Referencia a objeto no establecida como instancia de un objeto.")) return 0;
                    throw ex;
                }
            }

        }

        public int getNroxUser(string user)
        {
            int nro = 0;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select Nro_Afiliado from YOU_SHALL_NOT_CRASH.AFILIADO A join YOU_SHALL_NOT_CRASH.USUARIo U ON A.DNI=U.DNI_USUARIO WHERE A.Fecha_Baja IS NULL and U.USERNAME='" + user + "'", conexion);
                try
                {
                    conexion.Open();
                    nro = (int)cmd2.ExecuteScalar();
                    return nro;
                }
                catch (Exception ex)
                {
                    if (String.Equals(ex.Message, "Referencia a objeto no establecida como instancia de un objeto.")) return 0;
                    throw ex;
                }
            }

        }
        public int getIdPxUser(string user)
        {
            int id = 0;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_PROFESIONAL from YOU_SHALL_NOT_CRASH.PROFESIONAL P join YOU_SHALL_NOT_CRASH.USUARIo U ON P.DNI=U.DNI_USUARIO WHERE P.ACTIVO=1 and U.USERNAME='" + user + "'", conexion);
                try
                {
                    conexion.Open();
                    id = (int)cmd2.ExecuteScalar();
                    return id;
                }
                catch (Exception ex)
                {
                    if (String.Equals(ex.Message, "Referencia a objeto no establecida como instancia de un objeto.")) return 0;
                    throw ex;
                }
            }

        }
        public int getIdAxUser(string user)
        {
            return getIdAfiliadoxNro(getNroxUser(user).ToString());
        }
    }
}

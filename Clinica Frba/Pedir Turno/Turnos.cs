using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Pedir_Turno
{
    public partial class Turnos : Form1
    {
        int idAfiliado;
        int idProfesional;
        
        
        public Turnos(int idP, int idA)
        {
            InitializeComponent();
            idAfiliado = idA;
            idProfesional = idP;
            List<String> Lista_dias = new List<String>();
            
            try
            {

                using (SqlConnection conexion = this.obtenerConexion())
                {   //la declaracion de abajo solo está para q no pinche. hay q redefinir la busqueda para q lo haga con estos ids en vez de la busqueda con like.
                    //string pProfesional = "";
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(string.Format(
                             "SELECT t.ID_TURNO, (p.NOMBRE + ' ' + p.APELLIDO) as Nombre, t.FECHA as Fecha, (DATENAME ( weekday, t.FECHA )) as Dia FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.TURNO t on p.ID_PROFESIONAL = t.ID_PROFESIONAL where t.FECHA>=GETDATE() and t.Cancelado=0 and t.ID_PROFESIONAL={0} order by t.FECHA", idProfesional.ToString()), conexion);
                   
                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_TURNO"].Visible = false;
                    dataGridView1.ReadOnly = true; 
                    
                    cmd.Dispose();
                    
                    cmd = new SqlCommand(string.Format(
                        "SELECT distinct DATEPART ( WEEKDAY, t.FECHA ),(DATENAME ( weekday, t.FECHA )) as Dia FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.TURNO t on p.ID_PROFESIONAL = t.ID_PROFESIONAL where t.Cancelado=0 and t.FECHA>=GETDATE() and t.FECHA is not null and p.ID_PROFESIONAL={0}", idProfesional), conexion);

                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    while (reader.Read())
                    {
                         String dia = reader.GetString(1);
                        Lista_dias.Add(dia);
                    }
                    
                    cmd.Dispose();
                    reader.Close();
                    conexion.Close();
                    
                    comboBox4.DataSource = Lista_dias;
                    

                }//FIN USING
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();

            }
            
            
        }//FIN TURNOS





        public void button2_Click(object sender, EventArgs e)
        {
            List<Turno_por_profesional> lista_filtrados = new List<Turno_por_profesional>();
            //String profesional = (Convert.ToString(dataGridView1[0,0].Value));
            string dia =Convert.ToString(comboBox4.SelectedItem);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(string.Format(
                                 "SELECT t.ID_TURNO, (p.NOMBRE + ' ' + p.APELLIDO) as Nombre, t.FECHA as Fecha, (DATENAME ( weekday, t.FECHA )) as Dia FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.TURNO t on p.ID_PROFESIONAL = t.ID_PROFESIONAL where t.FECHA>=GETDATE() and t.Cancelado=0 and t.ID_PROFESIONAL={0} and (DATENAME ( weekday, t.FECHA )) like '%{1}%' order by t.FECHA", idProfesional.ToString(), dia), conexion);
                
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter2.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["ID_TURNO"].Visible = false;
                dataGridView1.ReadOnly = true;
                
                /*
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    Turno_por_profesional turno_filtrado = new Turno_por_profesional();
                    turno_filtrado.profesional = Convert.ToString(reader.GetString(0));
                    turno_filtrado.fecha = Convert.ToString(reader.GetSqlDateTime(1));
                    turno_filtrado.dia = Convert.ToString(reader.GetString(2));
                    lista_filtrados.Add(turno_filtrado);
                }

                cmd.Dispose();
                reader.Close();
                conexion.Close();

                dataGridView1.DataSource= lista_filtrados;*/
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime dia_reservacion = (dateTimePicker1.Value);
            //String profesional = Convert.ToString(dataGridView1[0,0].Value);

            String fecha_reservacion = dia_reservacion.ToString("d");
            Int64 hh_mm= Convert.ToInt64(comboBox1.SelectedItem);
            Int64 hh = hh_mm / 100;
            Int64 mm = hh_mm - hh * 100;
            String hora;

            if (mm != 0)
            {
                hora = Convert.ToString(hh) + ':' + Convert.ToString(mm);
            }
            else
            {
                hora = Convert.ToString(hh) + ':' + '0' + Convert.ToString(mm);
            }
            if (hh < 10)
            {
                fecha_reservacion = fecha_reservacion + ' ' + '0' + hora + ':' + "00.000";
            }
            else
            {
                fecha_reservacion = fecha_reservacion + ' ' + hora + ':' + "00.000";
            }
            Convert.ToDateTime(fecha_reservacion);

            
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(string.Format(
                    "SELECT t.Fecha FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.TURNO t on p.ID_PROFESIONAL = t.ID_PROFESIONAL and t.FECHA>=GETDATE() and t.Cancelado!= 1 and t.FECHA is not null and t.ID_PROFESIONAL={0} AND t.FECHA = '{1}'", idProfesional.ToString() ,fecha_reservacion), conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("El turno ya esta ocupado");
                    cmd.Dispose();
                    reader.Close();
                    conexion.Close();
                }
                else 
                {
                    cmd.Dispose();
                    reader.Close();
                    cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_turno", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha_reservacion;
                    cmd.Parameters.Add("@profesional", SqlDbType.Int).Value = idProfesional;
                    cmd.Parameters.Add("@afiliado", SqlDbType.Int).Value = idAfiliado;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Su turno ha sido ingresado correctamente");
                    conexion.Close();
                }
            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            String dia_actual = Convert.ToString(dateTimePicker1.Value);
            String profesional = Convert.ToString(dataGridView1[0, 0].Value);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand(string.Format(
                    "SELECT t.Hora_Inicio,t.Hora_Fin FROM YOU_SHALL_NOT_CRASH.PROFESIONAL p, YOU_SHALL_NOT_CRASH.ITEM_AGENDA t,YOU_SHALL_NOT_CRASH.AGENDA a where p.ID_PROFESIONAL = a.ID_PROFESIONAL and a.ID_AGENDA=t.id_agenda and p.ID_PROFESIONAL={0} AND DATEPART(weekday,'{1}') = Dia", idProfesional, dia_actual), conexion);
                SqlDataReader reader = cmd.ExecuteReader();


                reader.Read();

                if (reader.HasRows)
                {
                    Int64 hora_inicio = Convert.ToInt64(reader.GetInt32(0));
                    Int64 hora_fin = Convert.ToInt64(reader.GetInt32(1));
                    Int64 hora = hora_inicio;
                    Int64 hora_anterior = hora_inicio;
                    List<Int64> lista_hora = new List<Int64>();

                    while (hora < hora_fin)
                    {
                        lista_hora.Add(hora);
                        if (((hora - hora_anterior) == 0) || ((hora - hora_anterior) == 70))
                        {
                            hora_anterior = hora;
                            hora = hora + 30;
                        }
                        else
                        {
                            hora_anterior = hora;
                            hora = hora + 70;
                        }

                    }
                    comboBox1.DataSource = lista_hora;
                    conexion.Close();
                }
            }//Fin using

        }



    }//FIN CLASE TURNOS

    public class Turno_por_profesional
    {
        public String profesional { get; set; }
        public String fecha { get; set; }
        public String dia { get; set; }

        public void turno_por_profesional() { }

        public void turno_por_profesional(String pProfesional, String pFecha, String pDia)
        {  
            this.profesional= pProfesional;
            this.fecha = pFecha;
            this.dia = pDia;
        }

    }
}

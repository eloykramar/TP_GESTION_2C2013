using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Registrar_Agenda
{
    public partial class Registrar_Agenda : Form1
    {
        int idProfesional;
        String nombre;
        String apellido;
        
        public Registrar_Agenda(String unID, String unNombre, String unApellido)
        {
            InitializeComponent();
            label1.Text = "Registrando agenda para: " + unApellido + ", " + unNombre;
            idProfesional = Convert.ToInt32(unID);
            comboBox1.Items.Add("00");
            comboBox1.Items.Add("30");
            comboBox1.Text = "00";
            comboBox2.Items.Add("00");
            comboBox2.Items.Add("30");
            comboBox2.Text = "00";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            idProfesional = Convert.ToInt32(unID);
            nombre = unNombre;
            apellido = unApellido;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String horaDesde = numericUpDown1.Value.ToString() + ":" + comboBox1.Text;
            String horaHasta = numericUpDown2.Value.ToString() + ":" + comboBox2.Text;

            try
            {
                validar();

                if (checkBox1.Checked)
                {
                    listBox1.Items.Add("Lunes; " + horaDesde + " - " + horaHasta);
                    checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                }

                if (checkBox2.Checked)
                {
                    listBox1.Items.Add("Martes; " + horaDesde + " - " + horaHasta);
                    checkBox2.Checked = false;
                    checkBox2.Enabled = false;
                }

                if (checkBox3.Checked)
                {
                    listBox1.Items.Add("Miercoles; " + horaDesde + " - " + horaHasta);
                    checkBox3.Checked = false;
                    checkBox3.Enabled = false;
                }

                if (checkBox4.Checked)
                {
                    listBox1.Items.Add("Jueves; " + horaDesde + " - " + horaHasta);
                    checkBox4.Checked = false;
                    checkBox4.Enabled = false;
                }

                if (checkBox5.Checked)
                {
                    listBox1.Items.Add("Viernes; " + horaDesde + " - " + horaHasta);
                    checkBox5.Checked = false;
                    checkBox5.Enabled = false;
                }

                if (checkBox6.Checked)
                {
                    listBox1.Items.Add("Sabado; " + horaDesde + " - " + horaHasta);
                    checkBox6.Checked = false;
                    checkBox6.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }             
        }

        //boton limpiar
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            checkBox2.Enabled = true;
            checkBox2.Checked = false;
            checkBox3.Enabled = true;
            checkBox3.Checked = false;
            checkBox4.Enabled = true;
            checkBox4.Checked = false;
            checkBox5.Enabled = true;
            checkBox5.Checked = false;
            checkBox6.Enabled = true;
            checkBox6.Checked = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            comboBox1.Text = "00";
            comboBox2.Text = "00";
        }

        private void validar()
        {
            //que los horarios no sean null
            if (!(checkBox1.Checked) & !(checkBox2.Checked) & !(checkBox3.Checked) & !(checkBox4.Checked) & !(checkBox5.Checked) & !(checkBox6.Checked))
                throw new Exception("Debe seleccionar algun dia");
            
            if (numericUpDown1.Value == 0 | numericUpDown2.Value == 0)
                throw new Exception("Debe cargar las horas para los dias seleccionados");

            if ((numericUpDown2.Value == numericUpDown1.Value) & (comboBox1.Text == comboBox2.Text))
                throw new Exception("Hora desde debe ser distinta a hora hasta");

            if ((numericUpDown2.Value < numericUpDown1.Value) | (numericUpDown2.Value == numericUpDown1.Value & comboBox1.Text == "30"))
                throw new Exception("Hora desde menor que hora hasta");

            if ((numericUpDown1.Value < 7 | numericUpDown1.Value > 20 | numericUpDown2.Value < 7 | numericUpDown2.Value > 20) | (checkBox6.Checked & ((numericUpDown2.Value > 15) | (numericUpDown2.Value == 15 & comboBox2.Text == "30"))) )
                throw new Exception("Horarios fuera del horario de atención;la clínica atiende:;desde las 7:00 hasta las 20.00 para los días hábiles;desde las 10:00 hasta las 15:00 para los días sábados");                       
        }            

        //boton guardar
        private void button3_Click(object sender, EventArgs e)
        {
            int numeroDia = 0;
            int idAgenda = 0;
            int horarioDesde = Convert.ToInt32(numericUpDown1.Value) * 100 + Convert.ToInt32(comboBox1.Text);
            int horarioHasta = Convert.ToInt32(numericUpDown2.Value) * 100 + Convert.ToInt32(comboBox2.Text);

            try
            {                
                validarAgendaCargada();
                validarFechas();

                //FALTA VALIDAR QUE NO SE PASEN LAS HORAS SEMANALES

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_Agenda", conexion))
                    {                   
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idProfesional", SqlDbType.Int).Value = idProfesional;
                        cmd.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = dateTimePicker2.Value;
                        cmd.Parameters.Add("@respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        idAgenda = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);

                        if (idAgenda == -1)
                            throw new Exception("El profesional ya tiene una agenda cargada en las fechas seleccionadas");
                    }
                    
                    foreach (String lineaAgenda in listBox1.Items)
                    {
                        String[] primerPalabra = lineaAgenda.Split(';');
                        String dia = primerPalabra[0];
                        if (dia.Equals("Lunes"))
                            numeroDia = 1;
                        if (dia.Equals("Martes"))
                            numeroDia = 2;
                        if (dia.Equals("Miercoles"))
                            numeroDia = 3;
                        if (dia.Equals("Jueves"))
                            numeroDia = 4;
                        if (dia.Equals("Viernes"))
                            numeroDia = 5;
                        if (dia.Equals("Sabado"))
                            numeroDia = 6;

                        SqlCommand insertarItemsAgenda = new SqlCommand("USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_AGENDA VALUES (" + idAgenda + ", " + numeroDia + ", " + horarioDesde + ", " + horarioHasta + ")", conexion);
                        insertarItemsAgenda.ExecuteNonQuery();
                    }

                    new Dialogo("Agenda insertada para el profesional: " + apellido + ", " + nombre + ";Para el rango: " + dateTimePicker1.Value.ToShortDateString() + ", " + dateTimePicker2.Value.ToShortDateString(), "Aceptar").Show();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        private void validarFechas()
        {
            if (dateTimePicker1.Value < getFechaActual())
                throw new Exception("Fecha incial de validez de agenda menor a la fecha de hoy");

            if (dateTimePicker1.Value >= dateTimePicker2.Value)
                throw new Exception("Fecha incial de validez de agenda mayor o igual a la fecha final");
        }

        private void validarAgendaCargada()
        {
            if (listBox1.Items.Count == 0)
                throw new Exception("Agenda no cargada");
        }
    }
}

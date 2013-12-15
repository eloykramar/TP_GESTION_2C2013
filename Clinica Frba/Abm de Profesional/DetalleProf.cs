using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Abm_de_Profesional
{
    public partial class DetalleProf : Form1
    {
        DataTable tabla2 = new DataTable();   

        public DetalleProf()
        {
            arrancar(""); 
        }


        public DetalleProf(string textbox)
        {
            
            arrancar(textbox);
        }

        public bool vCamposObligatorios()
        {
            bool result = true;

            if (textBox2.Text == "") { errorProvider1.SetError(label3, "Campo obligatorio."); result = false; } else { errorProvider1.Clear(); }
            if (textBox3.Text == "") { errorProvider2.SetError(label2, "Campo obligatorio."); result = false; } else { errorProvider2.Clear(); }
            if (textBox4.Text == "") { errorProvider3.SetError(label4, "Campo obligatorio."); result = false; } else { errorProvider3.Clear(); }
            if (textBox5.Text == "") { errorProvider4.SetError(label5, "Campo obligatorio."); result = false; } else { errorProvider4.Clear(); }
            if (textBox6.Text == "") { errorProvider5.SetError(label7, "Campo obligatorio."); result = false; } else { errorProvider5.Clear(); }
            if (textBox7.Text == "") { errorProvider6.SetError(label14, "Campo obligatorio."); result = false; } else { errorProvider6.Clear(); }
            if (dateTimePicker1.Text == "") { errorProvider7.SetError(label6, "Seleccione una fecha."); result = false; } else { errorProvider7.Clear(); }
            if (!checkBox1.Checked && !checkBox2.Checked) { errorProvider8.SetError(checkBox2, "Seleccione una opción"); result = false; } else { errorProvider8.Clear(); }
            if (dataGridView1.Rows.Count == 1) { errorProvider9.SetError(dataGridView1, "Seleccione al menos una especialidad."); result = false; } else { errorProvider9.Clear(); }

            return result;
        }

        public void arrancar(string textbox)
        {   
            InitializeComponent();
            if (String.Equals(textbox,"")) return;
            textBox1.Text = textbox;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    
                    SqlDataAdapter adapter = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres = new DataTable();


                    adapter.Fill(tablaDeNombres);
                    comboBox2.DisplayMember = "Descripcion";
                    comboBox2.DataSource = tablaDeNombres;
                    comboBox2.SelectedItem = null;


                    //DataTable tabla2 = new DataTable();
                    cargarATablaParaDataGripView("USE GD2C2013 select e.DESCRIPCION from YOU_SHALL_NOT_CRASH.PROFESIONAL p join YOU_SHALL_NOT_CRASH.ESPECIALIDAD_PROFESIONAL ep on ep.ID_PROFESIONAL=p.ID_PROFESIONAL join YOU_SHALL_NOT_CRASH.ESPECIALIDAD e on e.CODIGO_ESPECIALIDAD=ep.CODIGO_ESPECIALIDAD where p.DNI=" + textbox.ToString(), ref tabla2, conexion);
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla2;

                    DataColumn[] key = new DataColumn[1];
                    key[0] = tabla2.Columns["DESCRIPCION"];
                    tabla2.PrimaryKey = key;
                    dataGridView1.Columns[0].ReadOnly = true;
                    DataGridViewButtonColumn botonQuitar = this.crearBotones("Seleccionar", "Quitar");
                    dataGridView1.Columns.Add(botonQuitar);


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres2 = new DataTable();


                    adapter.Fill(tablaDeNombres2);
                    comboBox2.DisplayMember = "Descripcion";
                    comboBox2.DataSource = tablaDeNombres2;
                    comboBox2.SelectedItem = null;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }


                   SqlCommand cmd = new SqlCommand("select p.DNI,p.APELLIDO,p.NOMBRE,p.DIRECCION,p.FECHA_NAC,p.MAIL,p.MATRICULA,p.SEXO,p.TELEFONO,p.ACTIVO from YOU_SHALL_NOT_CRASH.PROFESIONAL p where p.DNI=" + textbox, conexion);
                    conexion.Open();
                    SqlDataReader leer=cmd.ExecuteReader();

                    if (leer.Read() == true)
                    {

                        textBox2.Text = leer["APELLIDO"].ToString();
                        textBox3.Text = leer["NOMBRE"].ToString();
                        if (!leer["MAIL"].Equals(null)) { textBox4.Text = leer["MAIL"].ToString(); } else { textBox4.Text = ""; }
                        textBox5.Text = leer["DIRECCION"].ToString();
                        if (!leer["TELEFONO"].Equals(null)) { textBox6.Text = leer["TELEFONO"].ToString(); } else { textBox6.Text = ""; };
                        if (!leer["MATRICULA"].Equals(null)) { textBox7.Text = leer["MATRICULA"].ToString(); } else { textBox7.Text = ""; };
                        dateTimePicker1.Text = leer["FECHA_NAC"].ToString();
                        if (!leer["SEXO"].Equals(null)) { if (leer["SEXO"].Equals("F")) { checkBox1.Checked = true; } else { checkBox2.Checked = true; } }


                        //escondo checkbox si el prof ya esta activo
                        if (leer["ACTIVO"].ToString() == "True")
                        {
                            checkBox3.Checked = true;
                            checkBox3.Hide();
                            label8.Hide();
                        } else { checkBox3.Checked = false; }
                        
                    }
            }
        }

        



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
            else
            {
                if (checkBox2.Checked)
                {
                    checkBox1.Checked = false;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
            else
            {
                if (checkBox1.Checked)
                {
                    checkBox2.Checked = false;
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //GUARDAR
        private void button2_Click(object sender, EventArgs e) //si quieren, reemplazar x guardarProfesional();
        {
            if (vCamposObligatorios())
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.modificar_profesional", conexion))
                        {
                            conexion.Open();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = textBox3.Text;
                            cmd.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = textBox2.Text;
                            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = textBox1.Text;
                            cmd.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = textBox5.Text;
                            cmd.Parameters.Add("@matricula", SqlDbType.NVarChar).Value = textBox7.Text;
                            cmd.Parameters.Add("@fecha_nac", SqlDbType.NVarChar).Value = dateTimePicker1.Text;
                            if (checkBox1.Checked) { cmd.Parameters.Add("@sexo", SqlDbType.NVarChar).Value = checkBox1.Text; } else { cmd.Parameters.Add("@sexo", SqlDbType.NVarChar).Value = checkBox2.Text; }
                            cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = textBox4.Text;
                            cmd.Parameters.Add("@telefono", SqlDbType.Int).Value = textBox6.Text;
                            cmd.Parameters.Add("@activo", SqlDbType.Int).Value = checkBox3.Checked;
                            
                            cmd.Parameters.Add("@resu", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            int respuesta = Convert.ToInt32(cmd.Parameters["@resu"].Value);
                            if (respuesta == 1) { MessageBox.Show("Profesional modificado satisfactoriamente", "Aceptar"); }

                        }

                        using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.modificar_prof_espec", conexion))
                        {
                            string especialidad;

                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlCommand cmd2 = new SqlCommand("YOU_SHALL_NOT_CRASH.eliminar_prof_espec", conexion);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@dni", SqlDbType.Int).Value = textBox1.Text;
                            cmd2.ExecuteNonQuery();



                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                            {
                                int cant = dataGridView1.Rows.Count - 1;
                                especialidad = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("@dni", SqlDbType.NVarChar).Value = textBox1.Text;
                                cmd.Parameters.Add("@especialidad", SqlDbType.NVarChar).Value = especialidad;


                                cmd.ExecuteNonQuery();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        MessageBox.Show(ex.Message, "Error");

                        //(new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                    }
                }
            }
        }

        public virtual void guardarProfesional()
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "") 
            {
                if (tabla2.IsInitialized)
                {
                    DataRow row = tabla2.NewRow();
                    row[0] = comboBox2.Text.ToString();
                    try
                    {
                        tabla2.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show("Especialidad Repetida", "Error");
                    }
                }

                  
                
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != dataGridView1.RowCount - 1)
            {
                if (e.ColumnIndex == 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void limpiar()
        { 
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            dateTimePicker1.Text = "";
            comboBox2.Text = null;

            dataGridView1.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();   
        }

        
    }
}

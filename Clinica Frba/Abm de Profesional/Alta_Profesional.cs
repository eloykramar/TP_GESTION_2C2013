using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.ABM_de_Profesional
{
    public partial class Alta_Profesional : Form1
    {
        public Alta_Profesional()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter("USE GD2C2013 SELECT * FROM YOU_SHALL_NOT_CRASH.ESPECIALIDAD ORDER BY DESCRIPCION", conexion);
                    DataTable tablaDeNombres = new DataTable();


                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.DataSource = tablaDeNombres;
                    comboBox1.SelectedItem = null;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        public bool vCamposObligatorios()
        {
            bool result = true;

            if (textBox9.Text == "") { errorProvider1.SetError(label9, "Campo obligatorio"); result = false; } else { errorProvider1.Clear(); }
            if (!maskedTextBox1.MaskFull) { errorProvider2.SetError(label7, "Campo obligatorio"); result = false; } else { errorProvider2.Clear(); }
            //if (!maskedTextBox2.MaskFull) { errorProvider9.SetError(label8, "Campoooo"); result = false; } else { errorProvider9.Clear(); }
            if (textBox5.Text == "") { errorProvider3.SetError(label5, "Campo obligatorio"); result = false; } else { errorProvider3.Clear(); }
            if (maskedTextBox4.Text == "") { errorProvider4.SetError(label1, "Campo obligatorio"); result = false; } else { errorProvider4.Clear(); }
            if (maskedTextBox5.Text == "") { errorProvider5.SetError(label2, "Campo obligatorio"); result = false; } else { errorProvider5.Clear(); }
            if (textBox3.Text == "") { errorProvider6.SetError(label3, "Campo obligatorio"); result = false; } else { errorProvider6.Clear(); }
            if (listBox1.Items.Count == 0) { errorProvider7.SetError(label11, "Seleccione al menos una especialidad"); result = false; } else { errorProvider7.Clear(); }
            if (!checkBox1.Checked && !checkBox2.Checked) { errorProvider8.SetError(label10, "Seleccione una opción"); result = false; } else { errorProvider8.Clear(); }

            return result;
        }

        public bool vProfesionalRepetido()
        {
            bool result = true;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.dni_profesional_nuevo", conexion))
                    {
                        conexion.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@dni", SqlDbType.Int).Value = textBox3.Text;
                        cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        int respuesta = Convert.ToInt32(cmd.Parameters["@resultado"].Value);

                        if(respuesta == 1)
                        {
                            (new Dialogo("Ya existe el profesional con D.N.I.:"+textBox3.Text+".", "Aceptar")).ShowDialog();
                            result = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

            return result;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string especialidad = comboBox1.Text;
                comboBox1.Items.Remove(especialidad);
                if (!listBox1.Items.Contains(especialidad))
                {
                    listBox1.Items.Add(especialidad);
                    comboBox1.Text = "";
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string especialidad = listBox1.Text;
                listBox1.Items.Remove(especialidad);

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

        private void maskedTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        //LIMPIAR
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox || c is MaskedTextBox || c is ListBox)
                {
                    c.Text = "";
                }
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }

            }
            listBox1.Items.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool res;
            bool res2;

            if (res=vCamposObligatorios())
            {
                if (res2 = vProfesionalRepetido())
                {
                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.insertar_profesional", conexion))
                            {
                                conexion.Open();

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = maskedTextBox4.Text;
                                cmd.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = maskedTextBox5.Text;
                                cmd.Parameters.Add("@dni", SqlDbType.Int).Value = textBox3.Text;
                                cmd.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = textBox5.Text;
                                cmd.Parameters.Add("@matricula", SqlDbType.NVarChar).Value = textBox9.Text;
                                cmd.Parameters.Add("@fecha_nac", SqlDbType.NVarChar).Value = maskedTextBox1.Text + " " + maskedTextBox2.Text;
                                if (checkBox1.Checked) { cmd.Parameters.Add("@sexo", SqlDbType.NVarChar).Value = checkBox1.Text; } else { cmd.Parameters.Add("@sexo", SqlDbType.NVarChar).Value = checkBox2.Text; }
                                cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = textBox4.Text;
                                cmd.Parameters.Add("@telefono", SqlDbType.Int).Value = textBox1.Text;
                                cmd.Parameters.Add("@resu", SqlDbType.Int).Direction = ParameterDirection.Output;

                                cmd.ExecuteNonQuery();

                                int respuesta = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                                if (respuesta == 1) { (new Dialogo("Todo ok", "Aceptar")).ShowDialog();}

                              }

                            using (SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.insertar_prof_espec", conexion))
                            {
                                conexion.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                foreach (var UnItem in listBox1.Items)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.Add("@especialidad", SqlDbType.NVarChar).Value = UnItem;
                                    
                                    cmd.Parameters.Add("@dni", SqlDbType.Int).Value = textBox3.Text;
                                    //cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                                    cmd.ExecuteNonQuery();

                                    //int respuesta = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                                    //(new Dialogo("Todo okkkk " + UnItem+ respuesta, "Aceptar")).ShowDialog();
                                    
                                }
                             }
                           }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                            (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                        }
                    }
                    
                }
            }
            else
            {
                (new Dialogo("Complete los campos obligatorios", "Aceptar")).ShowDialog();
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Generar_Receta
{
    public partial class CompletarBono : Form1
    {
        int id_receta, idAfiliado, id_bono_farmacia;
        String descripcion;

        public CompletarBono(int idRec,int idAfi, int idB)
        {
            InitializeComponent();
            id_receta = idRec;
            idAfiliado = idAfi;
            id_bono_farmacia = idB;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                //Lleno medicamentos:
                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 SELECT ID_Medicamento,Descripcion FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO", conexion);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter2.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["ID_Medicamento"].Visible = false;
                dataGridView1.ReadOnly = true;
                //--------------------
                //lleno combos
                List<int> cantidades1 = new List<int>();
                cantidades1.Add(1);
                cantidades1.Add(2);
                cantidades1.Add(3);
                cantidades1.Add(4);
                cantidades1.Add(5);
                comboBoxCant1.DataSource = cantidades1;
                comboBoxCant2.DataSource = cantidades1;
                comboBoxCant3.DataSource = cantidades1;
                comboBoxCant4.DataSource = cantidades1;
                comboBoxCant5.DataSource = cantidades1;
                //--------------

            }
        }

        private void buttAgregar_Click(object sender, EventArgs e)
        {
            descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);

            if (textBoxMed1.Text == "")
            {
                if (textBoxMed2.Text != descripcion)
                {
                    if (textBoxMed3.Text != descripcion)
                    {
                        if (textBoxMed4.Text != descripcion)
                        {
                            if (textBoxMed5.Text != descripcion)
                            {
                                textBoxMed1.Text = descripcion;
                            }
                        }
                     }
                }
            }
        }//Fin agregar 1

        private void buttQuitar_Click(object sender, EventArgs e)
        {
            textBoxMed1.Clear();
        }

        private void buttAgregar2_Click(object sender, EventArgs e)
        {
            descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
            if (textBoxMed2.Text == "")
            {
                if (textBoxMed1.Text != descripcion)
                {
                    if (textBoxMed3.Text != descripcion)
                    {
                        if (textBoxMed4.Text != descripcion)
                        {
                            if (textBoxMed5.Text != descripcion)
                            {
                                textBoxMed2.Text = descripcion;
                            }
                        }
                    }
                }
            }
        }//Fin agregar 2

        private void buttAgregar3_Click(object sender, EventArgs e)
        {
            descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
            if (textBoxMed3.Text == "")
            {
                if (textBoxMed1.Text != descripcion)
                {
                    if (textBoxMed2.Text != descripcion)
                    {
                        if (textBoxMed4.Text != descripcion)
                        {
                            if (textBoxMed5.Text != descripcion)
                            {
                                textBoxMed3.Text = descripcion;
                            }
                        }
                    }
                }
            }
        }//Fin agregar 3

        private void buttAgregar4_Click(object sender, EventArgs e)
        {
            descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
            if (textBoxMed4.Text == "")
            {
                if (textBoxMed1.Text != descripcion)
                {
                    if (textBoxMed2.Text != descripcion)
                    {
                        if (textBoxMed3.Text != descripcion)
                        {
                            if (textBoxMed5.Text != descripcion)
                            {
                                textBoxMed4.Text = descripcion;
                            }
                        }
                    }
                }
            }
        }//Fin agregar 4

        private void buttAgregar5_Click(object sender, EventArgs e)
        {
            descripcion = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
            if (textBoxMed5.Text == "")
            {
                if (textBoxMed1.Text != descripcion)
                {
                    if (textBoxMed2.Text != descripcion)
                    {
                        if (textBoxMed3.Text != descripcion)
                        {
                            if (textBoxMed4.Text != descripcion)
                            {
                                textBoxMed5.Text = descripcion;
                            }
                        }
                    }
                }
            }
        }//Fin agregar 5

        private void buttQuitar2_Click(object sender, EventArgs e)
        {
            textBoxMed2.Clear();
        }

        private void buttQuitar3_Click(object sender, EventArgs e)
        {
            textBoxMed3.Clear();     
        }

        private void buttQuitar4_Click(object sender, EventArgs e)
        {
            textBoxMed4.Clear();
        }

        private void buttQuitar5_Click(object sender, EventArgs e)
        {
            textBoxMed5.Clear();
        }

        private void buttFiltrar_Click(object sender, EventArgs e)
        {
            String filtro = textBoxBuscar.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(
                    "USE GD2C2013 SELECT ID_Medicamento,Descripcion FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion like '%" + filtro + "%'",conexion);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter2.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["ID_Medicamento"].Visible = false;
                dataGridView1.ReadOnly = true;
                cmd.Dispose();
                adapter2.Dispose();
                conexion.Close();
            }
            
        } // Fin filtrar

        private void buttAceptar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlDataReader reader;


                SqlCommand cmd = new SqlCommand(string.Format(
                            "SELECT FECHA_EMISION FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_Bono_Farmacia ={0}", id_bono_farmacia), conexion);


                DateTime fecha_emision = Convert.ToDateTime(cmd.ExecuteScalar());
                DateTime fecha_vencimiento = fecha_emision.AddDays(60);
                DateTime dia_de_implementacion = this.fechaActual;
                cmd.Dispose();
                if (fecha_vencimiento > dia_de_implementacion)  //verifico que no este vencida
                {

                    cmd.Dispose();

                    cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Actualizacion_bono_farmacia", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_receta", SqlDbType.Int).Value = id_receta;
                    cmd.Parameters.Add("@fecha_vencimiento", SqlDbType.DateTime).Value = fecha_vencimiento;
                    cmd.Parameters.Add("@dia_de_implementacion", SqlDbType.DateTime).Value = dia_de_implementacion;
                    cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();


                    String contenido;

                    if (textBoxMed1.Text != "")
                    {
                        contenido = textBoxMed1.Text;
                        cmd = new SqlCommand(string.Format(
                            "USE GD2C2013 SELECT ID_Medicamento FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion = '{0}'", contenido), conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_medicamento = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                            cmd.Dispose();
                            reader.Close();
                            cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_bono_farmacia", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                            cmd.Parameters.Add("@id_medicamento", SqlDbType.Int).Value = id_medicamento;
                            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = comboBoxCant1.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            reader.Close();
                        }
                    }

                    if (textBoxMed2.Text != "")
                    {
                        contenido = textBoxMed2.Text;
                        cmd = new SqlCommand(string.Format(
                            "USE GD2C2013 SELECT ID_Medicamento FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion = '{0}'", contenido), conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_medicamento = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                            cmd.Dispose();
                            reader.Close();
                            cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_bono_farmacia", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                            cmd.Parameters.Add("@id_medicamento", SqlDbType.Int).Value = id_medicamento;
                            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = comboBoxCant2.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            reader.Close();
                        }
                    }
                    if (textBoxMed3.Text != "")
                    {
                        contenido = textBoxMed3.Text;
                        cmd = new SqlCommand(string.Format(
                            "USE GD2C2013 SELECT ID_Medicamento FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion = '{0}'", contenido), conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_medicamento = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                            cmd.Dispose();
                            reader.Close();
                            cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_bono_farmacia", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                            cmd.Parameters.Add("@id_medicamento", SqlDbType.Int).Value = id_medicamento;
                            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = comboBoxCant3.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            reader.Close();
                        }
                    }
                    if (textBoxMed4.Text != "")
                    {
                        cmd.Dispose();
                        contenido = textBoxMed4.Text;
                        cmd = new SqlCommand(string.Format(
                            "USE GD2C2013 SELECT ID_Medicamento FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion = '{0}'", contenido), conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_medicamento = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                            cmd.Dispose();
                            reader.Close();
                            cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_bono_farmacia", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                            cmd.Parameters.Add("@id_medicamento", SqlDbType.Int).Value = id_medicamento;
                            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = comboBoxCant4.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            reader.Close();
                        }
                    }
                    if (textBoxMed5.Text != "")
                    {
                        cmd.Dispose();
                        contenido = textBoxMed5.Text;
                        cmd = new SqlCommand(string.Format(
                            "USE GD2C2013 SELECT ID_Medicamento FROM YOU_SHALL_NOT_CRASH.MEDICAMENTO WHERE Descripcion = '{0}'", contenido), conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_medicamento = Convert.ToInt32(reader.GetSqlInt32(0).Value);
                            cmd.Dispose();
                            reader.Close();
                            cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_bono_farmacia", conexion);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id_bono_farmacia", SqlDbType.Int).Value = id_bono_farmacia;
                            cmd.Parameters.Add("@id_medicamento", SqlDbType.Int).Value = id_medicamento;
                            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = comboBoxCant5.Text;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            reader.Close();
                        }
                    }

                    MessageBox.Show("Bono farmacia ingresado correctamente");
                }//IF FECHA VENCIMIENTO
                conexion.Close();
            }//FIN USING
            Close();
        }//FIN BOTON ACEPTAR

        private void buttSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

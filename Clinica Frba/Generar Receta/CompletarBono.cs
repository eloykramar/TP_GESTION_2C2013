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

        public CompletarBono(int idRec,int idAfi, int idB)
        {
            InitializeComponent();
            id_receta = idRec;
            idAfiliado = idAfi;
            id_bono_farmacia = idB;

            //lleno combos
            List<int> cantidades1 = new List<int>();
            cantidades1.Add(0);
            cantidades1.Add(1);
            cantidades1.Add(2);
            cantidades1.Add(3);
            cantidades1.Add(4);
            cantidades1.Add(5);
            comboBoxCant1.DataSource = new List<int>(cantidades1);
            comboBoxCant2.DataSource = new List<int>(cantidades1);
            comboBoxCant3.DataSource = new List<int>(cantidades1);
            comboBoxCant4.DataSource = new List<int>(cantidades1);
            comboBoxCant5.DataSource = new List<int>(cantidades1);
            //--------------

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();                
                //lleno con los medicamentos viejos
                SqlCommand cmd = new SqlCommand(
                    "USE GD2C2013 SELECT I.ID_Medicamento, M.Descripcion, I.Cantidad FROM YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA I JOIN YOU_SHALL_NOT_CRASH.MEDICAMENTO M"+
                    " ON I.ID_MEDICAMENTO=M.ID_MEDICAMENTO WHERE I.ID_BONO_FARMACIA= " + id_bono_farmacia, conexion);
                SqlDataReader meds = cmd.ExecuteReader();
                if (meds.HasRows)
                {
                    if (meds.Read())
                    {
                        textBoxMed1.Tag = Convert.ToString(meds["ID_Medicamento"]);
                        textBoxMed1.Text = Convert.ToString(meds["Descripcion"]);
                        comboBoxCant1.SelectedIndex = Convert.ToInt32(meds["Cantidad"]);
                        comboBoxCant1.Enabled = false;
                        buttAgregar.Enabled = false;
                        buttQuitar.Enabled = false;
                    }
                    if (meds.Read())
                    {
                        textBoxMed2.Tag = Convert.ToString(meds["ID_Medicamento"]);
                        textBoxMed2.Text = Convert.ToString(meds["Descripcion"]);
                        comboBoxCant2.SelectedIndex = Convert.ToInt32(meds["Cantidad"]);
                        comboBoxCant2.Enabled = false;
                        buttAgregar2.Enabled = false;
                        buttQuitar2.Enabled = false;
                    }
                    if (meds.Read())
                    {
                        textBoxMed3.Tag = Convert.ToString(meds["ID_Medicamento"]);
                        textBoxMed3.Text = Convert.ToString(meds["Descripcion"]);
                        comboBoxCant3.SelectedIndex = Convert.ToInt32(meds["Cantidad"]);
                        comboBoxCant3.Enabled = false;
                        buttAgregar3.Enabled = false;
                        buttQuitar3.Enabled = false;
                    }
                    if (meds.Read())
                    {
                        textBoxMed4.Tag = Convert.ToString(meds["ID_Medicamento"]);
                        textBoxMed4.Text = Convert.ToString(meds["Descripcion"]);
                        comboBoxCant4.SelectedIndex = Convert.ToInt32(meds["Cantidad"]);
                        comboBoxCant4.Enabled = false;
                        buttAgregar4.Enabled = false;
                        buttQuitar4.Enabled = false;
                    }
                    if (meds.Read())
                    {
                        textBoxMed5.Tag = Convert.ToString(meds["ID_Medicamento"]);
                        textBoxMed5.Text = Convert.ToString(meds["Descripcion"]);
                        comboBoxCant5.SelectedIndex = Convert.ToInt32(meds["Cantidad"]);
                        comboBoxCant5.Enabled = false;
                        buttAgregar5.Enabled = false;
                        buttQuitar5.Enabled = false;
                    }
                }
                meds.Dispose();
                cmd.Dispose();



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
                

            }
        }

        private void buttAgregar_Click(object sender, EventArgs e)
        {
            if (textBoxMed1.Text == "")
            {
                textBoxMed1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
                textBoxMed1.Tag = Convert.ToString(dataGridView1.CurrentRow.Cells["ID_Medicamento"].Value);
            }
        }//Fin agregar 1

        private void buttAgregar2_Click(object sender, EventArgs e)
        {
            if (textBoxMed2.Text == "")
            {
                textBoxMed2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
                textBoxMed2.Tag = Convert.ToString(dataGridView1.CurrentRow.Cells["ID_Medicamento"].Value);
            }
        }//Fin agregar 2

        private void buttAgregar3_Click(object sender, EventArgs e)
        {
            if (textBoxMed3.Text == "")
            {
                textBoxMed3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
                textBoxMed3.Tag = Convert.ToString(dataGridView1.CurrentRow.Cells["ID_Medicamento"].Value);
            }
        }//Fin agregar 3

        private void buttAgregar4_Click(object sender, EventArgs e)
        {
            if (textBoxMed4.Text == "")
            {
                textBoxMed4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
                textBoxMed4.Tag = Convert.ToString(dataGridView1.CurrentRow.Cells["ID_Medicamento"].Value);
            }
        }//Fin agregar 4

        private void buttAgregar5_Click(object sender, EventArgs e)
        {
            if (textBoxMed5.Text == "")
            {
                textBoxMed5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Descripcion"].Value);
                textBoxMed5.Tag = Convert.ToString(dataGridView1.CurrentRow.Cells["ID_Medicamento"].Value);
            }
        }//Fin agregar 5

        private void buttQuitar_Click(object sender, EventArgs e)
        {
            textBoxMed1.Clear();
            textBoxMed1.Tag = "";
            comboBoxCant1.SelectedIndex = 0;
        }
        
        private void buttQuitar2_Click(object sender, EventArgs e)
        {
            textBoxMed2.Clear();
            textBoxMed2.Tag = "";
            comboBoxCant2.SelectedIndex = 0;
        }

        private void buttQuitar3_Click(object sender, EventArgs e)
        {
            textBoxMed3.Clear();
            textBoxMed3.Tag = "";
            comboBoxCant3.SelectedIndex = 0;    
        }

        private void buttQuitar4_Click(object sender, EventArgs e)
        {
            textBoxMed4.Clear();
            textBoxMed4.Tag = "";
            comboBoxCant4.SelectedIndex = 0;
        }

        private void buttQuitar5_Click(object sender, EventArgs e)
        {
            textBoxMed5.Clear();
            textBoxMed5.Tag = "";
            comboBoxCant5.SelectedIndex = 0;
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
            int cant=0;
            ComboBox C;
            foreach (Control a in ActiveForm.Controls) //<-- loop object is Control type.
            {
                if (a is ComboBox) //<-- test if loop object is a ComboBox
                {
                    C = (ComboBox)a;
                    if (C.SelectedIndex < 0) C.SelectedIndex = 0;
                    cant += C.SelectedIndex;
                }
            }
 
            if (cant > 5)
            {
                MessageBox.Show("No puede haber mas de 5 unidades de medicamentos en el mismo bono.");
                return;
            }
            
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand(string.Format(
                            "SELECT FECHA_EMISION FROM YOU_SHALL_NOT_CRASH.BONO_FARMACIA WHERE ID_Bono_Farmacia ={0}", id_bono_farmacia), conexion);


                DateTime fecha_emision = Convert.ToDateTime(cmd.ExecuteScalar());
                DateTime fecha_vencimiento = fecha_emision.AddDays(60);
                DateTime dia_de_implementacion = this.fechaActual;
                cmd.Dispose();
                if (fecha_vencimiento > dia_de_implementacion)  //verifico que no este vencida
                {

                    cmd = new SqlCommand("DELETE FROM YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA WHERE ID_Bono_Farmacia =" + id_bono_farmacia, conexion);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();


                    int cantidad;
                    string id;
                    string sql ="";
                    if (!String.IsNullOrEmpty((string)textBoxMed1.Tag))
                    {
                        id = textBoxMed1.Tag.ToString();
                        cantidad= comboBoxCant1.SelectedIndex;
                        if (cantidad > 0) sql += String.Format(" ( {0}, {1}, {2} ),", id_bono_farmacia, id, cantidad);
                    }
                    if (!String.IsNullOrEmpty((string)textBoxMed2.Tag))
                    {
                        id = textBoxMed2.Tag.ToString();
                        cantidad= comboBoxCant2.SelectedIndex;
                        if (cantidad > 0) sql += String.Format(" ( {0}, {1}, {2} ),", id_bono_farmacia, id, cantidad);
                    }
                    if (!String.IsNullOrEmpty((string)textBoxMed3.Tag))
                    {
                        id = textBoxMed3.Tag.ToString();
                        cantidad= comboBoxCant3.SelectedIndex;
                        if (cantidad > 0) sql += String.Format(" ( {0}, {1}, {2} ),", id_bono_farmacia, id, cantidad);
                    }
                    if (!String.IsNullOrEmpty((string)textBoxMed4.Tag))
                    {
                        id = textBoxMed4.Tag.ToString();
                        cantidad= comboBoxCant4.SelectedIndex;
                        if (cantidad>0) sql += String.Format(" ( {0}, {1}, {2} ),", id_bono_farmacia, id, cantidad);
                    }
                    if (!String.IsNullOrEmpty((string)textBoxMed5.Tag))
                    {
                        id = textBoxMed5.Tag.ToString();
                        cantidad= comboBoxCant5.SelectedIndex;
                        if (cantidad > 0) sql += String.Format(" ( {0}, {1}, {2} ),", id_bono_farmacia, id, cantidad);
                    }

                    if (sql.Length == 0)
                    {
                        MessageBox.Show("Debe cargar al menos un medicamento.");
                        conexion.Close();
                        return;
                    }
                    sql = "USE GD2C2013 INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (ID_BONO_FARMACIA, ID_MEDICAMENTO, CANTIDAD) VALUES" + sql;
                    sql = sql.Substring(0, sql.Length - 1);

                    cmd = new SqlCommand(sql, conexion);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

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

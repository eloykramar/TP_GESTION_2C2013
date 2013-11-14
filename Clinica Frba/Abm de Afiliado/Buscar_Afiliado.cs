using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.ABM_de_Afiliado
{
    public partial class Buscar_Afiliado : Form1
    {
        public List<string> users = null;
        public List<string> dnis = null;
        public List<string> values = null;
        private void InitializeComponent()
        {
            this.Busqueda = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Busqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Busqueda
            // 
            this.Busqueda.Controls.Add(this.comboBox1);
            this.Busqueda.Controls.Add(this.label3);
            this.Busqueda.Controls.Add(this.label2);
            this.Busqueda.Controls.Add(this.textBox2);
            this.Busqueda.Controls.Add(this.label1);
            this.Busqueda.Controls.Add(this.textBox1);
            this.Busqueda.Controls.Add(this.btnBuscar);
            this.Busqueda.Controls.Add(this.btnClean);
            this.Busqueda.Location = new System.Drawing.Point(28, 33);
            this.Busqueda.Name = "Busqueda";
            this.Busqueda.Size = new System.Drawing.Size(515, 188);
            this.Busqueda.TabIndex = 5;
            this.Busqueda.TabStop = false;
            this.Busqueda.Text = "Busqueda";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(112, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Plan Medico:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "DNI:";
            // 
            // textBox2
            // 
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox2.Location = new System.Drawing.Point(112, 69);
            this.textBox2.MaxLength = 18;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(127, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(425, 152);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(84, 30);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(6, 152);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(84, 30);
            this.btnClean.TabIndex = 0;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 227);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(515, 212);
            this.dataGridView1.TabIndex = 4;
            // 
            // Buscar_Afiliado
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 462);
            this.Controls.Add(this.Busqueda);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Buscar_Afiliado";
            this.Text = "ABM Afiliados";
            this.Busqueda.ResumeLayout(false);
            this.Busqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

            textBox1.Focus();
        }
        
        public Buscar_Afiliado()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select ID_Plan, Descripcion from YOU_SHALL_NOT_CRASH.PLAN_MEDICO", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();
                    tablaDeNombres.Rows.Add();
                    tablaDeNombres.AcceptChanges();
                    adapter.Fill(tablaDeNombres);
                    comboBox1.DisplayMember = "Descripcion";
                    comboBox1.ValueMember = "ID_Plan";
                    comboBox1.DataSource = tablaDeNombres;

                    //lleno el datagrid
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_Afiliado, (Nombre+' '+Apellido) Nombre, DNI, Direccion, Telefono from YOU_SHALL_NOT_CRASH.AFILIADO WHERE Fecha_Baja IS NULL", conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["ID_Afiliado"].Visible = false;
                    dataGridView1.ReadOnly = true;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                string nom = " AND (Nombre+' '+Apellido) like '%" + textBox1.Text + "%'";
                string dni = " AND DNI=" + textBox2.Text;
                string plan = " AND ID_Plan=" + comboBox1.SelectedValue;

                string where = "where Fecha_Baja IS NULL";
                if (!String.Equals(textBox1.Text, "")) where += nom;
                if (!String.Equals(textBox2.Text, "")) where += dni;
                if (!String.Equals(comboBox1.SelectedValue.ToString(), "")) where += plan;
                
               

                //lleno el datagrid

                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select ID_Afiliado, (Nombre+' '+Apellido) Nombre, DNI, Direccion, Telefono from YOU_SHALL_NOT_CRASH.AFILIADO " + where, conexion);

                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter2.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns["ID_Afiliado"].Visible = false;
                dataGridView1.ReadOnly = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(textBox2.Text, ""))
            {
                string Str = textBox2.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros");
                    textBox2.Text = "";
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            dataGridView1.DataSource = "";
        }



        public GroupBox Busqueda;
        public ComboBox comboBox1;
        public Label label3;
        public Label label2;
        public TextBox textBox2;
        public Label label1;
        public TextBox textBox1;
        public Button btnBuscar;
        public Button btnClean;
        public DataGridView dataGridView1;




    }
}

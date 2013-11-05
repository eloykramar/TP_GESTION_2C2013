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
    public partial class ABM_Afiliado : Form1
    {

        private void InitializeComponent()
        {
            this.Busqueda = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.Busqueda.Controls.Add(this.button2);
            this.Busqueda.Controls.Add(this.button1);
            this.Busqueda.Location = new System.Drawing.Point(28, 24);
            this.Busqueda.Name = "Busqueda";
            this.Busqueda.Size = new System.Drawing.Size(515, 197);
            this.Busqueda.TabIndex = 5;
            this.Busqueda.TabStop = false;
            this.Busqueda.Text = "Busqueda";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(112, 116);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Plan Medico:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "DNI:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(112, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(127, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Buscar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Limpiar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 227);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(515, 212);
            this.dataGridView1.TabIndex = 4;
            // 
            // ABM_Afiliado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 462);
            this.Controls.Add(this.Busqueda);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ABM_Afiliado";
            this.Text = "ABM Afiliados";
            this.Busqueda.ResumeLayout(false);
            this.Busqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
        
        public ABM_Afiliado()
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
                    SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.AFILIADO", conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    adapter2.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.ReadOnly = true;


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        private GroupBox Busqueda;
        private ComboBox comboBox1;
        private Label label3;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private DataGridView dataGridView1;

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {   string nom=" (Nombre+' '+Apellido) like '%" + textBox1.Text + "%' AND";
                string dni = " DNI=" + textBox2.Text + " AND";
                string plan = " ID_Plan=" + comboBox1.SelectedValue + " AND";

                string where = "";

                if (!String.Equals(textBox1.Text, "")) where = nom;
                if (!String.Equals(textBox2.Text, "")) where += dni;
                if (!String.Equals(comboBox1.SelectedValue.ToString(), "")) where += plan;
                if (!String.Equals(where, "")) {
                    where = "where"+where;
                where = where.Substring(0, where.Length -4);
                }
               

                //lleno el datagrid

                SqlCommand cmd2 = new SqlCommand("USE GD2C2013 select * from YOU_SHALL_NOT_CRASH.AFILIADO " + where, conexion);

                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter2.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.ReadOnly = true;
            }
        }

    }
}

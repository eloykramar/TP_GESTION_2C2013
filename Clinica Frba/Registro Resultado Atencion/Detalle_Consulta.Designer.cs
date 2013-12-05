namespace Clinica_Frba.Registro_Resultado_Atencion
{
    partial class Registro_Consulta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelSin = new System.Windows.Forms.Button();
            this.btnAddSin = new System.Windows.Forms.Button();
            this.listSin = new System.Windows.Forms.ListBox();
            this.dgvSin = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelEnf = new System.Windows.Forms.Button();
            this.btnAddEnf = new System.Windows.Forms.Button();
            this.listEnf = new System.Windows.Forms.ListBox();
            this.dgvEnf = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSin)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnf)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelSin);
            this.groupBox1.Controls.Add(this.btnAddSin);
            this.groupBox1.Controls.Add(this.listSin);
            this.groupBox1.Controls.Add(this.dgvSin);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 329);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sintomas";
            // 
            // btnDelSin
            // 
            this.btnDelSin.Location = new System.Drawing.Point(103, 136);
            this.btnDelSin.Name = "btnDelSin";
            this.btnDelSin.Size = new System.Drawing.Size(39, 28);
            this.btnDelSin.TabIndex = 3;
            this.btnDelSin.Text = "↓↓";
            this.btnDelSin.UseVisualStyleBackColor = true;
            this.btnDelSin.Click += new System.EventHandler(this.btnDelSin_Click);
            // 
            // btnAddSin
            // 
            this.btnAddSin.Location = new System.Drawing.Point(47, 136);
            this.btnAddSin.Name = "btnAddSin";
            this.btnAddSin.Size = new System.Drawing.Size(39, 28);
            this.btnAddSin.TabIndex = 3;
            this.btnAddSin.Text = "↑↑";
            this.btnAddSin.UseVisualStyleBackColor = true;
            this.btnAddSin.Click += new System.EventHandler(this.btnAddSin_Click);
            // 
            // listSin
            // 
            this.listSin.FormattingEnabled = true;
            this.listSin.Location = new System.Drawing.Point(13, 21);
            this.listSin.Name = "listSin";
            this.listSin.Size = new System.Drawing.Size(165, 108);
            this.listSin.TabIndex = 1;
            // 
            // dgvSin
            // 
            this.dgvSin.AllowUserToAddRows = false;
            this.dgvSin.AllowUserToDeleteRows = false;
            this.dgvSin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSin.Location = new System.Drawing.Point(13, 170);
            this.dgvSin.Name = "dgvSin";
            this.dgvSin.ReadOnly = true;
            this.dgvSin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSin.Size = new System.Drawing.Size(165, 146);
            this.dgvSin.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelEnf);
            this.groupBox2.Controls.Add(this.btnAddEnf);
            this.groupBox2.Controls.Add(this.listEnf);
            this.groupBox2.Controls.Add(this.dgvEnf);
            this.groupBox2.Location = new System.Drawing.Point(211, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 329);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enfermedades";
            // 
            // btnDelEnf
            // 
            this.btnDelEnf.Location = new System.Drawing.Point(103, 136);
            this.btnDelEnf.Name = "btnDelEnf";
            this.btnDelEnf.Size = new System.Drawing.Size(39, 28);
            this.btnDelEnf.TabIndex = 3;
            this.btnDelEnf.Text = "↓↓";
            this.btnDelEnf.UseVisualStyleBackColor = true;
            this.btnDelEnf.Click += new System.EventHandler(this.btnDelEnf_Click);
            // 
            // btnAddEnf
            // 
            this.btnAddEnf.Location = new System.Drawing.Point(47, 136);
            this.btnAddEnf.Name = "btnAddEnf";
            this.btnAddEnf.Size = new System.Drawing.Size(39, 28);
            this.btnAddEnf.TabIndex = 3;
            this.btnAddEnf.Text = "↑↑";
            this.btnAddEnf.UseVisualStyleBackColor = true;
            this.btnAddEnf.Click += new System.EventHandler(this.btnAddEnf_Click);
            // 
            // listEnf
            // 
            this.listEnf.FormattingEnabled = true;
            this.listEnf.Location = new System.Drawing.Point(13, 21);
            this.listEnf.Name = "listEnf";
            this.listEnf.Size = new System.Drawing.Size(165, 108);
            this.listEnf.TabIndex = 1;
            // 
            // dgvEnf
            // 
            this.dgvEnf.AllowUserToAddRows = false;
            this.dgvEnf.AllowUserToDeleteRows = false;
            this.dgvEnf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnf.Location = new System.Drawing.Point(13, 170);
            this.dgvEnf.Name = "dgvEnf";
            this.dgvEnf.ReadOnly = true;
            this.dgvEnf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEnf.Size = new System.Drawing.Size(165, 146);
            this.dgvEnf.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 347);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 34);
            this.button2.TabIndex = 5;
            this.button2.Text = "Receta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(321, 347);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 34);
            this.button3.TabIndex = 5;
            this.button3.Text = "Guardar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Registro_Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 389);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Registro_Consulta";
            this.Text = "Resultado Consulta Medica";
            this.Load += new System.EventHandler(this.Registro_Consulta_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSin)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSin;
        private System.Windows.Forms.Button btnDelSin;
        private System.Windows.Forms.Button btnAddSin;
        private System.Windows.Forms.ListBox listSin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelEnf;
        private System.Windows.Forms.Button btnAddEnf;
        private System.Windows.Forms.ListBox listEnf;
        private System.Windows.Forms.DataGridView dgvEnf;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

    }
}
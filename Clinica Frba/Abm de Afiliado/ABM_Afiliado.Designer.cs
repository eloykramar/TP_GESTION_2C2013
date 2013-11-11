namespace Clinica_Frba.ABM_de_Afiliado
{
    partial class ABM_Afiliado
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModif = new System.Windows.Forms.Button();
            this.btnAlta = new System.Windows.Forms.Button();
            this.Busqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // Busqueda
            // 
            this.Busqueda.Controls.Add(this.btnDelete);
            this.Busqueda.Controls.Add(this.btnModif);
            this.Busqueda.Controls.Add(this.btnAlta);
            this.Busqueda.Controls.SetChildIndex(this.btnClean, 0);
            this.Busqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.Busqueda.Controls.SetChildIndex(this.textBox1, 0);
            this.Busqueda.Controls.SetChildIndex(this.label1, 0);
            this.Busqueda.Controls.SetChildIndex(this.textBox2, 0);
            this.Busqueda.Controls.SetChildIndex(this.label2, 0);
            this.Busqueda.Controls.SetChildIndex(this.label3, 0);
            this.Busqueda.Controls.SetChildIndex(this.comboBox1, 0);
            this.Busqueda.Controls.SetChildIndex(this.btnAlta, 0);
            this.Busqueda.Controls.SetChildIndex(this.btnModif, 0);
            this.Busqueda.Controls.SetChildIndex(this.btnDelete, 0);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(425, 111);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 30);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModif
            // 
            this.btnModif.Location = new System.Drawing.Point(425, 67);
            this.btnModif.Name = "btnModif";
            this.btnModif.Size = new System.Drawing.Size(84, 30);
            this.btnModif.TabIndex = 13;
            this.btnModif.Text = "Modificar";
            this.btnModif.UseVisualStyleBackColor = true;
            this.btnModif.Click += new System.EventHandler(this.btnModif_Click);
            // 
            // btnAlta
            // 
            this.btnAlta.Location = new System.Drawing.Point(425, 23);
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(84, 30);
            this.btnAlta.TabIndex = 11;
            this.btnAlta.Text = "Nuevo";
            this.btnAlta.UseVisualStyleBackColor = true;
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // ABM_Afiliado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 458);
            this.Name = "ABM_Afiliado";
            this.Text = "Form1";
            this.Busqueda.ResumeLayout(false);
            this.Busqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnModif;
        public System.Windows.Forms.Button btnAlta;
    }
}
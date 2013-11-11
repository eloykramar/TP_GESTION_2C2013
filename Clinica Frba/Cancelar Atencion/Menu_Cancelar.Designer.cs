namespace Clinica_Frba.Cancelar_Atencion
{
    partial class Menu_Cancelar
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
            this.btnCancProf = new System.Windows.Forms.Button();
            this.btnCancAfi = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnCancAfi);
            this.groupBox1.Controls.Add(this.btnCancProf);
            this.groupBox1.Location = new System.Drawing.Point(16, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione el tipo de Cancelacion";
            // 
            // btnCancProf
            // 
            this.btnCancProf.Location = new System.Drawing.Point(15, 32);
            this.btnCancProf.Name = "btnCancProf";
            this.btnCancProf.Size = new System.Drawing.Size(190, 30);
            this.btnCancProf.TabIndex = 0;
            this.btnCancProf.Text = "Cancelar Atencion Profesional";
            this.btnCancProf.UseVisualStyleBackColor = true;
            this.btnCancProf.Click += new System.EventHandler(this.btnCancProf_Click);
            // 
            // btnCancAfi
            // 
            this.btnCancAfi.Location = new System.Drawing.Point(15, 91);
            this.btnCancAfi.Name = "btnCancAfi";
            this.btnCancAfi.Size = new System.Drawing.Size(190, 30);
            this.btnCancAfi.TabIndex = 0;
            this.btnCancAfi.Text = "Cancelar Turno Afiliado";
            this.btnCancAfi.UseVisualStyleBackColor = true;
            this.btnCancAfi.Click += new System.EventHandler(this.btnCancAfi_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(15, 150);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(190, 30);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // Menu_Cancelar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 225);
            this.Controls.Add(this.groupBox1);
            this.Name = "Menu_Cancelar";
            this.Text = "Menu_Cancelar";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCancAfi;
        private System.Windows.Forms.Button btnCancProf;
    }
}
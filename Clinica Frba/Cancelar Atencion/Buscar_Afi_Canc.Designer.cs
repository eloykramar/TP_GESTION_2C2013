namespace Clinica_Frba.Cancelar_Atencion
{
    partial class Buscar_Afi_Canc
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
            this.btnBuscarProf = new System.Windows.Forms.Button();
            this.Busqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscarProf
            // 
            this.btnBuscarProf.Location = new System.Drawing.Point(431, 445);
            this.btnBuscarProf.Name = "btnBuscarProf";
            this.btnBuscarProf.Size = new System.Drawing.Size(112, 30);
            this.btnBuscarProf.TabIndex = 10;
            this.btnBuscarProf.Text = "Continuar";
            this.btnBuscarProf.UseVisualStyleBackColor = true;
            this.btnBuscarProf.Click += new System.EventHandler(this.btnBuscarProf_Click);
            // 
            // Buscar_Afi_Canc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 482);
            this.Controls.Add(this.btnBuscarProf);
            this.Name = "Buscar_Afi_Canc";
            this.Text = "Buscar_Afi_Canc";
            this.Controls.SetChildIndex(this.Busqueda, 0);
            this.Controls.SetChildIndex(this.btnBuscarProf, 0);
            this.Busqueda.ResumeLayout(false);
            this.Busqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBuscarProf;

    }
}
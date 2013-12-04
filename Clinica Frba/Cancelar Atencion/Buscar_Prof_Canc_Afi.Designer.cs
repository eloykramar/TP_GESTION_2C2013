namespace Clinica_Frba.Cancelar_Atencion
{
    partial class Buscar_Prof_Canc_Afi
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
            this.Busqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(347, 57);
            // 
            // btnTurnos
            // 
            this.btnTurnos.Location = new System.Drawing.Point(400, 15);
            this.btnTurnos.Size = new System.Drawing.Size(95, 25);
            this.btnTurnos.Text = "Cancelar Turno";
            this.btnTurnos.Click += new System.EventHandler(this.btnTurnos_Click);
            // 
            // Buscar_Prof_Canc_Afi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 453);
            this.Name = "Buscar_Prof_Canc_Afi";
            this.Text = "Buscar_Prof_Canc";
            this.Busqueda.ResumeLayout(false);
            this.Busqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
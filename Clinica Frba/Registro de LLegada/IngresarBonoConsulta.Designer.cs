namespace Clinica_Frba.Registro_de_LLegada
{
    partial class IngresarBonoConsulta
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
            this.textBono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttAceptar = new System.Windows.Forms.Button();
            this.buttSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBono
            // 
            this.textBono.Location = new System.Drawing.Point(33, 42);
            this.textBono.Name = "textBono";
            this.textBono.Size = new System.Drawing.Size(100, 20);
            this.textBono.TabIndex = 0;
            this.textBono.TextChanged += new System.EventHandler(this.textBono_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingrese bono consulta:";
            // 
            // buttAceptar
            // 
            this.buttAceptar.Location = new System.Drawing.Point(94, 81);
            this.buttAceptar.Name = "buttAceptar";
            this.buttAceptar.Size = new System.Drawing.Size(67, 23);
            this.buttAceptar.TabIndex = 2;
            this.buttAceptar.Text = "Aceptar";
            this.buttAceptar.UseVisualStyleBackColor = true;
            this.buttAceptar.Click += new System.EventHandler(this.buttAceptar_Click);
            // 
            // buttSalir
            // 
            this.buttSalir.Location = new System.Drawing.Point(12, 81);
            this.buttSalir.Name = "buttSalir";
            this.buttSalir.Size = new System.Drawing.Size(67, 23);
            this.buttSalir.TabIndex = 3;
            this.buttSalir.Text = "Salir";
            this.buttSalir.UseVisualStyleBackColor = true;
            this.buttSalir.Click += new System.EventHandler(this.buttSalir_Click);
            // 
            // IngresarBonoConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 123);
            this.Controls.Add(this.buttSalir);
            this.Controls.Add(this.buttAceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBono);
            this.Name = "IngresarBonoConsulta";
            this.Text = "IngresarBonoConsulta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBono;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttAceptar;
        private System.Windows.Forms.Button buttSalir;
    }
}
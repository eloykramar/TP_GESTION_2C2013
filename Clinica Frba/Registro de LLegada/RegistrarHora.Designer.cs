namespace Clinica_Frba.Registro_de_LLegada
{
    partial class RegistrarHora
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
            this.Fecha = new System.Windows.Forms.Label();
            this.Hora = new System.Windows.Forms.Label();
            this.dateTimePickerHora = new System.Windows.Forms.DateTimePicker();
            this.buttAceptar = new System.Windows.Forms.Button();
            this.buttSalir = new System.Windows.Forms.Button();
            this.dateTimePickerFecha = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // Fecha
            // 
            this.Fecha.AutoSize = true;
            this.Fecha.Location = new System.Drawing.Point(12, 34);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(40, 13);
            this.Fecha.TabIndex = 1;
            this.Fecha.Text = "Fecha:";
            // 
            // Hora
            // 
            this.Hora.AutoSize = true;
            this.Hora.Location = new System.Drawing.Point(12, 68);
            this.Hora.Name = "Hora";
            this.Hora.Size = new System.Drawing.Size(33, 13);
            this.Hora.TabIndex = 2;
            this.Hora.Text = "Hora:";
            // 
            // dateTimePickerHora
            // 
            this.dateTimePickerHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerHora.Location = new System.Drawing.Point(58, 62);
            this.dateTimePickerHora.Name = "dateTimePickerHora";
            this.dateTimePickerHora.ShowUpDown = true;
            this.dateTimePickerHora.Size = new System.Drawing.Size(82, 20);
            this.dateTimePickerHora.TabIndex = 3;
            // 
            // buttAceptar
            // 
            this.buttAceptar.Location = new System.Drawing.Point(102, 102);
            this.buttAceptar.Name = "buttAceptar";
            this.buttAceptar.Size = new System.Drawing.Size(67, 23);
            this.buttAceptar.TabIndex = 4;
            this.buttAceptar.Text = "Aceptar";
            this.buttAceptar.UseVisualStyleBackColor = true;
            this.buttAceptar.Click += new System.EventHandler(this.buttAceptar_Click);
            // 
            // buttSalir
            // 
            this.buttSalir.Location = new System.Drawing.Point(15, 102);
            this.buttSalir.Name = "buttSalir";
            this.buttSalir.Size = new System.Drawing.Size(67, 23);
            this.buttSalir.TabIndex = 5;
            this.buttSalir.Text = "Salir";
            this.buttSalir.UseVisualStyleBackColor = true;
            this.buttSalir.Click += new System.EventHandler(this.buttSalir_Click);
            // 
            // dateTimePickerFecha
            // 
            this.dateTimePickerFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFecha.Location = new System.Drawing.Point(58, 28);
            this.dateTimePickerFecha.Name = "dateTimePickerFecha";
            this.dateTimePickerFecha.Size = new System.Drawing.Size(82, 20);
            this.dateTimePickerFecha.TabIndex = 0;
            // 
            // RegistrarHora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(189, 137);
            this.Controls.Add(this.buttSalir);
            this.Controls.Add(this.buttAceptar);
            this.Controls.Add(this.dateTimePickerHora);
            this.Controls.Add(this.Hora);
            this.Controls.Add(this.Fecha);
            this.Controls.Add(this.dateTimePickerFecha);
            this.MaximizeBox = false;
            this.Name = "RegistrarHora";
            this.Text = "RegistrarHora";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Fecha;
        private System.Windows.Forms.Label Hora;
        private System.Windows.Forms.DateTimePicker dateTimePickerHora;
        private System.Windows.Forms.Button buttAceptar;
        private System.Windows.Forms.Button buttSalir;
        private System.Windows.Forms.DateTimePicker dateTimePickerFecha;
    }
}
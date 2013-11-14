namespace Clinica_Frba.Cancelar_Atencion
{
    partial class DiaCancelado
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerDiaIni = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerHoraIni = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDiaFin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerHoraFin = new System.Windows.Forms.DateTimePicker();
            this.butCanc = new System.Windows.Forms.Button();
            this.butSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione franja horaria a cancelar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Desde:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hasta:";
            // 
            // dateTimePickerDiaIni
            // 
            this.dateTimePickerDiaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDiaIni.Location = new System.Drawing.Point(72, 40);
            this.dateTimePickerDiaIni.Name = "dateTimePickerDiaIni";
            this.dateTimePickerDiaIni.Size = new System.Drawing.Size(88, 20);
            this.dateTimePickerDiaIni.TabIndex = 3;
            // 
            // dateTimePickerHoraIni
            // 
            this.dateTimePickerHoraIni.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerHoraIni.Location = new System.Drawing.Point(168, 40);
            this.dateTimePickerHoraIni.Name = "dateTimePickerHoraIni";
            this.dateTimePickerHoraIni.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerHoraIni.TabIndex = 4;
            // 
            // dateTimePickerDiaFin
            // 
            this.dateTimePickerDiaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDiaFin.Location = new System.Drawing.Point(72, 86);
            this.dateTimePickerDiaFin.Name = "dateTimePickerDiaFin";
            this.dateTimePickerDiaFin.Size = new System.Drawing.Size(88, 20);
            this.dateTimePickerDiaFin.TabIndex = 5;
            // 
            // dateTimePickerHoraFin
            // 
            this.dateTimePickerHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerHoraFin.Location = new System.Drawing.Point(168, 86);
            this.dateTimePickerHoraFin.Name = "dateTimePickerHoraFin";
            this.dateTimePickerHoraFin.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerHoraFin.TabIndex = 6;
            // 
            // butCanc
            // 
            this.butCanc.Location = new System.Drawing.Point(168, 148);
            this.butCanc.Name = "butCanc";
            this.butCanc.Size = new System.Drawing.Size(83, 44);
            this.butCanc.TabIndex = 7;
            this.butCanc.Text = "Cancelar turno";
            this.butCanc.UseVisualStyleBackColor = true;
            this.butCanc.Click += new System.EventHandler(this.butCanc_Click);
            // 
            // butSalir
            // 
            this.butSalir.Location = new System.Drawing.Point(32, 148);
            this.butSalir.Name = "butSalir";
            this.butSalir.Size = new System.Drawing.Size(83, 44);
            this.butSalir.TabIndex = 8;
            this.butSalir.Text = "Salir";
            this.butSalir.UseVisualStyleBackColor = true;
            this.butSalir.Click += new System.EventHandler(this.butSalir_Click);
            // 
            // DiaCancelado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(277, 224);
            this.Controls.Add(this.butSalir);
            this.Controls.Add(this.butCanc);
            this.Controls.Add(this.dateTimePickerHoraFin);
            this.Controls.Add(this.dateTimePickerDiaFin);
            this.Controls.Add(this.dateTimePickerHoraIni);
            this.Controls.Add(this.dateTimePickerDiaIni);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DiaCancelado";
            this.Text = "DiaCancelado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerDiaIni;
        private System.Windows.Forms.DateTimePicker dateTimePickerHoraIni;
        private System.Windows.Forms.DateTimePicker dateTimePickerDiaFin;
        private System.Windows.Forms.DateTimePicker dateTimePickerHoraFin;
        private System.Windows.Forms.Button butCanc;
        private System.Windows.Forms.Button butSalir;
    }
}
namespace Clinica_Frba.Generar_receta
{
    partial class Receta
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
            this.richTextBoxDiagnostico = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttDiagnostico = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttBonoFarm = new System.Windows.Forms.Button();
            this.buttSalir = new System.Windows.Forms.Button();
            this.dataGridViewSintomas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSintomas)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxDiagnostico
            // 
            this.richTextBoxDiagnostico.Location = new System.Drawing.Point(104, 22);
            this.richTextBoxDiagnostico.Name = "richTextBoxDiagnostico";
            this.richTextBoxDiagnostico.Size = new System.Drawing.Size(223, 170);
            this.richTextBoxDiagnostico.TabIndex = 0;
            this.richTextBoxDiagnostico.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Diagnostico:";
            // 
            // buttDiagnostico
            // 
            this.buttDiagnostico.Location = new System.Drawing.Point(199, 314);
            this.buttDiagnostico.Name = "buttDiagnostico";
            this.buttDiagnostico.Size = new System.Drawing.Size(89, 44);
            this.buttDiagnostico.TabIndex = 2;
            this.buttDiagnostico.Text = "Generar diagnostico";
            this.buttDiagnostico.UseVisualStyleBackColor = true;
            this.buttDiagnostico.Click += new System.EventHandler(this.buttDiagnostico_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Síntoma:";
            // 
            // buttBonoFarm
            // 
            this.buttBonoFarm.Location = new System.Drawing.Point(372, 314);
            this.buttBonoFarm.Name = "buttBonoFarm";
            this.buttBonoFarm.Size = new System.Drawing.Size(89, 44);
            this.buttBonoFarm.TabIndex = 5;
            this.buttBonoFarm.Text = "Completar bono farmacia";
            this.buttBonoFarm.UseVisualStyleBackColor = true;
            this.buttBonoFarm.Click += new System.EventHandler(this.buttBonoFarm_Click);
            // 
            // buttSalir
            // 
            this.buttSalir.Location = new System.Drawing.Point(15, 314);
            this.buttSalir.Name = "buttSalir";
            this.buttSalir.Size = new System.Drawing.Size(89, 44);
            this.buttSalir.TabIndex = 6;
            this.buttSalir.Text = "Salir";
            this.buttSalir.UseVisualStyleBackColor = true;
            this.buttSalir.Click += new System.EventHandler(this.buttSalir_Click);
            // 
            // dataGridViewSintomas
            // 
            this.dataGridViewSintomas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSintomas.Location = new System.Drawing.Point(104, 208);
            this.dataGridViewSintomas.Name = "dataGridViewSintomas";
            this.dataGridViewSintomas.Size = new System.Drawing.Size(240, 69);
            this.dataGridViewSintomas.TabIndex = 7;
            // 
            // Receta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 401);
            this.Controls.Add(this.dataGridViewSintomas);
            this.Controls.Add(this.buttSalir);
            this.Controls.Add(this.buttBonoFarm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttDiagnostico);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxDiagnostico);
            this.Name = "Receta";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSintomas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxDiagnostico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttDiagnostico;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttBonoFarm;
        private System.Windows.Forms.Button buttSalir;
        private System.Windows.Forms.DataGridView dataGridViewSintomas;

    }
}
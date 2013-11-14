using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clinica_Frba.DetalleAfiliado
{
    public partial class DetalleAfiliado : Form1
    {
        public DetalleAfiliado()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //lleno combo de planes
                    SqlCommand cmd = new SqlCommand("USE GD2C2013 select ID_Plan, Descripcion from YOU_SHALL_NOT_CRASH.PLAN_MEDICO", conexion);

                    SqlDataAdapter adapterPlanes = new SqlDataAdapter(cmd);
                    DataTable tablaDePlanes = new DataTable();
                    tablaDePlanes.Rows.Add();
                    tablaDePlanes.AcceptChanges();
                    adapterPlanes.Fill(tablaDePlanes);
                    cmbPlan.DisplayMember = "Descripcion";
                    cmbPlan.ValueMember = "ID_Plan";
                    cmbPlan.DataSource = tablaDePlanes;

                                        
                    //lleno combo de estados civiles
                    cmd = new SqlCommand("USE GD2C2013 select ID_Estado_Civil, Descripcion from YOU_SHALL_NOT_CRASH.ESTADO_CIVIL", conexion);

                    SqlDataAdapter adapterCivil = new SqlDataAdapter(cmd);
                    DataTable tablaDeEstCiviles = new DataTable();
                    tablaDeEstCiviles.Rows.Add();
                    tablaDeEstCiviles.AcceptChanges();
                    adapterCivil.Fill(tablaDeEstCiviles);
                    cmbCivil.DisplayMember = "Descripcion";
                    cmbCivil.ValueMember = "ID_Estado_Civil";
                    cmbCivil.DataSource = tablaDeEstCiviles;

                    //lleno el combo de sexo
                    List<KeyValuePair<char, string>> opsSex = new List<KeyValuePair<char, string>>();
                    opsSex.Add(new KeyValuePair<char, string>('F',"Femenino"));
                    opsSex.Add(new KeyValuePair<char, string>('M',"Masculino"));
                    cmbSexo.DataSource = opsSex;
                    cmbSexo.ValueMember = "Key";
                    cmbSexo.DisplayMember = "Value";

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        private void DetalleAfiliado_Load(object sender, EventArgs e)
        {

        }

        public virtual void guardar()
        {
        }

        private void validar()
        {
            string problemas = "";
            if (String.Equals(txtDNI.Text, "")){
                problemas += "\n El DNI es un campo necesario.";
            }
            if (String.Equals(txtNombre.Text, "")){
                problemas+="\n El Nombre es un campo necesario.";
            }
            if (String.Equals(txtApellido.Text, "")){
                problemas+="\n El Apellido es un campo necesario.";
            }
            if (cmbCivil.SelectedIndex==0){
                problemas+="\n Elija un Estado Civil.";
            }
            if (cmbPlan.SelectedIndex==0){
                problemas+="\n Elija un Plan Medico.";
            }
            if (String.Equals(txtUser.Text, "")){
                problemas+="\n El Nombre de Usuario es un campo necesario.";
            }
            
            if (String.Equals(problemas, "")){
                this.guardar(); 
            }else{
                MessageBox.Show(this,"Revise los siguientes problemas:" + problemas, "Error");
            }

        }


        public virtual void btnGuardar_Click(object sender, EventArgs e)
        {
            this.validar();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(txtDNI.Text, ""))
            {
                string Str = txtDNI.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros", "Error");
                    txtDNI.Text = "";
                }
            }
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            if (!String.Equals(txtTel.Text, ""))
            {
                string Str = txtTel.Text.Trim();
                long Num;

                bool isNum = long.TryParse(Str, out Num);

                if (!isNum)
                {
                    MessageBox.Show("Solo se aceptan numeros enteros", "Error");
                    txtTel.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TextBox t in groupBox1.Controls.OfType<TextBox>())
                    if (!t.ReadOnly) t.Text = "";

                foreach (ComboBox c in groupBox1.Controls.OfType<ComboBox>())
                    c.SelectedIndex = 0;

                numConsultas.Value = 0;
                numFACargo.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

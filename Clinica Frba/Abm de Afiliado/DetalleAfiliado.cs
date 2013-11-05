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

        public virtual void btnGuardar_Click(object sender, EventArgs e)
        {
            this.guardar();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        





 



        

    }
}

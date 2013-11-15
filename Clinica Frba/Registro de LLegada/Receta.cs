using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Clinica_Frba.Generar_Receta;

namespace Clinica_Frba.Generar_receta
{
    public partial class Receta : Form1
    {
        int idProfesional, idAfiliado, idTurno, id_diagnostico,id_receta;

        public Receta(int idProf, int idAfi, int idT)
        {
            InitializeComponent();
            idProfesional = idProf;
            idAfiliado = idAfi;
            idTurno = idT;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(string.Format(
                           "Select ID_SINTOMA, DESCRIPCION FROM YOU_SHALL_NOT_CRASH.SINTOMA"), conexion);

                SqlDataReader reader = cmd.ExecuteReader();

                List<Sintomas> lista_sintomas = new List<Sintomas>();

                while (reader.Read())
                {
                    Sintomas sintoma = new Sintomas();
                    sintoma.id_sintoma = Convert.ToInt32(reader.GetSqlDecimal(0).Value);
                    sintoma.descripcion = Convert.ToString(reader.GetSqlString(1).Value);
                    lista_sintomas.Add(sintoma);
                }

                dataGridViewSintomas.DataSource = lista_sintomas;
                dataGridViewSintomas.ReadOnly = true;

                cmd.Dispose();
                reader.Close();

                cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_receta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                cmd = new SqlCommand(string.Format(
                    "SELECT MAX(ID_RECETA) FROM YOU_SHALL_NOT_CRASH.RECETA"), conexion);
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    id_receta = Convert.ToInt32(reader.GetSqlDecimal(0).Value);
                }
                cmd.Dispose();
                reader.Close();
                conexion.Close();

            }
        }

        private void buttDiagnostico_Click(object sender, EventArgs e)
        {
            String descripcion = richTextBoxDiagnostico.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                if (richTextBoxDiagnostico.Text != "")
                {
                    
                    //Inserto diagnostico
                    SqlCommand cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_diagnostico", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idturno", SqlDbType.Int).Value = idTurno;
                    cmd.Parameters.Add("@idprofesional", SqlDbType.Int).Value = idProfesional;
                    cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = descripcion;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    
                    cmd = new SqlCommand(string.Format(
                        "SELECT MAX(ID_DIAGNOSTICO) FROM YOU_SHALL_NOT_CRASH.DIAGNOSTICO"), conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        id_diagnostico = Convert.ToInt32(reader.GetSqlDecimal(0).Value);
                        reader.Close();
                        cmd.Dispose();

                        int id_sintoma = Convert.ToInt32(dataGridViewSintomas.CurrentRow.Cells[0].Value);

                        //Inserto item diagnostico y receta
                        cmd = new SqlCommand("YOU_SHALL_NOT_CRASH.Insertar_item_diagnostico_y_receta", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_diagnostico", SqlDbType.Int).Value = id_diagnostico;
                        cmd.Parameters.Add("@idsintoma", SqlDbType.Int).Value = id_sintoma;
                        cmd.Parameters.Add("@id_receta", SqlDbType.Int).Value = id_receta;
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Diagnostico generado satisfactoriamente");
                    cmd.Dispose();
                    

                }
                else MessageBox.Show("No ha ingresado descripción");

                conexion.Close();
            }
            
        }

        private void buttBonoFarm_Click(object sender, EventArgs e)
        {
            new CompletarBono(id_receta,idAfiliado).ShowDialog();
        }

        private void buttSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class Sintomas
    {
        public int id_sintoma{ get; set; }
        public String descripcion { get; set; }
     
        public void sintomas() { }

        public void sintomas(int pId_sintoma, String pDescripcion)
        {
            this.id_sintoma = pId_sintoma;
            this.descripcion = pDescripcion;
        }
    }
}

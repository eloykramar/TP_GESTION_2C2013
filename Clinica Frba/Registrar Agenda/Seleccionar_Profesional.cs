using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinica_Frba.Registrar_Agenda
{
    public partial class Seleccionar_Profesional : Form1
    {
        public Seleccionar_Profesional()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                DataTable tabla = new DataTable();
                cargarATablaParaDataGripView("USE GD2C2013 select ID_PROFESIONAL, NOMBRE, APELLIDO, DNI from YOU_SHALL_NOT_CRASH.PROFESIONAL", ref tabla, conexion);
                dataGridView1.DataSource = tabla;
                DataGridViewButtonColumn botonRegistrar = this.crearBotones("", "Registrar Agenda");
                dataGridView1.Columns.Add(botonRegistrar);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String idProfesional = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String nombre = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                String apellido = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();


                if (e.ColumnIndex == 0)
                {                    
                    (new Registrar_Agenda(idProfesional, nombre, apellido)).Show();
                }

            }
        }
                    
    }
}

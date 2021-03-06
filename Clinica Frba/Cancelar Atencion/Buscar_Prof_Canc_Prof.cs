﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clinica_Frba.Pedir_Turno;

namespace Clinica_Frba.Cancelar_Atencion
{
    public partial class Buscar_Prof_Canc_Prof : Pedir_Turnos
    {
        int idP;
        public Buscar_Prof_Canc_Prof(int x)
            : base(x)
        {
            InitializeComponent();
        }
        public override void turnos()
        {
            //abro ventana para dar baja
            idP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_Profesional"].Value.ToString());
            String nombreCompletoP = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
            (new Turnos(idP, nombreCompletoP)).ShowDialog();
        }
    }
}

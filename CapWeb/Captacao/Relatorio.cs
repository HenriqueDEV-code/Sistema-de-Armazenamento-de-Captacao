﻿using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CapWeb.Captacao
{
    public partial class Relatorio : MaterialForm
    {

        private string DBA;
        public Relatorio(string DBA)
        {
           this.DBA = DBA;
            InitializeComponent();
        }
    }




}

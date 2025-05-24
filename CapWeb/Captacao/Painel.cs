using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapWeb.Captacao
{
    public partial class Painel : MaterialForm
    {

        private string DBA;
        
        public Painel(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
        }


        
    }
}

using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapWeb.Captacao
{
    public partial class Cadastros : MaterialForm
    {
        private string DBA;
        public Cadastros(string DBA)
        {
            this.DBA = DBA;
           
            InitializeComponent();
        }
    }
}

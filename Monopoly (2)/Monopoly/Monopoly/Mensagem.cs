using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class Mensagem : Form
    {
        public Mensagem(string info)
        {
            InitializeComponent();
            ControlBox = false;

            label1.Text = info;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

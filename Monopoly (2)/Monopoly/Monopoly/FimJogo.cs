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
    using Classes;

    public partial class FimJogo : Form
    {
        public FimJogo(int index)
        {
            InitializeComponent();
            label1.Text = Tabuleiro.jogadores[index].Nome + " GANHOU!";
            pictureBox1.Image = Tabuleiro.jogadores[index].Peça;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu m = new Menu();
            m.Show();
        }

        private void FimJogo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

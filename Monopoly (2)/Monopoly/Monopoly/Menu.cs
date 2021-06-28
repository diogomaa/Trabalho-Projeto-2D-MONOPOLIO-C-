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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void sairbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // OCULTAR MENU PRINCIPAL
            sairbutton.Visible = false;
            opçoesButton.Visible = false;
            playbutton.Visible = false;

            // MOSTRAR MENU DAS OPÇOES
            voltarButton.Visible = true;
            label2.Visible = true;


        }

        private void voltarButton_Click(object sender, EventArgs e)
        {
            // OCULTAR MENU DAS OPÇOES
            voltarButton.Visible = false;
            label2.Visible = false;


            // MOSTRAR MENU PRINCIPAL
            sairbutton.Visible = true;
            opçoesButton.Visible = true;
            playbutton.Visible = true;
        }

        private void playbutton_Click(object sender, EventArgs e)
        {
            /*Jogo form2 = new Jogo();
            form2.Show();
            form2.Hide();*/


            this.Hide();
            //novoJogo form1 = new novoJogo();
            Setup form1 = new Setup();
            form1.Show();

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

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

    public partial class MostrarCartasSorte : Form
    {
        public MostrarCartasSorte(string info)
        {
            InitializeComponent();
            ControlBox = false;

            //Mete tudo invisivel
            pictureBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            label3.Text = info;

            Bitmap MyImage;
            string caminho = @"..\..\Resources\";
            string nomeFile = "EXAMPLE.PNG";

            //Colocar a imagem correspondente ao local
            if (SorteOuCaixa())
            {
                nomeFile = "sorte.png";
            } else
            {
                nomeFile = "caixa_comunidade.png";
            }
            MyImage = new Bitmap(caminho + nomeFile);
            pictureBox1.Image = (Image)MyImage;
            pictureBox1.Visible = true;
        }

        private bool SorteOuCaixa()
        {
            // TRUE se for SORTE
            // FALSE se for Caixa

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 7 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 22 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 36)
            {
                return true;

            } else if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 2 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 17 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 33)
            {
                return false;
            } else
            {
                MessageBox.Show("ERRO");
                return false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;

            // Mostra o titulo correspondente à carta
            if (SorteOuCaixa()) label2.Visible = true; else label1.Visible = true;

            label3.Visible = true;

            if (label3.Text.Equals("PAGUE UMA MULTA DE 1.000€ OU,\nRETIRE OUTRA CARTA."))
            {
                button2.Visible = true;
                button3.Visible = true;
            } else
            {
                button1.Visible = true;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RetirarCartaSorte.NovaCarta = false;
            RetirarCartaSorte.continuar = true;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RetirarCartaSorte.NovaCarta = true;
            RetirarCartaSorte.continuar = true;
            this.Close();
        }
    }
}

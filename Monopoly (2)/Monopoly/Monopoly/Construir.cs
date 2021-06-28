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

    public partial class Construir : Form
    {
        

        public Construir(string tipo)
        {
            InitializeComponent();

            // Algumas variaveis importantes
            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            int precoCasa = 5000;

            t.Text = tipo;
               

            // Mete tudo invisivel
            invisivel();


            // Mete a cor no topo da janela
            pictureBox1.BackColor = Color.FromName(tipo);

            // Meto o preço de cada casa conforme o seu preço
            if (tipo.Equals("Brown") || tipo.Equals("Blue")) precoCasa = 5000;
            if (tipo.Equals("Pink") || tipo.Equals("Orange")) precoCasa = 10000;
            if (tipo.Equals("Red") || tipo.Equals("Yellow")) precoCasa = 15000;
            if (tipo.Equals("Green") || tipo.Equals("Purple")) precoCasa = 20000;

            label16.Text = precoCasa.ToString();

            label1.Text = precoCasa.ToString() + " € / Cada";
            label2.Text = precoCasa.ToString() + " € / Cada";
            label3.Text = precoCasa.ToString() + " € / Cada";



            for (int i=0; i<k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {    
                    count++;
                    if (count == 1)
                    {
                        
                        groupBox1.Text = Tabuleiro.jogadores[index].Propriedades[i].Nome;
                        groupBox1.Visible = true;
                        label4.Visible = true;
                        label7.Text = getrenda(i).ToString() + " €";
                        label7.Visible = true;

                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label10.Visible = true;
                            label13.Text = getNextRenda(i).ToString() + " €";
                            label13.Visible = true;
                        }
                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button2.Visible = false; else button1.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button1.Visible = false; else button2.Visible = true;

                    }

                    if (count == 2)
                    {
                        groupBox2.Text = Tabuleiro.jogadores[index].Propriedades[i].Nome;
                        groupBox2.Visible = true;
                        label5.Visible = true;
                        label8.Text = getrenda(i).ToString() + " €";
                        label8.Visible = true;

                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label11.Visible = true;
                            label14.Text = getNextRenda(i).ToString() + " €";
                            label14.Visible = true;
                        } else
                        {
                            label14.Visible = false;
                        }

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button4.Visible = false; else button3.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button3.Visible = false; else button4.Visible = true;
                    }

                    if (count == 3)
                    {
                        groupBox3.Text = Tabuleiro.jogadores[index].Propriedades[i].Nome;
                        groupBox3.Visible = true;
                        label6.Visible = true;
                        label9.Text = getrenda(i).ToString() + " €";
                        label9.Visible = true;

                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label12.Visible = true;
                            label15.Text = getNextRenda(i).ToString() + " €";
                            label15.Visible = true;
                        } else
                        {
                            label15.Visible = false;
                        }

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button6.Visible = false; else button5.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button5.Visible = false; else button6.Visible = true;
                    }
                }
            }
            
        }

        private int getrenda(int i)
        {
            int index = Tabuleiro.AtualJogadorIndex;

            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) return Tabuleiro.jogadores[index].Propriedades[i].Renda;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 1) return Tabuleiro.jogadores[index].Propriedades[i].Renda1;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 2) return Tabuleiro.jogadores[index].Propriedades[i].Renda2;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 3) return Tabuleiro.jogadores[index].Propriedades[i].Renda3;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 4) return Tabuleiro.jogadores[index].Propriedades[i].Renda4;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) return Tabuleiro.jogadores[index].Propriedades[i].Renda5;

            return 0;
        }

        private int getNextRenda(int i)
        {
            int index = Tabuleiro.AtualJogadorIndex;

            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) return Tabuleiro.jogadores[index].Propriedades[i].Renda1;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 1) return Tabuleiro.jogadores[index].Propriedades[i].Renda2;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 2) return Tabuleiro.jogadores[index].Propriedades[i].Renda3;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 3) return Tabuleiro.jogadores[index].Propriedades[i].Renda4;
            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 4) return Tabuleiro.jogadores[index].Propriedades[i].Renda5;

            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // COMPRAR UMA CASA NA PROPRIEDADE 1

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);

            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 1)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas++;
                        Tabuleiro.jogadores[index].RetirarDinheiro(quant);

                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button2.Visible = false; else button1.Visible = true;
                       // if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button1.Visible = false; else button2.Visible = true;

                        label7.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label10.Visible = true;
                            label13.Text = getNextRenda(i).ToString() + " €";
                            label13.Visible = true;
                        } else
                        {
                            label13.Visible = false;
                        }
                    }
                }
            }

            

        }

        private void invisivel ()
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            pictureBox1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // COMPRAR UMA CASA NA PROPRIEDADE 2

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);


            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 2)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas++;
                        Tabuleiro.jogadores[index].RetirarDinheiro(quant);

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button4.Visible = false; else button3.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button3.Visible = false; else button4.Visible = true;

                        label8.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label11.Visible = true;
                            label14.Text = getNextRenda(i).ToString() + " €";
                            label14.Visible = true;
                        }
                        else
                        {
                            label14.Visible = false;
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // COMPRAR UMA CASA NA PROPRIEDADE 3

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);


            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 3)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas++;
                        Tabuleiro.jogadores[index].RetirarDinheiro(quant);

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas > 4) button6.Visible = false; else button5.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button5.Visible = false; else button6.Visible = true;

                        label9.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label12.Visible = true;
                            label15.Text = getNextRenda(i).ToString() + " €";
                            label15.Visible = true;
                        }
                        else
                        {
                            label15.Visible = false;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // VENDER UMA CASA NA PROPRIEDADE 1

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);


            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 1)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas--;
                        Tabuleiro.jogadores[index].Depositar(quant);

                        //if (Tabuleiro.jogadores[index].Propriedades[i].Casas ==) button2.Visible = false; else button1.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button1.Visible = false; else button2.Visible = true;

                        label7.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label10.Visible = true;
                            label13.Text = getNextRenda(i).ToString() + " €";
                            label13.Visible = true;
                        }
                        else
                        {
                            label13.Visible = false;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // VENDER UMA CASA NA PROPRIEDADE 1

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);

            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 2)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas--;
                        Tabuleiro.jogadores[index].Depositar(quant);

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas > 4) button4.Visible = false; else button3.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button3.Visible = false; else button4.Visible = true;

                        label8.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label11.Visible = true;
                            label14.Text = getNextRenda(i).ToString() + " €";
                            label14.Visible = true;
                        }
                        else
                        {
                            label14.Visible = false;
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // VENDER UMA CASA NA PROPRIEDADE 1

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int count = 0;
            string tipo = t.Text;
            int quant = Int32.Parse(label16.Text);


            for (int i = 0; i < k; i++)
            {
                if (Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString().Equals(tipo))
                {
                    count++;
                    if (count == 3)
                    {
                        Tabuleiro.jogadores[index].Propriedades[i].Casas--;
                        Tabuleiro.jogadores[index].Depositar(quant);

                        // Desativar butao comprar quando chega ao limite
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5) button6.Visible = false; else button5.Visible = true;
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 0) button5.Visible = false; else button6.Visible = true;

                        label9.Text = getrenda(i).ToString() + " €";
                        if (Tabuleiro.jogadores[index].Propriedades[i].Casas != 5)
                        {
                            label12.Visible = true;
                            label15.Text = getNextRenda(i).ToString() + " €";
                            label15.Visible = true;
                        }
                        else
                        {
                            label15.Visible = false;
                        }
                    }
                }
            }
        }
    }
}

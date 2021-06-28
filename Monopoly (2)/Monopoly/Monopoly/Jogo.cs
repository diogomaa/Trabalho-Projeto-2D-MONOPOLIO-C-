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

    public partial class Jogo : Form
    {
        public Jogo()
        {
            InitializeComponent();

            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;

            setupImagensTab();

            update();
        }

        private void TerminarTurnoButton_Click(object sender, EventArgs e)
        {

            TerminaTurno();
 
        }

        private void TerminaTurno()
        {
            
            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso == true)
                // Retira um turno para sair da prisao;
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao -= 1;

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao == 0)
                // 
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso = false;


            // Muda o turno para o proximo jogador
            Tabuleiro.AtualJogadorIndex += 1;
            if (Tabuleiro.AtualJogadorIndex == Tabuleiro.NrTotalJogadores)
            {
                Tabuleiro.AtualJogadorIndex = 0;
                Tabuleiro.NrRondas++;
                label21.Text = "Ronda Nº " + Tabuleiro.NrRondas;
            }

            // Se o jogador tiver dado falencia, passa ao proximo
            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].falencia)
            {
                TerminaTurno();
            }

            // Mostra o botao das trades depois da ronda 5
            if (Tabuleiro.NrRondas == 5)
            {
                button3.Visible = true;
            }

            button1.Visible = false;

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso == false)
            {
                TerminarTurnoButton.Visible = false;
                dadosButton.Visible = true;
                button2.Visible = false;

            }
            else if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].CartaCadeia == false)
            {
                TerminarTurnoButton.Visible = false;
                dadosButton.Visible = true;
                button2.Text = "Pague 5.000€ ou espere " + Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao + " turnos.";
                button2.Visible = true;
            } else
            {
                TerminarTurnoButton.Visible = false;
                dadosButton.Visible = true;
                button2.Text = "Use a carta para Sair ou espere " + Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao + " turnos.";
                button2.Visible = true;
            }

            label4.Text = Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Nome;
            update();
            TerminarTurnoButton.Visible = false;
        }

        private void updatePropJog( int index)
        {
            comboBox1.Items.Add("");
            comboBox1.SelectedIndex = comboBox1.Items.Count -1;
            comboBox1.Items.Clear();

            int k = Tabuleiro.jogadores[index].Propriedades.Count;

            
                for (int i = 0; i < k; i++)
                {
                    comboBox1.Items.Add(Tabuleiro.jogadores[index].Propriedades[i].Nome);

                }
             
        }

        private void dadosButton_Click(object sender, EventArgs e)
        {
            Random rng = new Random();

            // Guarda a posição atual do jogador atual
            int posicaoAtualJogador = Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual;

            // Roda os dados e soma os seus resultados
            int Dado1 = rng.Next(1, 7);
            int Dado2 = rng.Next(1, 7);
            int totalDados = Dado1 + Dado2;

            // Mostra no form o resultado dos dados
            dado1label.Text = Dado1.ToString();
            dado2label.Text = Dado2.ToString();

            // Se faltar 1 turno na prisão Mostra o Button de sair da prisão no ecrã              
            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao == 1)
            {
                button2.Visible = false;
            }

            //Se o jogador atual estiver preso
            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso == true)
            {
                // Se o resultado dos dados forem iguais (double)
                if (Dado1 == Dado2)
                {
                    // Mostra a mensagem no ecrã
                    Mensagem m = new Mensagem("Você está Livre da Cadeia.");
                    m.ShowDialog();
                    // Remove o botão de sair da cadeia
                    button2.Visible = false;
                    // Remove o jogador da cadeia
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso = false;
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao = 0;
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].SetPosicao(posicaoAtualJogador + totalDados);
                }
            }
            // Se não estiver preso!
            else
                // Coloca o jogador na posição dos dados
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].SetPosicao(posicaoAtualJogador + totalDados);

            // Se o resultado dos dados for igual
            if (Dado1 == Dado2)
            {
                // Vai acrescentar ao contador de doubles +1 e remover o butao de terminar turno
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].doubleCount ++;
                TerminarTurnoButton.Visible = false;
                dadosButton.Visible = true;

                // Se o jogador tiver 3 double consecutivos
                if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].doubleCount == 3)
                {
                    // O Jogador vai para a cadeia
                    Mensagem m = new Mensagem("Tirou Double várias vezes. Está Preso!");
                    m.ShowDialog();
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].SetPosicao(10);
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso = true;
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao = 3;
                }
            } else
            // Caso nao seja double
            {
                // Mete o contador a zero e deixa o jogador terminar o turno
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].doubleCount = 0;
                TerminarTurnoButton.Visible = true;
                dadosButton.Visible = false;
            }

            Tabuleiro.SomaDados = totalDados;
            // Atualiza os dados do jogo
            label4.Text = Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
            update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Propriedade propriedadeAtual = (Propriedade)Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual];

            if (propriedadeAtual.Preco > Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Dinheiro)
            {
                Mensagem m = new Mensagem("Você não tem dinheiro Suficiente.");
                m.ShowDialog();
            } else
            {
                Tabuleiro.AddPropriedadeJogador(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual, Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Index - 1);

                updatePropJog(Tabuleiro.AtualJogadorIndex);
                update();
                button1.Visible = false;
            }
        }


        //Player1Nome.Text = Tabuleiro.tabuleiro.Count().ToString();

        private void update()
        {
            // Outros Updates
            InvisivelJogad();
            updatePosJog();
            updateCasas();


            // atualiza o nome do jogador atual
            //label1.Text = Tabuleiro.AtualJogadorIndex.ToString();
            label1.Text = Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Nome.ToString();
            label1.ForeColor = Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].cor;
            // atualiza o dinheiro do jogador atual
            dinheiroJogLabel.Text = Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Dinheiro.ToString();

            if (Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 0 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 2 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 4 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 7 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 10 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 17 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 20 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 22 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 30 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 33 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 36 ||
                Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index == 38 
                
                )
            {
                button1.Visible = false;
                
            }
            else
            {
                if (Tabuleiro.VerPropriedadeDonoNull(Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Index) == true)
                {
                    button1.Visible = true;
                }
                else
                {
                    button1.Visible = false;
                }

            }


           
            if (!Tabuleiro.VerPropriedadeDonoNull(1)) { prop1.Visible = true; prop1.BackColor = Tabuleiro.verCorProprietario(1); } else { prop1.Visible = false; }
            if (!Tabuleiro.VerPropriedadeDonoNull(3)) { prop3.Visible = true; prop3.BackColor = Tabuleiro.verCorProprietario(3); } else { prop3.Visible = false; }
            if (!Tabuleiro.VerPropriedadeDonoNull(5)) { prop5.Visible = true; prop5.BackColor = Tabuleiro.verCorProprietario(5); } else { prop5.Visible = false; } 
            if (!Tabuleiro.VerPropriedadeDonoNull(6)) { prop6.Visible = true; prop6.BackColor = Tabuleiro.verCorProprietario(6); } else { prop6.Visible = false; }
            if (!Tabuleiro.VerPropriedadeDonoNull(8)) { prop8.Visible = true; prop8.BackColor = Tabuleiro.verCorProprietario(8); } else { prop8.Visible = false; }
            if (!Tabuleiro.VerPropriedadeDonoNull(9)) { prop9.Visible = true; prop9.BackColor = Tabuleiro.verCorProprietario(9); } else { prop9.Visible = false; }

            if (!Tabuleiro.VerPropriedadeDonoNull(11)) { prop11.Visible = true; prop11.BackColor = Tabuleiro.verCorProprietario(11); } else prop11.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(12)) { prop12.Visible = true; prop12.BackColor = Tabuleiro.verCorProprietario(12); } else prop12.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(13)) { prop13.Visible = true; prop13.BackColor = Tabuleiro.verCorProprietario(13); } else prop13.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(14)) { prop14.Visible = true; prop14.BackColor = Tabuleiro.verCorProprietario(14); } else prop14.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(15)) { prop15.Visible = true; prop15.BackColor = Tabuleiro.verCorProprietario(15); } else prop15.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(16)) { prop16.Visible = true; prop16.BackColor = Tabuleiro.verCorProprietario(16); } else prop16.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(18)) { prop18.Visible = true; prop18.BackColor = Tabuleiro.verCorProprietario(18); } else prop18.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(19)) { prop19.Visible = true; prop19.BackColor = Tabuleiro.verCorProprietario(19); } else prop19.Visible = false;

            if (!Tabuleiro.VerPropriedadeDonoNull(21)) { prop21.Visible = true; prop21.BackColor = Tabuleiro.verCorProprietario(21); } else prop21.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(23)) { prop23.Visible = true; prop23.BackColor = Tabuleiro.verCorProprietario(23); } else prop23.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(24)) { prop24.Visible = true; prop24.BackColor = Tabuleiro.verCorProprietario(24); } else prop24.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(25)) { prop25.Visible = true; prop25.BackColor = Tabuleiro.verCorProprietario(25); } else prop25.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(26)) { prop26.Visible = true; prop26.BackColor = Tabuleiro.verCorProprietario(26); } else prop26.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(27)) { prop27.Visible = true; prop27.BackColor = Tabuleiro.verCorProprietario(27); } else prop27.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(28)) { prop28.Visible = true; prop28.BackColor = Tabuleiro.verCorProprietario(28); } else prop28.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(29)) { prop29.Visible = true; prop29.BackColor = Tabuleiro.verCorProprietario(29); } else prop29.Visible = false;

            if (!Tabuleiro.VerPropriedadeDonoNull(31)) { prop31.Visible = true; prop31.BackColor = Tabuleiro.verCorProprietario(31); } else prop31.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(32)) { prop32.Visible = true; prop32.BackColor = Tabuleiro.verCorProprietario(32); } else prop32.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(34)) { prop34.Visible = true; prop34.BackColor = Tabuleiro.verCorProprietario(34); } else prop34.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(35)) { prop35.Visible = true; prop35.BackColor = Tabuleiro.verCorProprietario(35); } else prop35.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(37)) { prop37.Visible = true; prop37.BackColor = Tabuleiro.verCorProprietario(37); } else prop37.Visible = false;
            if (!Tabuleiro.VerPropriedadeDonoNull(39)) { prop39.Visible = true; prop39.BackColor = Tabuleiro.verCorProprietario(39); } else prop39.Visible = false;

            updatePropJog(Tabuleiro.AtualJogadorIndex);

            

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Dinheiro >= 0)
            {
                //TerminarTurnoButton.Visible = true;
                button4.Visible = false;
            }
            else
            {
                TerminarTurnoButton.Visible = false;
                button4.Visible = true;
            }

            
        }

        private void Jogo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Dinheiro < 5000)
            {
                Mensagem m = new Mensagem("Você não tem dinheiro Suficiente.");
                m.ShowDialog();
            }
            else
            {
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].RetirarDinheiro(5000);
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].turnosPrisao = 0;
                Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].estaPreso = false;
                button2.Visible = false;
            }
        }

        private void Jogo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            invisibleProp();

            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int propindex;

            if (comboBox1.Items.Equals(""))
            {
                MessageBox.Show("ERRO");
            } else
            {
                for (int i = 0; i < k; i++)
                {
                    if (comboBox1.SelectedItem.Equals(Tabuleiro.jogadores[index].Propriedades[i].Nome))
                    {
                        propindex = Tabuleiro.jogadores[index].Propriedades[i].Index;

                        // Mostra Button da Hipoteca e o seu Valor

                        if (Tabuleiro.jogadores[index].Propriedades[i].hipotecado == false)
                        {
                            HipotecaButton.Text = "Hipotecar";
                            HipotecaButton.Visible = true;
                            ValorHipot.Text = Tabuleiro.jogadores[index].Propriedades[i].Hipoteca.ToString() + " €";
                            ValorHipot.Visible = true;
                        } else
                        {
                            HipotecaButton.Text = "Desipotecar";
                            HipotecaButton.Visible = true;
                            ValorHipot.Text = Tabuleiro.jogadores[index].Propriedades[i].Hipoteca.ToString() + " €";
                            ValorHipot.Visible = true;
                        }
                        

                        // Mostra Nome da Propriedade
                        NomeProp.Text = Tabuleiro.jogadores[index].Propriedades[i].Nome;
                        NomeProp.Visible = true;

                        if (propindex != 5 && propindex != 15 && propindex != 25 && propindex != 35 && propindex != 12 && propindex != 28)
                        {
                            // Atualiza a picture box com a cor da casa
                            PB_Color.BackColor = Color.FromName(Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString());
                            PB_Color.Visible = true;

                            // Calcula o preço de uma casa dependendo da zona do tabuleiro
                            int pc;
                            if (Tabuleiro.jogadores[index].Propriedades[i].Index < 10)
                                pc = 5000;
                            else if (Tabuleiro.jogadores[index].Propriedades[i].Index < 20)
                                pc = 10000;
                            else if (Tabuleiro.jogadores[index].Propriedades[i].Index < 30)
                                pc = 15000;
                            else
                                pc = 20000;
                            // Mostra o Button para Contruir
                            PreçoCasa.Text = pc.ToString() + " €";
                            PreçoCasa.Visible = true;
                            BuildButton.Visible = true;

                            // Mostra as Casas/hotel que tem a Propriedade
                            pictureBox2.Visible = true;
                            pictureBox3.Visible = true;

                            // Se tiver 5 Casas, mostra que tem 1 hotel
                            if (Tabuleiro.jogadores[index].Propriedades[i].Casas == 5)
                            {
                                NrCasas.Text = "4";
                                NrHotel.Text = "1";
                            } else // Senao mostra quantas casas tem!
                            {
                                NrCasas.Text = Tabuleiro.jogadores[index].Propriedades[i].Casas.ToString();
                                NrHotel.Text = "0";    
                            }
                            // Mostra o numero de casas
                            NrCasas.Visible = true;
                            NrHotel.Visible = true;

                            //Omitir dados das estações e companhias
                            label16.Visible = false;
                            label17.Visible = false;
                            label18.Visible = false;
                            label19.Visible = false;
                            label20.Visible = false;

                            // Mostra os dados da propriedade
                            label3.Visible = true;
                            label5.Visible = true;
                            label8.Visible = true;
                            label7.Visible = true;
                            label6.Visible = true;
                            label9.Visible = true;

                            // Mostra os Valores das rendas
                            label10.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda.ToString();
                            label11.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda1.ToString();
                            label12.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda2.ToString();
                            label13.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda3.ToString();
                            label14.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda4.ToString();
                            label15.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda5.ToString();
                            label10.Visible = true;
                            label11.Visible = true;
                            label12.Visible = true;
                            label13.Visible = true;
                            label14.Visible = true;
                            label15.Visible = true;                          

                        } else if (propindex == 5 || propindex == 15 || propindex == 25 || propindex == 35)
                        {
                            // Omitir dados das porpriedades
                            label3.Visible = false;
                            label5.Visible = false;
                            label8.Visible = false;
                            label7.Visible = false;
                            label6.Visible = false;
                            label9.Visible = false;
                            label14.Visible = false;
                            label15.Visible = false;
                            label20.Visible = false;

                            // Mostrar dados das estações
                            label16.Visible = true;
                            label17.Visible = true;
                            label18.Visible = true;
                            label19.Visible = true;

                            // Rendas das estações
                            int soma = 0;
                            label10.Text = Tabuleiro.jogadores[index].Propriedades[i].Renda.ToString();
                            soma = Tabuleiro.jogadores[index].Propriedades[i].Renda * 2;
                            label11.Text = soma.ToString();
                            soma *= 2;
                            label12.Text = soma.ToString();
                            soma *= 2;
                            label13.Text = soma.ToString();
                            label10.Visible = true;
                            label11.Visible = true;
                            label12.Visible = true;
                            label13.Visible = true;

                        } else
                        {
                            // Esconder dados de estaçoes e propriedades
                            label3.Visible = false;
                            label5.Visible = false;
                            label8.Visible = false;
                            label7.Visible = false;
                            label6.Visible = false;
                            label9.Visible = false;
                            label14.Visible = false;
                            label15.Visible = false;
                            label16.Visible = false;
                            label17.Visible = false;
                            label18.Visible = false;
                            label19.Visible = false;
                            label10.Visible = false;
                            label11.Visible = false;
                            label12.Visible = false;
                            label13.Visible = false;
                            label14.Visible = false;
                            label15.Visible = false;

                            // Mostrar dados da companhia
                            label20.Visible = true;
                        }


                    }
                }
                
            }

        }

        private void invisibleProp()
        {
            HipotecaButton.Visible = false;
            ValorHipot.Visible = false;
            PB_Color.Visible = false;
            NomeProp.Visible = false;
            PreçoCasa.Visible = false;
            BuildButton.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            NrCasas.Visible = false;
            NrHotel.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            label6.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;


        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int propindex = 0;

            for (int i = 0; i < k; i++)
            {
                if (comboBox1.SelectedItem.Equals(Tabuleiro.jogadores[index].Propriedades[i].Nome))
                {
                    propindex = i;
                }
            }

            if(verGrupoCor(Tabuleiro.jogadores[index].Propriedades[propindex].TipoPropriedade.ToString()))
            {
                // Comprar Casa
                Construir c = new Construir(Tabuleiro.jogadores[index].Propriedades[propindex].TipoPropriedade.ToString());
                c.ShowDialog();

            }  else
            {
                Mensagem m = new Mensagem("Precisas de ter todas as Propriedades\nde um grupo para construir casas.");
                m.ShowDialog();
            }

            update();
        }

        private bool verGrupoCor(string tipo)
        {
            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            string x = "";

            int count = 0;
           // int castanho = 0, azul = 0, rosa = 0, laranja = 0, vermelho = 0, amarelo = 0, verde = 0, roxo = 0;

            for (int i = 0; i < k; i++)
            {
                x = Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString();
                if (x.Equals(tipo)) count++;

                /*
                if (x.Equals("Brown")) castanho++;
                if (x.Equals("Blue")) azul++;
                if (x.Equals("Pink")) rosa++;
                if (x.Equals("Orange")) laranja++;
                if (x.Equals("Red")) vermelho++;
                if (x.Equals("Yellow")) amarelo++;
                if (x.Equals("Green")) verde++;
                if (x.Equals("Purple")) roxo++;
                */
            }

            if (tipo.Equals("Brown") || tipo.Equals("Purple") || tipo.Equals("Companhia"))
            {
                if (count == 2)
                {
                    return true;
                } else
                {
                    return false;
                }
            }

            if (count == 3)
            {
                return true;
            } else
            {
                return false;
            }

            /*
            if (castanho == 2) return true;
            if (azul == 3) return true;
            if (rosa == 3) return true;
            if (laranja == 3) return true;
            if (vermelho == 3) return true;
            if (amarelo == 3) return true;
            if (verde == 3) return true;
            if (roxo == 2) return true;*/
            
        }

        private int verEstacoes()
        {
            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            string x = "";

            int count = 0;

            for (int i = 0; i < k; i++)
            {
                x = Tabuleiro.jogadores[index].Propriedades[i].TipoPropriedade.ToString();
                if (x.Equals("Estação")) count++;
                    return count;
            }

            return 0;
        }

        private void HipotecaButton_Click(object sender, EventArgs e)
        {
            int index = Tabuleiro.AtualJogadorIndex;
            int k = Tabuleiro.jogadores[index].Propriedades.Count;
            int propindex = 0;

            for (int i = 0; i < k; i++)
            {
                if (comboBox1.SelectedItem.Equals(Tabuleiro.jogadores[index].Propriedades[i].Nome))
                {
                    propindex = i;
                }
            }

            if (Tabuleiro.jogadores[index].Propriedades[propindex].hipotecado == false)
            {
                int v = Tabuleiro.jogadores[index].Propriedades[propindex].Hipoteca;
                Tabuleiro.jogadores[index].RetirarDinheiro(v);
                Tabuleiro.jogadores[index].Propriedades[propindex].hipotecado = true;
                HipotecaButton.Text = "Desipotecar";
                HipotecaButton.Visible = true;

            } else if (Tabuleiro.jogadores[index].Dinheiro < Tabuleiro.jogadores[index].Propriedades[propindex].Hipoteca)
            {
                Mensagem m = new Mensagem("Você não tem dinheiro Suficiente.");
                m.ShowDialog();
            } else
            {
                int v = Tabuleiro.jogadores[index].Propriedades[propindex].Hipoteca;
                Tabuleiro.jogadores[index].Depositar(v);
                Tabuleiro.jogadores[index].Propriedades[propindex].hipotecado = false;
                HipotecaButton.Text = "Hipotecar";
                HipotecaButton.Visible = true;
            }
            update();
        }

        private void setupImagensTab()
        {
            switch (Tabuleiro.NrTotalJogadores)
            {
                case 2:
                    Jog1Img();
                    Jog2Img();
                    break;
                case 3:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    break;
                case 4:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    Jog4Img();
                    break;
                case 5:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    Jog4Img();
                    Jog5Img();
                    break;
                case 6:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    Jog4Img();
                    Jog5Img();
                    Jog6Img();
                    break;
                case 7:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    Jog4Img();
                    Jog5Img();
                    Jog6Img();
                    Jog7Img();
                    break;
                case 8:
                    Jog1Img();
                    Jog2Img();
                    Jog3Img();
                    Jog4Img();
                    Jog5Img();
                    Jog6Img();
                    Jog7Img();
                    Jog8Img();
                    break;

            }
        }

        private void Jog1Img()
        {
            // Imagens
            J1_00.Image = Tabuleiro.jogadores[0].Peça;
            J1_01.Image = Tabuleiro.jogadores[0].Peça;
            J1_02.Image = Tabuleiro.jogadores[0].Peça;
            J1_03.Image = Tabuleiro.jogadores[0].Peça;
            J1_04.Image = Tabuleiro.jogadores[0].Peça;
            J1_05.Image = Tabuleiro.jogadores[0].Peça;
            J1_06.Image = Tabuleiro.jogadores[0].Peça;
            J1_07.Image = Tabuleiro.jogadores[0].Peça;
            J1_08.Image = Tabuleiro.jogadores[0].Peça;
            J1_09.Image = Tabuleiro.jogadores[0].Peça;
            J1_10.Image = Tabuleiro.jogadores[0].Peça;
            J1_10P.Image = Tabuleiro.jogadores[0].Peça;
            J1_11.Image = Tabuleiro.jogadores[0].Peça;
            J1_12.Image = Tabuleiro.jogadores[0].Peça;
            J1_13.Image = Tabuleiro.jogadores[0].Peça;
            J1_14.Image = Tabuleiro.jogadores[0].Peça;
            J1_15.Image = Tabuleiro.jogadores[0].Peça;
            J1_16.Image = Tabuleiro.jogadores[0].Peça;
            J1_17.Image = Tabuleiro.jogadores[0].Peça;
            J1_18.Image = Tabuleiro.jogadores[0].Peça;
            J1_19.Image = Tabuleiro.jogadores[0].Peça;
            J1_20.Image = Tabuleiro.jogadores[0].Peça;
            J1_21.Image = Tabuleiro.jogadores[0].Peça;
            J1_22.Image = Tabuleiro.jogadores[0].Peça;
            J1_23.Image = Tabuleiro.jogadores[0].Peça;
            J1_24.Image = Tabuleiro.jogadores[0].Peça;
            J1_25.Image = Tabuleiro.jogadores[0].Peça;
            J1_26.Image = Tabuleiro.jogadores[0].Peça;
            J1_27.Image = Tabuleiro.jogadores[0].Peça;
            J1_28.Image = Tabuleiro.jogadores[0].Peça;
            J1_29.Image = Tabuleiro.jogadores[0].Peça;
            J1_31.Image = Tabuleiro.jogadores[0].Peça;
            J1_32.Image = Tabuleiro.jogadores[0].Peça;
            J1_33.Image = Tabuleiro.jogadores[0].Peça;
            J1_34.Image = Tabuleiro.jogadores[0].Peça;
            J1_35.Image = Tabuleiro.jogadores[0].Peça;
            J1_36.Image = Tabuleiro.jogadores[0].Peça;
            J1_37.Image = Tabuleiro.jogadores[0].Peça;
            J1_38.Image = Tabuleiro.jogadores[0].Peça;
            J1_39.Image = Tabuleiro.jogadores[0].Peça;
            // Cor Fundo
            J1_00.BackColor = Tabuleiro.jogadores[0].cor;
            J1_01.BackColor = Tabuleiro.jogadores[0].cor;
            J1_02.BackColor = Tabuleiro.jogadores[0].cor;
            J1_03.BackColor = Tabuleiro.jogadores[0].cor;
            J1_04.BackColor = Tabuleiro.jogadores[0].cor;
            J1_05.BackColor = Tabuleiro.jogadores[0].cor;
            J1_06.BackColor = Tabuleiro.jogadores[0].cor;
            J1_07.BackColor = Tabuleiro.jogadores[0].cor;
            J1_08.BackColor = Tabuleiro.jogadores[0].cor;
            J1_09.BackColor = Tabuleiro.jogadores[0].cor;
            J1_10.BackColor = Tabuleiro.jogadores[0].cor;
            J1_10P.BackColor = Tabuleiro.jogadores[0].cor;
            J1_11.BackColor = Tabuleiro.jogadores[0].cor;
            J1_12.BackColor = Tabuleiro.jogadores[0].cor;
            J1_13.BackColor = Tabuleiro.jogadores[0].cor;
            J1_14.BackColor = Tabuleiro.jogadores[0].cor;
            J1_15.BackColor = Tabuleiro.jogadores[0].cor;
            J1_16.BackColor = Tabuleiro.jogadores[0].cor;
            J1_17.BackColor = Tabuleiro.jogadores[0].cor;
            J1_18.BackColor = Tabuleiro.jogadores[0].cor;
            J1_19.BackColor = Tabuleiro.jogadores[0].cor;
            J1_20.BackColor = Tabuleiro.jogadores[0].cor;
            J1_21.BackColor = Tabuleiro.jogadores[0].cor;
            J1_22.BackColor = Tabuleiro.jogadores[0].cor;
            J1_23.BackColor = Tabuleiro.jogadores[0].cor;
            J1_24.BackColor = Tabuleiro.jogadores[0].cor;
            J1_25.BackColor = Tabuleiro.jogadores[0].cor;
            J1_26.BackColor = Tabuleiro.jogadores[0].cor;
            J1_27.BackColor = Tabuleiro.jogadores[0].cor;
            J1_28.BackColor = Tabuleiro.jogadores[0].cor;
            J1_29.BackColor = Tabuleiro.jogadores[0].cor;
            J1_31.BackColor = Tabuleiro.jogadores[0].cor;
            J1_32.BackColor = Tabuleiro.jogadores[0].cor;
            J1_33.BackColor = Tabuleiro.jogadores[0].cor;
            J1_34.BackColor = Tabuleiro.jogadores[0].cor;
            J1_35.BackColor = Tabuleiro.jogadores[0].cor;
            J1_36.BackColor = Tabuleiro.jogadores[0].cor;
            J1_37.BackColor = Tabuleiro.jogadores[0].cor;
            J1_38.BackColor = Tabuleiro.jogadores[0].cor;
            J1_39.BackColor = Tabuleiro.jogadores[0].cor;
        }

        private void Jog2Img()
        {
            // Imagem
            J2_00.Image = Tabuleiro.jogadores[1].Peça;
            J2_01.Image = Tabuleiro.jogadores[1].Peça;
            J2_02.Image = Tabuleiro.jogadores[1].Peça;
            J2_03.Image = Tabuleiro.jogadores[1].Peça;
            J2_04.Image = Tabuleiro.jogadores[1].Peça;
            J2_05.Image = Tabuleiro.jogadores[1].Peça;
            J2_06.Image = Tabuleiro.jogadores[1].Peça;
            J2_07.Image = Tabuleiro.jogadores[1].Peça;
            J2_08.Image = Tabuleiro.jogadores[1].Peça;
            J2_09.Image = Tabuleiro.jogadores[1].Peça;
            J2_10.Image = Tabuleiro.jogadores[1].Peça;
            J2_10P.Image = Tabuleiro.jogadores[1].Peça;
            J2_11.Image = Tabuleiro.jogadores[1].Peça;
            J2_12.Image = Tabuleiro.jogadores[1].Peça;
            J2_13.Image = Tabuleiro.jogadores[1].Peça;
            J2_14.Image = Tabuleiro.jogadores[1].Peça;
            J2_15.Image = Tabuleiro.jogadores[1].Peça;
            J2_16.Image = Tabuleiro.jogadores[1].Peça;
            J2_17.Image = Tabuleiro.jogadores[1].Peça;
            J2_18.Image = Tabuleiro.jogadores[1].Peça;
            J2_19.Image = Tabuleiro.jogadores[1].Peça;
            J2_20.Image = Tabuleiro.jogadores[1].Peça;
            J2_21.Image = Tabuleiro.jogadores[1].Peça;
            J2_22.Image = Tabuleiro.jogadores[1].Peça;
            J2_23.Image = Tabuleiro.jogadores[1].Peça;
            J2_24.Image = Tabuleiro.jogadores[1].Peça;
            J2_25.Image = Tabuleiro.jogadores[1].Peça;
            J2_26.Image = Tabuleiro.jogadores[1].Peça;
            J2_27.Image = Tabuleiro.jogadores[1].Peça;
            J2_28.Image = Tabuleiro.jogadores[1].Peça;
            J2_29.Image = Tabuleiro.jogadores[1].Peça;
            J2_31.Image = Tabuleiro.jogadores[1].Peça;
            J2_32.Image = Tabuleiro.jogadores[1].Peça;
            J2_33.Image = Tabuleiro.jogadores[1].Peça;
            J2_34.Image = Tabuleiro.jogadores[1].Peça;
            J2_35.Image = Tabuleiro.jogadores[1].Peça;
            J2_36.Image = Tabuleiro.jogadores[1].Peça;
            J2_37.Image = Tabuleiro.jogadores[1].Peça;
            J2_38.Image = Tabuleiro.jogadores[1].Peça;
            J2_39.Image = Tabuleiro.jogadores[1].Peça;
            // Cor Fundo
            J2_00.BackColor = Tabuleiro.jogadores[1].cor;
            J2_01.BackColor = Tabuleiro.jogadores[1].cor;
            J2_02.BackColor = Tabuleiro.jogadores[1].cor;
            J2_03.BackColor = Tabuleiro.jogadores[1].cor;
            J2_04.BackColor = Tabuleiro.jogadores[1].cor;
            J2_05.BackColor = Tabuleiro.jogadores[1].cor;
            J2_06.BackColor = Tabuleiro.jogadores[1].cor;
            J2_07.BackColor = Tabuleiro.jogadores[1].cor;
            J2_08.BackColor = Tabuleiro.jogadores[1].cor;
            J2_09.BackColor = Tabuleiro.jogadores[1].cor;
            J2_10.BackColor = Tabuleiro.jogadores[1].cor;
            J2_10P.BackColor = Tabuleiro.jogadores[1].cor;
            J2_11.BackColor = Tabuleiro.jogadores[1].cor;
            J2_12.BackColor = Tabuleiro.jogadores[1].cor;
            J2_13.BackColor = Tabuleiro.jogadores[1].cor;
            J2_14.BackColor = Tabuleiro.jogadores[1].cor;
            J2_15.BackColor = Tabuleiro.jogadores[1].cor;
            J2_16.BackColor = Tabuleiro.jogadores[1].cor;
            J2_17.BackColor = Tabuleiro.jogadores[1].cor;
            J2_18.BackColor = Tabuleiro.jogadores[1].cor;
            J2_19.BackColor = Tabuleiro.jogadores[1].cor;
            J2_20.BackColor = Tabuleiro.jogadores[1].cor;
            J2_21.BackColor = Tabuleiro.jogadores[1].cor;
            J2_22.BackColor = Tabuleiro.jogadores[1].cor;
            J2_23.BackColor = Tabuleiro.jogadores[1].cor;
            J2_24.BackColor = Tabuleiro.jogadores[1].cor;
            J2_25.BackColor = Tabuleiro.jogadores[1].cor;
            J2_26.BackColor = Tabuleiro.jogadores[1].cor;
            J2_27.BackColor = Tabuleiro.jogadores[1].cor;
            J2_28.BackColor = Tabuleiro.jogadores[1].cor;
            J2_29.BackColor = Tabuleiro.jogadores[1].cor;
            J2_31.BackColor = Tabuleiro.jogadores[1].cor;
            J2_32.BackColor = Tabuleiro.jogadores[1].cor;
            J2_33.BackColor = Tabuleiro.jogadores[1].cor;
            J2_34.BackColor = Tabuleiro.jogadores[1].cor;
            J2_35.BackColor = Tabuleiro.jogadores[1].cor;
            J2_36.BackColor = Tabuleiro.jogadores[1].cor;
            J2_37.BackColor = Tabuleiro.jogadores[1].cor;
            J2_38.BackColor = Tabuleiro.jogadores[1].cor;
            J2_39.BackColor = Tabuleiro.jogadores[1].cor;
        }

        private void Jog3Img()
        {
            //Imagem
            J3_00.Image = Tabuleiro.jogadores[2].Peça;
            J3_01.Image = Tabuleiro.jogadores[2].Peça;
            J3_02.Image = Tabuleiro.jogadores[2].Peça;
            J3_03.Image = Tabuleiro.jogadores[2].Peça;
            J3_04.Image = Tabuleiro.jogadores[2].Peça;
            J3_05.Image = Tabuleiro.jogadores[2].Peça;
            J3_06.Image = Tabuleiro.jogadores[2].Peça;
            J3_07.Image = Tabuleiro.jogadores[2].Peça;
            J3_08.Image = Tabuleiro.jogadores[2].Peça;
            J3_09.Image = Tabuleiro.jogadores[2].Peça;
            J3_10.Image = Tabuleiro.jogadores[2].Peça;
            J3_10P.Image = Tabuleiro.jogadores[2].Peça;
            J3_11.Image = Tabuleiro.jogadores[2].Peça;
            J3_12.Image = Tabuleiro.jogadores[2].Peça;
            J3_13.Image = Tabuleiro.jogadores[2].Peça;
            J3_14.Image = Tabuleiro.jogadores[2].Peça;
            J3_15.Image = Tabuleiro.jogadores[2].Peça;
            J3_16.Image = Tabuleiro.jogadores[2].Peça;
            J3_17.Image = Tabuleiro.jogadores[2].Peça;
            J3_18.Image = Tabuleiro.jogadores[2].Peça;
            J3_19.Image = Tabuleiro.jogadores[2].Peça;
            J3_20.Image = Tabuleiro.jogadores[2].Peça;
            J3_21.Image = Tabuleiro.jogadores[2].Peça;
            J3_22.Image = Tabuleiro.jogadores[2].Peça;
            J3_23.Image = Tabuleiro.jogadores[2].Peça;
            J3_24.Image = Tabuleiro.jogadores[2].Peça;
            J3_25.Image = Tabuleiro.jogadores[2].Peça;
            J3_26.Image = Tabuleiro.jogadores[2].Peça;
            J3_27.Image = Tabuleiro.jogadores[2].Peça;
            J3_28.Image = Tabuleiro.jogadores[2].Peça;
            J3_29.Image = Tabuleiro.jogadores[2].Peça;
            J3_31.Image = Tabuleiro.jogadores[2].Peça;
            J3_32.Image = Tabuleiro.jogadores[2].Peça;
            J3_33.Image = Tabuleiro.jogadores[2].Peça;
            J3_34.Image = Tabuleiro.jogadores[2].Peça;
            J3_35.Image = Tabuleiro.jogadores[2].Peça;
            J3_36.Image = Tabuleiro.jogadores[2].Peça;
            J3_37.Image = Tabuleiro.jogadores[2].Peça;
            J3_38.Image = Tabuleiro.jogadores[2].Peça;
            J3_39.Image = Tabuleiro.jogadores[2].Peça;
            // Cor Fundo
            J3_00.BackColor = Tabuleiro.jogadores[2].cor;
            J3_01.BackColor = Tabuleiro.jogadores[2].cor;
            J3_02.BackColor = Tabuleiro.jogadores[2].cor;
            J3_03.BackColor = Tabuleiro.jogadores[2].cor;
            J3_04.BackColor = Tabuleiro.jogadores[2].cor;
            J3_05.BackColor = Tabuleiro.jogadores[2].cor;
            J3_06.BackColor = Tabuleiro.jogadores[2].cor;
            J3_07.BackColor = Tabuleiro.jogadores[2].cor;
            J3_08.BackColor = Tabuleiro.jogadores[2].cor;
            J3_09.BackColor = Tabuleiro.jogadores[2].cor;
            J3_10.BackColor = Tabuleiro.jogadores[2].cor;
            J3_10P.BackColor = Tabuleiro.jogadores[2].cor;
            J3_11.BackColor = Tabuleiro.jogadores[2].cor;
            J3_12.BackColor = Tabuleiro.jogadores[2].cor;
            J3_13.BackColor = Tabuleiro.jogadores[2].cor;
            J3_14.BackColor = Tabuleiro.jogadores[2].cor;
            J3_15.BackColor = Tabuleiro.jogadores[2].cor;
            J3_16.BackColor = Tabuleiro.jogadores[2].cor;
            J3_17.BackColor = Tabuleiro.jogadores[2].cor;
            J3_18.BackColor = Tabuleiro.jogadores[2].cor;
            J3_19.BackColor = Tabuleiro.jogadores[2].cor;
            J3_20.BackColor = Tabuleiro.jogadores[2].cor;
            J3_21.BackColor = Tabuleiro.jogadores[2].cor;
            J3_22.BackColor = Tabuleiro.jogadores[2].cor;
            J3_23.BackColor = Tabuleiro.jogadores[2].cor;
            J3_24.BackColor = Tabuleiro.jogadores[2].cor;
            J3_25.BackColor = Tabuleiro.jogadores[2].cor;
            J3_26.BackColor = Tabuleiro.jogadores[2].cor;
            J3_27.BackColor = Tabuleiro.jogadores[2].cor;
            J3_28.BackColor = Tabuleiro.jogadores[2].cor;
            J3_29.BackColor = Tabuleiro.jogadores[2].cor;
            J3_31.BackColor = Tabuleiro.jogadores[2].cor;
            J3_32.BackColor = Tabuleiro.jogadores[2].cor;
            J3_33.BackColor = Tabuleiro.jogadores[2].cor;
            J3_34.BackColor = Tabuleiro.jogadores[2].cor;
            J3_35.BackColor = Tabuleiro.jogadores[2].cor;
            J3_36.BackColor = Tabuleiro.jogadores[2].cor;
            J3_37.BackColor = Tabuleiro.jogadores[2].cor;
            J3_38.BackColor = Tabuleiro.jogadores[2].cor;
            J3_39.BackColor = Tabuleiro.jogadores[2].cor;
        }

        private void Jog4Img()
        {
            //Imagem
            J4_00.Image = Tabuleiro.jogadores[3].Peça;
            J4_01.Image = Tabuleiro.jogadores[3].Peça;
            J4_02.Image = Tabuleiro.jogadores[3].Peça;
            J4_03.Image = Tabuleiro.jogadores[3].Peça;
            J4_04.Image = Tabuleiro.jogadores[3].Peça;
            J4_05.Image = Tabuleiro.jogadores[3].Peça;
            J4_06.Image = Tabuleiro.jogadores[3].Peça;
            J4_07.Image = Tabuleiro.jogadores[3].Peça;
            J4_08.Image = Tabuleiro.jogadores[3].Peça;
            J4_09.Image = Tabuleiro.jogadores[3].Peça;
            J4_10.Image = Tabuleiro.jogadores[3].Peça;
            J4_10P.Image = Tabuleiro.jogadores[3].Peça;
            J4_11.Image = Tabuleiro.jogadores[3].Peça;
            J4_12.Image = Tabuleiro.jogadores[3].Peça;
            J4_13.Image = Tabuleiro.jogadores[3].Peça;
            J4_14.Image = Tabuleiro.jogadores[3].Peça;
            J4_15.Image = Tabuleiro.jogadores[3].Peça;
            J4_16.Image = Tabuleiro.jogadores[3].Peça;
            J4_17.Image = Tabuleiro.jogadores[3].Peça;
            J4_18.Image = Tabuleiro.jogadores[3].Peça;
            J4_19.Image = Tabuleiro.jogadores[3].Peça;
            J4_20.Image = Tabuleiro.jogadores[3].Peça;
            J4_21.Image = Tabuleiro.jogadores[3].Peça;
            J4_22.Image = Tabuleiro.jogadores[3].Peça;
            J4_23.Image = Tabuleiro.jogadores[3].Peça;
            J4_24.Image = Tabuleiro.jogadores[3].Peça;
            J4_25.Image = Tabuleiro.jogadores[3].Peça;
            J4_26.Image = Tabuleiro.jogadores[3].Peça;
            J4_27.Image = Tabuleiro.jogadores[3].Peça;
            J4_28.Image = Tabuleiro.jogadores[3].Peça;
            J4_29.Image = Tabuleiro.jogadores[3].Peça;
            J4_31.Image = Tabuleiro.jogadores[3].Peça;
            J4_32.Image = Tabuleiro.jogadores[3].Peça;
            J4_33.Image = Tabuleiro.jogadores[3].Peça;
            J4_34.Image = Tabuleiro.jogadores[3].Peça;
            J4_35.Image = Tabuleiro.jogadores[3].Peça;
            J4_36.Image = Tabuleiro.jogadores[3].Peça;
            J4_37.Image = Tabuleiro.jogadores[3].Peça;
            J4_38.Image = Tabuleiro.jogadores[3].Peça;
            J4_39.Image = Tabuleiro.jogadores[3].Peça;
            //Cor Fundo
            J4_00.BackColor = Tabuleiro.jogadores[3].cor;
            J4_01.BackColor = Tabuleiro.jogadores[3].cor;
            J4_02.BackColor = Tabuleiro.jogadores[3].cor;
            J4_03.BackColor = Tabuleiro.jogadores[3].cor;
            J4_04.BackColor = Tabuleiro.jogadores[3].cor;
            J4_05.BackColor = Tabuleiro.jogadores[3].cor;
            J4_06.BackColor = Tabuleiro.jogadores[3].cor;
            J4_07.BackColor = Tabuleiro.jogadores[3].cor;
            J4_08.BackColor = Tabuleiro.jogadores[3].cor;
            J4_09.BackColor = Tabuleiro.jogadores[3].cor;
            J4_10.BackColor = Tabuleiro.jogadores[3].cor;
            J4_10P.BackColor = Tabuleiro.jogadores[3].cor;
            J4_11.BackColor = Tabuleiro.jogadores[3].cor;
            J4_12.BackColor = Tabuleiro.jogadores[3].cor;
            J4_13.BackColor = Tabuleiro.jogadores[3].cor;
            J4_14.BackColor = Tabuleiro.jogadores[3].cor;
            J4_15.BackColor = Tabuleiro.jogadores[3].cor;
            J4_16.BackColor = Tabuleiro.jogadores[3].cor;
            J4_17.BackColor = Tabuleiro.jogadores[3].cor;
            J4_18.BackColor = Tabuleiro.jogadores[3].cor;
            J4_19.BackColor = Tabuleiro.jogadores[3].cor;
            J4_20.BackColor = Tabuleiro.jogadores[3].cor;
            J4_21.BackColor = Tabuleiro.jogadores[3].cor;
            J4_22.BackColor = Tabuleiro.jogadores[3].cor;
            J4_23.BackColor = Tabuleiro.jogadores[3].cor;
            J4_24.BackColor = Tabuleiro.jogadores[3].cor;
            J4_25.BackColor = Tabuleiro.jogadores[3].cor;
            J4_26.BackColor = Tabuleiro.jogadores[3].cor;
            J4_27.BackColor = Tabuleiro.jogadores[3].cor;
            J4_28.BackColor = Tabuleiro.jogadores[3].cor;
            J4_29.BackColor = Tabuleiro.jogadores[3].cor;
            J4_31.BackColor = Tabuleiro.jogadores[3].cor;
            J4_32.BackColor = Tabuleiro.jogadores[3].cor;
            J4_33.BackColor = Tabuleiro.jogadores[3].cor;
            J4_34.BackColor = Tabuleiro.jogadores[3].cor;
            J4_35.BackColor = Tabuleiro.jogadores[3].cor;
            J4_36.BackColor = Tabuleiro.jogadores[3].cor;
            J4_37.BackColor = Tabuleiro.jogadores[3].cor;
            J4_38.BackColor = Tabuleiro.jogadores[3].cor;
            J4_39.BackColor = Tabuleiro.jogadores[3].cor;
        }

        private void Jog5Img()
        {
            // Imagem
            J5_00.Image = Tabuleiro.jogadores[4].Peça;
            J5_01.Image = Tabuleiro.jogadores[4].Peça;
            J5_02.Image = Tabuleiro.jogadores[4].Peça;
            J5_03.Image = Tabuleiro.jogadores[4].Peça;
            J5_04.Image = Tabuleiro.jogadores[4].Peça;
            J5_05.Image = Tabuleiro.jogadores[4].Peça;
            J5_06.Image = Tabuleiro.jogadores[4].Peça;
            J5_07.Image = Tabuleiro.jogadores[4].Peça;
            J5_08.Image = Tabuleiro.jogadores[4].Peça;
            J5_09.Image = Tabuleiro.jogadores[4].Peça;
            J5_10.Image = Tabuleiro.jogadores[4].Peça;
            J5_10P.Image = Tabuleiro.jogadores[4].Peça;
            J5_11.Image = Tabuleiro.jogadores[4].Peça;
            J5_12.Image = Tabuleiro.jogadores[4].Peça;
            J5_13.Image = Tabuleiro.jogadores[4].Peça;
            J5_14.Image = Tabuleiro.jogadores[4].Peça;
            J5_15.Image = Tabuleiro.jogadores[4].Peça;
            J5_16.Image = Tabuleiro.jogadores[4].Peça;
            J5_17.Image = Tabuleiro.jogadores[4].Peça;
            J5_18.Image = Tabuleiro.jogadores[4].Peça;
            J5_19.Image = Tabuleiro.jogadores[4].Peça;
            J5_20.Image = Tabuleiro.jogadores[4].Peça;
            J5_21.Image = Tabuleiro.jogadores[4].Peça;
            J5_22.Image = Tabuleiro.jogadores[4].Peça;
            J5_23.Image = Tabuleiro.jogadores[4].Peça;
            J5_24.Image = Tabuleiro.jogadores[4].Peça;
            J5_25.Image = Tabuleiro.jogadores[4].Peça;
            J5_26.Image = Tabuleiro.jogadores[4].Peça;
            J5_27.Image = Tabuleiro.jogadores[4].Peça;
            J5_28.Image = Tabuleiro.jogadores[4].Peça;
            J5_29.Image = Tabuleiro.jogadores[4].Peça;
            J5_31.Image = Tabuleiro.jogadores[4].Peça;
            J5_32.Image = Tabuleiro.jogadores[4].Peça;
            J5_33.Image = Tabuleiro.jogadores[4].Peça;
            J5_34.Image = Tabuleiro.jogadores[4].Peça;
            J5_35.Image = Tabuleiro.jogadores[4].Peça;
            J5_36.Image = Tabuleiro.jogadores[4].Peça;
            J5_37.Image = Tabuleiro.jogadores[4].Peça;
            J5_38.Image = Tabuleiro.jogadores[4].Peça;
            J5_39.Image = Tabuleiro.jogadores[4].Peça;
            //Cor Fundo
            J5_00.BackColor = Tabuleiro.jogadores[4].cor;
            J5_01.BackColor = Tabuleiro.jogadores[4].cor;
            J5_02.BackColor = Tabuleiro.jogadores[4].cor;
            J5_03.BackColor = Tabuleiro.jogadores[4].cor;
            J5_04.BackColor = Tabuleiro.jogadores[4].cor;
            J5_05.BackColor = Tabuleiro.jogadores[4].cor;
            J5_06.BackColor = Tabuleiro.jogadores[4].cor;
            J5_07.BackColor = Tabuleiro.jogadores[4].cor;
            J5_08.BackColor = Tabuleiro.jogadores[4].cor;
            J5_09.BackColor = Tabuleiro.jogadores[4].cor;
            J5_10.BackColor = Tabuleiro.jogadores[4].cor;
            J5_10P.BackColor = Tabuleiro.jogadores[4].cor;
            J5_11.BackColor = Tabuleiro.jogadores[4].cor;
            J5_12.BackColor = Tabuleiro.jogadores[4].cor;
            J5_13.BackColor = Tabuleiro.jogadores[4].cor;
            J5_14.BackColor = Tabuleiro.jogadores[4].cor;
            J5_15.BackColor = Tabuleiro.jogadores[4].cor;
            J5_16.BackColor = Tabuleiro.jogadores[4].cor;
            J5_17.BackColor = Tabuleiro.jogadores[4].cor;
            J5_18.BackColor = Tabuleiro.jogadores[4].cor;
            J5_19.BackColor = Tabuleiro.jogadores[4].cor;
            J5_20.BackColor = Tabuleiro.jogadores[4].cor;
            J5_21.BackColor = Tabuleiro.jogadores[4].cor;
            J5_22.BackColor = Tabuleiro.jogadores[4].cor;
            J5_23.BackColor = Tabuleiro.jogadores[4].cor;
            J5_24.BackColor = Tabuleiro.jogadores[4].cor;
            J5_25.BackColor = Tabuleiro.jogadores[4].cor;
            J5_26.BackColor = Tabuleiro.jogadores[4].cor;
            J5_27.BackColor = Tabuleiro.jogadores[4].cor;
            J5_28.BackColor = Tabuleiro.jogadores[4].cor;
            J5_29.BackColor = Tabuleiro.jogadores[4].cor;
            J5_31.BackColor = Tabuleiro.jogadores[4].cor;
            J5_32.BackColor = Tabuleiro.jogadores[4].cor;
            J5_33.BackColor = Tabuleiro.jogadores[4].cor;
            J5_34.BackColor = Tabuleiro.jogadores[4].cor;
            J5_35.BackColor = Tabuleiro.jogadores[4].cor;
            J5_36.BackColor = Tabuleiro.jogadores[4].cor;
            J5_37.BackColor = Tabuleiro.jogadores[4].cor;
            J5_38.BackColor = Tabuleiro.jogadores[4].cor;
            J5_39.BackColor = Tabuleiro.jogadores[4].cor;
        }

        private void Jog6Img()
        {
            //Imagem
            J6_00.Image = Tabuleiro.jogadores[5].Peça;
            J6_01.Image = Tabuleiro.jogadores[5].Peça;
            J6_02.Image = Tabuleiro.jogadores[5].Peça;
            J6_03.Image = Tabuleiro.jogadores[5].Peça;
            J6_04.Image = Tabuleiro.jogadores[5].Peça;
            J6_05.Image = Tabuleiro.jogadores[5].Peça;
            J6_06.Image = Tabuleiro.jogadores[5].Peça;
            J6_07.Image = Tabuleiro.jogadores[5].Peça;
            J6_08.Image = Tabuleiro.jogadores[5].Peça;
            J6_09.Image = Tabuleiro.jogadores[5].Peça;
            J6_10.Image = Tabuleiro.jogadores[5].Peça;
            J6_10P.Image = Tabuleiro.jogadores[5].Peça;
            J6_11.Image = Tabuleiro.jogadores[5].Peça;
            J6_12.Image = Tabuleiro.jogadores[5].Peça;
            J6_13.Image = Tabuleiro.jogadores[5].Peça;
            J6_14.Image = Tabuleiro.jogadores[5].Peça;
            J6_15.Image = Tabuleiro.jogadores[5].Peça;
            J6_16.Image = Tabuleiro.jogadores[5].Peça;
            J6_17.Image = Tabuleiro.jogadores[5].Peça;
            J6_18.Image = Tabuleiro.jogadores[5].Peça;
            J6_19.Image = Tabuleiro.jogadores[5].Peça;
            J6_20.Image = Tabuleiro.jogadores[5].Peça;
            J6_21.Image = Tabuleiro.jogadores[5].Peça;
            J6_22.Image = Tabuleiro.jogadores[5].Peça;
            J6_23.Image = Tabuleiro.jogadores[5].Peça;
            J6_24.Image = Tabuleiro.jogadores[5].Peça;
            J6_25.Image = Tabuleiro.jogadores[5].Peça;
            J6_26.Image = Tabuleiro.jogadores[5].Peça;
            J6_27.Image = Tabuleiro.jogadores[5].Peça;
            J6_28.Image = Tabuleiro.jogadores[5].Peça;
            J6_29.Image = Tabuleiro.jogadores[5].Peça;
            J6_31.Image = Tabuleiro.jogadores[5].Peça;
            J6_32.Image = Tabuleiro.jogadores[5].Peça;
            J6_33.Image = Tabuleiro.jogadores[5].Peça;
            J6_34.Image = Tabuleiro.jogadores[5].Peça;
            J6_35.Image = Tabuleiro.jogadores[5].Peça;
            J6_36.Image = Tabuleiro.jogadores[5].Peça;
            J6_37.Image = Tabuleiro.jogadores[5].Peça;
            J6_38.Image = Tabuleiro.jogadores[5].Peça;
            J6_39.Image = Tabuleiro.jogadores[5].Peça;
            //Cor Fundo
            J6_00.BackColor = Tabuleiro.jogadores[5].cor;
            J6_01.BackColor = Tabuleiro.jogadores[5].cor;
            J6_02.BackColor = Tabuleiro.jogadores[5].cor;
            J6_03.BackColor = Tabuleiro.jogadores[5].cor;
            J6_04.BackColor = Tabuleiro.jogadores[5].cor;
            J6_05.BackColor = Tabuleiro.jogadores[5].cor;
            J6_06.BackColor = Tabuleiro.jogadores[5].cor;
            J6_07.BackColor = Tabuleiro.jogadores[5].cor;
            J6_08.BackColor = Tabuleiro.jogadores[5].cor;
            J6_09.BackColor = Tabuleiro.jogadores[5].cor;
            J6_10.BackColor = Tabuleiro.jogadores[5].cor;
            J6_10P.BackColor = Tabuleiro.jogadores[5].cor;
            J6_11.BackColor = Tabuleiro.jogadores[5].cor;
            J6_12.BackColor = Tabuleiro.jogadores[5].cor;
            J6_13.BackColor = Tabuleiro.jogadores[5].cor;
            J6_14.BackColor = Tabuleiro.jogadores[5].cor;
            J6_15.BackColor = Tabuleiro.jogadores[5].cor;
            J6_16.BackColor = Tabuleiro.jogadores[5].cor;
            J6_17.BackColor = Tabuleiro.jogadores[5].cor;
            J6_18.BackColor = Tabuleiro.jogadores[5].cor;
            J6_19.BackColor = Tabuleiro.jogadores[5].cor;
            J6_20.BackColor = Tabuleiro.jogadores[5].cor;
            J6_21.BackColor = Tabuleiro.jogadores[5].cor;
            J6_22.BackColor = Tabuleiro.jogadores[5].cor;
            J6_23.BackColor = Tabuleiro.jogadores[5].cor;
            J6_24.BackColor = Tabuleiro.jogadores[5].cor;
            J6_25.BackColor = Tabuleiro.jogadores[5].cor;
            J6_26.BackColor = Tabuleiro.jogadores[5].cor;
            J6_27.BackColor = Tabuleiro.jogadores[5].cor;
            J6_28.BackColor = Tabuleiro.jogadores[5].cor;
            J6_29.BackColor = Tabuleiro.jogadores[5].cor;
            J6_31.BackColor = Tabuleiro.jogadores[5].cor;
            J6_32.BackColor = Tabuleiro.jogadores[5].cor;
            J6_33.BackColor = Tabuleiro.jogadores[5].cor;
            J6_34.BackColor = Tabuleiro.jogadores[5].cor;
            J6_35.BackColor = Tabuleiro.jogadores[5].cor;
            J6_36.BackColor = Tabuleiro.jogadores[5].cor;
            J6_37.BackColor = Tabuleiro.jogadores[5].cor;
            J6_38.BackColor = Tabuleiro.jogadores[5].cor;
            J6_39.BackColor = Tabuleiro.jogadores[5].cor;
        }

        private void Jog7Img()
        {
            // Imagem
            J7_00.Image = Tabuleiro.jogadores[6].Peça;
            J7_01.Image = Tabuleiro.jogadores[6].Peça;
            J7_02.Image = Tabuleiro.jogadores[6].Peça;
            J7_03.Image = Tabuleiro.jogadores[6].Peça;
            J7_04.Image = Tabuleiro.jogadores[6].Peça;
            J7_05.Image = Tabuleiro.jogadores[6].Peça;
            J7_06.Image = Tabuleiro.jogadores[6].Peça;
            J7_07.Image = Tabuleiro.jogadores[6].Peça;
            J7_08.Image = Tabuleiro.jogadores[6].Peça;
            J7_09.Image = Tabuleiro.jogadores[6].Peça;
            J7_10.Image = Tabuleiro.jogadores[6].Peça;
            J7_10P.Image = Tabuleiro.jogadores[6].Peça;
            J7_11.Image = Tabuleiro.jogadores[6].Peça;
            J7_12.Image = Tabuleiro.jogadores[6].Peça;
            J7_13.Image = Tabuleiro.jogadores[6].Peça;
            J7_14.Image = Tabuleiro.jogadores[6].Peça;
            J7_15.Image = Tabuleiro.jogadores[6].Peça;
            J7_16.Image = Tabuleiro.jogadores[6].Peça;
            J7_17.Image = Tabuleiro.jogadores[6].Peça;
            J7_18.Image = Tabuleiro.jogadores[6].Peça;
            J7_19.Image = Tabuleiro.jogadores[6].Peça;
            J7_20.Image = Tabuleiro.jogadores[6].Peça;
            J7_21.Image = Tabuleiro.jogadores[6].Peça;
            J7_22.Image = Tabuleiro.jogadores[6].Peça;
            J7_23.Image = Tabuleiro.jogadores[6].Peça;
            J7_24.Image = Tabuleiro.jogadores[6].Peça;
            J7_25.Image = Tabuleiro.jogadores[6].Peça;
            J7_26.Image = Tabuleiro.jogadores[6].Peça;
            J7_27.Image = Tabuleiro.jogadores[6].Peça;
            J7_28.Image = Tabuleiro.jogadores[6].Peça;
            J7_29.Image = Tabuleiro.jogadores[6].Peça;
            J7_31.Image = Tabuleiro.jogadores[6].Peça;
            J7_32.Image = Tabuleiro.jogadores[6].Peça;
            J7_33.Image = Tabuleiro.jogadores[6].Peça;
            J7_34.Image = Tabuleiro.jogadores[6].Peça;
            J7_35.Image = Tabuleiro.jogadores[6].Peça;
            J7_36.Image = Tabuleiro.jogadores[6].Peça;
            J7_37.Image = Tabuleiro.jogadores[6].Peça;
            J7_38.Image = Tabuleiro.jogadores[6].Peça;
            J7_39.Image = Tabuleiro.jogadores[6].Peça;
            // Cor Fundo
            J7_00.BackColor = Tabuleiro.jogadores[6].cor;
            J7_01.BackColor = Tabuleiro.jogadores[6].cor;
            J7_02.BackColor = Tabuleiro.jogadores[6].cor;
            J7_03.BackColor = Tabuleiro.jogadores[6].cor;
            J7_04.BackColor = Tabuleiro.jogadores[6].cor;
            J7_05.BackColor = Tabuleiro.jogadores[6].cor;
            J7_06.BackColor = Tabuleiro.jogadores[6].cor;
            J7_07.BackColor = Tabuleiro.jogadores[6].cor;
            J7_08.BackColor = Tabuleiro.jogadores[6].cor;
            J7_09.BackColor = Tabuleiro.jogadores[6].cor;
            J7_10.BackColor = Tabuleiro.jogadores[6].cor;
            J7_10P.BackColor = Tabuleiro.jogadores[6].cor;
            J7_11.BackColor = Tabuleiro.jogadores[6].cor;
            J7_12.BackColor = Tabuleiro.jogadores[6].cor;
            J7_13.BackColor = Tabuleiro.jogadores[6].cor;
            J7_14.BackColor = Tabuleiro.jogadores[6].cor;
            J7_15.BackColor = Tabuleiro.jogadores[6].cor;
            J7_16.BackColor = Tabuleiro.jogadores[6].cor;
            J7_17.BackColor = Tabuleiro.jogadores[6].cor;
            J7_18.BackColor = Tabuleiro.jogadores[6].cor;
            J7_19.BackColor = Tabuleiro.jogadores[6].cor;
            J7_20.BackColor = Tabuleiro.jogadores[6].cor;
            J7_21.BackColor = Tabuleiro.jogadores[6].cor;
            J7_22.BackColor = Tabuleiro.jogadores[6].cor;
            J7_23.BackColor = Tabuleiro.jogadores[6].cor;
            J7_24.BackColor = Tabuleiro.jogadores[6].cor;
            J7_25.BackColor = Tabuleiro.jogadores[6].cor;
            J7_26.BackColor = Tabuleiro.jogadores[6].cor;
            J7_27.BackColor = Tabuleiro.jogadores[6].cor;
            J7_28.BackColor = Tabuleiro.jogadores[6].cor;
            J7_29.BackColor = Tabuleiro.jogadores[6].cor;
            J7_31.BackColor = Tabuleiro.jogadores[6].cor;
            J7_32.BackColor = Tabuleiro.jogadores[6].cor;
            J7_33.BackColor = Tabuleiro.jogadores[6].cor;
            J7_34.BackColor = Tabuleiro.jogadores[6].cor;
            J7_35.BackColor = Tabuleiro.jogadores[6].cor;
            J7_36.BackColor = Tabuleiro.jogadores[6].cor;
            J7_37.BackColor = Tabuleiro.jogadores[6].cor;
            J7_38.BackColor = Tabuleiro.jogadores[6].cor;
            J7_39.BackColor = Tabuleiro.jogadores[6].cor;
        }

        private void Jog8Img()
        {
            //Imagem
            J8_00.Image = Tabuleiro.jogadores[7].Peça;
            J8_01.Image = Tabuleiro.jogadores[7].Peça;
            J8_02.Image = Tabuleiro.jogadores[7].Peça;
            J8_03.Image = Tabuleiro.jogadores[7].Peça;
            J8_04.Image = Tabuleiro.jogadores[7].Peça;
            J8_05.Image = Tabuleiro.jogadores[7].Peça;
            J8_06.Image = Tabuleiro.jogadores[7].Peça;
            J8_07.Image = Tabuleiro.jogadores[7].Peça;
            J8_08.Image = Tabuleiro.jogadores[7].Peça;
            J8_09.Image = Tabuleiro.jogadores[7].Peça;
            J8_10.Image = Tabuleiro.jogadores[7].Peça;
            J8_10P.Image = Tabuleiro.jogadores[7].Peça;
            J8_11.Image = Tabuleiro.jogadores[7].Peça;
            J8_12.Image = Tabuleiro.jogadores[7].Peça;
            J8_13.Image = Tabuleiro.jogadores[7].Peça;
            J8_14.Image = Tabuleiro.jogadores[7].Peça;
            J8_15.Image = Tabuleiro.jogadores[7].Peça;
            J8_16.Image = Tabuleiro.jogadores[7].Peça;
            J8_17.Image = Tabuleiro.jogadores[7].Peça;
            J8_18.Image = Tabuleiro.jogadores[7].Peça;
            J8_19.Image = Tabuleiro.jogadores[7].Peça;
            J8_20.Image = Tabuleiro.jogadores[7].Peça;
            J8_21.Image = Tabuleiro.jogadores[7].Peça;
            J8_22.Image = Tabuleiro.jogadores[7].Peça;
            J8_23.Image = Tabuleiro.jogadores[7].Peça;
            J8_24.Image = Tabuleiro.jogadores[7].Peça;
            J8_25.Image = Tabuleiro.jogadores[7].Peça;
            J8_26.Image = Tabuleiro.jogadores[7].Peça;
            J8_27.Image = Tabuleiro.jogadores[7].Peça;
            J8_28.Image = Tabuleiro.jogadores[7].Peça;
            J8_29.Image = Tabuleiro.jogadores[7].Peça;
            J8_31.Image = Tabuleiro.jogadores[7].Peça;
            J8_32.Image = Tabuleiro.jogadores[7].Peça;
            J8_33.Image = Tabuleiro.jogadores[7].Peça;
            J8_34.Image = Tabuleiro.jogadores[7].Peça;
            J8_35.Image = Tabuleiro.jogadores[7].Peça;
            J8_36.Image = Tabuleiro.jogadores[7].Peça;
            J8_37.Image = Tabuleiro.jogadores[7].Peça;
            J8_38.Image = Tabuleiro.jogadores[7].Peça;
            J8_39.Image = Tabuleiro.jogadores[7].Peça;
            // Cor Fundo
            J8_00.BackColor = Tabuleiro.jogadores[7].cor;
            J8_01.BackColor = Tabuleiro.jogadores[7].cor;
            J8_02.BackColor = Tabuleiro.jogadores[7].cor;
            J8_03.BackColor = Tabuleiro.jogadores[7].cor;
            J8_04.BackColor = Tabuleiro.jogadores[7].cor;
            J8_05.BackColor = Tabuleiro.jogadores[7].cor;
            J8_06.BackColor = Tabuleiro.jogadores[7].cor;
            J8_07.BackColor = Tabuleiro.jogadores[7].cor;
            J8_08.BackColor = Tabuleiro.jogadores[7].cor;
            J8_09.BackColor = Tabuleiro.jogadores[7].cor;
            J8_10.BackColor = Tabuleiro.jogadores[7].cor;
            J8_10P.BackColor = Tabuleiro.jogadores[7].cor;
            J8_11.BackColor = Tabuleiro.jogadores[7].cor;
            J8_12.BackColor = Tabuleiro.jogadores[7].cor;
            J8_13.BackColor = Tabuleiro.jogadores[7].cor;
            J8_14.BackColor = Tabuleiro.jogadores[7].cor;
            J8_15.BackColor = Tabuleiro.jogadores[7].cor;
            J8_16.BackColor = Tabuleiro.jogadores[7].cor;
            J8_17.BackColor = Tabuleiro.jogadores[7].cor;
            J8_18.BackColor = Tabuleiro.jogadores[7].cor;
            J8_19.BackColor = Tabuleiro.jogadores[7].cor;
            J8_20.BackColor = Tabuleiro.jogadores[7].cor;
            J8_21.BackColor = Tabuleiro.jogadores[7].cor;
            J8_22.BackColor = Tabuleiro.jogadores[7].cor;
            J8_23.BackColor = Tabuleiro.jogadores[7].cor;
            J8_24.BackColor = Tabuleiro.jogadores[7].cor;
            J8_25.BackColor = Tabuleiro.jogadores[7].cor;
            J8_26.BackColor = Tabuleiro.jogadores[7].cor;
            J8_27.BackColor = Tabuleiro.jogadores[7].cor;
            J8_28.BackColor = Tabuleiro.jogadores[7].cor;
            J8_29.BackColor = Tabuleiro.jogadores[7].cor;
            J8_31.BackColor = Tabuleiro.jogadores[7].cor;
            J8_32.BackColor = Tabuleiro.jogadores[7].cor;
            J8_33.BackColor = Tabuleiro.jogadores[7].cor;
            J8_34.BackColor = Tabuleiro.jogadores[7].cor;
            J8_35.BackColor = Tabuleiro.jogadores[7].cor;
            J8_36.BackColor = Tabuleiro.jogadores[7].cor;
            J8_37.BackColor = Tabuleiro.jogadores[7].cor;
            J8_38.BackColor = Tabuleiro.jogadores[7].cor;
            J8_39.BackColor = Tabuleiro.jogadores[7].cor;
        }

        /*
        private void setImagem(String peça, ComboBox comb)
        {
            if (comb.SelectedItem == null) return;
            Bitmap MyImage;
            string caminho = @"..\..\Resources\";
            string nomeFile = "EXAMPLE.PNG";
            int Item = comb.SelectedIndex;

            switch (Item)
            {
                case 0:
                    nomeFile = "player1image.image.png";
                    break;
                case 1:
                    nomeFile = "player2image.image.png";
                    break;
                case 2:
                    nomeFile = "player3image.image.png";
                    break;
                case 3:
                    nomeFile = "player4image.image.png";
                    break;
                case 4:
                    nomeFile = "player5image.image.png";
                    break;
                case 5:
                    nomeFile = "Player6Image.jpg";
                    break;
                case 6:
                    nomeFile = "Player7Image.png";
                    break;
                case 7:
                    nomeFile = "Player8Image.png";
                    break;
                default:
                    break;
            }

            MyImage = new Bitmap(caminho + nomeFile);
            pic.Image = (Image)MyImage;
        }*/

        private void button3_Click(object sender, EventArgs e)
        {
            Trocar t = new Trocar(Tabuleiro.AtualJogadorIndex);

            t.Show();

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void NrCasas_Click(object sender, EventArgs e)
        {

        }

        private void casa1_Click(object sender, EventArgs e)
        {

        }

        private void prop39_Click(object sender, EventArgs e)
        {

        }

        private void prop1_Click(object sender, EventArgs e)
        {

        }

        private void updatePosJog()
        {
            for (int i=0; i < Tabuleiro.NrTotalJogadores; i++)
            {
                switch(i)
                {
                    case 0:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J1_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J1_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J1_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J1_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J1_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J1_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J1_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J1_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J1_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J1_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J1_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J1_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J1_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J1_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J1_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J1_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J1_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J1_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J1_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J1_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J1_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J1_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J1_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J1_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J1_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J1_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J1_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J1_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J1_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J1_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J1_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J1_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J1_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J1_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J1_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J1_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J1_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J1_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J1_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J1_39.Visible = true;
                        break;
                    case 1:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J2_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J2_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J2_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J2_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J2_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J2_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J2_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J2_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J2_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J2_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J2_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J2_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J2_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J2_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J2_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J2_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J2_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J2_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J2_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J2_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J2_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J2_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J2_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J2_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J2_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J2_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J2_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J2_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J2_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J2_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J2_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J2_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J2_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J2_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J2_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J2_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J2_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J2_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J2_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J2_39.Visible = true;
                        break;
                    case 2:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J3_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J3_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J3_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J3_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J3_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J3_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J3_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J3_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J3_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J3_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J3_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J3_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J3_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J3_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J3_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J3_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J3_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J3_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J3_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J3_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J3_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J3_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J3_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J3_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J3_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J3_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J3_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J3_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J3_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J3_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J3_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J3_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J3_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J3_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J3_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J3_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J3_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J3_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J3_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J3_39.Visible = true;
                        break;
                    case 3:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J4_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J4_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J4_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J4_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J4_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J4_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J4_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J4_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J4_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J4_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J4_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J4_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J4_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J4_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J4_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J4_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J4_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J4_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J4_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J4_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J4_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J4_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J4_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J4_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J4_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J4_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J4_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J4_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J4_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J4_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J4_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J4_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J4_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J4_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J4_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J4_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J4_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J4_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J4_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J4_39.Visible = true;
                        break;
                    case 4:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J5_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J5_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J5_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J5_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J5_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J5_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J5_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J5_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J5_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J5_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J5_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J5_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J5_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J5_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J5_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J5_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J5_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J5_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J5_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J5_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J5_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J5_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J5_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J5_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J5_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J5_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J5_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J5_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J5_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J5_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J5_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J5_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J5_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J5_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J5_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J5_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J5_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J5_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J5_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J5_39.Visible = true;
                        break;
                    case 5:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J6_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J6_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J6_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J6_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J6_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J6_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J6_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J6_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J6_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J6_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J6_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J6_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J6_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J6_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J6_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J6_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J6_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J6_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J6_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J6_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J6_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J6_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J6_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J6_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J6_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J6_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J6_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J6_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J6_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J6_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J6_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J6_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J6_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J6_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J6_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J6_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J6_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J6_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J6_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J6_39.Visible = true;
                        break;
                    case 6:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J7_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J7_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J7_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J7_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J7_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J7_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J7_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J7_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J7_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J7_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J7_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J7_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J7_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J7_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J7_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J7_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J7_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J7_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J7_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J7_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J7_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J7_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J7_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J7_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J7_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J7_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J7_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J7_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J7_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J7_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J7_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J7_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J7_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J7_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J7_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J7_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J7_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J7_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J7_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J7_39.Visible = true;
                        break;
                    case 7:
                        if (Tabuleiro.jogadores[i].posicaoAtual == 0) J8_00.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 1) J8_01.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 2) J8_02.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 3) J8_03.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 4) J8_04.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 5) J8_05.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 6) J8_06.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 7) J8_07.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 8) J8_08.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 9) J8_09.Visible = true;
                        if (Tabuleiro.jogadores[i].estaPreso == true) J8_10P.Visible = true;
                        else if (Tabuleiro.jogadores[i].posicaoAtual == 10) J8_10.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 11) J8_11.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 12) J8_12.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 13) J8_13.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 14) J8_14.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 15) J8_15.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 16) J8_16.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 17) J8_17.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 18) J8_18.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 19) J8_19.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 20) J8_20.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 21) J8_21.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 22) J8_22.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 23) J8_23.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 24) J8_24.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 25) J8_25.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 26) J8_26.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 27) J8_27.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 28) J8_28.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 29) J8_29.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 31) J8_31.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 32) J8_32.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 33) J8_33.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 34) J8_34.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 35) J8_35.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 36) J8_36.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 37) J8_37.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 38) J8_38.Visible = true;
                        if (Tabuleiro.jogadores[i].posicaoAtual == 39) J8_39.Visible = true;
                        break;
                }
            }
        }

        private void InvisivelJogad()
        {
            //Jogador1
            J1_00.Visible = false; J1_01.Visible = false; J1_02.Visible = false; J1_03.Visible = false; J1_04.Visible = false;
            J1_05.Visible = false; J1_06.Visible = false; J1_07.Visible = false; J1_08.Visible = false; J1_09.Visible = false;
            J1_10.Visible = false; J1_10P.Visible = false; J1_11.Visible = false; J1_12.Visible = false; J1_13.Visible = false;
            J1_14.Visible = false; J1_15.Visible = false; J1_16.Visible = false; J1_17.Visible = false; J1_18.Visible = false;
            J1_19.Visible = false; J1_20.Visible = false; J1_21.Visible = false; J1_22.Visible = false; J1_23.Visible = false;
            J1_24.Visible = false; J1_25.Visible = false; J1_26.Visible = false; J1_27.Visible = false; J1_28.Visible = false;
            J1_29.Visible = false; J1_31.Visible = false; J1_32.Visible = false; J1_33.Visible = false; J1_34.Visible = false;
            J1_35.Visible = false; J1_36.Visible = false; J1_37.Visible = false; J1_38.Visible = false; J1_39.Visible = false;
            //Jogador2
            J2_00.Visible = false; J2_01.Visible = false; J2_02.Visible = false; J2_03.Visible = false; J2_04.Visible = false;
            J2_05.Visible = false; J2_06.Visible = false; J2_07.Visible = false; J2_08.Visible = false; J2_09.Visible = false;
            J2_10.Visible = false; J2_10P.Visible = false; J2_11.Visible = false; J2_12.Visible = false; J2_13.Visible = false;
            J2_14.Visible = false; J2_15.Visible = false; J2_16.Visible = false; J2_17.Visible = false; J2_18.Visible = false;
            J2_19.Visible = false; J2_20.Visible = false; J2_21.Visible = false; J2_22.Visible = false; J2_23.Visible = false;
            J2_24.Visible = false; J2_25.Visible = false; J2_26.Visible = false; J2_27.Visible = false; J2_28.Visible = false;
            J2_29.Visible = false; J2_31.Visible = false; J2_32.Visible = false; J2_33.Visible = false; J2_34.Visible = false;
            J2_35.Visible = false; J2_36.Visible = false; J2_37.Visible = false; J2_38.Visible = false; J2_39.Visible = false;
            //Jogador3
            J3_00.Visible = false; J3_01.Visible = false; J3_02.Visible = false; J3_03.Visible = false; J3_04.Visible = false;
            J3_05.Visible = false; J3_06.Visible = false; J3_07.Visible = false; J3_08.Visible = false; J3_09.Visible = false;
            J3_10.Visible = false; J3_10P.Visible = false; J3_11.Visible = false; J3_12.Visible = false; J3_13.Visible = false;
            J3_14.Visible = false; J3_15.Visible = false; J3_16.Visible = false; J3_17.Visible = false; J3_18.Visible = false;
            J3_19.Visible = false; J3_20.Visible = false; J3_21.Visible = false; J3_22.Visible = false; J3_23.Visible = false;
            J3_24.Visible = false; J3_25.Visible = false; J3_26.Visible = false; J3_27.Visible = false; J3_28.Visible = false;
            J3_29.Visible = false; J3_31.Visible = false; J3_32.Visible = false; J3_33.Visible = false; J3_34.Visible = false;
            J3_35.Visible = false; J3_36.Visible = false; J3_37.Visible = false; J3_38.Visible = false; J3_39.Visible = false;
            //Jogador4
            J4_00.Visible = false; J4_01.Visible = false; J4_02.Visible = false; J4_03.Visible = false; J4_04.Visible = false;
            J4_05.Visible = false; J4_06.Visible = false; J4_07.Visible = false; J4_08.Visible = false; J4_09.Visible = false;
            J4_10.Visible = false; J4_10P.Visible = false; J4_11.Visible = false; J4_12.Visible = false; J4_13.Visible = false;
            J4_14.Visible = false; J4_15.Visible = false; J4_16.Visible = false; J4_17.Visible = false; J4_18.Visible = false;
            J4_19.Visible = false; J4_20.Visible = false; J4_21.Visible = false; J4_22.Visible = false; J4_23.Visible = false;
            J4_24.Visible = false; J4_25.Visible = false; J4_26.Visible = false; J4_27.Visible = false; J4_28.Visible = false;
            J4_29.Visible = false; J4_31.Visible = false; J4_32.Visible = false; J4_33.Visible = false; J4_34.Visible = false;
            J4_35.Visible = false; J4_36.Visible = false; J4_37.Visible = false; J4_38.Visible = false; J4_39.Visible = false;
            //Jogador5
            J5_00.Visible = false; J5_01.Visible = false; J5_02.Visible = false; J5_03.Visible = false; J5_04.Visible = false;
            J5_05.Visible = false; J5_06.Visible = false; J5_07.Visible = false; J5_08.Visible = false; J5_09.Visible = false;
            J5_10.Visible = false; J5_10P.Visible = false; J5_11.Visible = false; J5_12.Visible = false; J5_13.Visible = false;
            J5_14.Visible = false; J5_15.Visible = false; J5_16.Visible = false; J5_17.Visible = false; J5_18.Visible = false;
            J5_19.Visible = false; J5_20.Visible = false; J5_21.Visible = false; J5_22.Visible = false; J5_23.Visible = false;
            J5_24.Visible = false; J5_25.Visible = false; J5_26.Visible = false; J5_27.Visible = false; J5_28.Visible = false;
            J5_29.Visible = false; J5_31.Visible = false; J5_32.Visible = false; J5_33.Visible = false; J5_34.Visible = false;
            J5_35.Visible = false; J5_36.Visible = false; J5_37.Visible = false; J5_38.Visible = false; J5_39.Visible = false;
            //Jogador6
            J6_00.Visible = false; J6_01.Visible = false; J6_02.Visible = false; J6_03.Visible = false; J6_04.Visible = false;
            J6_05.Visible = false; J6_06.Visible = false; J6_07.Visible = false; J6_08.Visible = false; J6_09.Visible = false;
            J6_10.Visible = false; J6_10P.Visible = false; J6_11.Visible = false; J6_12.Visible = false; J6_13.Visible = false;
            J6_14.Visible = false; J6_15.Visible = false; J6_16.Visible = false; J6_17.Visible = false; J6_18.Visible = false;
            J6_19.Visible = false; J6_20.Visible = false; J6_21.Visible = false; J6_22.Visible = false; J6_23.Visible = false;
            J6_24.Visible = false; J6_25.Visible = false; J6_26.Visible = false; J6_27.Visible = false; J6_28.Visible = false;
            J6_29.Visible = false; J6_31.Visible = false; J6_32.Visible = false; J6_33.Visible = false; J6_34.Visible = false;
            J6_35.Visible = false; J6_36.Visible = false; J6_37.Visible = false; J6_38.Visible = false; J6_39.Visible = false;
            //Jogador7
            J7_00.Visible = false; J7_01.Visible = false; J7_02.Visible = false; J7_03.Visible = false; J7_04.Visible = false;
            J7_05.Visible = false; J7_06.Visible = false; J7_07.Visible = false; J7_08.Visible = false; J7_09.Visible = false;
            J7_10.Visible = false; J7_10P.Visible = false; J7_11.Visible = false; J7_12.Visible = false; J7_13.Visible = false;
            J7_14.Visible = false; J7_15.Visible = false; J7_16.Visible = false; J7_17.Visible = false; J7_18.Visible = false;
            J7_19.Visible = false; J7_20.Visible = false; J7_21.Visible = false; J7_22.Visible = false; J7_23.Visible = false;
            J7_24.Visible = false; J7_25.Visible = false; J7_26.Visible = false; J7_27.Visible = false; J7_28.Visible = false;
            J7_29.Visible = false; J7_31.Visible = false; J7_32.Visible = false; J7_33.Visible = false; J7_34.Visible = false;
            J7_35.Visible = false; J7_36.Visible = false; J7_37.Visible = false; J7_38.Visible = false; J7_39.Visible = false;
            //Jogador8
            J8_00.Visible = false; J8_01.Visible = false; J8_02.Visible = false; J8_03.Visible = false; J8_04.Visible = false;
            J8_05.Visible = false; J8_06.Visible = false; J8_07.Visible = false; J8_08.Visible = false; J8_09.Visible = false;
            J8_10.Visible = false; J8_10P.Visible = false; J8_11.Visible = false; J8_12.Visible = false; J8_13.Visible = false;
            J8_14.Visible = false; J8_15.Visible = false; J8_16.Visible = false; J8_17.Visible = false; J8_18.Visible = false;
            J8_19.Visible = false; J8_20.Visible = false; J8_21.Visible = false; J8_22.Visible = false; J8_23.Visible = false;
            J8_24.Visible = false; J8_25.Visible = false; J8_26.Visible = false; J8_27.Visible = false; J8_28.Visible = false;
            J8_29.Visible = false; J8_31.Visible = false; J8_32.Visible = false; J8_33.Visible = false; J8_34.Visible = false;
            J8_35.Visible = false; J8_36.Visible = false; J8_37.Visible = false; J8_38.Visible = false; J8_39.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
           // MostrarCartasSorte m = new MostrarCartasSorte();
           // m.Show();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //REMOVE O JOGADOR DO JOGO
            Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].falencia = true;

            //VER SE EXISTE VENCEDOR
            if(VerVencedor() != 99)
            {
                this.Hide();
                FimJogo f = new FimJogo(VerVencedor());
                f.Show();
            }

            // Dá todas as Propriedades obtidas ao Banco
            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Propriedades.Count > 0)
            {
                for (int i = 0; i < Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Propriedades.Count; i++)
                {
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Propriedades[i].Casas = 0;
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Propriedades[i].Dono = null;
                    Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].Propriedades[i].hipotecado = false;
                }
            }
            

            // Move para o proximo jogador
            Tabuleiro.AtualJogadorIndex += 1;
            if (Tabuleiro.AtualJogadorIndex == Tabuleiro.NrTotalJogadores)
            {
                Tabuleiro.AtualJogadorIndex = 0;
                Tabuleiro.NrRondas++;
                label21.Text = "Ronda Nº " + Tabuleiro.NrRondas;
            }

            update();

        }

        private int VerVencedor()
        {
            int count=0;
            int IndexVencedor=99;

            for (int i=0; i < Tabuleiro.NrTotalJogadores; i++)
            {
                if(Tabuleiro.jogadores[i].falencia)
                {
                    count++;
                } else
                {
                    IndexVencedor = i;
                }
            }

            if (count == Tabuleiro.NrTotalJogadores-1)
            {
                return IndexVencedor;
            }

            return 99;
        }

        private void updateCasas()
        {
            //Mete tudo Invisivel
            CasasInvisible();

            for (int i=0; i < Tabuleiro.NrTotalJogadores; i++)
            {
                if (Tabuleiro.jogadores[i].Propriedades.Count > 0)
                {
                    for (int j = 0; j < Tabuleiro.jogadores[i].Propriedades.Count; j++)
                    {
                        MostrarCasas(Tabuleiro.jogadores[i].Propriedades[j].Index, Tabuleiro.jogadores[i].Propriedades[j].Casas);
                    }
                }
            }
        }

        private void MostrarCasas(int prop, int casas)
        {
            // CASA 1
            if (prop == 1)
            {
                switch(casas)
                {
                    case 1:
                        C1_01.Visible = true;
                        break;
                    case 2:
                        C1_01.Visible = true;
                        C1_02.Visible = true;
                        break;
                    case 3:
                        C1_01.Visible = true;
                        C1_02.Visible = true;
                        C1_03.Visible = true;
                        break;
                    case 4:
                        C1_01.Visible = true;
                        C1_02.Visible = true;
                        C1_03.Visible = true;
                        C1_04.Visible = true;
                        break;
                    case 5:
                        H1.Visible = true;
                        break;
                }
            }

            // CASA 3
            if (prop == 3)
            {
                switch (casas)
                {
                    case 1:
                        C3_01.Visible = true;
                        break;
                    case 2:
                        C3_01.Visible = true;
                        C3_02.Visible = true;
                        break;
                    case 3:
                        C3_01.Visible = true;
                        C3_02.Visible = true;
                        C3_03.Visible = true;
                        break;
                    case 4:
                        C3_01.Visible = true;
                        C3_02.Visible = true;
                        C3_03.Visible = true;
                        C3_04.Visible = true;
                        break;
                    case 5:
                        H3.Visible = true;
                        break;
                }
            }

            // CASA 6
            if (prop == 6)
            {
                switch (casas)
                {
                    case 1:
                        C6_01.Visible = true;
                        break;
                    case 2:
                        C6_01.Visible = true;
                        C6_02.Visible = true;
                        break;
                    case 3:
                        C6_01.Visible = true;
                        C6_02.Visible = true;
                        C6_03.Visible = true;
                        break;
                    case 4:
                        C6_01.Visible = true;
                        C6_02.Visible = true;
                        C6_03.Visible = true;
                        C6_04.Visible = true;
                        break;
                    case 5:
                        H6.Visible = true;
                        break;
                }
            }

            // CASA 8
            if (prop == 8)
            {
                switch (casas)
                {
                    case 1:
                        C8_01.Visible = true;
                        break;
                    case 2:
                        C8_01.Visible = true;
                        C8_02.Visible = true;
                        break;
                    case 3:
                        C8_01.Visible = true;
                        C8_02.Visible = true;
                        C8_03.Visible = true;
                        break;
                    case 4:
                        C8_01.Visible = true;
                        C8_02.Visible = true;
                        C8_03.Visible = true;
                        C8_04.Visible = true;
                        break;
                    case 5:
                        H8.Visible = true;
                        break;
                }
            }

            // CASA 9
            if (prop == 9)
            {
                switch (casas)
                {
                    case 1:
                        C9_01.Visible = true;
                        break;
                    case 2:
                        C9_01.Visible = true;
                        C9_02.Visible = true;
                        break;
                    case 3:
                        C9_01.Visible = true;
                        C9_02.Visible = true;
                        C9_03.Visible = true;
                        break;
                    case 4:
                        C9_01.Visible = true;
                        C9_02.Visible = true;
                        C9_03.Visible = true;
                        C9_04.Visible = true;
                        break;
                    case 5:
                        H9.Visible = true;
                        break;
                }
            }

            // CASA 11
            if (prop == 11)
            {
                switch (casas)
                {
                    case 1:
                        C11_01.Visible = true;
                        break;
                    case 2:
                        C11_01.Visible = true;
                        C11_02.Visible = true;
                        break;
                    case 3:
                        C11_01.Visible = true;
                        C11_02.Visible = true;
                        C11_03.Visible = true;
                        break;
                    case 4:
                        C11_01.Visible = true;
                        C11_02.Visible = true;
                        C11_03.Visible = true;
                        C11_04.Visible = true;
                        break;
                    case 5:
                        H11.Visible = true;
                        break;
                }
            }

            // CASA 13
            if (prop == 13)
            {
                switch (casas)
                {
                    case 1:
                        C13_01.Visible = true;
                        break;
                    case 2:
                        C13_01.Visible = true;
                        C13_02.Visible = true;
                        break;
                    case 3:
                        C13_01.Visible = true;
                        C13_02.Visible = true;
                        C13_03.Visible = true;
                        break;
                    case 4:
                        C13_01.Visible = true;
                        C13_02.Visible = true;
                        C13_03.Visible = true;
                        C13_04.Visible = true;
                        break;
                    case 5:
                        H13.Visible = true;
                        break;
                }
            }

            // CASA 14
            if (prop == 14)
            {
                switch (casas)
                {
                    case 1:
                        C14_01.Visible = true;
                        break;
                    case 2:
                        C14_01.Visible = true;
                        C14_02.Visible = true;
                        break;
                    case 3:
                        C14_01.Visible = true;
                        C14_02.Visible = true;
                        C14_03.Visible = true;
                        break;
                    case 4:
                        C14_01.Visible = true;
                        C14_02.Visible = true;
                        C14_03.Visible = true;
                        C14_04.Visible = true;
                        break;
                    case 5:
                        H14.Visible = true;
                        break;
                }
            }

            // CASA 16
            if (prop == 16)
            {
                switch (casas)
                {
                    case 1:
                        C16_01.Visible = true;
                        break;
                    case 2:
                        C16_01.Visible = true;
                        C16_02.Visible = true;
                        break;
                    case 3:
                        C16_01.Visible = true;
                        C16_02.Visible = true;
                        C16_03.Visible = true;
                        break;
                    case 4:
                        C16_01.Visible = true;
                        C16_02.Visible = true;
                        C16_03.Visible = true;
                        C16_04.Visible = true;
                        break;
                    case 5:
                        H16.Visible = true;
                        break;
                }
            }

            // CASA 18
            if (prop == 18)
            {
                switch (casas)
                {
                    case 1:
                        C18_01.Visible = true;
                        break;
                    case 2:
                        C18_01.Visible = true;
                        C18_02.Visible = true;
                        break;
                    case 3:
                        C18_01.Visible = true;
                        C18_02.Visible = true;
                        C18_03.Visible = true;
                        break;
                    case 4:
                        C18_01.Visible = true;
                        C18_02.Visible = true;
                        C18_03.Visible = true;
                        C18_04.Visible = true;
                        break;
                    case 5:
                        H18.Visible = true;
                        break;
                }
            }

            // CASA 19
            if (prop == 19)
            {
                switch (casas)
                {
                    case 1:
                        C19_01.Visible = true;
                        break;
                    case 2:
                        C19_01.Visible = true;
                        C19_02.Visible = true;
                        break;
                    case 3:
                        C19_01.Visible = true;
                        C19_02.Visible = true;
                        C19_03.Visible = true;
                        break;
                    case 4:
                        C19_01.Visible = true;
                        C19_02.Visible = true;
                        C19_03.Visible = true;
                        C19_04.Visible = true;
                        break;
                    case 5:
                        H19.Visible = true;
                        break;
                }
            }

            // CASA 21
            if (prop == 21)
            {
                switch (casas)
                {
                    case 1:
                        C21_01.Visible = true;
                        break;
                    case 2:
                        C21_01.Visible = true;
                        C21_02.Visible = true;
                        break;
                    case 3:
                        C21_01.Visible = true;
                        C21_02.Visible = true;
                        C21_03.Visible = true;
                        break;
                    case 4:
                        C21_01.Visible = true;
                        C21_02.Visible = true;
                        C21_03.Visible = true;
                        C21_04.Visible = true;
                        break;
                    case 5:
                        H21.Visible = true;
                        break;
                }
            }

            // CASA 23
            if (prop == 23)
            {
                switch (casas)
                {
                    case 1:
                        C23_01.Visible = true;
                        break;
                    case 2:
                        C23_01.Visible = true;
                        C23_02.Visible = true;
                        break;
                    case 3:
                        C23_01.Visible = true;
                        C23_02.Visible = true;
                        C23_03.Visible = true;
                        break;
                    case 4:
                        C23_01.Visible = true;
                        C23_02.Visible = true;
                        C23_03.Visible = true;
                        C23_04.Visible = true;
                        break;
                    case 5:
                        H23.Visible = true;
                        break;
                }
            }

            // CASA 24
            if (prop == 24)
            {
                switch (casas)
                {
                    case 1:
                        C24_01.Visible = true;
                        break;
                    case 2:
                        C24_01.Visible = true;
                        C24_02.Visible = true;
                        break;
                    case 3:
                        C24_01.Visible = true;
                        C24_02.Visible = true;
                        C24_03.Visible = true;
                        break;
                    case 4:
                        C24_01.Visible = true;
                        C24_02.Visible = true;
                        C24_03.Visible = true;
                        C24_04.Visible = true;
                        break;
                    case 5:
                        H24.Visible = true;
                        break;
                }
            }

            // CASA 26
            if (prop == 26)
            {
                switch (casas)
                {
                    case 1:
                        C26_01.Visible = true;
                        break;
                    case 2:
                        C26_01.Visible = true;
                        C26_02.Visible = true;
                        break;
                    case 3:
                        C26_01.Visible = true;
                        C26_02.Visible = true;
                        C26_03.Visible = true;
                        break;
                    case 4:
                        C26_01.Visible = true;
                        C26_02.Visible = true;
                        C26_03.Visible = true;
                        C26_04.Visible = true;
                        break;
                    case 5:
                        H26.Visible = true;
                        break;
                }
            }

            // CASA 27
            if (prop == 27)
            {
                switch (casas)
                {
                    case 1:
                        C27_01.Visible = true;
                        break;
                    case 2:
                        C27_01.Visible = true;
                        C27_02.Visible = true;
                        break;
                    case 3:
                        C27_01.Visible = true;
                        C27_02.Visible = true;
                        C27_03.Visible = true;
                        break;
                    case 4:
                        C27_01.Visible = true;
                        C27_02.Visible = true;
                        C27_03.Visible = true;
                        C27_04.Visible = true;
                        break;
                    case 5:
                        H27.Visible = true;
                        break;
                }
            }

            // CASA 29
            if (prop == 29)
            {
                switch (casas)
                {
                    case 1:
                        C29_01.Visible = true;
                        break;
                    case 2:
                        C29_01.Visible = true;
                        C29_02.Visible = true;
                        break;
                    case 3:
                        C29_01.Visible = true;
                        C29_02.Visible = true;
                        C29_03.Visible = true;
                        break;
                    case 4:
                        C29_01.Visible = true;
                        C29_02.Visible = true;
                        C29_03.Visible = true;
                        C29_04.Visible = true;
                        break;
                    case 5:
                        H29.Visible = true;
                        break;
                }
            }

            // CASA 31
            if (prop == 31)
            {
                switch (casas)
                {
                    case 1:
                        C31_01.Visible = true;
                        break;
                    case 2:
                        C31_01.Visible = true;
                        C31_02.Visible = true;
                        break;
                    case 3:
                        C31_01.Visible = true;
                        C31_02.Visible = true;
                        C31_03.Visible = true;
                        break;
                    case 4:
                        C31_01.Visible = true;
                        C31_02.Visible = true;
                        C31_03.Visible = true;
                        C31_04.Visible = true;
                        break;
                    case 5:
                        H31.Visible = true;
                        break;
                }
            }

            // CASA 32
            if (prop == 32)
            {
                switch (casas)
                {
                    case 1:
                        C32_01.Visible = true;
                        break;
                    case 2:
                        C32_01.Visible = true;
                        C32_02.Visible = true;
                        break;
                    case 3:
                        C32_01.Visible = true;
                        C32_02.Visible = true;
                        C32_03.Visible = true;
                        break;
                    case 4:
                        C32_01.Visible = true;
                        C32_02.Visible = true;
                        C32_03.Visible = true;
                        C32_04.Visible = true;
                        break;
                    case 5:
                        H32.Visible = true;
                        break;
                }
            }

            // CASA 34
            if (prop == 34)
            {
                switch (casas)
                {
                    case 1:
                        C34_01.Visible = true;
                        break;
                    case 2:
                        C34_01.Visible = true;
                        C34_02.Visible = true;
                        break;
                    case 3:
                        C34_01.Visible = true;
                        C34_02.Visible = true;
                        C34_03.Visible = true;
                        break;
                    case 4:
                        C34_01.Visible = true;
                        C34_02.Visible = true;
                        C34_03.Visible = true;
                        C34_04.Visible = true;
                        break;
                    case 5:
                        H34.Visible = true;
                        break;
                }
            }

            // CASA 37
            if (prop == 37)
            {
                switch (casas)
                {
                    case 1:
                        C37_01.Visible = true;
                        break;
                    case 2:
                        C37_01.Visible = true;
                        C37_02.Visible = true;
                        break;
                    case 3:
                        C37_01.Visible = true;
                        C37_02.Visible = true;
                        C37_03.Visible = true;
                        break;
                    case 4:
                        C37_01.Visible = true;
                        C37_02.Visible = true;
                        C37_03.Visible = true;
                        C37_04.Visible = true;
                        break;
                    case 5:
                        H37.Visible = true;
                        break;
                }
            }

            // CASA 39
            if (prop == 39)
            {
                switch (casas)
                {
                    case 1:
                        C39_01.Visible = true;
                        break;
                    case 2:
                        C39_01.Visible = true;
                        C39_02.Visible = true;
                        break;
                    case 3:
                        C39_01.Visible = true;
                        C39_02.Visible = true;
                        C39_03.Visible = true;
                        break;
                    case 4:
                        C39_01.Visible = true;
                        C39_02.Visible = true;
                        C39_03.Visible = true;
                        C39_04.Visible = true;
                        break;
                    case 5:
                        H39.Visible = true;
                        break;
                }
            }
        }

        private void CasasInvisible()
        {
            //PROP 1
            C1_01.Visible = false;
            C1_02.Visible = false;
            C1_03.Visible = false;
            C1_04.Visible = false;
            H1.Visible = false;

            //PROP 3
            C3_01.Visible = false;
            C3_02.Visible = false;
            C3_03.Visible = false;
            C3_04.Visible = false;
            H3.Visible = false;

            //PROP 6
            C6_01.Visible = false;
            C6_02.Visible = false;
            C6_03.Visible = false;
            C6_04.Visible = false;
            H6.Visible = false;

            //PROP 8
            C8_01.Visible = false;
            C8_02.Visible = false;
            C8_03.Visible = false;
            C8_04.Visible = false;
            H8.Visible = false;

            //PROP 9
            C9_01.Visible = false;
            C9_02.Visible = false;
            C9_03.Visible = false;
            C9_04.Visible = false;
            H9.Visible = false;

            //PROP 11
            C11_01.Visible = false;
            C11_02.Visible = false;
            C11_03.Visible = false;
            C11_04.Visible = false;
            H11.Visible = false;

            //PROP 13
            C13_01.Visible = false;
            C13_02.Visible = false;
            C13_03.Visible = false;
            C13_04.Visible = false;
            H13.Visible = false;

            //PROP 14
            C14_01.Visible = false;
            C14_02.Visible = false;
            C14_03.Visible = false;
            C14_04.Visible = false;
            H14.Visible = false;

            //PROP 16
            C16_01.Visible = false;
            C16_02.Visible = false;
            C16_03.Visible = false;
            C16_04.Visible = false;
            H16.Visible = false;

            //PROP 18
            C18_01.Visible = false;
            C18_02.Visible = false;
            C18_03.Visible = false;
            C18_04.Visible = false;
            H18.Visible = false;

            //PROP 19
            C19_01.Visible = false;
            C19_02.Visible = false;
            C19_03.Visible = false;
            C19_04.Visible = false;
            H19.Visible = false;

            //PROP 21
            C21_01.Visible = false;
            C21_02.Visible = false;
            C21_03.Visible = false;
            C21_04.Visible = false;
            H21.Visible = false;

            //PROP 23
            C23_01.Visible = false;
            C23_02.Visible = false;
            C23_03.Visible = false;
            C23_04.Visible = false;
            H23.Visible = false;

            //PROP 24
            C24_01.Visible = false;
            C24_02.Visible = false;
            C24_03.Visible = false;
            C24_04.Visible = false;
            H24.Visible = false;

            //PROP 26
            C26_01.Visible = false;
            C26_02.Visible = false;
            C26_03.Visible = false;
            C26_04.Visible = false;
            H26.Visible = false;

            //PROP 27
            C27_01.Visible = false;
            C27_02.Visible = false;
            C27_03.Visible = false;
            C27_04.Visible = false;
            H27.Visible = false;

            //PROP 29
            C29_01.Visible = false;
            C29_02.Visible = false;
            C29_03.Visible = false;
            C29_04.Visible = false;
            H29.Visible = false;

            //PROP 31
            C31_01.Visible = false;
            C31_02.Visible = false;
            C31_03.Visible = false;
            C31_04.Visible = false;
            H31.Visible = false;

            //PROP 32
            C32_01.Visible = false;
            C32_02.Visible = false;
            C32_03.Visible = false;
            C32_04.Visible = false;
            H32.Visible = false;

            //PROP 34
            C34_01.Visible = false;
            C34_02.Visible = false;
            C34_03.Visible = false;
            C34_04.Visible = false;
            H34.Visible = false;

            //PROP 37
            C37_01.Visible = false;
            C37_02.Visible = false;
            C37_03.Visible = false;
            C37_04.Visible = false;
            H37.Visible = false;

            //PROP 37
            C39_01.Visible = false;
            C39_02.Visible = false;
            C39_03.Visible = false;
            C39_04.Visible = false;
            H39.Visible = false;
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}

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

    public partial class Trocar : Form
    {
        public Trocar(int PlayerIndex)
        {
            InitializeComponent();

            // Inicializa os dados do jogador 1
            label6.Text = Tabuleiro.jogadores[PlayerIndex].Nome;
            label3.Text = "Propriedades de " + Tabuleiro.jogadores[PlayerIndex].Nome + ":";
            trackBar1.Maximum = Tabuleiro.jogadores[PlayerIndex].Dinheiro;
            numericUpDown1.Maximum = Tabuleiro.jogadores[PlayerIndex].Dinheiro;

            // Adiona as Propriedades do jogador 1 à listbox
            int k = Tabuleiro.jogadores[PlayerIndex].Propriedades.Count;
            for (int i = 0; i < k; i++)
            {
                checkedListBox1.Items.Add(Tabuleiro.jogadores[PlayerIndex].Propriedades[i].Nome);
            }


            // Adiciona à combobox os jogadores
            for (int i=0; i< Tabuleiro.NrTotalJogadores; i++)
            {
                if(i != PlayerIndex)
                {
                    comboBox1.Items.Add(Tabuleiro.jogadores[i].Nome);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox2.Items.Clear();
            checkBox2.Checked = false;

            // Associar o Nome ao index
            for (int i = 0; i < Tabuleiro.NrTotalJogadores; i++)
            {
                // BUG QUE NAO SEI RESOLVER, MAS SEM ISTO NAO MOSTRA OS RESULTADOS
                // #ESTRANHO
                Mensagem m = new Mensagem("");
                m.Show();
                m.Close();
                
                // ---
                if (comboBox1.SelectedText.Equals(Tabuleiro.jogadores[i].Nome))
                {
                    label2.Text = "Propriedades de " + Tabuleiro.jogadores[i].Nome + ":";
                    label2.Visible = true;

                    //Adiciona as Propriedades do jogodar à lista
                    int k = Tabuleiro.jogadores[i].Propriedades.Count;
                    for (int j = 0; j < k; j++)
                    {
                        checkedListBox2.Items.Add(Tabuleiro.jogadores[i].Propriedades[j].Nome);
                    }
                    checkedListBox2.Visible = true;

                    // Adiciona o maximo do dinheiro do jogador
                    label5.Visible = true;
                    trackBar2.Value = 0;
                    trackBar2.Maximum = Tabuleiro.jogadores[i].Dinheiro;
                    trackBar2.Visible = true;
                    numericUpDown2.Value = 0;
                    numericUpDown2.Maximum = Tabuleiro.jogadores[i].Dinheiro;
                    numericUpDown2.Visible = true;

                    // Mete a CheckBox de confirmação
                    checkBox2.Checked = false;
                    checkBox2.Visible = true;
                }

            }
        }

        // Atualizar os valores das trackbars e numericUpDowns

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
            checkBox2.Checked = false;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Convert.ToInt32(numericUpDown2.Value);
            checkBox2.Checked = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
            checkBox1.Checked = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToInt32(numericUpDown1.Value);
            checkBox1.Checked = false;
        }

        // Mostrar o botão quando as 2 checkbox estiverem TRUE
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                if (checkBox1.Checked == true)
                    button1.Visible = true;
                else
                    button1.Visible = false;
            else
                button1.Visible = false;  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                if (checkBox2.Checked == true)
                    button1.Visible = true;
                else
                    button1.Visible = false;
            else
                button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trade(getJog1Index(), getJog2Index());

            Jogo j = new Jogo();
            j.Update();
            this.Hide();

        }

        private int getJog1Index()
        {
            for (int i = 0; i < Tabuleiro.NrTotalJogadores; i++)
            {
                //MessageBox.Show("DEBUG JOG1 INDEX");
                if (label6.Text.Equals(Tabuleiro.jogadores[i].Nome))
                {
                    //MessageBox.Show(Tabuleiro.jogadores[i].Nome + Tabuleiro.jogadores[i].Index);
                    return Tabuleiro.jogadores[i].Index;
                }
            }
            return 0;
        }

        private int getJog2Index()
        {
            for (int j = 0; j < Tabuleiro.NrTotalJogadores; j++)
            {
                //MessageBox.Show("DEBUG JOG2 INDEX");
                if (comboBox1.SelectedItem.ToString().Equals(Tabuleiro.jogadores[j].Nome))
                {
                    //MessageBox.Show(Tabuleiro.jogadores[j].Nome + Tabuleiro.jogadores[j].Index);
                    return Tabuleiro.jogadores[j].Index;              
                }
            }
            return 0;
        }

        

        private void Trade(int J1, int J2)
        {
            // Passar as Propriedades selecionas do Jogador 1 para o Jogador 2
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                foreach (string item in checkedListBox1.CheckedItems)
                {
                    int propindex = VerIndex(checkedListBox1, item.ToString(), J1-1);
                    Propriedade propriedadeAtual = (Propriedade)Tabuleiro.tabuleiro[propindex];
                    Tabuleiro.jogadores[J1-1].Propriedades.Remove(propriedadeAtual);
                    Tabuleiro.jogadores[J2-1].Propriedades.Add(propriedadeAtual);
                    propriedadeAtual.Dono = Tabuleiro.jogadores[J2-1];

                }
            }
            // Passar as Propriedades selecionas do Jogador 2 para o Jogador 1
            if (checkedListBox2.CheckedItems.Count > 0)
            {
                foreach (string item in checkedListBox2.CheckedItems)
                {
                    int propindex = VerIndex(checkedListBox2, item.ToString(), J2-1);
                    Propriedade propriedadeAtual = (Propriedade)Tabuleiro.tabuleiro[propindex];
                    Tabuleiro.jogadores[J2 - 1].Propriedades.Remove(propriedadeAtual);
                    Tabuleiro.jogadores[J1 - 1].Propriedades.Add(propriedadeAtual);
                    propriedadeAtual.Dono = Tabuleiro.jogadores[J1 - 1];
                }
            }

            // Passar o dinheiro do Jogador 1 para o Jogador 2
            if (Convert.ToInt32(numericUpDown1.Value) >= 0)
            {
                Tabuleiro.jogadores[J1 - 1].RetirarDinheiro(Convert.ToInt32(numericUpDown1.Value));
                Tabuleiro.jogadores[J2 - 1].Depositar(Convert.ToInt32(numericUpDown1.Value));
            }
            
            // Passar o dinheiro do Jogador 2 para o Jogador 1
            if (Convert.ToInt32(numericUpDown2.Value) > 0)
            {
                Tabuleiro.jogadores[J2 - 1].RetirarDinheiro(Convert.ToInt32(numericUpDown2.Value));
                Tabuleiro.jogadores[J1 - 1].Depositar(Convert.ToInt32(numericUpDown2.Value));
            }

            Mensagem m = new Mensagem("TROCA EFETUADA COM SUCESSO!");
            m.ShowDialog();
        }

        // Vê o Index de uma Propriedade
        private int VerIndex (CheckedListBox clb, string nome, int JogIndex)
        {
            for (int i=0; i < Tabuleiro.tabuleiro.Count; i++)
            {
                //MessageBox.Show(Tabuleiro.tabuleiro.Count.ToString());
                //MessageBox.Show("DEBUG : " + Tabuleiro.jogadores[JogIndex].Propriedades[i].Nome);
                if(Tabuleiro.jogadores[JogIndex].Propriedades[i].Nome.Equals(nome))
                {
                    return Tabuleiro.jogadores[JogIndex].Propriedades[i].Index;
                }
            }
            return 0;
        }

        private void Trocar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Jogo j = new Jogo();
            j.Update();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkBox1.Checked = false;

        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void checkedListBox2_SelectedValueChanged_1(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void checkedListBox2_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

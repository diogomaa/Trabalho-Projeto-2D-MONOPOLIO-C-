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
    public partial class Setup : Form
    {


        public Setup()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(numericUpDown1.Value);

            switch (count)
            {
                case 2:
                    // Mete invisivel o 3
                    pictureBox32.Visible = false;
                    comboBox3.Visible = false;
                    Player3Nome.Visible = false;
                    label3.Visible = false;
                    pictureBox4.Visible = false;
                    break;

                case 3:
                    // Mete invisivel o 4
                    pictureBox40.Visible = false;
                    comboBox4.Visible = false;
                    Player4Nome.Visible = false;
                    label4.Visible = false;
                    pictureBox5.Visible = false;

                    // Mostra o 3
                    pictureBox32.Visible = true;
                    comboBox3.Visible = true;
                    Player3Nome.Visible = true;
                    label3.Visible = true;
                    pictureBox4.Visible = true;
                    break;

                case 4:
                    // Mete invisivel o 5
                    pictureBox48.Visible = false;
                    comboBox5.Visible = false;
                    Player5Nome.Visible = false;
                    label5.Visible = false;
                    pictureBox6.Visible = false;

                    // Mostra o 4
                    pictureBox40.Visible = true;
                    comboBox4.Visible = true;
                    Player4Nome.Visible = true;
                    label4.Visible = true;
                    pictureBox5.Visible = true;
                    break;

                case 5:
                    // Mete Invisivel o 6
                    pictureBox56.Visible = false;
                    comboBox6.Visible = false;
                    Player6Nome.Visible = false;
                    label6.Visible = false;
                    pictureBox7.Visible = false;

                    // Mostra o 5
                    pictureBox48.Visible = true;
                    comboBox5.Visible = true;
                    Player5Nome.Visible = true;
                    label5.Visible = true;
                    pictureBox6.Visible = true;
                    break;

                case 6:
                    // Invisivel o 7
                    pictureBox64.Visible = false;
                    comboBox7.Visible =false;
                    Player7Nome.Visible = false;
                    label7.Visible = false;
                    pictureBox8.Visible = false;

                    // Mete Invisivel o 6
                    pictureBox56.Visible = true;
                    comboBox6.Visible = true;
                    Player6Nome.Visible = true;
                    label6.Visible = true;
                    pictureBox7.Visible = true;
                    break;

                case 7:
                    // Invisivel o 8
                    pictureBox72.Visible = false;
                    comboBox8.Visible = false;
                    label8.Visible = false;
                    Player8Nome.Visible = false;
                    pictureBox9.Visible = false;

                    // Mostra o 7
                    pictureBox64.Visible = true;
                    comboBox7.Visible = true;
                    Player7Nome.Visible = true;
                    label7.Visible = true;
                    pictureBox8.Visible = true;
                    break;

                case 8:
                    // Mostra o 8
                    pictureBox72.Visible = true;
                    comboBox8.Visible = true;
                    label8.Visible = true;
                    Player8Nome.Visible = true;
                    pictureBox9.Visible = true;
                    break;
            }

        }

        private bool Verificar()
        {
            int count = Convert.ToInt32(numericUpDown1.Value);

            // Verificar se todos os jogadores têm um nome
            switch (count)
            {
                case 2:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 3:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 4:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals("") || Player4Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 5:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals("") || Player4Nome.Text.Equals("")
                       || Player5Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 6:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals("") || Player4Nome.Text.Equals("")
                       || Player5Nome.Text.Equals("") || Player6Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 7:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals("") || Player4Nome.Text.Equals("")
                       || Player5Nome.Text.Equals("") || Player6Nome.Text.Equals("") || Player7Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
                case 8:
                    if (Player1Nome.Text.Equals("") || Player2Nome.Text.Equals("") || Player3Nome.Text.Equals("") || Player4Nome.Text.Equals("")
                       || Player5Nome.Text.Equals("") || Player6Nome.Text.Equals("") || Player7Nome.Text.Equals("") || Player8Nome.Text.Equals(""))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser Nulos!");
                        return false;
                    }
                    break;
            }

            // Verificar se não existem nome iguais
            switch (count)
            {
                case 2:
                    // 1~2
                    if (Player1Nome.Text.Equals(Player2Nome.Text))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 3:
                    // 1~2 1~3 | 2~3
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 4:
                    // 1~2 1~3 1~4 | 2~3 2~4 | 3~4
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text)) || (Player1Nome.Text.Equals(Player4Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)) || (Player2Nome.Text.Equals(Player4Nome.Text))
                        || (Player3Nome.Text.Equals(Player4Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 5:
                    // 1~2 1~3 1~4 1~5 | 2~3 2~4 2~5 | 3~4 3~5 | 4~5
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text)) || (Player1Nome.Text.Equals(Player4Nome.Text)) || (Player1Nome.Text.Equals(Player5Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)) || (Player2Nome.Text.Equals(Player4Nome.Text)) || (Player2Nome.Text.Equals(Player5Nome.Text))
                        || (Player3Nome.Text.Equals(Player4Nome.Text)) || (Player3Nome.Text.Equals(Player5Nome.Text))
                        || (Player4Nome.Text.Equals(Player5Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 6:
                    // 1~2 1~3 1~4 1~5 1~6 | 2~3 2~4 2~5 2~6 | 3~4 3~5 3~6 | 4~5 4~6 | 5~6
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text)) || (Player1Nome.Text.Equals(Player4Nome.Text)) || (Player1Nome.Text.Equals(Player5Nome.Text)) || (Player1Nome.Text.Equals(Player6Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)) || (Player2Nome.Text.Equals(Player4Nome.Text)) || (Player2Nome.Text.Equals(Player5Nome.Text)) || (Player2Nome.Text.Equals(Player6Nome.Text))
                        || (Player3Nome.Text.Equals(Player4Nome.Text)) || (Player3Nome.Text.Equals(Player5Nome.Text)) || (Player3Nome.Text.Equals(Player6Nome.Text))
                        || (Player4Nome.Text.Equals(Player5Nome.Text)) || (Player4Nome.Text.Equals(Player6Nome.Text))
                        || (Player5Nome.Text.Equals(Player6Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 7:
                    // 1~2 1~3 1~4 1~5 1~6 1~7 | 2~3 2~4 2~5 2~6 2~7 | 3~4 3~5 3~6 3~7 | 4~5 4~6 4~7 | 5~6 5~7 | 6~7
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text)) || (Player1Nome.Text.Equals(Player4Nome.Text)) || (Player1Nome.Text.Equals(Player5Nome.Text)) || (Player1Nome.Text.Equals(Player6Nome.Text)) || (Player1Nome.Text.Equals(Player7Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)) || (Player2Nome.Text.Equals(Player4Nome.Text)) || (Player2Nome.Text.Equals(Player5Nome.Text)) || (Player2Nome.Text.Equals(Player6Nome.Text)) || (Player2Nome.Text.Equals(Player7Nome.Text))
                        || (Player3Nome.Text.Equals(Player4Nome.Text)) || (Player3Nome.Text.Equals(Player5Nome.Text)) || (Player3Nome.Text.Equals(Player6Nome.Text)) || (Player3Nome.Text.Equals(Player7Nome.Text))
                        || (Player4Nome.Text.Equals(Player5Nome.Text)) || (Player4Nome.Text.Equals(Player6Nome.Text)) || (Player4Nome.Text.Equals(Player7Nome.Text))
                        || (Player5Nome.Text.Equals(Player6Nome.Text)) || (Player5Nome.Text.Equals(Player7Nome.Text))
                        || (Player6Nome.Text.Equals(Player7Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;
                case 8:
                    // 1~2 1~3 1~4 1~5 1~6 1~7 1~8 | 2~3 2~4 2~5 2~6 2~7 2~8 | 3~4 3~5 3~6 3~7 3~8 | 4~5 4~6 4~7 4~8 | 5~6 5~7 5~8 | 6~7 6~8 | 7~8
                    if (Player1Nome.Text.Equals(Player2Nome.Text) || (Player1Nome.Text.Equals(Player3Nome.Text)) || (Player1Nome.Text.Equals(Player4Nome.Text)) || (Player1Nome.Text.Equals(Player5Nome.Text)) || (Player1Nome.Text.Equals(Player6Nome.Text)) || (Player1Nome.Text.Equals(Player7Nome.Text)) || (Player1Nome.Text.Equals(Player8Nome.Text))
                        || (Player2Nome.Text.Equals(Player3Nome.Text)) || (Player2Nome.Text.Equals(Player4Nome.Text)) || (Player2Nome.Text.Equals(Player5Nome.Text)) || (Player2Nome.Text.Equals(Player6Nome.Text)) || (Player2Nome.Text.Equals(Player7Nome.Text)) || (Player2Nome.Text.Equals(Player8Nome.Text))
                        || (Player3Nome.Text.Equals(Player4Nome.Text)) || (Player3Nome.Text.Equals(Player5Nome.Text)) || (Player3Nome.Text.Equals(Player6Nome.Text)) || (Player3Nome.Text.Equals(Player7Nome.Text)) || (Player3Nome.Text.Equals(Player8Nome.Text))
                        || (Player4Nome.Text.Equals(Player5Nome.Text)) || (Player4Nome.Text.Equals(Player6Nome.Text)) || (Player4Nome.Text.Equals(Player7Nome.Text)) || (Player4Nome.Text.Equals(Player8Nome.Text))
                        || (Player5Nome.Text.Equals(Player6Nome.Text)) || (Player5Nome.Text.Equals(Player7Nome.Text)) || (Player5Nome.Text.Equals(Player8Nome.Text))
                        || (Player6Nome.Text.Equals(Player7Nome.Text)) || (Player6Nome.Text.Equals(Player8Nome.Text))
                        || (Player7Nome.Text.Equals(Player8Nome.Text)))
                    {
                        MessageBox.Show("Os nomes dos Jogadores não podem ser iguais");
                        return false;
                    }
                    break;

                
            }
            //  vereficar se existem PEÇAS IGUAIS
            switch (count)
            {
                case 2:
                    if (comboBox1.Text == comboBox2.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 3:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                        || comboBox2.Text == comboBox3.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 4:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                       || comboBox1.Text == comboBox4.Text || comboBox2.Text == comboBox3.Text
                       || comboBox2.Text == comboBox4.Text || comboBox3.Text == comboBox4.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 5:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                       || comboBox1.Text == comboBox4.Text || comboBox1.Text == comboBox5.Text
                       || comboBox2.Text == comboBox3.Text || comboBox2.Text == comboBox4.Text
                       || comboBox2.Text == comboBox5.Text || comboBox3.Text == comboBox4.Text
                       || comboBox3.Text == comboBox5.Text || comboBox4.Text == comboBox5.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 6:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                       || comboBox1.Text == comboBox4.Text || comboBox1.Text == comboBox5.Text
                       || comboBox1.Text == comboBox6.Text || comboBox2.Text == comboBox3.Text
                       || comboBox2.Text == comboBox4.Text || comboBox2.Text == comboBox5.Text
                       || comboBox2.Text == comboBox6.Text || comboBox3.Text == comboBox4.Text
                       || comboBox3.Text == comboBox5.Text || comboBox3.Text == comboBox6.Text
                       || comboBox4.Text == comboBox5.Text || comboBox4.Text == comboBox6.Text
                       || comboBox5.Text == comboBox6.Text)

                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 7:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                       || comboBox1.Text == comboBox4.Text || comboBox1.Text == comboBox5.Text
                       || comboBox1.Text == comboBox6.Text || comboBox1.Text == comboBox7.Text
                       || comboBox2.Text == comboBox3.Text || comboBox2.Text == comboBox4.Text
                       || comboBox2.Text == comboBox5.Text || comboBox2.Text == comboBox6.Text
                       || comboBox2.Text == comboBox7.Text || comboBox3.Text == comboBox4.Text
                       || comboBox3.Text == comboBox5.Text || comboBox3.Text == comboBox6.Text
                       || comboBox3.Text == comboBox7.Text || comboBox4.Text == comboBox5.Text
                       || comboBox4.Text == comboBox6.Text || comboBox4.Text == comboBox7.Text
                       || comboBox5.Text == comboBox6.Text || comboBox5.Text == comboBox7.Text
                       || comboBox6.Text == comboBox7.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 8:
                    if (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text
                       || comboBox1.Text == comboBox4.Text || comboBox1.Text == comboBox5.Text
                       || comboBox1.Text == comboBox6.Text || comboBox1.Text == comboBox7.Text || comboBox1.Text == comboBox8.Text
                       || comboBox2.Text == comboBox3.Text || comboBox2.Text == comboBox4.Text
                       || comboBox2.Text == comboBox5.Text || comboBox2.Text == comboBox6.Text
                       || comboBox2.Text == comboBox7.Text || comboBox2.Text == comboBox8.Text || comboBox3.Text == comboBox4.Text
                       || comboBox3.Text == comboBox5.Text || comboBox3.Text == comboBox6.Text
                       || comboBox3.Text == comboBox7.Text || comboBox3.Text == comboBox8.Text || comboBox4.Text == comboBox5.Text
                       || comboBox4.Text == comboBox6.Text || comboBox4.Text == comboBox7.Text || comboBox4.Text == comboBox8.Text
                       || comboBox5.Text == comboBox6.Text || comboBox5.Text == comboBox7.Text || comboBox5.Text == comboBox8.Text
                       || comboBox6.Text == comboBox7.Text || comboBox6.Text == comboBox8.Text || comboBox7.Text == comboBox8.Text)
                    {
                        MessageBox.Show("As Peças dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
            }
            // Verificar se todos os jogadores escolhem Peça (NÃO SEI SE HÁ ALGUMA MANERA DE COMEÇAR LOGO COM UM OBJETO)
            switch (count)
            {
                case 2:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 3:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 4:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA") || comboBox4.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 5:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA") || comboBox4.Text.Equals("ESCOLHA PEÇA")
                       || comboBox5.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 6:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA") || comboBox4.Text.Equals("ESCOLHA PEÇA")
                       || comboBox5.Text.Equals("ESCOLHA PEÇA") || comboBox6.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 7:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA") || comboBox4.Text.Equals("ESCOLHA PEÇA")
                       || comboBox5.Text.Equals("ESCOLHA PEÇA") || comboBox6.Text.Equals("ESCOLHA PEÇA") || comboBox7.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
                case 8:
                    if (comboBox1.Text.Equals("ESCOLHA PEÇA") || comboBox2.Text.Equals("ESCOLHA PEÇA") || comboBox3.Text.Equals("ESCOLHA PEÇA") || comboBox4.Text.Equals("ESCOLHA PEÇA")
                       || comboBox5.Text.Equals("ESCOLHA PEÇA") || comboBox6.Text.Equals("ESCOLHA PEÇA") || comboBox7.Text.Equals("ESCOLHA PEÇA") || comboBox8.Text.Equals("ESCOLHA PEÇA"))
                    {
                        MessageBox.Show("Escolha uma peça!");
                        return false;
                    }
                    break;
            }
            //  vereficar se existem CORES IGUAIS
            switch (count)
            {
                case 2:
                    // 1~3
                    if (pictureBox1.BackColor == pictureBox3.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 3:
                    // 1~3 1~4 | 3~4
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                        || pictureBox3.BackColor == pictureBox4.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 4:
                    // 1~3 1~4 1~5 | 3~4 3~5 | 4~5
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                       || pictureBox1.BackColor == pictureBox5.BackColor || pictureBox3.BackColor == pictureBox4.BackColor
                       || pictureBox3.BackColor == pictureBox5.BackColor || pictureBox4.BackColor == pictureBox5.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 5:
                    // 1~3 1~4 1~5 1~6 | 3~4 3~5 3~6 | 4~5 4~6 | 5~6
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                       || pictureBox1.BackColor == pictureBox5.BackColor || pictureBox1.BackColor == pictureBox6.BackColor
                       || pictureBox3.BackColor == pictureBox4.BackColor || pictureBox3.BackColor == pictureBox5.BackColor
                       || pictureBox3.BackColor == pictureBox6.BackColor || pictureBox4.BackColor == pictureBox5.BackColor
                       || pictureBox4.BackColor == pictureBox6.BackColor || pictureBox5.BackColor == pictureBox6.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 6:
                    // 1~3 1~4 1~5 1~6 1~7 | 3~4 3~5 3~6 3~7 | 4~5 4~6 4~7 | 5~6 5~7 | 6~7
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                       || pictureBox1.BackColor == pictureBox5.BackColor || pictureBox1.BackColor == pictureBox6.BackColor
                       || pictureBox1.BackColor == pictureBox7.BackColor || pictureBox3.BackColor == pictureBox4.BackColor
                       || pictureBox3.BackColor == pictureBox5.BackColor || pictureBox3.BackColor == pictureBox6.BackColor
                       || pictureBox3.BackColor == pictureBox7.BackColor || pictureBox4.BackColor == pictureBox5.BackColor
                       || pictureBox4.BackColor == pictureBox6.BackColor || pictureBox4.BackColor == pictureBox7.BackColor
                       || pictureBox5.BackColor == pictureBox6.BackColor || pictureBox5.BackColor == pictureBox7.BackColor
                       || pictureBox6.BackColor == pictureBox7.BackColor)

                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 7:
                    // 1~3 1~4 1~5 1~6 1~7 1~8 | 3~4 3~5 3~6 3~7 3~8 | 4~5 4~6 4~7 4~8 | 5~6 5~7 5~8 | 6~7 6~8 | 7~8
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                       || pictureBox1.BackColor == pictureBox5.BackColor || pictureBox1.BackColor == pictureBox6.BackColor
                       || pictureBox1.BackColor == pictureBox7.BackColor || pictureBox1.BackColor == pictureBox8.BackColor
                       || pictureBox3.BackColor == pictureBox4.BackColor || pictureBox3.BackColor == pictureBox5.BackColor
                       || pictureBox3.BackColor == pictureBox6.BackColor || pictureBox3.BackColor == pictureBox7.BackColor
                       || pictureBox3.BackColor == pictureBox8.BackColor || pictureBox4.BackColor == pictureBox5.BackColor
                       || pictureBox4.BackColor == pictureBox6.BackColor || pictureBox4.BackColor == pictureBox7.BackColor
                       || pictureBox4.BackColor == pictureBox8.BackColor || pictureBox5.BackColor == pictureBox6.BackColor
                       || pictureBox5.BackColor == pictureBox7.BackColor || pictureBox5.BackColor == pictureBox8.BackColor
                       || pictureBox6.BackColor == pictureBox7.BackColor || pictureBox6.BackColor == pictureBox8.BackColor
                       || pictureBox7.BackColor == pictureBox8.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
                case 8:
                    // 1~3 1~4 1~5 1~6 1~7 1~8 1~9 | 3~4 3~5 3~6 3~7 3~8 3~9 | 4~5 4~6 4~7 4~8 4~9 | 5~6 5~7 5~8 5~9 | 6~7 6~8 6~9 | 7~8 7~9 | 8~9
                    if (pictureBox1.BackColor == pictureBox3.BackColor || pictureBox1.BackColor == pictureBox4.BackColor
                       || pictureBox1.BackColor == pictureBox5.BackColor || pictureBox1.BackColor == pictureBox6.BackColor
                       || pictureBox1.BackColor == pictureBox7.BackColor || pictureBox1.BackColor == pictureBox8.BackColor || pictureBox1.BackColor == pictureBox9.BackColor
                       || pictureBox3.BackColor == pictureBox4.BackColor || pictureBox3.BackColor == pictureBox5.BackColor
                       || pictureBox3.BackColor == pictureBox6.BackColor || pictureBox3.BackColor == pictureBox7.BackColor
                       || pictureBox3.BackColor == pictureBox8.BackColor || pictureBox3.BackColor == pictureBox9.BackColor || pictureBox4.BackColor == pictureBox5.BackColor
                       || pictureBox4.BackColor == pictureBox6.BackColor || pictureBox4.BackColor == pictureBox7.BackColor
                       || pictureBox4.BackColor == pictureBox8.BackColor || pictureBox4.BackColor == pictureBox9.BackColor || pictureBox5.BackColor == pictureBox6.BackColor
                       || pictureBox5.BackColor == pictureBox7.BackColor || pictureBox5.BackColor == pictureBox8.BackColor || pictureBox5.BackColor == pictureBox9.BackColor
                       || pictureBox6.BackColor == pictureBox7.BackColor || pictureBox6.BackColor == pictureBox8.BackColor || pictureBox6.BackColor == pictureBox9.BackColor
                       || pictureBox7.BackColor == pictureBox8.BackColor || pictureBox7.BackColor == pictureBox9.BackColor || pictureBox8.BackColor == pictureBox9.BackColor)
                    {
                        MessageBox.Show("As cores dos jogadores não podem ser iguais!");
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void Startbutton_Click(object sender, EventArgs e)
        {

            bool continuar = false;

            continuar = Verificar();

            if (continuar)
            {
                int NrJogadores = Convert.ToInt32(numericUpDown1.Value);
                this.Hide();

                string nome1 = Player1Nome.Text;
                string nome2 = Player2Nome.Text;
                string nome3 = Player3Nome.Text;
                string nome4 = Player4Nome.Text;
                string nome5 = Player5Nome.Text;
                string nome6 = Player6Nome.Text;
                string nome7 = Player7Nome.Text;
                string nome8 = Player8Nome.Text;

                Color cor1 = pictureBox1.BackColor;
                Color cor2 = pictureBox3.BackColor;
                Color cor3 = pictureBox4.BackColor;
                Color cor4 = pictureBox5.BackColor;
                Color cor5 = pictureBox6.BackColor;
                Color cor6 = pictureBox7.BackColor;
                Color cor7 = pictureBox8.BackColor;
                Color cor8 = pictureBox9.BackColor;

                Image peca1 = Player1Image.Image;
                Image peca2 = pictureBox24.Image;
                Image peca3 = pictureBox32.Image;
                Image peca4 = pictureBox40.Image;
                Image peca5 = pictureBox48.Image;
                Image peca6 = pictureBox56.Image;
                Image peca7 = pictureBox64.Image;
                Image peca8 = pictureBox72.Image;

                Classes.Tabuleiro.InitializeBoard(NrJogadores, nome1, nome2, nome3, nome4, nome5, nome6, nome7, nome8, cor1, cor2, cor3, cor4, cor5, cor6, cor7, cor8,peca1,peca2,peca3,peca4,peca5,peca6,peca7,peca8);

                Jogo form1 = new Jogo();
                form1.Show();
            }
        }

        private void setCores(PictureBox pic)
        {
            ColorDialog dialogo = new ColorDialog();
            dialogo.Color = pic.BackColor;

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                pic.BackColor = dialogo.Color;
                pic.Invalidate();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            setCores(pictureBox1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            setCores(pictureBox3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            setCores(pictureBox4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            setCores(pictureBox5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            setCores(pictureBox6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            setCores(pictureBox7);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            setCores(pictureBox8);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            setCores(pictureBox9);
        }

        private void setImagem(PictureBox pic, ComboBox comb)
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
        }

        // ComboBox's das Imagens 'ICONS'
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(Player1Image, comboBox1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox24, comboBox2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox32, comboBox3);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox40, comboBox4);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox48, comboBox5);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox56, comboBox6);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox64, comboBox7);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            setImagem(pictureBox72, comboBox8);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }

        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

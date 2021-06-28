using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public class Jogador
    {
        public const int dinheiroInicial = 150000;  
        public const int totalDeEspaços = 40;

        public Jogador(int index, string nome, Color cor, Image Peça)
        {
            this.Index = index;
            this.Nome = nome;
            this.cor = cor;
            this.Peça = Peça;
        }

        // Coloca o jogador numa nova posição
        public void SetPosicao(int novaPosicao)
        {
            
            int posicaoModificada = novaPosicao;

            // Se a nova posição for menor que zero
            if (posicaoModificada < 0)
            {
                // Soma 40 à posição 
                posicaoModificada += totalDeEspaços;
            }
            // Se a nova posição for maior ou igual aos total de espaços (40)
            if (posicaoModificada >= totalDeEspaços)
            {
                // Retira 40 à nova posição e mostra a mensagem de passagem na casa Partida
                posicaoModificada = posicaoModificada - totalDeEspaços;
                Mensagem m = new Mensagem("Você passou pela casa partida e Recebeu 20.000€");
                m.ShowDialog();
                this.Depositar(20000);
            }
            // Coloca o jogador na nova posição
            this.posicaoAtual = posicaoModificada;
        }

        public void Depositar(int quantia)
        {
            this.Dinheiro += quantia;
            if (this.Dinheiro > 0)
            {
                //Mostrar botão de terminar turno
                Jogo j = new Jogo();
                j.TerminarTurnoButton.Visible = true;
                j.button4.Visible = false;
            } else
            {
                //Mostrar botão de falencia
                Jogo j = new Jogo();
                j.TerminarTurnoButton.Visible = false;
                j.button4.Visible = true;
            }
        }

        public void RetirarDinheiro(int quantia)
        {
            if (this.Dinheiro > 0)     
                this.Dinheiro -= quantia;

        }

        // Lista de propriedades de cada jogador
        public List<Propriedade> Propriedades { get; set; } = new List<Propriedade>();

        // Guarda a posição atual do jogador, defenindo a 0 no inicio do jogo.
        public int posicaoAtual { get; set; } = 0;

        // --------------------------------------------------------------------------------
        public int Index { get; private set; }

        // Boolean para saber se está na cadeia, definido como false no inicio
        public bool estaPreso { get; set; } = false;

        // Dinheiro do Jogador, definindo como 150,000 no inicio
        public int Dinheiro { get; private set; } = dinheiroInicial;

        public int turnosPrisao { get; set; } = 0;

        public int doubleCount { get; set; } = 0;

        public Color cor { get; private set; }

        public string Nome { get; private set; }
        public Image Peça { get; private set; }

        public bool falencia { get; set; } = false;

        public bool CartaCadeia { get; set; } = false;
    }
}

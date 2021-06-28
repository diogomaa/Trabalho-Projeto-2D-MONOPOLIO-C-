using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    

    public class Propriedade : Espacos
    {
        

        public Propriedade(int index, string nome, TipoPropriedade tipoPropriedade, int preco, int renda, int hipoteca, int renda1, int renda2, int renda3, int renda4, int renda5) :base(index,nome)
        {
            this.TipoPropriedade= tipoPropriedade;
            this.Preco = preco;
            this.Renda = renda;
            this.Dono = null;
            this.Hipoteca = hipoteca;
            this.Renda1 = renda1;
            this.Renda2 = renda2;
            this.Renda3 = renda3;
            this.Renda4 = renda4;
            this.Renda5 = renda5;

        }

        public TipoPropriedade TipoPropriedade { get; set; }
        public Jogador Dono { get; set; }
        public int Preco { get; set; }
        public int Renda { get; set; }
        public int Hipoteca { get; set; }
        public int Renda1 { get; set; }
        public int Renda2 { get; set; }
        public int Renda3 { get; set; }
        public int Renda4 { get; set; }
        public int Renda5 { get; set; }
        public int Casas { get; set; } = 0;
        public bool hipotecado { get; set; } = false;

        public override string ActOnPlayer(Jogador jogador)
        {
            int i=0;

            if (this.Dono == jogador)
            {
                
                return this.Index + " - Estás em " + this.Nome + "."; 
                
            }
            else if (this.Dono == null)
            {
                
                return this.Index + " - " + this.Nome + " está disponivel para compra.";
            }
            else if (this.hipotecado == false)
            {
                if (this.TipoPropriedade != TipoPropriedade.Estação || this.TipoPropriedade != TipoPropriedade.Companhia)
                {
                    // PROPRIEDADES
                    if (Casas == 0) { i = this.Renda; jogador.RetirarDinheiro(this.Renda); this.Dono.Depositar(this.Renda); }
                    if (Casas == 1) { i = this.Renda1; jogador.RetirarDinheiro(this.Renda1); this.Dono.Depositar(this.Renda1); }
                    if (Casas == 2) { i = this.Renda2; jogador.RetirarDinheiro(this.Renda2); this.Dono.Depositar(this.Renda2); }
                    if (Casas == 3) { i = this.Renda3; jogador.RetirarDinheiro(this.Renda3); this.Dono.Depositar(this.Renda3); }
                    if (Casas == 4) { i = this.Renda4; jogador.RetirarDinheiro(this.Renda4); this.Dono.Depositar(this.Renda4); }
                    if (Casas == 5) { i = this.Renda5; jogador.RetirarDinheiro(this.Renda5); this.Dono.Depositar(this.Renda5); }

                    Mensagem m = new Mensagem(string.Format("{0} pertece ao jogador {1}.\nPagou a ele {2}", this.Nome, this.Dono.Nome, i.ToString()));
                    m.ShowDialog();

                    return this.Nome;

                }
                else if (this.TipoPropriedade == TipoPropriedade.Estação)
                {
                    // ESTAÇOES
                    int counter=0;
                    int multiplicador = 0;
                    // Countar quantas estaçoes tem o jogador
                    for (int j = 0; j < jogador.Propriedades.Count; j++)
                    {
                        if (jogador.Propriedades[j].TipoPropriedade == TipoPropriedade.Estação)
                        {
                            counter++;
                        }
                    }

                    switch (counter)
                    {
                        case 1:
                            multiplicador = 1;
                            break;
                        case 2:
                            multiplicador = 2;
                            break;
                        case 3:
                            multiplicador = 4;
                            break;
                        case 4:
                            multiplicador = 8;
                            break;
                    }

                    if (Casas == 0) { i = this.Renda * multiplicador; jogador.RetirarDinheiro(i); this.Dono.Depositar(i); }

                    Mensagem m = new Mensagem(string.Format("{0} pertece ao jogador {1}.\nPagou a ele {2}", this.Nome, this.Dono.Nome, i.ToString()));
                    m.ShowDialog();
                    return this.Nome;
                }
                else
                {
                    // COMPANHIAS
                    int counter = 0;
                    int valor = 0;


                    for (int j = 0; j < jogador.Propriedades.Count; j++)
                    {
                        if (jogador.Propriedades[j].TipoPropriedade == TipoPropriedade.Companhia)
                        {
                            counter++;
                        }
                    }

                    switch (counter)
                    {
                        case 1:
                            valor = 400 * Tabuleiro.SomaDados;
                            break;
                        case 2:
                            valor = 1000 * Tabuleiro.SomaDados;
                            break;
                    }

                            if (Casas == 0) { i = valor; jogador.RetirarDinheiro(i); this.Dono.Depositar(i); }

                    Mensagem m = new Mensagem(string.Format("{0} pertece ao jogador {1}.\nPagou a ele {2}", this.Nome, this.Dono.Nome, i.ToString()));
                    m.ShowDialog();
                    return this.Nome;
                }
            } else
            {
                Mensagem m = new Mensagem(string.Format("A Propriedade está Hipotecada.\nNão precisas de pagar Renda."));
                m.ShowDialog();

                return this.Nome;
            }
            
            
        }
    }
}

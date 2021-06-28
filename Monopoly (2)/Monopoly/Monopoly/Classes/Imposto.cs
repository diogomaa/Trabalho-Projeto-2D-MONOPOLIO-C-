using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public class Imposto : Espacos
    {
        public int quantiaImposto { get; private set; }
        public Imposto(int index, string name, int quantiaImposto) :base(index,name)
        {
            this.quantiaImposto = quantiaImposto;
        }

        public override string ActOnPlayer(Jogador jogador)
        {
            Mensagem m = new Mensagem("Pague um imposto de " + this.quantiaImposto + " €!");
            m.ShowDialog();

            jogador.RetirarDinheiro(this.quantiaImposto);

            return this.Nome;
        }


    }
}

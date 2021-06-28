using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public abstract class Espacos
    {
        public Espacos(int index, string nome)
        {
            this.Index = index;
            this.Nome = nome;
        }

        public int Index { get; private set; }

        public string Nome { get; private set; }

        public abstract string ActOnPlayer(Jogador jogador);

    }
}

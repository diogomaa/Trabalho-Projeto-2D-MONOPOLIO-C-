using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public class CartasSorte : Espacos
    {
        public CartasSorte(int index, string nome) :base(index, nome)
        {

        }

        public override string ActOnPlayer(Jogador jogador)
        {
            return RetirarCartaSorte.GenerateRandomCard(jogador);
            
        }

        public bool continuar;
        public bool outraCarta;
    }
}

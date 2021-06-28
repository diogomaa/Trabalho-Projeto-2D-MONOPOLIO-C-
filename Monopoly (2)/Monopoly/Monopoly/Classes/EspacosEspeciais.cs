using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{

    public class EspacosEspeciais : Espacos
    {

        public EspacosEspeciais(int index, string nome) :base(index, nome)
        {

        }
        public override string ActOnPlayer(Jogador jogador)
        {
            if (this.Index == 0)
            {
                return "Estás na Casa Partida!";
            }
            else if (this.Index == 10)
            {
                if (jogador.estaPreso == false)
                    return "Estás a visitar a Cadeia";
                else 
                    return "Estás na Cadeia";
                
            }
            else if (this.Index == 20)
            {
                return "Estás no Parque de Estacionamento. Não acontece nada.";
            }
            else
            {
                jogador.estaPreso = true;
                jogador.turnosPrisao = 3;
                jogador.SetPosicao(10);
                return "Estás na Prisão! Não vais poder jogar nos proximos 3 turnos.";
            }
        }

    }
}

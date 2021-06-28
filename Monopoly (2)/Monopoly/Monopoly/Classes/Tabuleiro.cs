using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public static class Tabuleiro
    {
        public static List<Jogador> jogadores;
        public static List<Espacos> tabuleiro;
        public static int AtualJogadorIndex;
        public static int NrTotalJogadores;
        public static int NrRondas;
        public static int SomaDados;
        

        public static void InitializeBoard(int NrJogadores, string nome1, string nome2, string nome3, string nome4, string nome5, string nome6, string nome7, string nome8, Color cor1, Color cor2, Color cor3,Color cor4, Color cor5, Color cor6, Color cor7, Color cor8, Image peça1, Image peça2, Image peça3, Image peça4, Image peça5, Image peça6, Image peça7, Image peça8)
        {
            NrTotalJogadores = NrJogadores;
            AtualJogadorIndex = 0;
            NrRondas = 1;
            switch (NrJogadores)
            {
                case 2:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                    };
                    break;
                case 3:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                    };
                    break;
                case 4:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                        new Jogador(4,nome4,cor4,peça4),
                    };
                    break;
                case 5:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                        new Jogador(4,nome4,cor4,peça4),
                        new Jogador(5,nome5,cor5,peça5),
                    };
                    break;
                case 6:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                        new Jogador(4,nome4,cor4,peça4),
                        new Jogador(5,nome5,cor5,peça5),
                        new Jogador(6,nome6,cor6,peça6),
                    };
                    break;
                case 7:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                        new Jogador(4,nome4,cor4,peça4),
                        new Jogador(5,nome5,cor5,peça5),
                        new Jogador(6,nome6,cor6,peça6),
                        new Jogador(7,nome7,cor7,peça7),
                    };
                    break;
                case 8:
                    jogadores = new List<Jogador>()
                    {
                        new Jogador(1,nome1,cor1,peça1),
                        new Jogador(2,nome2,cor2,peça2),
                        new Jogador(3,nome3,cor3,peça3),
                        new Jogador(4,nome4,cor4,peça4),
                        new Jogador(5,nome5,cor5,peça5),
                        new Jogador(6,nome6,cor6,peça6),
                        new Jogador(7,nome7,cor7,peça7),
                        new Jogador(8,nome8,cor8,peça8),
                    };
                    break;
            }
            
            
            tabuleiro = new List<Espacos>()
                {
                new EspacosEspeciais(0,"PARTIDA! Passando Aqui Receba 20.000€"),
                new Propriedade(1,"Campo Grande (Lisboa)",TipoPropriedade.Brown,6000, 200, 3000, 1000, 3000, 9000, 16000, 25000),
                new CartasSorte(2,"Caixa da Comunidade"),
                new Propriedade(3,"Av. Fernão de Magalhães (Porto)",TipoPropriedade.Brown,6000, 400, 3000, 1000, 3000, 9000, 16000, 25000),
                new Imposto(4,"Pague Imposto Sobre Capitais", 20000),
                new Propriedade(5,"Estação do Rossio",TipoPropriedade.Estação,20000, 2500, 10000, 0, 0, 0, 0, 0),
                new Propriedade(6,"Avenida Almirante Reis (Lisboa)",TipoPropriedade.Blue,10000, 600, 5000, 3000, 9000, 27000, 40000, 55000),
                new CartasSorte(7,"SORTE"),
                new Propriedade(8,"Avenida TODI (Setúbal)",TipoPropriedade.Blue,10000, 600, 5000, 3000, 9000, 27000, 40000, 55000),
                new Propriedade(9,"Av. 24 de Julho (Lisboa)",TipoPropriedade.Blue,12000, 800, 6000, 4000, 10000, 30000, 45000, 60000),
                new EspacosEspeciais(10,"Cadeia"),
                new Propriedade(11,"Av. Central (Braga)",TipoPropriedade.Pink,14000, 1000, 7000, 5000, 15000, 45000, 62500, 75000),
                new Propriedade(12,"Companhia Eletrecidade",TipoPropriedade.Companhia,15000, 400, 7500, 0, 0, 0, 0, 0),
                new Propriedade(13,"Rua Ferreira Borges (Coimbra)",TipoPropriedade.Pink, 14000, 1000, 7000, 5000, 15000, 45000, 62500, 75000),
                new Propriedade(14,"Av. de Roma (Lisboa)",TipoPropriedade.Pink,16000, 1200, 8000, 6000, 18000, 50000, 70000, 90000),
                new Propriedade(15,"Estação de Campanhã",TipoPropriedade.Estação, 20000, 2500, 10000, 0, 0, 0, 0, 0),
                new Propriedade(16,"Avenida da Boavista (Porto)",TipoPropriedade.Orange,18000, 1400, 9000, 7000, 20000, 55000, 75000, 95000),
                new CartasSorte(17,"Caixa da Comunidade"),
                new Propriedade(18,"Avenida da República (Lisboa)",TipoPropriedade.Orange, 18000, 1400, 9000, 7000, 20000, 55000, 75000, 95000),
                new Propriedade(19,"Rua de Sá da Bandeira (Porto)",TipoPropriedade.Orange, 20000, 1600, 10000, 8000, 22000, 60000, 80000, 100000),
                new EspacosEspeciais(20,"Estacionamento Livre"),
                new Propriedade(21,"Rua de Santa Catarina (Porto)",TipoPropriedade.Red, 22000, 1800, 11000, 9000, 25000, 70000, 87500, 105000),
                new CartasSorte(22,"SORTE"),
                new Propriedade(23,"Praça de Alvalade (Lisboa)",TipoPropriedade.Red, 22000, 1800, 11000, 9000, 25000, 70000, 87500, 105000),
                new Propriedade(24,"Rua Júlio Diniz (Porto)",TipoPropriedade.Red, 24000, 2000, 12000, 10000, 30000, 75000, 92500, 110000),
                new Propriedade(25,"Estação de S.Bento",TipoPropriedade.Estação, 20000, 2500, 10000, 0, 0, 0, 0, 0),
                new Propriedade(26,"Rua 31 de Janeiro (Porto)",TipoPropriedade.Yellow, 26000, 2200, 13000, 11000, 33000, 80000, 97500, 115000),
                new Propriedade(27,"Av. Fontes Pereira de Melo (Lisboa)",TipoPropriedade.Yellow, 26000, 2200, 13000, 11000, 33000, 80000, 97500, 115000),
                new Propriedade(28,"Companhia das Águas",TipoPropriedade.Companhia, 15000, 2000, 7500, 0, 0, 0, 0, 0),
                new Propriedade(29,"Avenida dos Aliados (Porto)",TipoPropriedade.Yellow, 28000, 2400, 14000, 12000, 36000, 85000, 102500, 120000),
                new EspacosEspeciais(30,"Vá para a Cadeia!"),
                new Propriedade(31,"Avenida da Liberdade (Lisboa)",TipoPropriedade.Green, 30000, 2600, 15000, 13000, 39000, 90000, 110000, 127500),
                new Propriedade(32,"Praça da Liberdade (Porto)",TipoPropriedade.Green, 30000, 2600, 15000, 13000, 39000, 90000, 110000, 127500),
                new CartasSorte(33,"Caixa da Comunidade"),
                new Propriedade(34,"Rua do Ouro (Lisboa)",TipoPropriedade.Green, 32000, 2800, 16000, 15000, 45000, 100000, 120000, 140000),
                new Propriedade(35,"Estação de St Apolónia",TipoPropriedade.Estação, 20000, 2500, 10000, 0, 0, 0, 0, 0),
                new CartasSorte(36,"SORTE"),
                new Propriedade(37,"Rua Augusta (Lisboa)",TipoPropriedade.Purple, 35000, 3500, 17500, 17500, 50000, 110000, 130000, 150000),
                new Imposto(38,"Imposto de Luxo",10000),
                new Propriedade(39,"Rossio (Lisboa)",TipoPropriedade.Purple, 40000, 5000, 20000, 20000, 60000, 140000, 170000, 200000),
                };
        }

        // Adiciona uma Propriedade a um jogador
        public static void AddPropriedadeJogador(int propriedadeIndex, int jogadorIndex)
        {
            Propriedade propriedadeAtual = (Propriedade)tabuleiro[propriedadeIndex];
            propriedadeAtual.Dono = jogadores[jogadorIndex];

            jogadores[jogadorIndex].Propriedades.Add(propriedadeAtual);
            jogadores[jogadorIndex].RetirarDinheiro(propriedadeAtual.Preco);
        }

        //public static void 

        public static bool VerPropriedadeDonoNull (int propriedadeIndex)
        {
            Propriedade propriedadeAtual = (Propriedade)tabuleiro[propriedadeIndex];
           
            if (propriedadeAtual.Dono == null)
            {
                return true;

            } else
            {
                return false;
            }
            
        }

        public static Color verCorProprietario (int propriedadeIndex)
        {
            Propriedade propriedadeAtual = (Propriedade)tabuleiro[propriedadeIndex];

            if (propriedadeAtual.Dono == null)
            {
                return Color.Black;

            }
            else
            {
                return propriedadeAtual.Dono.cor;
            }

            
        }
        /*public static String verPeça(int propriedadeIndex)
        {
            Propriedade propriedadeAtual = (Propriedade)tabuleiro[propriedadeIndex];

            if (propriedadeAtual.Dono == null)
            {
                return Color.Black;

            }
            else
            {
                return propriedadeAtual.Dono.cor;
            }


        }*/

    }
}

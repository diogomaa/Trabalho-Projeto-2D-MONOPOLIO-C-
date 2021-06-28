using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Classes
{
    public static class RetirarCartaSorte
    {
        private static readonly Random rng = new Random();
        public static bool continuar = false;
        public static bool NovaCarta = false;

        private static List<Func<Jogador, string>> listaCartasSorte = new List<Func<Jogador, string>>
        {

            PagueEmbriaguez,
            Recue3Casas,
            AvanceRossio,
            AvanceAvCentral,
            DespesasEscolares,
            VaiPreso,
            PalavrasCruzadas,
            Aniversario,
            AvanceCasaPartida,
            AvanceEstacaoCampanha,
            BancaDividendos,
            RecebaEmprestimoConstrucao,
            Receba7JurosAcoes,
            RecebaRendaAnual,
            PagueContaHospital,
            RecueCampoGrande,
            PagueSeguro,
            ReembolsoImpostoCapitais,
            ErroBanca,
            ContaMedico,
            Heranca,
            VendaStock,
            SegundoConcursoBeleza,
            MultaVelocidade,
            ReparacoesRua,               
            ReparacoesCasas,           
            AvanceJulioDiniz,
            PagueMultaOuSorte,           
            LivreCadeia,                  

        };

        private static string PagueEmbriaguez(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("MULTA POR EMBRIAGUEZ.\nPAGUE 20.000€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(20000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string Recue3Casas(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECUE TRÊS CASAS.");
            c.ShowDialog();

            jogador.SetPosicao(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual - 3);

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]); ;
            
        }

        private static string AvanceRossio(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("AVANCE ATÉ AO ROSSIO (LISBOA)\nRECEBE 20000€.");
            c.ShowDialog();

            jogador.posicaoAtual = 39;
            jogador.Depositar(20000);

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
        }

        private static string AvanceAvCentral(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("AVANCE ATÉ À AV. CENTRAL (BRAGA).\n SE PASSAR PELA CASA PARTIDA RECEBA\n20.000€.");
            c.ShowDialog();

            int ap = jogador.posicaoAtual;

            jogador.posicaoAtual = 11;

            if (ap > 11)
            {
                jogador.Depositar(20000);
            }

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
        }

        private static string DespesasEscolares(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("PAGUE POR DESPESAS ESCOLARES,\n15.000€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(15000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string VaiPreso(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VÁ PARA A CADEIA.\nVÁ DIRETAMENTE PARA A CADEIA, SEM PASSAR\nNA CASA PARTIDA E SEM RECEBER\n20.000€.");
            c.ShowDialog();

            jogador.SetPosicao(10);
            jogador.estaPreso = true;
            jogador.turnosPrisao = 3;

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].Nome;
        }

        private static string PalavrasCruzadas(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VOCÊ GANHOU O CONCURSO DAS\nPALAVRAS CRUZADAS,\nRECEBA 10.000€.");
            c.ShowDialog();

            jogador.Depositar(10000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string Aniversario(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("É O SEU ANIVERSÁRIO.\nRECEBA 1.000€ DE CADA JOGADOR.");
            c.ShowDialog();

            Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].RetirarDinheiro(30);
            Tabuleiro.jogadores[(Tabuleiro.AtualJogadorIndex + 1) % 2].Depositar(30);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string AvanceCasaPartida(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("AVANCE ATÉ À CASA PARTIDA.");
            c.ShowDialog();

            jogador.SetPosicao(0);

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]); 
        }

        private static string AvanceEstacaoCampanha(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VÁ ATÉ À ESTAÇÃO DE CAMPANHÃ.\nSE PASSAR NA CASA PARTIDA\nRECEBA 20.000€.");
            c.ShowDialog();

            int ap = jogador.posicaoAtual;

            jogador.SetPosicao(15);

            if (ap > 15)
            {
                jogador.Depositar(20000);
            }

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
        }

        private static string BancaDividendos(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("A BANCA PAGA-LHE DIVIDENDOS\nNO VALOR DE 5.000€.");
            c.ShowDialog();

            jogador.Depositar(5000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string RecebaEmprestimoConstrucao(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECEBA O SEU EMPRÉSTIMO PARA CONSTRUÇÃO,\n15.000€.");
            c.ShowDialog();

            jogador.Depositar(15000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string Receba7JurosAcoes(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECEBA 7% DE JUROS SOBRE AS SUAS AÇÕES,\n2.500€.");
            c.ShowDialog();

            jogador.Depositar(2500);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string RecebaRendaAnual(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECEBA A SUA RENDA ANUAL,\n10.000€.");
            c.ShowDialog();

            jogador.Depositar(10000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string PagueContaHospital(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("PAGUE A CONTA DO HOSPITAL,\n10.000€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(10000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string RecueCampoGrande(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECUE ATÉ AO CAMPO GRANDE (LISBOA).");
            c.ShowDialog();

            jogador.SetPosicao(1);

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
        }

        private static string PagueSeguro(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("PAGUE O SEU SEGURO,\n5.000€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(5000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string ReembolsoImpostoCapitais(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECEBA, POR REEMBOLSO DO IMPOSTO SOBRE CAPITAIS,\n2.000€.");
            c.ShowDialog();

            jogador.Depositar(2000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string ErroBanca(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("ERRO DA BANCA A SEU FAVOR. RECEBA\n20.000€.");
            c.ShowDialog();

            jogador.Depositar(20000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string ContaMedico(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("PAGUE A CONTA DO MÉDICO,\n5.000€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(5000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string Heranca(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VOCÊ HERDOU\n10.000€.");
            c.ShowDialog();

            jogador.Depositar(10000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string VendaStock(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("RECEBA, PELA VENDA DO SEU STOCK,\n5.000€.");
            c.ShowDialog();

            jogador.Depositar(5000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string SegundoConcursoBeleza(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VOCÊ FICOU EM SEGUNDO LUGAR NUM\nCONCURSO DE BELEZA. RECEBA\n1.000€.");
            c.ShowDialog();

            jogador.Depositar(1000);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string MultaVelocidade(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("MULTA, POR EXCESSO DE VELOCIDADE,\n1.500€.");
            c.ShowDialog();

            jogador.RetirarDinheiro(1500);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string ReparacoesRua(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VOCÊ TEM DE PAGAR, PARA REPARAÇÕES DA RUA:\n     POR CASA          4.000€\n     POR HOTEL          11.500€");
            c.ShowDialog();

            // Vai buscar a posição atual do jogador
            int pa = jogador.posicaoAtual;
            int posProp = 0;
            int valor = 0;
 
            if (jogador.Propriedades.Count > 0)
            {
                // Percorre as propriedades do jogador uma a uma
                for (int i = 0; i < jogador.Propriedades.Count; i++)
                {
                    if (jogador.Propriedades[i].Casas > 0)
                    {
                        posProp = jogador.Propriedades[i].Index;
                    }

                    if(VerMesmaRua(pa,posProp))
                    {
                        if (jogador.Propriedades[i].Casas < 5)
                        {
                            valor += jogador.Propriedades[i].Casas * 4000;
                        } else
                        {
                            valor += 11500;
                        }
                    }
                }
            }

            jogador.RetirarDinheiro(valor);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string ReparacoesCasas(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("FAÇA REPARAÇÕES EM TODAS AS SUAS CASAS:\n   PAGUE, POR CADA CASA         2.500€\n   PAGUE, POR CADA HOTEL          10.000€");
            c.ShowDialog();

            // Vai buscar a posição atual do jogador
            int pa = jogador.posicaoAtual;
            int posProp = 0;
            int valor = 0;

            if (jogador.Propriedades.Count > 0)
            {
                // Percorre as propriedades do jogador uma a uma
                for (int i = 0; i < jogador.Propriedades.Count; i++)
                {
                    if (jogador.Propriedades[i].Casas > 0)
                    {
                        posProp = jogador.Propriedades[i].Index;
                    }

                    if (jogador.Propriedades[i].Casas < 5)
                    {
                        valor += jogador.Propriedades[i].Casas * 2500;
                    }
                    else
                    {
                        valor += 1000;
                    }
                }
            }

            jogador.RetirarDinheiro(valor);

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string AvanceJulioDiniz(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("AVANCE ATÉ À RUA JÚLIO DINIZ (PORTO).\nSE PASSAR NA CASA PARTIDA\nRECEBA 20.000€.");
            c.ShowDialog();

            int ap = jogador.posicaoAtual;

            jogador.SetPosicao(24);

            if (ap > 24)
            {
                jogador.Depositar(20000);
            }

            return Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
        }

        private static string PagueMultaOuSorte(Jogador jogador)
        {
            continuar = false;
            NovaCarta = false;

            MostrarCartasSorte c = new MostrarCartasSorte("PAGUE UMA MULTA DE 1.000€ OU,\nRETIRE OUTRA CARTA.");
            c.ShowDialog();

            if(continuar)
            {
                if(NovaCarta)
                {
                    Tabuleiro.tabuleiro[Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual].ActOnPlayer(Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex]);
                } else
                {
                    jogador.RetirarDinheiro(1000);
                }
            }

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        private static string LivreCadeia(Jogador jogador)
        {
            MostrarCartasSorte c = new MostrarCartasSorte("VOCÊ ESTÁ LIVRE DA CADEIA.\nESTA CARTA PODE SER CONSERVADA\nATÉ SER NECESSÁRIA\n\nMAX 1 p/ Jogador.");
            c.ShowDialog();

            jogador.CartaCadeia = true;

            if (SorteOuCaixa()) return "SORTE"; else return "CAIXA DA COMUNIDADE";
        }

        public static string GenerateRandomCard(Jogador jogador)
        {
            listaCartasSorte = listaCartasSorte
                .OrderBy(x => rng.Next())
                .ToList();

            Func<Jogador, string> randomChanceCard = listaCartasSorte[rng.Next(0, 28)];

            return randomChanceCard.Invoke(jogador);
        }

        private static bool SorteOuCaixa()
        {
            // TRUE se for SORTE
            // FALSE se for Caixa

            if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 7 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 22 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 36)
            {
                return true;
                
            }
            else if (Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 2 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 17 || Tabuleiro.jogadores[Tabuleiro.AtualJogadorIndex].posicaoAtual == 33)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private static bool VerMesmaRua(int posJog, int posProp)
        {
            if (posJog < 10)
            {
                // Se estiver na rua entre 0 e 10
                if (posProp < 10) return true;
            }
            else if (posJog < 20)
            {
                // Se estiver na rua entre 10 e 20
                if(posProp < 20) return true;
            }
            else if (posJog < 30)
            {
                // Se estiver na rua entre 20 e 30
                if(posProp < 30) return true;
            }
            else if (posJog < 40)
            {
                // Se estiver na rua entre 30 e 40
                if(posProp < 40) return true;
            }

            return false;
        }

    }
}

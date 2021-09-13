using System;
using System.Collections.Generic;
using System.Text;
using xadrez_console.Tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console {
    class Tela {
        public static void imprimirPartida(PartidaDeXadrez partida) {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
            if (partida.Xeque) {
                Console.WriteLine("XEQUE!");
            }
        }
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida) {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas:");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas:");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void imprimirConjunto(HashSet<Peca> conjunto) {
            Console.Write("[");
            foreach (var item in conjunto){
                Console.Write(item + " ");
            }
            Console.Write("]");
        }
        public static void imprimirTabuleiro(MesaDeTabuleiro tab) {
            for (int i = 0; i < tab.Linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++) {
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirTabuleiro(MesaDeTabuleiro tab, bool[,] posicoesPossiveis) {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++) {
                    if (posicoesPossiveis[i, j]) {
                        Console.BackgroundColor = fundoAlterado;
                    } else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez lerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("- ");
            } else {
                if (peca.Cor == Cor.Branca) {
                    Console.Write(peca);
                } else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }



        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using xadrez_console.Tabuleiro;

namespace xadrez_console {
    class Tela {
        public static void imprimirTabuleiro (MesaDeTabuleiro tab) {
            for (int i = 0; i < tab.Linhas; i++) {
                for (int j = 0; j < tab.Colunas; j++) {
                    if(tab.peca(i,j) == null) {
                        Console.Write("- ");
                    } else {
                        Console.Write(tab.peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

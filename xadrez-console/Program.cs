using System;
using xadrez_console.Tabuleiro;
using xadrez_console.xadrez;
namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            try {
                var tab = new MesaDeTabuleiro(8, 8);
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

                tab.colocarPeca(new Torre(tab, Cor.Branca), new Posicao(3, 5));
                Tela.imprimirTabuleiro(tab);
            }catch(TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}

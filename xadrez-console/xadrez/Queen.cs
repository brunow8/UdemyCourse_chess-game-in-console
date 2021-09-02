using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Queen : Peca {
        public Queen(MesaDeTabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "Q";
        }
    }
}
using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Cavalo : Peca {
        public Cavalo(MesaDeTabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "C";
        }
    }
}
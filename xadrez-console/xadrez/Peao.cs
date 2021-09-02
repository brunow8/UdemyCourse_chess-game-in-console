using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Peao : Peca {
        public Peao(MesaDeTabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "P";
        }
    }
}
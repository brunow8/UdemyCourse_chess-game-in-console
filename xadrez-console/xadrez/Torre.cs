using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Torre : Peca {
        public Torre(MesaDeTabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "T";
        }
    }
}

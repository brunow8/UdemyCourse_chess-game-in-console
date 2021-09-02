using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Bispo : Peca {
        public Bispo(MesaDeTabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "B";
        }
    }
}
using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class Rei : Peca {
        public Rei (MesaDeTabuleiro tab, Cor cor) : base(tab, cor) { 
        }
        public override string ToString() {
            return "R";
        }
    }
}

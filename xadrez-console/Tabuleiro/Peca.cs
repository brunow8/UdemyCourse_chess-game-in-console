namespace xadrez_console.Tabuleiro {
    class Peca {
        public Posicao Posicao { get; set; }
        public Cor Cor { get;  protected set; }
        public int QtdMovimentos { get; protected set; }
        public MesaDeTabuleiro Tab { get; protected set; }

        public Peca(MesaDeTabuleiro tab, Cor cor) {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }
    }
}

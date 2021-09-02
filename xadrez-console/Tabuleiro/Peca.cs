namespace xadrez_console.Tabuleiro {
    class Peca {
        public Posicao Posicao { get; set; }
        public Cor Cor { get;  protected set; }
        public int QtdMovimentos { get; protected set; }
        public MesaDeTabuleiro Tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, MesaDeTabuleiro tab) {
            Posicao = posicao;
            Cor = cor;
            Tab = tab;
            QtdMovimentos = 0;
        }
    }
}

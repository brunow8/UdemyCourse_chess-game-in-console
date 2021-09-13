namespace xadrez_console.Tabuleiro {
    abstract class Peca {
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
        public bool existemMovimentosPossiveis() {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++) {
                for (int j = 0; j < Tab.Colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool podeMoverPara(Posicao pos) {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] movimentosPossiveis();

        public void incrementarQtdMovimento() {
            QtdMovimentos++;
        }
        public void decrementarQtdMovimento() {
            QtdMovimentos--;
        }
    }
}

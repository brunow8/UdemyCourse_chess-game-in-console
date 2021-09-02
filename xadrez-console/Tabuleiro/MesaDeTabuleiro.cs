namespace xadrez_console.Tabuleiro {
    class MesaDeTabuleiro {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public MesaDeTabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas, colunas]; 
        }
        public Peca peca(int linha, int coluna) {
            return pecas[linha, coluna];
        }
        public void colocarPeca(Peca p, Posicao pos) {
            pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }
    }
}

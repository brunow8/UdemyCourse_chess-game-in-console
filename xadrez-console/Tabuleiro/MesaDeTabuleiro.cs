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
    }
}

using System.Collections.Generic;
using xadrez_console.Tabuleiro;
namespace xadrez_console.xadrez {
    class PartidaDeXadrez {
        public MesaDeTabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual;
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            this.tab = new MesaDeTabuleiro(8, 8);
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;
            colocarPecas();
        }
        public Peca executaMovimento (Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca pecacapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino); 
            if(pecacapturada != null) {
                capturadas.Add(pecacapturada);
            }
            //#jogadaespecial roque pequeno 
            if(p is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimento();
                tab.colocarPeca(T, destinoT);
            }
            //#jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimento();
                tab.colocarPeca(T, destinoT);
            }
            return pecacapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecacapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimento();
            if(pecacapturada != null){
                tab.colocarPeca(pecacapturada, destino);
                capturadas.Remove(pecacapturada);
            }
            tab.colocarPeca(p, origem);

            //jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destino);
                T.decrementarQtdMovimento();
                tab.colocarPeca(T, origemT);
            }
            //#jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimento();
                tab.colocarPeca(T, origemT);
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca pecacapturada = executaMovimento(origem, destino);
            if (estaEmXeque(JogadorAtual)) {
                desfazMovimento(origem, destino, pecacapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (estaEmXeque(adversaria(JogadorAtual))) {
                Xeque = true;
            } else { 
                Xeque = false;
            }
            if (testeXequeMate(adversaria(JogadorAtual))) {
                terminada = true;
            } else {
                Turno++;
                mudaJogador();
            }
        }
        public void validarPosicaoOrigem(Posicao pos) {
            if(tab.peca(pos) == null){
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != tab.peca(pos).Cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existemMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida");
            }

        }
        public void validarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        public void mudaJogador() {
            if(JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            } else {
                JogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var item in capturadas) {
                if(item.Cor == cor) {
                    aux.Add(item);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var item in pecas) {
                if (item.Cor == cor) {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor) {
            if(cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor) {
            foreach(Peca x in pecasEmJogo(cor)) {
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor) {
            Peca R = rei(cor);
            if(R == null) {
                throw new TabuleiroException("Não tem rei da cor" + cor + " no tabuleiro.");
            }
            foreach (var item in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = item.movimentosPossiveis();
                if(mat[R.Posicao.Linha, R.Posicao.Coluna]) {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequeMate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (var item in pecasEmJogo(cor)) {
                bool[,] mat = item.movimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++) {
                    for (int j = 0; j < tab.Colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = item.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecacapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecacapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }

                }
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));


            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
        }
    }
}

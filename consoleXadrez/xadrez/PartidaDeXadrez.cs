using consoleXadrez.tabuleiro;
using System;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }  //mudança do private para public, porem apenas com get para visualização
        public int Turno { get; private set; } //mudança do private para public, porem apenas com get para visualização
        public Cor JogadorAtual { get; private set; } //mudança de private para public, porém apenas com o get para visualização
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez() //predefinições de um começo de jogo
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem); /*Executar o movimento é retirar sua peça do lugar e mover para outro lugar e introdução em Peca.cs*/
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino); //execução apenas se não ouver peça, método criado em Tabuleiro.cs
            Tab.colocarPeca(p, destino);
            if (pecaCapturada != null) //lógica para adicionar as peças capturadas 
            {
                Capturadas.Add(pecaCapturada);
            }

            //jogada roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                Tab.colocarPeca(T, destinoT);
            }
            //jogada roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                Tab.colocarPeca(T, destinoT);
            }

            //jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.retirarPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);

            //jogada roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, origemT);
            }

            //jogada roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, origemT);
            }

            //jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) /* inclusão para uma jogada de cada vez entre branco e preto*/
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque");
            }
            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (testeXequemate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }

            Peca p = Tab.peca(destino);

            //jogada en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos) //exception 
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida");
            }
            if (JogadorAtual != Tab.peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é a sua");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de Destino inválida");
            }
        }

        private void mudaJogador() /* lógica de mudança de jogador para o método realizarJogada*/
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) //separação de cor para cada peca capturada
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor)) //aqui se usa o "is" pois é uma subclasse para testar se a instancia da superclasse Peca possui a subclasse Rei
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor) //método para testar o Xequemate
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(x.Posicao, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //método para refatorar
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void colocarPecas() //Refatoração
        {
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branco));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branco));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco, this));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branco));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('a', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('b', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('c', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('d', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('e', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('f', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('g', 2, new Peao(Tab, Cor.Branco, this));
            colocarNovaPeca('h', 2, new Peao(Tab, Cor.Branco, this));

            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preto));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preto, this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('a', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('b', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('c', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('d', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('e', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('f', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('g', 7, new Peao(Tab, Cor.Preto, this));
            colocarNovaPeca('h', 7, new Peao(Tab, Cor.Preto, this));


            //REFATORADO
            //Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c',1).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 2).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('d', 2).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 2).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 1).toPosicao());
            //Tab.colocarPeca(new Rei(Tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());

            //Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 8).toPosicao());
            //Tab.colocarPeca(new Rei(Tab, Cor.Preto), new PosicaoXadrez('d', 8).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 8).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 7).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('d', 7).toPosicao());
            //Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 7).toPosicao());
        }
    }
}

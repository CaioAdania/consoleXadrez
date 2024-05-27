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

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem); /*Executar o movimento é retirar sua peça do lugar e mover para outro lugar e introdução em Peca.cs*/
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino); //execução apenas se não ouver peça, método criado em Tabuleiro.cs
            Tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino) /* inclusão para uma jogada de cada vez entre branco e preto*/
        {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(Tab.peca(pos) == null)
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
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de Destino inválida");
            }
        }

        private void mudaJogador() /* lógica de mudança de jogador para o método realizarJogada*/
        {
            if(JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        private void colocarPecas()
        {
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c',1).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('d', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 2).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 1).toPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());

            Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 8).toPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Preto), new PosicaoXadrez('d', 8).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 8).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('c', 7).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('d', 7).toPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('e', 7).toPosicao());

        }
    }
}

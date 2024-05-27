﻿using consoleXadrez.tabuleiro;
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

        public PartidaDeXadrez() //predefinições de um começo de jogo
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem); /*Executar o movimento é retirar sua peça do lugar e mover para outro lugar e introdução em Peca.cs*/
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino); //execução apenas se não ouver peça, método criado em Tabuleiro.cs
            Tab.colocarPeca(p, destino);
            if(pecaCapturada != null) //lógica para adicionar as peças capturadas 
            {
                Capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) /* inclusão para uma jogada de cada vez entre branco e preto*/
        {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos) //exception 
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

        public HashSet<Peca> pecasCapturadas(Cor cor) //separação de cor para cada peca capturada
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) 
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Pecas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //método para refatorar
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void colocarPecas() //Refatoração
        {
            colocarNovaPeca('c', 1, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('c', 2, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('d', 2, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('e', 2, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('e', 1, new Torre(Tab, Cor.Branco));
            colocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));

            colocarNovaPeca('c', 8, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('d', 8, new Rei(Tab, Cor.Preto));
            colocarNovaPeca('e', 8, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('c', 7, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('d', 7, new Torre(Tab, Cor.Preto));
            colocarNovaPeca('e', 7, new Torre(Tab, Cor.Preto));

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

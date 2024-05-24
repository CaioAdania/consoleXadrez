using consoleXadrez.tabuleiro;
using System;


namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[ , ] pecas; //matriz

        public Tabuleiro(int linhas, int colunas) //construtor
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas, colunas]; //recebe como argumento
        }

        public Peca peca(int linha, int coluna) //método para conseguir retornar a matriz Peca que é private em Tela.cs
        {
            return pecas[linha, coluna];
        } 
        public Peca peca(Posicao pos) //sobrecarga, pois Peca é private
        {
            return pecas[pos.Linha, pos.Coluna];
        }

        public bool existePeca(Posicao pos) //caso alguma peça ja esteja no lugar será lançado uma exceção
        {
            validarPosicao(pos); //se a posição for inválida lança a excessão
            return peca(pos) != null; //se isso for verdade, existe uma peça nesta posição
        }
        public void colocarPeca(Peca p, Posicao pos) //método para ir na matriz em que foi escolhida 
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição");//tratamento caso tenha uma peça na posição ja
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public bool posicaoValida(Posicao pos) //método para testar se a posição é válida ou não
        {
            if (pos.Linha<0 || pos.Linha>=Linhas || pos.Coluna<0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) //excessão personalizada
        {
            if (!posicaoValida(pos)) //se a pos não for valida.
            {
                throw new TabuleiroException("Posição Inválida");
            }
        }
    }
}

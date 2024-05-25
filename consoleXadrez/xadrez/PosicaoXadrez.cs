using System;
using tabuleiro;

namespace xadrez
{
    internal class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a'); /*no caso do '8', o tabuleiro do xadrez começa de baixo, logo se fosse posição 3, teria que ser no lugar no vetor 5, 8-5=3 
                                                          já no caso da coluna foi definido 'a', pois internamente 'a' é um numero inteiro 1, logo se for 1-a= 0, caracterizando o vetor 0*/
        }
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}

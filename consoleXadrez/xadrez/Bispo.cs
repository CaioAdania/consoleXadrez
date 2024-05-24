using consoleXadrez.tabuleiro;
using System;
using tabuleiro;

namespace xadrez
{
    internal class Bispo : Peca //subclasse de Peca.cs
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "B";
        }
    }
}

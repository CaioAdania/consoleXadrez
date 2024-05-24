using consoleXadrez.tabuleiro;
using System;
using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca //subclasse de Peca.cs
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) //base para repassar os atributos da superclasse
        {

        }
        public override string ToString()
        {
            return "T";
        }
    }
}

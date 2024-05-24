using consoleXadrez.tabuleiro;
using System;
using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca //subclasse de Peca.cs
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) //base para repassar os atributos da superclasse
        {

        }
        public override string ToString()
        {
            return "R";
        }
    }
}

using consoleXadrez.tabuleiro;
using System;
using tabuleiro;

namespace xadrez
{
    internal class Dama : Peca //subclasse de Peca.cs
    {
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor) //base para repassar os atributos da superclasse
        {

        }
        public override string ToString()
        {
            return "D";
        }
    }
}

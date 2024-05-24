using System;
using tabuleiro;

namespace consoleXadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.imprimirTabuleiro(tab);

        }
    }
}

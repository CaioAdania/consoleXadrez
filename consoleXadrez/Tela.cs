using tabuleiro;
using System;


namespace consoleXadrez
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab) //método estático
        {
            for(int i = 0; i<tab.Linhas; i++) //logica para imprimir -
            {
                for (int j = 0; j < tab.Colunas; j++) 
                {
                    if(tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.peca(i,j) + " "); //em Tabuleiro.cs Peca é private, e em Tabuleiro.cs criamos um método public para ser acessavél 
                    }                    
                }
                Console.WriteLine(); //quebrar a linha ao fim do for
            }
        }
    }
}

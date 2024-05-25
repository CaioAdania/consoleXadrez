using tabuleiro;
using System;
using consoleXadrez.tabuleiro;


namespace consoleXadrez
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab) //método estático
        {
            for(int i = 0; i<tab.Linhas; i++) //logica para imprimir -
            {
                Console.Write(8-i + " "); //logica para adicionar os números das linhas
                for (int j = 0; j < tab.Colunas; j++) 
                {
                    if(tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j)); //instância do static void imprimirTela, para manter o padrão de colores implementado
                        Console.Write(" ");
                        
                        /*Console.Write(tab.peca(i,j) + " "); //em Tabuleiro.cs Peca é private, e em Tabuleiro.cs criamos um método public para ser acessivel 
                          Referência para lembrar do começo, em que a implementação de um private, era feito por um método público para ser acessivel
                         */
                    }                    
                }
                Console.WriteLine(); //quebrar a linha ao fim do for
            }
            Console.WriteLine("  a b c d e f g h"); //lógica para imprimir o identificação das colunas
        }
        public static void imprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor; /* comando Css, para salvar a cor padrão do background color padrão do console (no caso cinza)*/
                Console.ForegroundColor = ConsoleColor.Yellow; /* definindo o padrão do background color para yellow*/
                Console.Write(peca); /* aplicando a cor Yellow, caso a peça seja diferente de Cor.Branco, logo sera impresso na cor Yellow*/
                Console.ForegroundColor = aux; /* após a aplicação do Yellow, de volta a cor padrão */
            }
        }
    }
}

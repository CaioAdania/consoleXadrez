﻿using tabuleiro;
using System;
using consoleXadrez.tabuleiro;
using xadrez;
using System.Collections.Generic;


namespace consoleXadrez
{
    internal class Tela
    {

        public static void imprimirPartida(PartidaDeXadrez partida) //refatoração para limpar assembly
        {
            imprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }            
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida) //refatoração para limpar assembly e introdução de pecas capturadas
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) //mostrar peças capturadas
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("] ");
        }
        public static void imprimirTabuleiro(Tabuleiro tab) //método estático
        {
            for (int i = 0; i < tab.Linhas; i++) //logica para imprimir -
            {
                Console.Write(8 - i + " "); //logica para adicionar os números das linhas
                for (int j = 0; j < tab.Colunas; j++)
                {                                                            
                    imprimirPeca(tab.peca(i, j)); //instância do static void imprimirTela, para manter o padrão de colores implementado                                                                                
                }
                Console.WriteLine(); //quebrar a linha ao fim do for
            }
            Console.WriteLine("  a b c d e f g h"); //lógica para imprimir o identificação das colunas
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) //método estático sobrecarga
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++) //logica para imprimir -
            {
                Console.Write(8 - i + " "); //logica para adicionar os números das linhas
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j)); //instância do static void imprimirTela, para manter o padrão de colores implementado
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine(); //quebrar a linha ao fim do for
            }
            Console.WriteLine("  a b c d e f g h"); //lógica para imprimir o identificação das colunas
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {                           
                if (peca.Cor == Cor.Branco)
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
                Console.Write(" ");
            }
        }
    }
}


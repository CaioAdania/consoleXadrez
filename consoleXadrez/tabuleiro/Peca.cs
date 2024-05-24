using tabuleiro;

namespace consoleXadrez.tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro Tab {  get; protected set; }

        public Peca(Tabuleiro tab, Cor cor) //construtor
        {
            Posicao = null; //quando cria um tabuleiro, ela nao tem posição, logo é nulo.
            Tab = tab;
            Cor = cor;
            qteMovimentos = 0;
        }
    }
}

using tabuleiro;

namespace consoleXadrez.tabuleiro
{
    abstract class Peca //quando a classe possui ao menos um método abstract, a classe se torna abstract também
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

        public void incrementarQtdMovimentos() /*lá em PartidaDeXAdrez.cs com a criação de executarMovimento devemos incrementar o número de movimentos que a peça fez*/
        {
            qteMovimentos++;
        }

        public void decrementarQtdMovimentos() 
        {
            qteMovimentos--;
        }

        public bool existeMovimentosPossiveis() //introdução de movimentos possiveis, lógica para possiveis movimentos
        {
            bool[,] mat = movimentosPossiveis();
            for(int i = 0; i < Tab.Linhas; i++)
            {
                for(int j=0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] movimentosPossiveis(); //definindo para onde cada peça pode se mover, por ser uma categoria generica "Peca" ser amplo, não da pra dizer exatamente qual peça pode o que
        
                       
    }
}

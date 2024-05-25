namespace tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna) //construtor 
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void definirValores(int linha, int coluna) //2 hrs de bug aqui, por esquecer de passar a definição
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return Linha
                + ": " + Coluna;
        }
    }
}

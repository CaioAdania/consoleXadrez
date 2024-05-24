using System;

namespace tabuleiro
{
    internal class TabuleiroException : Exception //herdar classe 
    {
        public TabuleiroException(string msg) : base(msg)
        {

        }
    }
}

using System;

public class Torre : Peca
{
    public Torre(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) : base(tabuleiro, cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        if ((Linha != novaLinha && Coluna != novaColuna))
        return false;
        
        return Tabuleiro.CaminhoLivre(Linha, Coluna, novaLinha, novaColuna);
    }
}
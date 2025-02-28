using System;

public class Bispo : Peca
{
    public Bispo(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) : base(tabuleiro, cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        if (Math.Abs(novaLinha - Linha) != Math.Abs(novaColuna - Coluna))
        return false;

        return Tabuleiro.CaminhoLivre(Linha, Coluna, novaLinha, novaColuna);
    }
}
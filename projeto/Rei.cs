using System;

public class Rei : Peca
{
    public Rei(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) : base(tabuleiro, cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int diffLinha = Math.Abs(novaLinha - Linha);
        int diffColuna = Math.Abs(novaColuna - Coluna);

        return diffLinha <= 1 && diffColuna <= 1;
    }
}
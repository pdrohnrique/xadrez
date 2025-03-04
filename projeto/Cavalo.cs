using System;

public class Cavalo : Peca
{
    public Cavalo(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) : base(tabuleiro, cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int diffLinha = Math.Abs(novaLinha - Linha);
        int diffColuna = Math.Abs(novaColuna - Coluna);

        return (diffLinha == 2 && diffColuna == 1) || (diffLinha == 1 && diffColuna == 2);
    }
}
using System;

public class Torre : Peca
{
    public Torre(Cor cor, int linha, int coluna) : base(cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        return (Linha == novaLinha) || (Coluna == novaColuna);
    }
}
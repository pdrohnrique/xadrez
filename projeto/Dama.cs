using System;

public class Dama : Peca
{
    public Dama(Cor cor, int linha, int coluna) : base(cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int diffLinha = Math.Abs(novaLinha - Linha);
        int diffColuna = Math.Abs(novaColuna - Coluna);

        return (Linha == novaLinha) || (Coluna == novaColuna) || diffLinha == diffColuna;
        
    }
}
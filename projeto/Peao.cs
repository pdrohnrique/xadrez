using System;

public class Peao : Peca
{
    private bool PrimeiroMovimento = true;

    public Peao(Cor cor, int linha, int coluna) : base(cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int direcao = Cor == Cor.Branco ? 1 : -1;
        int diffLinha = novaLinha - Linha;

        bool movimentoBasico = (diffLinha == direcao) || (PrimeiroMovimento && diffLinha == 2 * direcao);

        if(movimentoBasico) PrimeiroMovimento = false;

        return movimentoBasico && Coluna == novaColuna;
    }
}
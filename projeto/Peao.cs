using System;

public class Peao : Peca
{
    private bool PrimeiroMovimento = true;

    public Peao(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) : base(tabuleiro, cor, linha, coluna) {}

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int direcao = Cor == Cor.Branco ? -1 : 1;
        int diffLinha = novaLinha - Linha;
        int diffColuna = novaColuna - Coluna;

        if (diffColuna == 0)
        {
            if (diffLinha == direcao)
            return Tabuleiro.GetPeca(novaLinha, novaColuna) == null;

            if (diffLinha == 2 * direcao && Linha == (Cor == Cor.Branco ? 6 : 1))
            return Tabuleiro.GetPeca(novaLinha, novaColuna) == null && Tabuleiro.GetPeca(Linha + direcao, Coluna) == null;

            else if (Math.Abs(diffColuna) == 1 && diffLinha == direcao)
            {
                Peca? pecaDestino = Tabuleiro.GetPeca(novaLinha, novaColuna);
                return pecaDestino != null && pecaDestino.Cor != Cor;
            }

            return false;
        }

        bool movimentoBasico = (diffLinha == direcao) || (PrimeiroMovimento && diffLinha == 2 * direcao);

        if(movimentoBasico) PrimeiroMovimento = false;

        return movimentoBasico && Coluna == novaColuna;
    }
}
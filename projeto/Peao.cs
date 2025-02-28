using System;

public class Peao : Peca
{
    private bool _primeiroMovimento = true;

    public Peao(Tabuleiro tabuleiro, Cor cor, int linha, int coluna) 
        : base(tabuleiro, cor, linha, coluna) { }

    public override bool MovimentoValido(int novaLinha, int novaColuna)
    {
        int direcao = Cor == Cor.Branco ? -1 : 1; // Branco sobe (linhas menores), Preto desce
        int diffLinha = novaLinha - Linha;
        int diffColuna = novaColuna - Coluna;

        // Movimento para frente (sem captura)
        if (diffColuna == 0)
        {
            // Movimento de 1 casa
            if (diffLinha == direcao)
            {
                return Tabuleiro.GetPeca(novaLinha, novaColuna) == null;
            }
            // Movimento de 2 casas (apenas no primeiro movimento)
            else if (diffLinha == 2 * direcao && _primeiroMovimento)
            {
                bool caminhoLivre = Tabuleiro.GetPeca(Linha + direcao, Coluna) == null &&
                                    Tabuleiro.GetPeca(novaLinha, novaColuna) == null;
                return caminhoLivre;
            }
            else
            {
                return false;
            }
        }
        // Captura na diagonal (diffColuna = ±1)
        else if (Math.Abs(diffColuna) == 1 && diffLinha == direcao)
        {
            Peca? pecaDestino = Tabuleiro.GetPeca(novaLinha, novaColuna);
            return pecaDestino != null && pecaDestino.Cor != Cor;
        }

        return false;
    }

    public override void AtualizarPosicao(int novaLinha, int novaColuna)
    {
        base.AtualizarPosicao(novaLinha, novaColuna);
        _primeiroMovimento = false; // Desativa após qualquer movimento
    }
}
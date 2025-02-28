using System;

public class Tabuleiro
{
    public Cor JogadorAtual { get; private set; } = Cor.Branco;

    public void ProximoTurno()
    {
        JogadorAtual = JogadorAtual == Cor.Branco ? Cor.Preto : Cor.Branco;
    }
    
    private Peca?[,] pecas;

    public Tabuleiro()
    {
        pecas = new Peca[8, 8];
        InicializarTabuleiro();
    }

    public void LimparTabuleiro()
    {
        pecas = new Peca[8, 8];
    }

    public void AdicionarPeca(Peca peca, int linha, int coluna)
    {
        pecas[linha, coluna] = peca;
    }

    public void InicializarTabuleiro()
    {
        for (int i = 0; i < 8; i++)
        {
            pecas[6, i] = new Peao(this, Cor.Branco, 6, i);
            pecas[1, i] = new Peao(this, Cor.Preto, 1, i);
        }

        pecas[7, 0] = new Torre(this, Cor.Branco, 7, 0);
        pecas[7, 7] = new Torre(this, Cor.Branco, 7, 7);
        pecas[0, 0] = new Torre(this, Cor.Preto, 0, 0);
        pecas[0, 7] = new Torre(this, Cor.Preto, 0, 7);

        pecas[7, 1] = new Cavalo(this, Cor.Branco, 7, 1);
        pecas[7, 6] = new Cavalo(this, Cor.Branco, 7, 6);
        pecas[0, 1] = new Cavalo(this, Cor.Preto, 0, 1);
        pecas[0, 6] = new Cavalo(this, Cor.Preto, 0, 6);

        pecas[7, 2] = new Bispo(this, Cor.Branco, 7, 2);
        pecas[7, 5] = new Bispo(this, Cor.Branco, 7, 5);
        pecas[0, 2] = new Bispo(this, Cor.Preto, 0, 2);
        pecas[0, 5] = new Bispo(this, Cor.Preto, 0, 5);

        pecas[7, 4] = new Rei(this, Cor.Branco, 7, 4);
        pecas[0, 4] = new Rei(this, Cor.Preto, 0, 4);

        pecas[7, 3] = new Dama(this, Cor.Branco, 7, 3);
        pecas[0, 3] = new Dama(this, Cor.Preto, 0, 3);
    }

    public bool MoverPeca(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
    {
        Peca? peca = GetPeca(linhaOrigem, colunaOrigem);

        if (peca == null || peca.Cor != JogadorAtual)
        throw new JogadaInvalidaException("Não é o turno deste jogador.");

        Peca? pecaDestino = pecas[linhaDestino, colunaDestino];
        if (pecaDestino != null && pecaDestino.Cor == peca.Cor)
        throw new JogadaInvalidaException("Não pode capturar uma peça da mesma cor.");

        if (!peca.MovimentoValido(linhaDestino, colunaDestino))
        throw new MovimentoInvalidoException("Movimento inválido para esta peça.");

        Peca? pecaDestino = GetPeca(linhaDestino, colunaDestino);
        Peca? copiaPecas = (Peca?[,])pecas.Clone();
        copiaPecas[linhaDestino, colunaDestino] = peca;
        copiaPecas[linhaOrigem, colunaOrigem] = null;

        bool reiEmXeque = VerificarXequeSimulado(copiaPecas, peca.Cor);
        if (reiEmXeque)
        throw new JogadaInvalidaException("Movimento deixa o rei em xeque!");
        
        pecas[linhaDestino, colunaDestino] = peca;
        pecas[linhaOrigem, colunaOrigem] = null;
        
        peca.AtualizarPosicao(linhaDestino, colunaDestino);
        ProximoTurno();
        return true;
    }

    private bool VerificarXequeSimulado(peca[,] pecasSimuladas, Cor cor)
    {
        (int reiLinha, int reiColuna) = EncontrarReiSimulado(pecasSimuladas, cor);

        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Peca? peca = pecasSimuladas[linha, coluna];

                if (peca != null && peca.Cor != cor && peca.MovimentoValido(reiLinha, reiColuna))
                return true;
            }
        }
        return false;
    }

    public bool XequeMate(Cor cor)
    {
        if (!ReiEmXeque(cor))
        return false;

        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Peca? peca = GetPeca(linha, coluna);

                if (peca == null || peca.Cor != cor)
                continue;

                for (int novaLinha = 0; novaLinha < 8; novaLinha++)
                {
                    for (int novaColuna = 0; novaColuna < 8; novaColuna++)
                    {
                        if (peca.MovimentoValido(novaLinha, novaColuna))
                        {
                            Peca?[,] copiaPecas = (Peca?[,])pecas.Clone();

                            copiaPecas[novaLinha, novaColuna] = peca;
                            copiaPecas[linha, coluna] = null;

                            if (!VerificarXequeSimulado(copiaPecas, cor))
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public bool CaminhoLivre(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
    {
        int deltaLinha = Math.Sign(linhaDestino - linhaOrigem);
        int deltaColuna = Math.Sign(colunaDestino - colunaOrigem);

        int linhaAtual = linhaOrigem + deltaLinha;
        int colunaAtual = colunaOrigem + deltaColuna;

        while (linhaAtual != linhaDestino || colunaAtual != colunaDestino)
        {
            if (GetPeca(linhaAtual, colunaAtual) != null)
            return false;

            linhaAtual += deltaLinha;
            colunaAtual += deltaColuna;
        }

        return true;
    }

    public (int Linha, int Coluna) EncontrarRei(Cor cor)
    {
        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Peca? peca = GetPeca(linha, coluna);
                if (peca is Rei && peca.Cor == cor)
                return (linha, coluna);
            }
        }
        throw new Exception($"Rei {cor} não encontrado!");
    }

    public bool ReiEmXeque(Cor cor)
    {
        (int reiLinha, int reiColuna) = EncontrarRei(cor);

        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Peca? peca = GetPeca(linha, coluna);
                if (peca != null && peca.Cor != cor && peca.MovimentoValido(reiLinha, reiColuna))
                return true;
            }
        }
        return false;
    }

    public Peca? GetPeca(int linha, int coluna)
    {
        return pecas[linha, coluna];
    }

    public Peca?[,] GetPecas()
    {
        return pecas;
    }
}
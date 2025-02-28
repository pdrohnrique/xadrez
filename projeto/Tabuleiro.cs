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

    public List<string> HistoricoJogadas { get; } = new List<string>();

    public bool MoverPeca(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
    {
        Peca? peca = GetPeca(linhaOrigem, colunaOrigem);

        if (peca == null || peca.Cor != JogadorAtual)
        throw new JogadaInvalidaException("Não é o turno deste jogador.");

        if (peca == null)
        throw new JogadaInvalidaException("Não há peça na posição selecionada.");

        if (linhaDestino < 0 || linhaDestino > 7 || colunaDestino < 0 || colunaDestino > 7)
        throw new MovimentoInvalidoException("Movimento fora do tabuleiro.");

        Peca? pecaDestino = pecas[linhaDestino, colunaDestino];
        if (pecaDestino != null && pecaDestino.Cor == peca.Cor)
        throw new JogadaInvalidaException("Não pode capturar uma peça da mesma cor.");

        if (!peca.MovimentoValido(linhaDestino, colunaDestino))
        throw new MovimentoInvalidoException("Movimento inválido para esta peça.");
        
        pecas[linhaDestino, colunaDestino] = peca;
        pecas[linhaOrigem, colunaOrigem] = null;

        HistoricoJogadas.Add($"{peca.GetType().Name} de {peca.Cor} movida para ({linhaDestino}, {colunaDestino})");
        
        ProximoTurno();
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

    public Peca? GetPeca(int linha, int coluna)
    {
        return pecas[linha, coluna];
    }

    public Peca?[,] GetPecas()
    {
        return pecas;
    }
}
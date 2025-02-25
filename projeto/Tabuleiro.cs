using System;

public class Tabuleiro
{
    public Cor JogadorAtual { get; private set; } = Cor.Branco;
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
            pecas[6, i] = new Peao(Cor.Branco, 6, i);
            pecas[1, i] = new Peao(Cor.Preto, 1, i);
        }

        pecas[7, 0] = new Torre(Cor.Branco, 7, 0);
        pecas[7, 7] = new Torre(Cor.Branco, 7, 7);
        pecas[0, 0] = new Torre(Cor.Preto, 0, 0);
        pecas[0, 7] = new Torre(Cor.Preto, 0, 7);

        pecas[7, 1] = new Cavalo(Cor.Branco, 7, 1);
        pecas[7, 6] = new Cavalo(Cor.Branco, 7, 6);
        pecas[0, 1] = new Cavalo(Cor.Preto, 0, 1);
        pecas[0, 6] = new Cavalo(Cor.Preto, 0, 6);

        pecas[7, 2] = new Bispo(Cor.Branco, 7, 2);
        pecas[7, 5] = new Bispo(Cor.Branco, 7, 5);
        pecas[0, 2] = new Bispo(Cor.Preto, 0, 2);
        pecas[0, 5] = new Bispo(Cor.Preto, 0, 5);

        pecas[7, 4] = new Rei(Cor.Branco, 7, 4);
        pecas[0, 4] = new Rei(Cor.Preto, 0, 4);

        pecas[7, 3] = new Dama(Cor.Branco, 7, 3);
        pecas[0, 3] = new Dama(Cor.Preto, 0, 3);
    }

    public List<string> HistoricoJogadas { get; } = new List<string>();

    public bool MoverPeca(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
    {
        Peca? peca = pecas[linhaOrigem, colunaOrigem];

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
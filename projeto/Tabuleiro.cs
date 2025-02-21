using System;

public class Tabuleiro
{
    private Peca?[,] pecas;
    

    public Tabuleiro()
    {
        pecas = new Peca[8, 8];
        InicializarTabuleiro();
    }

    private void InicializarTabuleiro()
    {
        for (int i = 0; i < 8; i++)
        {
            pecas[1, i] = new Peao(Cor.Branco, 1, i);
            pecas[6, i] = new Peao(Cor.Branco, 6, i);
        }

        pecas[0, 0] = new Torre(Cor.Branco, 0, 0);
        pecas[0, 7] = new Torre(Cor.Branco, 0, 7);
        pecas[7, 0] = new Torre(Cor.Preto, 0, 0);
        pecas[7, 7] = new Torre(Cor.Preto, 0, 7);

        pecas[0, 1] = new Cavalo(Cor.Branco, 0, 1);
        pecas[0, 6] = new Cavalo(Cor.Branco, 0, 6);
        pecas[7, 1] = new Cavalo(Cor.Preto, 7, 1);
        pecas[7, 6] = new Cavalo(Cor.Preto, 7, 6);

        pecas[0, 2] = new Bispo(Cor.Branco, 0, 2);
        pecas[0, 5] = new Bispo(Cor.Branco, 0, 5);
        pecas[7, 2] = new Bispo(Cor.Preto, 7, 2);
        pecas[7, 5] = new Bispo(Cor.Preto, 7, 5);

        pecas[0, 4] = new Rei(Cor.Branco, 0, 4);
        pecas[7, 4] = new Rei(Cor.Preto, 7, 4);

        pecas[0, 3] = new Dama(Cor.Branco, 0, 3);
        pecas[7, 3] = new Dama(Cor.Preto, 7, 3);
    }

    public bool MoverPeca(int linhaOrigem, int colunaOrigem, int linhaDestino, int colunaDestino)
    {
        Peca? peca = pecas[linhaOrigem, colunaOrigem];

        if (peca == null) {return false;}

        if (peca.MovimentoValido(linhaDestino, colunaDestino))
        {
            pecas[linhaDestino, colunaDestino] = peca;
            pecas[linhaOrigem, colunaOrigem] = null;
            return true;
        }
        else {return false;}
    }

    public Peca? GetPeca(int linha, int coluna)
    {
        return pecas[linha, coluna];
    }
}
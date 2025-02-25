using System;

public enum Cor {Branco, Preto}

public interface IMovivel
{
    bool MovimentoValido(int novaLinha, int novaColuna);
}
public abstract class Peca : IMovivel
{
    public Cor Cor {get; protected set;}
    public int Linha {get; protected set;}
    public int Coluna {get; protected set;}

    public Peca(Cor cor, int linha, int coluna)
    {
        Cor = cor;
        Linha = linha;
        Coluna = coluna;
    }

    public abstract bool MovimentoValido(int novaLinha, int novaColuna);
}
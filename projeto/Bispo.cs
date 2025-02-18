using System;

public class Bispo : Peca
{
    public Bispo(Cor cor, int linha, int coluna) : base(cor, linha, coluna)
    {
        //o movimento só será valido se a paridade das colunas for diferente
    }
}
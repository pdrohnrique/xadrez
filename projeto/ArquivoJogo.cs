using System.IO;
using System.Text.Json;

public static class ArquivoJogo
{
    public static void SalvarEstado(Tabuleiro tabuleiro, string caminhoArquivo)
    {
        var estado = new
        {
            Pecas = pecas.Cast<Peca?>().Select(p => new
            {
                Tipo = p?.GetType().Name,
                Cor = p?.Cor,
                Linha = index / 8,
                Coluna = index % 8
            }).ToArray()
        };

        string json = JsonSerializer.Serialize(estado);
        File.WriteAllText(caminhoArquivo, json);
    }

    public static Tabuleiro CarregarEstado(string caminhoArquivo)
    {
        string json = File.ReadAllText(caminhoArquivo);
        var estado = JsonSerializer.Deserialize<dynamic>(json);

        if (estado == null)
        throw new Exception("Arquivo de jogo corrompido.");
        Tabuleiro tabuleiro = new Tabuleiro();
        tabuleiro.LimparTabuleiro();

        foreach (var pecaData in estado["Pecas"])
        {
            if (pecaData["Tipo"] == null)
            continue;

            Cor cor = (Cor)Enum.Parse(typeof(Cor), pecaData["Cor"].ToString());
            int linha = int.Parse(pecaData["Linha"].ToString());
            int coluna = int.Parse(pecaData["Coluna"].ToString());

            Peca peca = CriarPeca(pecaData["Tipo"].ToString(), cor, linha, coluna);
            tabuleiro.AdicionarPeca(peca, linha, coluna);
        }

        return tabuleiro;
    }

    public class EstadoJogo
    {
        public PecasData[] Pecas { get; set; }
    }

    public class PecasData
    {
        public string Tipo { get; set; }
        public Cor cor { get; set; }
        public int Linha { get; set; }
        public int Coluna { get; set; }
    }

    private static Peca CriarPeca(string tipo, Cor cor, int linha, int coluna)
    {
        return tipo switch
        {
            "Peao" => new Peao(cor, linha, coluna),
            "Torre" => new Torre(cor, linha, coluna),
            "Cavalo" => new Cavalo(cor, linha, coluna),
            "Bispo" => new Bispo(cor, linha, coluna),
            "Dama" => new Dama(cor, linha, coluna),
            "Rei" => new Rei(cor, linha, coluna),
            _ => throw new NotSupportedException($"Tipo de peça não suportado: {tipo}")
        };
    }
}
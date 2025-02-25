using System;
using System.Drawing;
using System.Windows.Forms;

namespace projeto;

public partial class Form1 : Form
{
    private Tabuleiro tabuleiro;
    private Button[,] botoes;
    private Peca? pecaSelecionada;
    private int linhaSelecionada, colunaSelecionada;

    public Form1()
    {
        InitializeComponent();
        ConfigurarTabuleiroVisual();
        tabuleiro = new Tabuleiro();
        botoes = new Button[8, 8];
        CriarTabuleiro();
    }

    private void ConfigurarTabuleiroVisual()
    {
        this.Size = new Size(500, 500);
    }
    
    private void CriarTabuleiro()
    {
        int tamanhoBotao = 60;
        int tabuleiroX = 10;
        int tabuleiroY = 10;

        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Button botao = new Button
                {
                    Width = tamanhoBotao,
                    Height = tamanhoBotao,
                    Location = new Point(tabuleiroX + coluna * tamanhoBotao, tabuleiroY + linha * tamanhoBotao),
                    Tag = new Point(linha, coluna),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = (linha + coluna) % 2 == 0 ? Color.Beige : Color.Green,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                botao.Click += BotaoClicado;
                botoes[linha, coluna] = botao;
                Controls.Add(botao);
            }
        }

        tabuleiro.InicializarTabuleiro();
        AtualizarTabuleiro();
    }
    
    private void AtualizarTabuleiro()
    {
        for (int linha = 0; linha < 8; linha++)
        {
            for (int coluna = 0; coluna < 8; coluna++)
            {
                Peca? peca = tabuleiro.GetPeca(linha, coluna);
                botoes[linha, coluna].BackgroundImage = peca != null ? ObterImagemPeca(peca) : null;
            }
        }
    }

    public void ProximoTurno()
    {
        JogadorAtual = JogadorAtual == Cor.Branco ? Cor.Preto : Cor.Branco;
    }

    private Image? ObterImagemPeca(Peca peca)
    {
        string nomeImagem = peca switch
        {
            Peao => peca.Cor == Cor.Branco ? "peao_branco.png" : "peao_preto.png",
            Torre => peca.Cor == Cor.Branco ? "torre_branca.png" : "torre_preta.png",
            Bispo => peca.Cor == Cor.Branco ? "bispo_branco.png" : "bispo_preto.png",
            Cavalo => peca.Cor == Cor.Branco ? "cavalo_branco.png" : "cavalo_preto.png",
            Dama => peca.Cor == Cor.Branco ? "dama_branca.png" : "dama_preta.png",
            Rei => peca.Cor == Cor.Branco ? "rei_branco.png" : "rei_preto.png",
            _ => ""
        };

        if (!string.IsNullOrEmpty(nomeImagem))
        {
            Image img = Image.FromFile($"imagens/{nomeImagem}");
            return new Bitmap(img, new Size(60, 60));
        }
        return null;
    }
    
    private void BotaoClicado(object? sender, EventArgs e)
    {
        try
        {
            if (sender is not Button botao || botao.Tag is not Point posicao)
            return;

            int linha = posicao.X;
            int coluna = posicao.Y;

            if (pecaSelecionada == null)
            {
                pecaSelecionada = tabuleiro.GetPeca(linha, coluna);
                
                if (pecaSelecionada == null)
                return;
                
                linhaSelecionada = linha;
                colunaSelecionada = coluna;
            }
            else
            {
                tabuleiro.MoverPeca(linhaSelecionada, colunaSelecionada, linha, coluna);
                AtualizarTabuleiro();
                pecaSelecionada = null;
                tabuleiro.MoverPeca(...);
                tabuleiro.ProximoTurno();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            pecaSelecionada = null;
        }
    }
}
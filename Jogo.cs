using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Tetris
{
    public class Jogo
    {
        private int LARGURA_TABULEIRO = 10;
        private int ALTURA_TABULEIRO = 20;
        private int[,] tabuleiro;
        private Tetromino pecaAtual;
        private Jogador jogador;
        private GerenciadorDePontuacao gerenciadorPontuacao;
        private bool gameOver;
        private Random random;

        public Jogo(string nomeJogador)
        {
            this.tabuleiro = new int[this.ALTURA_TABULEIRO, this.LARGURA_TABULEIRO];
            this.jogador = new Jogador(nomeJogador);
            this.gerenciadorPontuacao = new GerenciadorDePontuacao();
            this.gameOver = false;
            this.random = new Random();
        }

        public void Iniciar()
        {
            this.GerarNovaPeca();
            while (!this.gameOver)
            {
                this.DesenharTela();
                this.ProcessarInput();
            }
            this.ExibirTelaDeFimDeJogo();
        }

        private void DesenharTela()
        {
            Console.Clear();

            int[,] telaParaDesenhar = new int[this.ALTURA_TABULEIRO, this.LARGURA_TABULEIRO];
            for (int i = 0; i < this.ALTURA_TABULEIRO; i++)
            {
                for (int j = 0; j < this.LARGURA_TABULEIRO; j++)
                {
                    telaParaDesenhar[i, j] = this.tabuleiro[i, j];
                }
            }

            if (this.pecaAtual != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (this.pecaAtual.Formato[i, j] == 1)
                        {
                            int y = this.pecaAtual.PosicaoY + i;
                            int x = this.pecaAtual.PosicaoX + j;
                            if (y >= 0 && y < this.ALTURA_TABULEIRO && x >= 0 && x < this.LARGURA_TABULEIRO)
                            {
                                telaParaDesenhar[y, x] = 2;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Jogador: " + this.jogador.Nome + " | Pontuação: " + this.jogador.Pontuacao);
            Console.WriteLine("Setas Esq/Dir para mover, Cima/Baixo para girar, Espaço para descer.");

            for (int i = 0; i < this.ALTURA_TABULEIRO; i++)
            {
                Console.Write("|");
                for (int j = 0; j < this.LARGURA_TABULEIRO; j++)
                {
                    int celula = telaParaDesenhar[i, j];
                    if (celula == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("■");
                    }
                    else if (celula == 2)
                    {
                        Console.ForegroundColor = this.pecaAtual.Cor;
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");
            }
            Console.WriteLine("+" + new string('-', this.LARGURA_TABULEIRO) + "+");
        }

        private void ProcessarInput()
        {
            ConsoleKey tecla = Console.ReadKey(true).Key;

            switch (tecla)
            {
                case ConsoleKey.LeftArrow: this.MoverPeca(-1, 0); break;
                case ConsoleKey.RightArrow: this.MoverPeca(1, 0); break;
                case ConsoleKey.Spacebar: this.MoverPeca(0, 1); break;
                case ConsoleKey.UpArrow: this.RotacionarPeca(true); break;
                case ConsoleKey.DownArrow: this.RotacionarPeca(false); break;
            }
        }

        private void GerarNovaPeca()
        {
            char tipoPeca;
            switch (this.random.Next(0, 3))
            {
                case 0: tipoPeca = 'I'; break;
                case 1: tipoPeca = 'L'; break;
                default: tipoPeca = 'T'; break;
            }

            this.pecaAtual = new Tetromino(tipoPeca, this.LARGURA_TABULEIRO / 2 - 1, 0);

            if (this.VerificarColisao(this.pecaAtual.PosicaoX, this.pecaAtual.PosicaoY, this.pecaAtual.Formato))
            {
                this.gameOver = true;
            }
        }

        private void MoverPeca(int dx, int dy)
        {
            if (!this.VerificarColisao(this.pecaAtual.PosicaoX + dx, this.pecaAtual.PosicaoY + dy, this.pecaAtual.Formato))
            {
                this.pecaAtual.PosicaoX = this.pecaAtual.PosicaoX + dx;
                this.pecaAtual.PosicaoY = this.pecaAtual.PosicaoY + dy;
            }
            else if (dy > 0)
            {
                this.FixarPeca();
                this.VerificarLinhasCompletas();
                this.GerarNovaPeca();
            }
        }

        private void RotacionarPeca(bool sentidoHorario)
        {
            this.pecaAtual.Girar(sentidoHorario);
            if (this.VerificarColisao(this.pecaAtual.PosicaoX, this.pecaAtual.PosicaoY, this.pecaAtual.Formato))
            {
                this.pecaAtual.Girar(!sentidoHorario);
            }
        }

        private bool VerificarColisao(int posX, int posY, int[,] formato)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (formato[i, j] == 1)
                    {
                        int xAbsoluto = posX + j;
                        int yAbsoluto = posY + i;

                        if (xAbsoluto < 0 || xAbsoluto >= this.LARGURA_TABULEIRO || yAbsoluto >= this.ALTURA_TABULEIRO || (yAbsoluto >= 0 && this.tabuleiro[yAbsoluto, xAbsoluto] == 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void FixarPeca()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.pecaAtual.Formato[i, j] == 1)
                    {
                        int x = this.pecaAtual.PosicaoX + j;
                        int y = this.pecaAtual.PosicaoY + i;
                        if (y >= 0)
                        {
                            this.tabuleiro[y, x] = 1;
                        }
                    }
                }
            }
            this.jogador.AdicionarPontos(this.pecaAtual.PontosPorEncaixe);
        }

        private void VerificarLinhasCompletas()
        {
            int linhasLimpas = 0;
            for (int i = this.ALTURA_TABULEIRO - 1; i >= 0; i--)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < this.LARGURA_TABULEIRO; j++)
                {
                    if (this.tabuleiro[i, j] == 0)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }

                if (linhaCompleta)
                {
                    linhasLimpas = linhasLimpas + 1;
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < this.LARGURA_TABULEIRO; j++)
                        {
                            this.tabuleiro[k, j] = this.tabuleiro[k - 1, j];
                        }
                    }
                    for (int j = 0; j < this.LARGURA_TABULEIRO; j++)
                    {
                        this.tabuleiro[0, j] = 0;
                    }
                    i = i + 1;
                }
            }

            if (linhasLimpas > 0)
            {
                int pontosBase = linhasLimpas * 300;
                int bonus = 0;
                if (linhasLimpas > 1)
                {
                    bonus = 100;
                }
                this.jogador.AdicionarPontos(pontosBase + bonus);
            }
        }

        private void ExibirTelaDeFimDeJogo()
        {
            Console.Clear();
            Console.WriteLine("================ GAME OVER ================");
            Console.WriteLine("Jogador: " + this.jogador.Nome);
            Console.WriteLine("Pontuação Final: " + this.jogador.Pontuacao);
            Console.WriteLine("===========================================");
            this.gerenciadorPontuacao.SalvarPontuacao(this.jogador);
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
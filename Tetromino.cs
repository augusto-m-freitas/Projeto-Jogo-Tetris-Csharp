using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Tetris
{
    public class Tetromino
    {
        private int[,] formato;
        private int posicaoX;
        private int posicaoY;
        private ConsoleColor cor;
        private int pontosPorEncaixe;

        public int[,] Formato { get { return this.formato; } }
        public int PosicaoX { get { return this.posicaoX; } set { this.posicaoX = value; } }
        public int PosicaoY { get { return this.posicaoY; } set { this.posicaoY = value; } }
        public ConsoleColor Cor { get { return this.cor; } }
        public int PontosPorEncaixe { get { return this.pontosPorEncaixe; } }

        public Tetromino(char tipo, int x, int y)
        {
            this.posicaoX = x;
            this.posicaoY = y;

            switch (tipo)
            {
                case 'I':
                    this.formato = new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
                    this.cor = ConsoleColor.Blue;
                    this.pontosPorEncaixe = 3;
                    break;
                case 'L':
                    this.formato = new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } };
                    this.cor = ConsoleColor.Yellow;
                    this.pontosPorEncaixe = 4;
                    break;
                case 'T':
                    this.formato = new int[3, 3] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    this.cor = ConsoleColor.Red;
                    this.pontosPorEncaixe = 5;
                    break;
            }
        }

        public void Girar(bool sentidoHorario)
        {
            int[,] novoFormato = new int[3, 3];
            int tamanho = 3;

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (sentidoHorario)
                    {
                        novoFormato[i, j] = this.formato[tamanho - 1 - j, i];
                    }
                    else
                    {
                        novoFormato[i, j] = this.formato[j, tamanho - 1 - i];
                    }
                }
            }
            this.formato = novoFormato;
        }
    }
}
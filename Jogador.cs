using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Tetris
{
    public class Jogador
    {
        private string nome;
        private int pontuacao;

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }

        public int Pontuacao
        {
            get { return this.pontuacao; }
            set { this.pontuacao = value; }
        }

        public Jogador(string nome)
        {
            this.Nome = nome;
            this.Pontuacao = 0;
        }

        public void AdicionarPontos(int pontos)
        {
            if (pontos > 0)
            {
                this.Pontuacao = this.Pontuacao + pontos;
            }
        }
    }
}

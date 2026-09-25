using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Tetris
{
    public class GerenciadorDePontuacao
    {
        private string NOME_ARQUIVO = "scores.txt";

        public void SalvarPontuacao(Jogador jogador)
        {
            try
            {
                StreamWriter escrever = new StreamWriter(this.NOME_ARQUIVO, true, Encoding.UTF8);
                string linhaParaSalvar = jogador.Nome + ";" + jogador.Pontuacao;
                escrever.WriteLine(linhaParaSalvar);
                escrever.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao salvar a pontuação: " + e.Message);
            }
        }
    }
}
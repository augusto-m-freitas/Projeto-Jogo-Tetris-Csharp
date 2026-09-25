using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Tetris
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Bem-vindo ao Tetris!");
            Console.Write("Digite seu nome: ");
            string nome = (Console.ReadLine());

            Jogo meuJogo = new Jogo(nome);
            meuJogo.Iniciar();
        }
    }
}

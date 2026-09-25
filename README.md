# Tetris Console - C# 🧱

Projeto desenvolvido em C# aplicando conceitos estruturais de matrizes e manipulação de arquivos. Consiste numa versão jogável do Tetris diretamente no terminal.

## ⚙️ Arquitetura e Funcionalidades
* **Motor de Jogo:** Loop contínuo de renderização e processamento de inputs no console.
* **Sistema de Colisão:** Validação de limites da matriz bidimensional 20x10 atuando como tabuleiro.
* **Tetrominós:** Peças (I, L, T) modeladas utilizando matrizes 3x3 com lógica nativa de transposição matemática para rotação horária e anti-horária.
* **Persistência de Dados:** Gravação automática do nome do jogador e pontuação final em arquivo texto.

## 🛠️ Stack Tecnológica
* C# / .NET
* Programação Orientada a Objetos
* Manipulação de Arquivos (I/O)

## 🚀 Como Executar
git clone  https://github.com/augusto-m-freitas/Projeto-Jogo-Tetris-Csharp.git
cd tetris-csharp-console
dotnet run

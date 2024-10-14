using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Digite seu nome: ");
        Console.ResetColor();
        string nomePessoa = Console.ReadLine();

        bool sair = false;
        List<Livro> catalogoLivros = InicializarCatalogoLivros();
        Dictionary<string, List<Livro>> livrosEmprestados = new Dictionary<string, List<Livro>>();

        while (!sair)
        {
            Console.Clear();
            ExibirCabecalho("Bem Vindo à Biblioteca");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Usuário: {nomePessoa}\n");
            Console.ResetColor();
            ExibirMenuPrincipal();

            if (int.TryParse(Console.ReadLine(), out int opcaoUser))
            {
                switch (opcaoUser)
                {
                    case 1:
                        GerenciarUsuario(nomePessoa, catalogoLivros, livrosEmprestados);
                        break;
                    case 2:
                        GerenciarAdmin(catalogoLivros);
                        break;
                    case 3:
                        sair = true;
                        ExibirMensagem("Saindo... Obrigado por usar a biblioteca!");
                        break;
                    default:
                        ExibirMensagem("Opção inválida!");
                        break;
                }
            }
            else
            {
                ExibirMensagem("Entrada inválida! Digite um número válido.");
            }
        }
    }

    static List<Livro> InicializarCatalogoLivros()
    {
        return new List<Livro>
        {
            new Livro("O Senhor dos Anéis", "J.R.R. Tolkien", "Fantasia", 3),
            new Livro("1984", "George Orwell", "Ficção Científica", 2),
            new Livro("Dom Casmurro", "Machado de Assis", "Literatura Brasileira", 4),
            new Livro("A Revolução dos Bichos", "George Orwell", "Fábula", 5),
            new Livro("A Menina que Roubava Livros", "Markus Zusak", "Ficção", 3),
            new Livro("O Alquimista", "Paulo Coelho", "Autoajuda", 2),
            new Livro("Harry Potter e a Pedra Filosofal", "J.K. Rowling", "Fantasia", 6),
            new Livro("Cem Anos de Solidão", "Gabriel García Márquez", "Literatura Latino-Americana", 1),
            new Livro("A Guerra dos Tronos", "George R.R. Martin", "Fantasia", 4),
            new Livro("O Pequeno Príncipe", "Antoine de Saint-Exupéry", "Literatura Infantil", 5)
        };
    }

    static void ExibirCabecalho(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("==========================================");
        Console.WriteLine("|         BEM VINDO A BIBLIOTECA!        |");
        Console.WriteLine("==========================================\n");
        Console.ResetColor();
    }

    static void ExibirMenuPrincipal()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("| 1. Usuário.                            |");
        Console.WriteLine("| 2. Administrador.                      |");
        Console.WriteLine("| 3. Sair.                               |");
        Console.WriteLine("------------------------------------------");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Escolha uma opção: ");
        Console.ResetColor();
    }

    static void ExibirMensagem(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{mensagem}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void GerenciarUsuario(string nomeUsuario, List<Livro> catalogoLivros, Dictionary<string, List<Livro>> livrosEmprestados)
    {
        bool sairUsuario = false;

        while (!sairUsuario)
        {
            Console.Clear();
            ExibirCabecalho("Gerenciar Usuário");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("| O que você deseja:                     |");
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("| 1. Consultar catálogo.                 |");
            Console.WriteLine("| 2. Devolver livros.                    |");
            Console.WriteLine("| 3. Pegar livro.                        |");
            Console.WriteLine("| 4. Voltar.                             |");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Escolha uma opção: ");
            
            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                        ExibirCatalogo(catalogoLivros);
                        break;
                    case 2:
                        DevolverLivros(nomeUsuario, livrosEmprestados, catalogoLivros);
                        break;
                    case 3:
                        PegarLivrosEmprestados(nomeUsuario, catalogoLivros, livrosEmprestados);
                        break;
                    case 4:
                        sairUsuario = true;
                        break;
                    default:
                        ExibirMensagem("Opção inválida.");
                        break;
                }
            }
            else
            {
                ExibirMensagem("Entrada inválida! Digite um número válido.");
            }
        }
    }

    static void GerenciarAdmin(List<Livro> catalogoLivros)
    {
        bool sairAdmin = false;

        while (!sairAdmin)
        {
            Console.Clear();
            ExibirCabecalho("Gerenciar Administrador");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("| O que você deseja como Administrador:  |");
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("| 1. Cadastrar livro.                    |");
            Console.WriteLine("| 2. Consultar catálogo.                 |");
            Console.WriteLine("| 3. Voltar.                             |");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
            Console.Write("Escolha uma opção: ");
            
            if (int.TryParse(Console.ReadLine(), out int opcaoAdmin))
            {
                switch (opcaoAdmin)
                {
                    case 1:
                        CadastrarLivro(catalogoLivros);
                        break;
                    case 2:
                        ExibirCatalogo(catalogoLivros);
                        break;
                    case 3:
                        sairAdmin = true;
                        break;
                    default:
                        ExibirMensagem("Opção inválida.");
                        break;
                }
            }
            else
            {
                ExibirMensagem("Entrada inválida! Digite um número válido.");
            }
        }
    }

    static void CadastrarLivro(List<Livro> catalogoLivros)
    {
        Console.Clear();
        ExibirCabecalho("Cadastrar Livro");
        Console.Write("Digite o título do livro: ");
        string titulo = Console.ReadLine();
        Console.Write("Digite o autor do livro: ");
        string autor = Console.ReadLine();
        Console.Write("Digite o gênero do livro: ");
        string genero = Console.ReadLine();
        Console.Write("Digite a quantidade disponível do livro: ");
        
        if (int.TryParse(Console.ReadLine(), out int quantidade))
        {
            catalogoLivros.Add(new Livro(titulo, autor, genero, quantidade));
            ExibirMensagem("Livro cadastrado com sucesso!");
        }
        else
        {
            ExibirMensagem("Quantidade inválida. Livro não cadastrado.");
        }
    }

    static void ExibirCatalogo(List<Livro> catalogoLivros)
    {
        Console.Clear();
        ExibirCabecalho("Catálogo de Livros");
        foreach (var livro in catalogoLivros)
        {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\nNome: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(livro.Titulo);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Autor: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(livro.Autor);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Gênero: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(livro.Genero);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Quantidade: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen; 
        Console.WriteLine(livro.Quantidade);
        Console.ResetColor();


        }
        Console.ResetColor();
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void DevolverLivros(string nomeUsuario, Dictionary<string, List<Livro>> livrosEmprestados, List<Livro> catalogoLivros)
    {
        if (!livrosEmprestados.ContainsKey(nomeUsuario) || livrosEmprestados[nomeUsuario].Count == 0)
        {
            ExibirMensagem("Você não tem livros emprestados.");
            return;
        }

        Console.Clear();
        ExibirCabecalho("Devolver Livros");
        Console.WriteLine("Livros emprestados:");
        for (int i = 0; i < livrosEmprestados[nomeUsuario].Count; i++)
        {
            Console.WriteLine($"{i + 1}. {livrosEmprestados[nomeUsuario][i].Titulo}");
        }
        Console.WriteLine($"{livrosEmprestados[nomeUsuario].Count + 1}. Cancelar");
        Console.Write("Escolha um número para devolver o livro: ");

        if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= livrosEmprestados[nomeUsuario].Count)
        {
            Livro livro = livrosEmprestados[nomeUsuario][indice - 1];
            livro.Quantidade++;
            livrosEmprestados[nomeUsuario].RemoveAt(indice - 1);
            ExibirMensagem("Livro devolvido com sucesso!");
        }
        else
        {
            ExibirMensagem("Operação cancelada.");
        }
    }

    static void PegarLivrosEmprestados(string nomeUsuario, List<Livro> catalogoLivros, Dictionary<string, List<Livro>> livrosEmprestados)
    {
        Console.Clear();
        ExibirCabecalho("Emprestar Livros");
        ExibirCatalogo(catalogoLivros);
        Console.Write("\nEscolha o número do livro para pegar emprestado: ");
        
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= catalogoLivros.Count)
        {
            Livro livroSelecionado = catalogoLivros[escolha - 1];
            if (livroSelecionado.Quantidade > 0)
            {
                livroSelecionado.Quantidade--;
                if (!livrosEmprestados.ContainsKey(nomeUsuario))
                {
                    livrosEmprestados[nomeUsuario] = new List<Livro>();
                }
                livrosEmprestados[nomeUsuario].Add(livroSelecionado);
                ExibirMensagem("Livro emprestado com sucesso!");
            }
            else
            {
                ExibirMensagem("Não há exemplares disponíveis para empréstimo.");
            }
        }
        else
        {
            ExibirMensagem("Opção inválida.");
        }
    }
}

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int Quantidade { get; set; }

    public Livro(string titulo, string autor, string genero, int quantidade)
    {
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        Quantidade = quantidade;
    }
}

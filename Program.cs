using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Digite seu nome: ");
        string nomePessoa = Console.ReadLine();

        bool sair = false;
        List<Livro> catalogoLivros = InicializarCatalogoLivros();
        Dictionary<string, List<Livro>> livrosEmprestados = new Dictionary<string, List<Livro>>();

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Bem Vindo à Biblioteca, " + nomePessoa + "! ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Usuário.");
            Console.WriteLine("2. Administrador.");
            Console.WriteLine("3. Sair.");
            int opcaoUser;

            if (int.TryParse(Console.ReadLine(), out opcaoUser))
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
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
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

    static void GerenciarUsuario(string nomeUsuario, List<Livro> catalogoLivros, Dictionary<string, List<Livro>> livrosEmprestados)
    {
        bool sairUsuario = false;

        while (!sairUsuario)
        {
            Console.Clear();
            Console.WriteLine("O que você deseja:");
            Console.WriteLine("1. Consultar catálogo.");
            Console.WriteLine("2. Devolver livros.");
            Console.WriteLine("3. Pegar livro.");
            Console.WriteLine("4. Voltar.");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

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
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void GerenciarAdmin(List<Livro> catalogoLivros)
    {
        bool sairAdmin = false;

        while (!sairAdmin)
        {
            Console.Clear();
            Console.WriteLine("O que você deseja como Administrador:");
            Console.WriteLine("1. Cadastrar livro.");
            Console.WriteLine("2. Consultar catálogo.");
            Console.WriteLine("3. Voltar.");
            Console.Write("Escolha uma opção: ");
            int opcaoAdmin = int.Parse(Console.ReadLine());

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
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarLivro(List<Livro> catalogoLivros)
    {
        Console.Write("Digite o título do livro: ");
        string titulo = Console.ReadLine();
        Console.Write("Digite o autor do livro: ");
        string autor = Console.ReadLine();
        Console.Write("Digite o gênero do livro: ");
        string genero = Console.ReadLine();
        Console.Write("Digite a quantidade disponível do livro: ");
        int quantidade = int.Parse(Console.ReadLine());

        catalogoLivros.Add(new Livro(titulo, autor, genero, quantidade));
        Console.WriteLine("Livro cadastrado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void ExibirCatalogo(List<Livro> catalogoLivros)
    {
        Console.Clear();
        Console.WriteLine("Catálogo de Livros:");
        foreach (var livro in catalogoLivros)
        {
            Console.WriteLine($"{livro.Titulo} - {livro.Autor} ({livro.Genero}) - Quantidade disponível: {livro.Quantidade}");
        }
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void DevolverLivros(string nomeUsuario, Dictionary<string, List<Livro>> livrosEmprestados, List<Livro> catalogoLivros)
    {
        if (!livrosEmprestados.ContainsKey(nomeUsuario) || livrosEmprestados[nomeUsuario].Count == 0)
        {
            Console.WriteLine("Você não tem livros emprestados.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Livros emprestados:");
        for (int i = 0; i < livrosEmprestados[nomeUsuario].Count; i++)
        {
            Console.WriteLine($"{i + 1}. {livrosEmprestados[nomeUsuario][i].Titulo}");
        }
        Console.WriteLine($"{livrosEmprestados[nomeUsuario].Count + 1}. Devolver todos os livros");

        Console.Write("Escolha um livro para devolver (ou o número para devolver todos): ");
        int escolha = int.Parse(Console.ReadLine());

        if (escolha == livrosEmprestados[nomeUsuario].Count + 1)
        {
            var livrosParaDevolver = new List<Livro>(livrosEmprestados[nomeUsuario]);
            foreach (var livro in livrosParaDevolver)
            {
                catalogoLivros.Add(livro);
            }
            livrosEmprestados[nomeUsuario].Clear();
            Console.WriteLine("Todos os livros foram devolvidos com sucesso!");
        }
        else if (escolha > 0 && escolha <= livrosEmprestados[nomeUsuario].Count)
        {
            var livroParaDevolver = livrosEmprestados[nomeUsuario][escolha - 1];
            livrosEmprestados[nomeUsuario].Remove(livroParaDevolver);
            catalogoLivros.Add(livroParaDevolver);
            Console.WriteLine("Livro devolvido com sucesso!");
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void PegarLivrosEmprestados(string nomeUsuario, List<Livro> catalogoLivros, Dictionary<string, List<Livro>> livrosEmprestados)
    {
        if (!livrosEmprestados.ContainsKey(nomeUsuario))
        {
            livrosEmprestados[nomeUsuario] = new List<Livro>();
        }

        if (livrosEmprestados[nomeUsuario].Count >= 3)
        {
            Console.WriteLine("Você já possui 3 livros emprestados. Devolva algum livro antes de pegar mais.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        List<Livro> livrosEscolhidos = new List<Livro>();
        int contador = 0;

        while (contador < 3)
        {
            Console.Clear();
            Console.WriteLine("Escolha um livro para pegar emprestado (ou digite 0 para finalizar):");
            var livrosDisponiveisParaEmprestimo = catalogoLivros.Where(l => l.Quantidade > 0 && !livrosEmprestados.Values.Any(e => e.Contains(l))).ToList();
            for (int i = 0; i < livrosDisponiveisParaEmprestimo.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {livrosDisponiveisParaEmprestimo[i].Titulo} - {livrosDisponiveisParaEmprestimo[i].Autor} (Disponível: {livrosDisponiveisParaEmprestimo[i].Quantidade})");
            }

            Console.Write("Escolha uma opção: ");
            int escolha = int.Parse(Console.ReadLine());

            if (escolha == 0)
            {
                break;
            }

            if (escolha > 0 && escolha <= livrosDisponiveisParaEmprestimo.Count)
            {
                var livroSelecionado = livrosDisponiveisParaEmprestimo[escolha - 1];
                livrosEscolhidos.Add(livroSelecionado);
                livroSelecionado.Quantidade--;
                contador++;
                Console.WriteLine($"Você escolheu: {livroSelecionado.Titulo}");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }

        if (livrosEscolhidos.Count > 0)
        {
            livrosEmprestados[nomeUsuario].AddRange(livrosEscolhidos);
            SalvarLivrosEmprestados(nomeUsuario, livrosEscolhidos);
            Console.WriteLine("Livros emprestados com sucesso!");
            Console.WriteLine("Você pegou os seguintes livros:");

            foreach (var livro in livrosEscolhidos)
            {
                Console.WriteLine($"- {livro.Titulo}");
            }
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void SalvarLivrosEmprestados(string nomeUsuario, List<Livro> livros)
    {
        string caminhoArquivo = $"{nomeUsuario}_livros_emprestados.txt";
        using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
        {
            sw.WriteLine($"Usuário: {nomeUsuario}");
            sw.WriteLine("Livros emprestados:");
            foreach (var livro in livros)
            {
                sw.WriteLine($"- {livro.Titulo} - {livro.Autor}");
            }
            sw.WriteLine();
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

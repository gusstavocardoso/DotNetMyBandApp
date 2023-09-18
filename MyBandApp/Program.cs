using System;
using System.Collections.Generic;

namespace MyBandApp;

class Program
{
    static void Main(string[] args)
    {
        BandRepository bandRepository = new BandRepository();

        while (true)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Adicionar Banda");
            Console.WriteLine("2 - Atualizar Banda");
            Console.WriteLine("3 - Excluir Banda");
            Console.WriteLine("4 - Listar Bandas");
            Console.WriteLine("5 - Sair");
            Console.Write("Opção: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        AddBand(bandRepository);
                        break;
                    case 2:
                        UpdateBand(bandRepository);
                        break;
                    case 3:
                        DeleteBand(bandRepository);
                        break;
                    case 4:
                        ListBands(bandRepository);
                        break;
                    case 5:
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

            Console.WriteLine();
        }
    }

    static void AddBand(BandRepository repository)
    {
        Console.WriteLine("Adicionar Banda:");

        Console.Write("Nome: ");
        string name = Console.ReadLine();

        Console.Write("País de Origem: ");
        string country = Console.ReadLine();

        Console.Write("Ano de Origem: ");
        if (int.TryParse(Console.ReadLine(), out int yearFormed))
        {
            Console.Write("Quantidade de Integrantes: ");
            if (int.TryParse(Console.ReadLine(), out int membersCount))
            {
                Console.Write("Estilo: ");
                string style = Console.ReadLine();

                Band band = new Band
                {
                    Name = name,
                    Country = country,
                    YearFormed = yearFormed,
                    MembersCount = membersCount,
                    Style = style
                };

                repository.CreateBand(band);
                Console.WriteLine("Banda adicionada com sucesso!");
            }
            else
            {
                Console.WriteLine("Quantidade de integrantes inválida.");
            }
        }
        else
        {
            Console.WriteLine("Ano de origem inválido.");
        }
    }

    static void UpdateBand(BandRepository repository)
    {
        Console.WriteLine("Atualizar Banda:");

        Console.Write("ID da Banda: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Band existingBand = repository.GetBandById(id);
            if (existingBand != null)
            {
                Console.Write("Novo Nome: ");
                string name = Console.ReadLine();

                Console.Write("Novo País de Origem: ");
                string country = Console.ReadLine();

                Console.Write("Novo Ano de Origem: ");
                if (int.TryParse(Console.ReadLine(), out int yearFormed))
                {
                    Console.Write("Nova Quantidade de Integrantes: ");
                    if (int.TryParse(Console.ReadLine(), out int membersCount))
                    {
                        Console.Write("Novo Estilo: ");
                        string style = Console.ReadLine();

                        Band updatedBand = new Band
                        {
                            Name = name,
                            Country = country,
                            YearFormed = yearFormed,
                            MembersCount = membersCount,
                            Style = style
                        };

                        repository.UpdateBand(id, updatedBand);
                        Console.WriteLine("Banda atualizada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nova quantidade de integrantes inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Novo ano de origem inválido.");
                }
            }
            else
            {
                Console.WriteLine("Banda não encontrada.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void DeleteBand(BandRepository repository)
    {
        Console.WriteLine("Excluir Banda:");

        Console.Write("ID da Banda: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Band existingBand = repository.GetBandById(id);
            if (existingBand != null)
            {
                repository.DeleteBand(id);
                Console.WriteLine("Banda excluída com sucesso!");
            }
            else
            {
                Console.WriteLine("Banda não encontrada.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void ListBands(BandRepository repository)
    {
        List<Band> bands = repository.GetAllBands();
        Console.WriteLine("Lista de Bandas:");

        foreach (var band in bands)
        {
            Console.WriteLine($"ID: {band.Id}, Nome: {band.Name}, País: {band.Country}, Ano de Origem: {band.YearFormed}, Integrantes: {band.MembersCount}, Estilo: {band.Style}");
        }
    }
}
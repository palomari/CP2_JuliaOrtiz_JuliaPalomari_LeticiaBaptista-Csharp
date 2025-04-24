using System;
using System.Collections.Generic;
using System.Linq;
namespace CP2
{
    class Program
    {
        static List<Paciente> pacientes = new();
        static List<Medico> medicos = new();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" BEM VINDO Á CLÍNICA JL ");
                Console.WriteLine("1. Cadastrar Paciente");
                Console.WriteLine("2. Cadastrar Médico");
                Console.WriteLine("3. Agendar Consulta");
                Console.WriteLine("4. Listar/Alterar Consultas");
                Console.WriteLine("5. Relatório Diário");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                switch (Console.ReadLine())
                {
                    case "1": CadastrarPaciente(); break;
                    case "2": CadastrarMedico(); break;
                    case "3": AgendarConsulta(); break;
                    case "4": AlterarConsultas(); break;
                    case "5": RelatorioDiario(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        static void CadastrarPaciente()
        {
            Console.Write("Nome do Paciente: ");
            string nome = Console.ReadLine();
            Console.Write("CPF do Paciente: ");
            string cpf = Console.ReadLine();
            pacientes.Add(new Paciente(nome, cpf));
            Console.WriteLine("Paciente cadastrado com sucesso!");
        }
        static void CadastrarMedico()
        {
            Console.Write("Nome do Médico: ");
            string nome = Console.ReadLine();
            Console.Write("CRM do Médico: ");
            string crm = Console.ReadLine();
            medicos.Add(new Medico(nome, crm));
            Console.WriteLine("Médico cadastrado com sucesso!");
        }
        static void AgendarConsulta()
        {
            if (!ValidarListas()) return;
            var medico = SelecionarMedico();
            var paciente = SelecionarPaciente();
            Console.Write("Data e hora da consulta (ex: 2025-04-25 14:00): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataHora))
            {
                Consulta novaConsulta = new(paciente, dataHora);
                medico.AdicionarConsulta(novaConsulta);
                Console.WriteLine("Consulta agendada com sucesso!");
            }
            else
            {
                Console.WriteLine("Data inválida.");
            }
        }
        static void AlterarConsultas()
        {
            if (medicos.Count == 0)
            {
                Console.WriteLine("Nenhum médico cadastrado.");
                return;
            }
            var medico = SelecionarMedico();
            var consultas = medico.ObterConsultas();
            if (consultas.Count == 0)
            {
                Console.WriteLine("Nenhuma consulta agendada para este médico.");
                return;
            }
            Console.WriteLine("\nConsultas:");
            for (int i = 0; i < consultas.Count; i++)
            {
                var c = consultas[i];
                Console.WriteLine($"{i + 1}. {c.DataHora} - {c.Paciente.Nome}");
            }
            Console.Write("Digite o número da consulta para remover (ou 0 para cancelar): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= consultas.Count)
            {
                medico.RemoverConsulta(consultas[index - 1]);
                Console.WriteLine("Consulta removida.");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
        static void RelatorioDiario()
        {
            Console.Write("Digite a data para o relatório (ex: 2025-04-25): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out var data))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            List<Consulta> todasConsultas = medicos
                .SelectMany(m => m.ObterConsultas())
                .Where(c => DateOnly.FromDateTime(c.DataHora) == data)
                .OrderBy(c => c.DataHora)
                .ToList();
            if (todasConsultas.Count == 0)
            {
                Console.WriteLine("Nenhuma consulta neste dia.");
                return;
            }
            Console.WriteLine($"\nRelatório do dia {data}:");
            Console.WriteLine($"Total de Consultas: {todasConsultas.Count}");
            Console.WriteLine($"Primeira Consulta: {todasConsultas.First().DataHora:T}");
            Console.WriteLine($"Última Consulta: {todasConsultas.Last().DataHora:T}");
            var intervalos = todasConsultas.Zip(todasConsultas.Skip(1), (a, b) => (b.DataHora - a.DataHora).TotalMinutes);
            double media = intervalos.Any() ? intervalos.Average() : 0;
            Console.WriteLine($"Intervalo médio entre consultas: {media:F1} minutos");
        }
        static bool ValidarListas()
        {
            if (medicos.Count == 0)
            {
                Console.WriteLine("Nenhum médico cadastrado.");
                return false;
            }
            if (pacientes.Count == 0)
            {
                Console.WriteLine("Nenhum paciente cadastrado.");
                return false;
            }
            return true;
        }
        static Medico SelecionarMedico()
        {
            Console.WriteLine("\nMédicos:");
            for (int i = 0; i < medicos.Count; i++)
                Console.WriteLine($"{i + 1}. {medicos[i].Nome} (CRM: {medicos[i].CRM})");
            Console.Write("Escolha o médico: ");
            int index = int.Parse(Console.ReadLine());
            return medicos[index - 1];
        }
        static Paciente SelecionarPaciente()
        {
            Console.WriteLine("\nPacientes:");
            for (int i = 0; i < pacientes.Count; i++)
                Console.WriteLine($"{i + 1}. {pacientes[i].Nome} (CPF: {pacientes[i].CPF})");
            Console.Write("Escolha o paciente: ");
            int index = int.Parse(Console.ReadLine());
            return pacientes[index - 1];
        }
    }
}
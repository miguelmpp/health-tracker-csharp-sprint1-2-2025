using System;

namespace ConsoleApp1
{
    internal class Program
    {
        // Capacidade fixa dos arrays
        const int CAPACIDADE = 200;

        // ARRAYS PARALELOS
        // 1 = Exercício (minutos)
        // 2 = Água (litros)
        // 3 = Sono (horas)
        static int[] tipos = new int[CAPACIDADE];
        static DateTime[] datas = new DateTime[CAPACIDADE];
        static double[] valores = new double[CAPACIDADE];
        static int count = 0; // Quantidade de registros preenchidos

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                MostrarMenu();
                int opcao = LerOpcao();

                switch (opcao)
                {
                    case 1:
                        AdicionarRegistro(tipos, datas, valores, ref count, CAPACIDADE);
                        Pausa();
                        break;
                    case 2:
                        ListarRegistros(tipos, datas, valores, count);
                        Pausa();
                        break;
                    case 3:
                        ExibirEstatisticas(tipos, valores, count);
                        Pausa();
                        break;
                    case 0:
                        executando = false;
                        break;
                }
            }

            Console.WriteLine("Programa encerrado. Até logo!");
        }

        // ================= MENU / INTERFACE =================

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("===== Health Tracker =====");
            Console.WriteLine("1) Adicionar registro");
            Console.WriteLine("2) Listar registros");
            Console.WriteLine("3) Exibir estatísticas");
            Console.WriteLine("0) Sair");
            Console.Write("Escolha: ");
        }

        static int LerOpcao()
        {
            while (true)
            {
                string entrada = Console.ReadLine() ?? "";

                int op;
                if (int.TryParse(entrada, out op))
                {
                    if (op == 0 || op == 1 || op == 2 || op == 3)
                    {
                        return op;
                    }
                }

                Console.Write("Opção inválida. Digite 0, 1, 2 ou 3: ");
            }
        }

        // ================= OPERAÇÕES PRINCIPAIS =================

        static void AdicionarRegistro(int[] tipos, DateTime[] datas, double[] valores, ref int count, int capacidade)
        {
            if (count >= capacidade)
            {
                Console.WriteLine();
                Console.WriteLine("Capacidade esgotada. Não é possível adicionar mais registros.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== Adicionar Registro ===");

            int tipo = LerTipo();
            DateTime data = LerData();
            double valor = LerValorPositivo(MensagemValorPorTipo(tipo));

            tipos[count] = tipo;
            datas[count] = data;
            valores[count] = valor;
            count++;

            Console.WriteLine();
            Console.WriteLine("[OK] Registro adicionado com sucesso!");
        }

        static void ListarRegistros(int[] tipos, DateTime[] datas, double[] valores, int count)
        {
            Console.WriteLine();
            Console.WriteLine("=== Listar Registros ===");

            if (count == 0)
            {
                Console.WriteLine("Nenhum registro cadastrado.");
                return;
            }

            Console.WriteLine("#   Data        Tipo       Valor");

            for (int i = 0; i < count; i++)
            {
                string dataStr = datas[i].ToString("dd/MM/yyyy");

                string tipoStr = NomeTipo(tipos[i]);
                tipoStr = tipoStr.PadRight(10);

                string valorStr = FormatarValorParaLista(tipos[i], valores[i]);

                string numero = (i + 1).ToString().PadRight(3);
                string dataCol = dataStr.PadRight(11);

                Console.WriteLine(numero + " " + dataCol + " " + tipoStr + " " + valorStr);
            }
        }

        static void ExibirEstatisticas(int[] tipos, double[] valores, int count)
        {
            Console.WriteLine();
            Console.WriteLine("=== Estatísticas ===");

            ExibirEstatisticasTipo(1, tipos, valores, count);
            ExibirEstatisticasTipo(2, tipos, valores, count);
            ExibirEstatisticasTipo(3, tipos, valores, count);
        }

        static void ExibirEstatisticasTipo(int tipoAlvo, int[] tipos, double[] valores, int count)
        {
            double soma = 0.0;
            int qtd = 0;

            for (int i = 0; i < count; i++)
            {
                if (tipos[i] == tipoAlvo)
                {
                    soma += valores[i];
                    qtd++;
                }
            }

            string nome = NomeTipo(tipoAlvo);

            if (qtd == 0)
            {
                string somaStrZero = FormatarSoma(tipoAlvo, 0.0);
                Console.WriteLine(nome + " -> Soma: " + somaStrZero + " | Média: -");
            }
            else
            {
                double media = soma / qtd;
                string somaStr = FormatarSoma(tipoAlvo, soma);
                string mediaStr = FormatarMedia(tipoAlvo, media);
                Console.WriteLine(nome + " -> Soma: " + somaStr + " | Média: " + mediaStr);
            }
        }

        // ================= LEITURA / VALIDAÇÃO =================

        static int LerTipo()
        {
            Console.Write("Tipo (1-Exercício | 2-Água | 3-Sono): ");

            while (true)
            {
                string entrada = Console.ReadLine() ?? "";

                int t;
                if (int.TryParse(entrada, out t))
                {
                    if (t == 1 || t == 2 || t == 3)
                    {
                        return t;
                    }
                }

                Console.Write("Tipo inválido. Digite 1 (Exercício), 2 (Água) ou 3 (Sono): ");
            }
        }

        static DateTime LerData()
        {
            Console.Write("Data (dd/MM/yyyy): ");

            while (true)
            {
                string entrada = Console.ReadLine() ?? "";

                DateTime data;
                if (DateTime.TryParse(entrada, out data))
                {
                    return data;
                }

                Console.Write("Data inválida. Use um formato de data válido (ex.: 08/11/2025): ");
            }
        }

        static double LerValorPositivo(string rotulo)
        {
            Console.Write(rotulo);

            while (true)
            {
                string entrada = Console.ReadLine() ?? "";

                double valor;
                if (double.TryParse(entrada, out valor))
                {
                    if (valor > 0)
                    {
                        return valor;
                    }
                }

                Console.Write("Valor inválido. Digite um número maior que zero: ");
            }
        }

        // ================= FUNÇÕES DE APOIO =================

        static string NomeTipo(int t)
        {
            if (t == 1)
            {
                return "Exercício";
            }
            else if (t == 2)
            {
                return "ÁGUA";
            }
            else if (t == 3)
            {
                return "SONO";
            }
            else
            {
                return "Desconhecido";
            }
        }

        static string UnidadeTipo(int t)
        {
            if (t == 1)
            {
                return "min";
            }
            else if (t == 2)
            {
                return "L";
            }
            else if (t == 3)
            {
                return "h";
            }
            else
            {
                return "";
            }
        }

        static string MensagemValorPorTipo(int t)
        {
            if (t == 1)
            {
                return "Valor (>0, em minutos): ";
            }
            else if (t == 2)
            {
                return "Valor (>0, em litros): ";
            }
            else if (t == 3)
            {
                return "Valor (>0, em horas): ";
            }
            else
            {
                return "Valor (>0): ";
            }
        }

        static string FormatarValorParaLista(int tipo, double valor)
        {
            if (tipo == 1)
            {
                int minutos = (int)Math.Round(valor);
                return minutos.ToString("0") + " " + UnidadeTipo(tipo);
            }
            else
            {
                return valor.ToString("F2") + " " + UnidadeTipo(tipo);
            }
        }

        static string FormatarSoma(int tipo, double soma)
        {
            if (tipo == 1)
            {
                int minutos = (int)Math.Round(soma);
                return minutos.ToString("0") + " " + UnidadeTipo(tipo);
            }
            else
            {
                return soma.ToString("F2") + " " + UnidadeTipo(tipo);
            }
        }

        static string FormatarMedia(int tipo, double media)
        {
            return media.ToString("F2") + " " + UnidadeTipo(tipo);
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}

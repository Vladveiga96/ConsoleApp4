using System;

class InvestimentoFinanceiro
{
    static void Main()
    {
        // =============================================
        // TAXAS DE JUROS: Atualize aqui de acordo com
        // a pesquisa da equipe
        // =============================================
        double taxaSelicAA = 0.15;    // 15% ao ano
        double taxaIPCAAA = 0.0523;   // 5,23% ao ano
        double taxaCDBAA = 0.1417;    // 14,17% ao ano
        double taxaPoupMes = 0.006752; // 0,6752% ao mês (~0,5%+TR)

        Console.WriteLine("=== Simulador de Investimentos ===");

        // =============================================
        // 1️⃣ Solicitação de dados pelo usuário
        // =============================================
        Console.Write("Informe o valor inicial: R$ ");
        double depositoInicial = double.Parse(Console.ReadLine());

        Console.Write("Informe o valor do depósito mensal: R$ ");
        double depositoMensal = double.Parse(Console.ReadLine());

        Console.WriteLine("Deseja informar o prazo em:\n1 - Anos\n2 - Meses");
        int unidade = int.Parse(Console.ReadLine());

        Console.Write("Informe o prazo: ");
        int prazo = int.Parse(Console.ReadLine());

        // Converte anos para meses se necessário
        if (unidade == 1) prazo *= 12;

        // =============================================
        // 2️⃣ Escolha do tipo de investimento
        // =============================================
        Console.WriteLine("\nEscolha o tipo de investimento:");
        Console.WriteLine("1 - Tesouro Selic");
        Console.WriteLine("2 - Tesouro IPCA");
        Console.WriteLine("3 - CDB");
        Console.WriteLine("4 - Poupança");
        int opcao = int.Parse(Console.ReadLine());

        // Calcula a taxa mensal a partir da taxa anual (exceto poupança)
        double taxaMensal;
        switch (opcao)
        {
            case 1:
                taxaMensal = Math.Pow(1 + taxaSelicAA, 1.0 / 12) - 1;
                break;
            case 2:
                taxaMensal = Math.Pow(1 + taxaIPCAAA, 1.0 / 12) - 1;
                break;
            case 3:
                taxaMensal = Math.Pow(1 + taxaCDBAA, 1.0 / 12) - 1;
                break;
            case 4:
                taxaMensal = taxaPoupMes;
                break;
            default:
                Console.WriteLine("Opção inválida!");
                return;
        }

        // =============================================
        // 3️⃣ Cálculo mês a mês
        // =============================================
        double saldo = depositoInicial;
        int pontoVirada = -1; // mês em que juros superam o depósito

        Console.WriteLine("\nMês\tDepósito\tJuros\t\tSaldo");
        Console.WriteLine("----------------------------------------------------");

        for (int mes = 1; mes <= prazo; mes++)
        {
            double juros = saldo * taxaMensal; // juros do mês
            saldo += juros + depositoMensal;   // saldo acumulado

            // Exibe tabela mês a mês
            Console.WriteLine($"{mes}\t{depositoMensal:F2}\t\t{juros:F2}\t\t{saldo:F2}");

            // 4️⃣ Verifica o ponto em que os juros superam os aportes
            if (pontoVirada == -1 && juros >= depositoMensal)
                pontoVirada = mes;
        }

        // =============================================
        // 5️⃣ Resultado final e ponto de virada
        // =============================================
        Console.WriteLine("\n====================================");
        Console.WriteLine($"Valor final acumulado: R$ {saldo:F2}");

        if (pontoVirada != -1)
        {
            int anos = pontoVirada / 12;
            int meses = pontoVirada % 12;
            Console.WriteLine($"A partir do mês {pontoVirada} (aprox. {anos} ano(s) e {meses} mês(es)), os juros superam o depósito mensal.");
        }
        else
        {
            Console.WriteLine("Os juros não superaram o depósito mensal no prazo informado.");
        }

        Console.WriteLine("====================================");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}

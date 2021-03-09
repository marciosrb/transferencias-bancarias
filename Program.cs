using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        private static List<Conta> listaContas = new List<Conta>();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços");
            Console.ReadLine();
        }

        private static void Depositar()
        {
            Console.WriteLine("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listaContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            Console.WriteLine("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listaContas[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            Console.WriteLine("Digite o número da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o número da conta de destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());

            Console.WriteLine($"Digite o valor a ser transferido para a conta número {indiceContaDestino}: ");
            double valorTransferencia = double.Parse(Console.ReadLine());
            
            listaContas[indiceContaOrigem].Transferir(valorTransferencia, listaContas[indiceContaDestino]);
        }

        private static void ListarContas()
        {
            Console.WriteLine("Listar contas");

            if (listaContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for (int i = 0; i < listaContas.Count; i++)
            {
                Conta conta = listaContas[i];
                Console.Write($"#{i} - ");
                Console.WriteLine(conta);
            }
        }

        private static bool TipoContaExiste(int tipoContaEntrada)
        {
            int[] tiposConta = {1, 2};
            bool existe = false;
            
            for (int i = 0; i < tiposConta.Length; i++)
            {
                if (tipoContaEntrada == tiposConta[i])
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }
        
        private static void InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            while (!TipoContaExiste(entradaTipoConta))
            {
                Console.Write("Os valores possíveis para tipo de conta são 1 para Conta Fisica ou 2 para Juridica. Digite um dos dois: ");
                entradaTipoConta = int.Parse(Console.ReadLine());
            }

            Console.Write("Digite o Nome do cliente: ");
            string entradaNome = Console.ReadLine();
            
            while (String.IsNullOrEmpty(entradaNome) || entradaNome.Length <= 2)
            {
                Console.Write("Um nome deve ter ao menos 3 letras. Entre com um nome válido: ");
                entradaNome = Console.ReadLine();
            }

            Console.Write("Digite o saldo inicial: ");
            double entradaSaldo = double.Parse(Console.ReadLine());

            while (entradaSaldo < 0)
            {
                Console.Write("O saldo de entrada não pode ser menor que 0. Digite novamente o saldo: ");
                entradaSaldo = Double.Parse(Console.ReadLine());
            }
            
            Console.Write("Digite o crédito: ");
            double entradaCredito = double.Parse(Console.ReadLine());
            
            while (entradaCredito < 0)
            {
                Console.Write("O crédito não pode ser menor que 0. Digite novamente o crédito: ");
                entradaCredito = Double.Parse(Console.ReadLine());
            }

            Conta novaConta = new Conta(
                tipoConta: (TipoConta)entradaTipoConta,
                saldo: entradaSaldo,
                credito: entradaCredito,
                nome: entradaNome
            );
            
            listaContas.Add(novaConta);
        }
        
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seus dispor");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}

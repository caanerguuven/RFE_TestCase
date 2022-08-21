using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RFECase.UIConsole.Handler;
using System;
using System.Threading.Tasks;

namespace RFECase.UIConsole
{
    internal class Program
    {
        private static IRFEHandler _handler;
        static void Main(string[] args)
        {
            var builder = new HostBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddHttpClient();
               services.AddTransient<IRFEHandler, RFEHandler>();
           }).UseConsoleLifetime();

            var host = builder.Build();

            _handler = host.Services.GetRequiredService<IRFEHandler>();

            Console.WriteLine("-------------");
            Console.WriteLine("Welcome");
            Console.WriteLine("-------------");


            BringSelectionPage().GetAwaiter().GetResult();

        }

        private static async Task BringSelectionPage()
        {
            var choice = BringMainPage();
            switch (choice)
            {
                case 1:
                    await BringLeftPage();
                    break;
                case 2:
                    await BringRightPage();
                    break;
                case 3:
                    await BringDifferencePage();
                    break;
            }
        }

        static int BringMainPage()
        {
            Console.WriteLine("Which process would you like to do ? ");
            Console.WriteLine("-------------");
            Console.WriteLine("1 - Enter Left Value");
            Console.WriteLine("2 - Enter Right Value");
            Console.WriteLine("3 - Get Differences");
            Console.WriteLine("-------------");
            var choice = Convert.ToInt16(Console.ReadLine());
            if (choice < 1 || choice > 3)
            {
                Console.Clear();
                Console.WriteLine("Wrong Choice ! Please try again");
                Task.Delay(2000);
                BringMainPage();
            }

            return choice;
        }

        private static async Task BringDifferencePage()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            Console.WriteLine("Enter ID :");
            var id = Console.ReadLine();
            var isInteger = int.TryParse(id, out int idInt);
            if (!isInteger)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Wrong Choice ! Please try again");
                await Task.Delay(2000);
                await BringRightPage();
                return;
            }

            var response = await _handler.GetDiff(idInt);
            Console.WriteLine("-------------");
            Console.WriteLine($" Response : {response}");
            Console.WriteLine("-------------");
            Console.WriteLine("You are redirected to Main Page....");
            await Task.Delay(5000);
            Console.Clear();

            await BringSelectionPage();
        }

        private static async Task BringRightPage()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            Console.WriteLine("Enter ID :");
            var id = Console.ReadLine();
            var isInteger = int.TryParse(id, out int idInt);
            if (!isInteger)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Wrong Choice ! Please try again");
                await Task.Delay(2000);
                await BringRightPage();
                return;
            }

            Console.WriteLine("Enter Right Input :");
            var rightValue = Console.ReadLine();


            var response = await _handler.SendToRight(idInt, rightValue);
            Console.WriteLine("-------------");
            Console.WriteLine($" Response : {response}");
            Console.WriteLine("-------------");
            Console.WriteLine("You are redirected to Main Page....");
            await Task.Delay(5000);
            Console.Clear();

            await BringSelectionPage();
        }

        private static async Task BringLeftPage()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            Console.WriteLine("Enter ID :");
            var id = Console.ReadLine();
            var isInteger = int.TryParse(id, out int idInt);
            if (!isInteger)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Wrong Choice ! Please try again");
                await Task.Delay(2000);
                await BringLeftPage();
                return;
            }

            Console.WriteLine("Enter Left Input :");
            var leftValue = Console.ReadLine();


            var response = await _handler.SendToLeft(idInt, leftValue);
            Console.WriteLine("-------------");
            Console.WriteLine($" Response : {response}");
            Console.WriteLine("-------------");
            Console.WriteLine("You are redirected to Main Page....");
            await Task.Delay(5000);
            Console.Clear();

            await BringSelectionPage();
        }


    }
}

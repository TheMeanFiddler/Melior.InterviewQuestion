using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;

namespace Melior.InterviewQuestion.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load configuration stored in appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the AccountDataStore from the appsettings.json
            var accountDataStoreConfig = configuration.GetSection("AccountDataStore").Value;

            // Configure the DI container
            var serviceCollection = new ServiceCollection();

            // Choose the AccountDataStore based on the configuration
            if (accountDataStoreConfig == "BackupAccountDataStore")
            {
                serviceCollection.AddSingleton<IAccountDataStore, BackupAccountDataStore>();
            }
            else
            {
                serviceCollection.AddSingleton<IAccountDataStore, AccountDataStore>();
            }

            // Add the other services
            var serviceProvider = serviceCollection
                .AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IMakePaymentRequestValidator, MakePaymentRequestValidator>()
                .AddSingleton<IAccountValidator, AccountValidator>()
                .BuildServiceProvider();

            // Example usage
            var paymentService = serviceProvider.GetService<IPaymentService>();
            paymentService.MakePayment(new MakePaymentRequest
            {
                DebtorAccount = new Account
                {
                    AccountNumber = "12345678",
                    Balance = 100,
                    Status = AccountStatus.Live
                },
                CreditorAccount = new Account
                {
                    AccountNumber = "87654321",
                    Balance = 0,
                    Status = AccountStatus.Live
                },
                Amount = 50,
                PaymentScheme = new PaymentSchemes.FasterPayments()
            });
        }
    }
}

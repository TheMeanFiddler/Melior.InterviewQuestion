using Melior.InterviewQuestion.Types;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, dummy account returned for brevity
            return new Account
            {
                AccountNumber = accountNumber,
                Balance = 100,
                Status = AccountStatus.Live,
                PaymentSchemes = new List<PaymentSchemes.IPaymentScheme>
                {
                    new PaymentSchemes.Bacs(),
                    new PaymentSchemes.Chaps(),
                    new PaymentSchemes.FasterPayments()
                }
            };
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}

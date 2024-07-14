using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.PaymentSchemes
{
    public class Bacs : IPaymentScheme
    {
        public string SchemeName => "Bacs";

        public bool MakePayment(Account debtorAccount, Account creditorAccount)
        {
            //Todo: Implement payment logic
            return true;
        }
    }
}

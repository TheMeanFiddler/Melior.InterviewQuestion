using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.PaymentSchemes
{
    public class Chaps : IPaymentScheme
    {
        public string SchemeName => "Chaps";
        public bool MakePayment(Account debtorAccount, Account creditorAccount)
        {
            //Todo: Implement payment logic
            return true;
        }
    }
}

using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.PaymentSchemes
{
    public class FasterPayments : IPaymentScheme
    {
        public string SchemeName => "FasterPayments";
        public bool MakePayment(Account debtorAccount, Account creditorAccount)
        {
            //Todo: Implement payment logic
            return true;
        }
    }
}

using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.PaymentSchemes
{
    public interface IPaymentScheme
    {
        string SchemeName { get; }
        bool MakePayment(Account debtorAccount, Account creditorAccount);

    }
}
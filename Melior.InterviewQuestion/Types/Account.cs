using Melior.InterviewQuestion.PaymentSchemes;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.Types
{
    /// <summary>
    /// Account for making ad receiving payments.
    /// Accounts are retrieved from the AccountDataStore and validate by the AccountValidator.
    /// </summary>
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }

        // A list of payment schemes that can be used like Bacs, Chaps and FasterPayments.
        public List<IPaymentScheme> PaymentSchemes { get; set; }

    }
}


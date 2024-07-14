using Melior.InterviewQuestion.PaymentSchemes;
using System;

namespace Melior.InterviewQuestion.Types
{
    /// <summary>
    /// Rayment request that gets processed by the PaymentService
    /// and validated by the MakePaymentRequestValidator
    /// </summary>
    public class MakePaymentRequest
    {
        public Account CreditorAccount { get; set; }

        public Account DebtorAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public IPaymentScheme PaymentScheme { get; set; }


    }
}

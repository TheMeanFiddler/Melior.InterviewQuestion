using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Validators
{
    public interface IMakePaymentRequestValidator
    {
        bool IsValid(MakePaymentRequest request);
    }
    public class MakePaymentRequestValidator : IMakePaymentRequestValidator
    {
        private readonly IAccountValidator _accountValidator;

        //Constructor Injection
        public MakePaymentRequestValidator(IAccountValidator accountValidator)
        {
            _accountValidator = accountValidator;
        }
        public bool IsValid(MakePaymentRequest request)
        {
            //validate required data is present
            if (request == null ||  request.DebtorAccount == null || request.CreditorAccount == null)
            {
                return false;
            }
            if (request.PaymentScheme == null)
            {
                return false;
            }
            if (request.Amount <= 0)
            {
                return false;
            }
            //validate debtor account - Make sure it is live
            if (!_accountValidator.IsValid(request.DebtorAccount))
            {
                return false;
            }
            //validate creditor account - Make sure it is live
            if (!_accountValidator.IsValid(request.CreditorAccount))
            {
                return false;
            }
            //Make sure there is enough balance in the debtor account
            if (request.Amount > request.DebtorAccount.Balance)
            {
                return false;
            }
            //Make sure the date is not in the past
            if (request.PaymentDate<System.DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}

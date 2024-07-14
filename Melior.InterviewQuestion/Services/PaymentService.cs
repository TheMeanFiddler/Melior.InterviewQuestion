using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;
using System.Configuration;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IAccountValidator _accountValidator;
        private readonly IMakePaymentRequestValidator _requestValidator;


        public PaymentService(
            IAccountDataStore accountDataStore,
            IAccountValidator accountValidator,
            IMakePaymentRequestValidator requestValidator)
        {
            _accountDataStore = accountDataStore;
            _accountValidator = accountValidator;
            _requestValidator = requestValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {

            var result = new MakePaymentResult();
            //Check the account is valid and there is enough balance and the payment scheme is valid
            if(!_requestValidator.IsValid(request))
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
            }
            if (result.Success)
            {
                //Update the account balances and save to the database
                request.DebtorAccount.Balance -= request.Amount;
                request.CreditorAccount.Balance += request.Amount;
                _accountDataStore.UpdateAccount(request.DebtorAccount);
                _accountDataStore.UpdateAccount(request.CreditorAccount);
            }

            return result;
        }
    }
}

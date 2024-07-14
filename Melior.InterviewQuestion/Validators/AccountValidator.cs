using Melior.InterviewQuestion.PaymentSchemes;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Validators
{
    public interface IAccountValidator
    {
        bool IsValid(Account account);
    }
    public class AccountValidator : IAccountValidator
    {
        public bool IsValid(Account account)
        {
            if (account == null)
            {
                return false;
            }
            if(account.Status!=AccountStatus.Live)
            {
                return false;
            }
            return true;
        }
    }


}

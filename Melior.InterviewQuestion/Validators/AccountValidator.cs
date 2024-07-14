using Melior.InterviewQuestion.PaymentSchemes;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Validators
{
    public interface IAccountValidator
    {
        bool IsValid(Account account);
    }
    /// <summary>
    /// Checks wether the account is valid for making and receiving payments.
    /// </summary>
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

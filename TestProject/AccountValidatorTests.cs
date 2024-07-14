using NUnit.Framework;
using Moq;
using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;

namespace TestProject
{
    [TestFixture]
    public class AccountValidatorTests
    {
        private AccountValidator _validator;
        private Mock<IAccountDataStore> _accountDataStoreMock;

        [SetUp]
        public void Setup()
        {
            _accountDataStoreMock = new Mock<IAccountDataStore>();
            _validator = new AccountValidator();
        }

        [Test]
        public void ShouldReturnFalseWhenAccountDoesNotExist()
        {
            // Arrange
            var accountNumber = "12345678";
            _accountDataStoreMock.Setup(ds => ds.GetAccount(accountNumber)).Returns((Account)null);

            // Act
            var testAccount = _accountDataStoreMock.Object.GetAccount(accountNumber);
            var result = _validator.IsValid(testAccount);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnFalseWhenAccountIsDisabled()
        {
            // Arrange
            var accountNumber = "12345678";
            var account = new Account { AccountNumber = accountNumber, Status = AccountStatus.Disabled };
            _accountDataStoreMock.Setup(ds => ds.GetAccount(accountNumber)).Returns(account);

            // Act
            var result = _validator.IsValid(_accountDataStoreMock.Object.GetAccount(accountNumber));

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnTrueWhenAccountIsLive()
        {
            // Arrange
            var accountNumber = "12345678";
            var account = new Account { AccountNumber = accountNumber, Status = AccountStatus.Live };
            _accountDataStoreMock.Setup(ds => ds.GetAccount(accountNumber)).Returns(account);

            // Act
            var result = _validator.IsValid(_accountDataStoreMock.Object.GetAccount(accountNumber));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenAccountIsIsInboundOnly()
        {
            // Arrange
            var accountNumber = "12345678";
            var account = new Account { AccountNumber = accountNumber, Status = AccountStatus.InboundPaymentsOnly };
            _accountDataStoreMock.Setup(ds => ds.GetAccount(accountNumber)).Returns(account);

            // Act
            var result = _validator.IsValid(_accountDataStoreMock.Object.GetAccount(accountNumber));

            // Assert
            Assert.IsFalse(result);
        }
    }
}

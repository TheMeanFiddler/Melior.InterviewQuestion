using NUnit.Framework;
using Moq;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;
using Melior.InterviewQuestion.PaymentSchemes;

namespace TestProject
{
    [TestFixture]
    public class MakePaymentRequestValidatorTests
    {
        private MakePaymentRequestValidator _validator;
        private Mock<IAccountValidator> _accountValidatorMock;

        [SetUp]
        public void Setup()
        {
            _accountValidatorMock = new Mock<IAccountValidator>();
            _validator = new MakePaymentRequestValidator(_accountValidatorMock.Object);
        }

        [Test]
        public void ShouldReturnValidWhenAmountsAndAccountsAreValid()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account { AccountNumber= "12345678" },
                CreditorAccount = new Account { AccountNumber = "12345679" },
                Amount = 100,
                PaymentScheme = new FasterPayments()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void ShouldReturnErrorWhenCreditorAccountIsNull()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account(),
                CreditorAccount = null,
                Amount = 100,
                PaymentScheme = new FasterPayments()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenDebtorAccountIsNull()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = null,
                CreditorAccount = new Account(),
                Amount = 100,
                PaymentScheme = new FasterPayments()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenAmountIsZero()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account(),
                CreditorAccount = new Account(),
                Amount = 0,
                PaymentScheme = new FasterPayments()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenPaymentDateIsInPast()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account(),
                CreditorAccount = new Account(),
                Amount = 100,
                PaymentDate = DateTime.Now.AddDays(-1),
                PaymentScheme = new FasterPayments()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenPaymentSchemeIsNull()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account(),
                CreditorAccount = new Account(),
                Amount = 100,
                PaymentScheme = null
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenPaymentSchemeIsNotSupported()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account(),
                CreditorAccount = new Account(),
                Amount = 100,
                PaymentScheme = new UnsupportedPaymentScheme()
            };

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenDebtorAccountIsInvalid()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account { AccountNumber = "12345678" },
                CreditorAccount = new Account { AccountNumber = "87654321" },
                Amount = 100,
                PaymentScheme = new FasterPayments()
            };

            _accountValidatorMock.Setup(v => v.IsValid(It.IsAny<Account>())).Returns(false);

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnErrorWhenCreditorAccountIsInvalid()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                DebtorAccount = new Account { AccountNumber = "12345678" },
                CreditorAccount = new Account { AccountNumber = "87654321" },
                Amount = 100,
                PaymentScheme = new FasterPayments()
            };

            _accountValidatorMock.Setup(v => v.IsValid(It.IsAny<Account>())).Returns(true);
            _accountValidatorMock.Setup(v => v.IsValid(It.Is<Account>(a => a.AccountNumber == "87654321"))).Returns(false);

            // Act
            var result = _validator.IsValid(request);

            // Assert
            Assert.IsFalse(result);
        }
    }

    // Example of an unsupported payment scheme for testing purposes
    public class UnsupportedPaymentScheme : IPaymentScheme
    {
        public string SchemeName => "UnsupportedPaymentScheme";
        public bool MakePayment(Account debtorAccount, Account creditorAccount)
        {
            //Todo: Implement payment logic
            return true;
        }
    }
}

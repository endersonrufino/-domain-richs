using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {        
        [TestMethod]
        public void Deeve_retornar_um_erro_quando_o_documento_existir(){
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "12345678901";
            command.Email = "batman@liga.com";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "Wayne CORP";
            command.PayerDocument = "123456789";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@liga.com";
            command.Street = "qwerty";
            command.Number = "123";
            command.Neighborhood = "qwerty";
            command.City = "qwerty";
            command.State = "qwerty";
            command.Country = "qwerty";
            command.ZipCode = "12345678";

            handler.Handle(command);
            
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        //Red, Green. Refactor

        [TestMethod]
        public void Deve_retornar_um_erro_quando_o_cnpj_for_invalido()
        {
            var document = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        public void Deve_retornar_um_sucesso_quando_o_cnpj_for_valido()
        {
            var document = new Document("12345678910112", EDocumentType.CNPJ);
            Assert.IsTrue(document.Valid);
        }

        [TestMethod]
        public void Deve_retornar_um_erro_quando_o_cpf_for_invalido()
        {
            var document = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        public void Deve_retornar_um_sucesso_quando_o_cpf_for_valido()
        {
             var document = new Document("12345678910", EDocumentType.CNPJ);
            Assert.IsTrue(document.Valid);
        }
    }
}
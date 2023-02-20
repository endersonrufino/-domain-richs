using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;


    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12345678901", EDocumentType.CPF);
        _email = new Email("bruce@waynecorp.com");
        _address = new Address("Rua 1", "10", "Bairro dos morcegos", "Gotham", "GT", "NY", "12145000");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);

    }

    [TestMethod]
    public void Deve_retornar_erro_quando_tiver_uma_escricao_ativa()
    {
        var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne CORP", _document, _address, _email);

        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }

    public void Deve_retornar_erro_quando_tiver_uma_escricao_sem_pagamento()
    {
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void Deve_retornar_sucesso_quando_adicionar_uma_escricao()
    {
        var payment = new PayPalPayment("123", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne CORP", _document, _address, _email);

        _subscription.AddPayment(payment);
        
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Valid);
    }
}

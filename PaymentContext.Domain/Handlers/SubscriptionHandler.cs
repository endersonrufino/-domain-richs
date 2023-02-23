using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //fail fast validations
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura.");
            }

            //verificar se o documento já esta cadastrado
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já estaem uso");
            }

            //verificar se o email já esta cadastrado
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este E-mail já estaem uso");
            }

            //gerar os vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.PaymentNumber,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            //gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //checar as notificações
            if (Invalid)
            {
                return new CommandResult(false, "Não foi possivel realizar a assinatura");
            }

            //salvar as informações
            _studentRepository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "sua assinatura foi criada");

            //retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //fail fast validations


            //verificar se o documento já esta cadastrado
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já estaem uso");
            }
            //verificar se o email já esta cadastrado
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este E-mail já estaem uso");
            }
            //gerar os vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.PaymentNumber,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            //gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //checar as notificações
            if (Invalid)
            {
                return new CommandResult(false, "Não foi possivel realizar a assinatura");
            }

            //salvar as informações
            _studentRepository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "sua assinatura foi criada");

            //retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //fail fast validations
            
            //verificar se o documento já esta cadastrado
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já estaem uso");
            }
            //verificar se o email já esta cadastrado
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este E-mail já estaem uso");
            }
            //gerar os vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.PaymentNumber,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            //gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNUmber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email);

            //relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //checar as notificações
            if (Invalid)
            {
                return new CommandResult(false, "Não foi possivel realizar a assinatura");
            }

            //salvar as informações
            _studentRepository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "sua assinatura foi criada");

            //retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
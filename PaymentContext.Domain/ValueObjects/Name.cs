using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firtsName, string lastName)
        {
            FirtsName = firtsName;
            LastName = lastName;

            if (string.IsNullOrEmpty(FirtsName))
            {
                AddNotification("Name.FirstName", "Nome inválido");
            }
        }

        public string FirtsName { get; private set; }
        public string LastName { get; private set; }
    }
}
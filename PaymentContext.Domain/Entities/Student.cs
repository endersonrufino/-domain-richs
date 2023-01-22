using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Entities;

public class Student
{
    public Student(string firtsName, string lastName, string document, string email)
    {
        FirtsName = firtsName;
        LastName = lastName;
        Document = document;
        Email = email;        
    }

    public string FirtsName { get; private set; }
    public string LastName { get; private set; }
    public string Document { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public List<Subscription> Subscriptions { get; set; }
}

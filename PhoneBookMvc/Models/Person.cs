using System;
using System.Collections.Generic;

namespace PhoneBookMvc.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Phone> Phones { get; set; } // Navigation property
}

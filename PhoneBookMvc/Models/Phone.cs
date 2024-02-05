using System;
using System.Collections.Generic;

namespace PhoneBookMvc.Models;
public class Phone
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public PhoneType Type { get; set; }
    public int PersonId { get; set; } // Foreign key to Person
    public Person Person { get; set; } // Navigation property
}

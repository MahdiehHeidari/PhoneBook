using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookMvc.Models;

public class Person
{
    public int Id { get; set; }
    [Required]
    [StringLength(10,MinimumLength =3)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Phone> Phones { get; set; } // Navigation property
}

using System;
namespace PhoneBookMvc.Models
{


    public class PersonViewModel
    {
        public IEnumerable<Phone> phones { get; set; }
        public Person persons { get; set; }

        public PersonViewModel()
        {
            persons = new Person();
        }
    }
}
using System.Collections.Generic;
using Domain.App;

namespace WebApp.ViewModels.Test
{
    public class TestViewModel
    {
        public ICollection<PaymentType> PaymentTypes { get; set; } = default!;
        public ICollection<Person> Persons { get; set; } = default!;
    }
}
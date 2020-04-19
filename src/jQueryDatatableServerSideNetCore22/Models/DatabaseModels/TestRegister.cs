using System;

namespace jQueryDatatableServerSideNetCore22.Models.DatabaseModels
{
    public class TestRegister
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public string Street { get; set; }

        public string Phone { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string Notes { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

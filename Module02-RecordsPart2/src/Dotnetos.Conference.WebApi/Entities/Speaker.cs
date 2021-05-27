namespace Dotnetos.Conference.WebApi.Entities
{
    public class Speaker
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public Speaker(string firstName, string lastName, string company)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
        }
    }
}

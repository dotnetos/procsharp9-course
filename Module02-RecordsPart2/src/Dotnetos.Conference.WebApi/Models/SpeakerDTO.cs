namespace Dotnetos.Conference.WebApi.Models
{
    public class SpeakerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public bool PresentingOnline { get; set; }

        public SpeakerDTO()
        {
        }

        public SpeakerDTO(string firstName, string lastName, string company, bool presentingOnline)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            PresentingOnline = presentingOnline;
        }

        public override string ToString() => $"{FirstName} {LastName} ({Company}), presenting online: {PresentingOnline}";
    }
}

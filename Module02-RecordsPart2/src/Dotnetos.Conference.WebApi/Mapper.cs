using Dotnetos.Conference.WebApi.Entities;
using Dotnetos.Conference.WebApi.Models;

namespace Dotnetos.Conference.WebApi
{
    public class Mapper
    {
        public SpeakerDTO ToDto(Speaker speaker)
        {
            var dto = new SpeakerDTO();
            dto.FirstName = speaker.FirstName;
            dto.LastName = speaker.LastName;
            dto.Company = speaker.Company;
            dto.PresentingOnline = speaker is OnlineSpeaker;
            return dto;
        }
    }
}

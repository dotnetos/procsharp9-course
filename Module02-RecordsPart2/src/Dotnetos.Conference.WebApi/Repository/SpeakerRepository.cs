using System.Linq;
using Dotnetos.Conference.WebApi.Entities;

namespace Dotnetos.Conference.WebApi.Repository
{
    public class SpeakerRepository
    {
        // not exactly true, fortunately in 2018 we had all speakers on the stage :)
        private readonly Speaker[] _speakers2018 = {
            new("Adam", "Sitnik", "Microsoft"),
            new("Alexandre", "Mutel", "Unity Technologies"),
            new OnlineSpeaker("Andrey", "Akinshin", "JetBrains", true),
            new("Shay", "Rojansky", "Microsoft"),
            new("Jarosław", "Pałka", "Neo4j"),
            new OnlineSpeaker("Łukasz", "Pyrzyk", "Dotnetos", false),
            new OnlineSpeaker("Konrad", "Kokosa", "Dotnetos", false),
            new OnlineSpeaker("Szymon", "Kulec", "Dotnetos", true),
        };

        public Speaker[] GetAll() => _speakers2018.ToArray();
    }
}

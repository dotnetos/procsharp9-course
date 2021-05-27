using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dotnetos.Conference.WebApi.Repository;
using Microsoft.Extensions.Logging;

namespace Dotnetos.Conference.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakersController : ControllerBase
    {
        private readonly SpeakerRepository _repository;
        private readonly Mapper _mapper;
        private readonly ILogger _logger;

        public SpeakersController(SpeakerRepository repository, Mapper mapper, ILogger<SpeakersController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var speakers = _repository.GetAll();
            var dtos = speakers.Select(_mapper.ToDto).ToArray();
            _logger.LogInformation("Found {speakers count}, including: {speakers}", dtos.Length, string.Join("; ", dtos.Select(x => x.ToString())));
            return Ok(dtos);
        }
    }
}

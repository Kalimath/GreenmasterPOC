using MbDevelopment.Greenmaster.BotanicalWebService.Controllers.Base;
using MbDevelopment.Greenmaster.Contracts.WebApi.PlantDetails.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaticData.Time.Durations;

namespace MbDevelopment.Greenmaster.BotanicalWebService.Controllers.Characteristics;

[Route(FlowerInfoApi.Route)]
public class FlowerInfoController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpeciesDetailsDto), 200)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        return Ok(); //TODO
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlowerInfoCommand createCommand)
    {
        return await ExecutePost(createCommand);
    }
    
    
}

public class CreateFlowerInfoCommand(
    string speciesId,
    Month[] bloomPeriod,
    bool multiColor,
    Color[] colors,
    bool isFragrant,
    bool attractsPollinators)
    : IRequest<SpeciesDetailsDto>
{
    //TODO: check if it ain't better to continue with the existing logic inside the AdminPortal project
    public string SpeciesId { get;} = speciesId;
    public Month[] BloomPeriod { get; } = bloomPeriod;
    public bool MultiColor { get; } = multiColor;
    public Color[] Colors { get; } = colors;
    public bool IsFragrant { get; } = isFragrant;
    public bool AttractsPollinators { get; } = attractsPollinators;
}

public enum Color
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple
}

public class SpeciesDetailsDto
{
}
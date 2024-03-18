using MbDevelopment.Greenmaster.Contracts.WebApi.Taxonomy.Dtos;
using MediatR;

namespace MbDevelopment.Greenmaster.BotanicalWebService.CQRS;

public class GetAllKingdomsQuery : IRequest<IEnumerable<KingdomDto>>
{
}
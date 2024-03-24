using HashidsNet;
using MbDevelopment.Greenmaster.BotanicalWebService.Mappers;
using MbDevelopment.Greenmaster.Contracts.Commands.Taxonomy.Kingdom;
using MbDevelopment.Greenmaster.Contracts.Dtos;
using MbDevelopment.Greenmaster.DataAccess.Base;
using MediatR;

namespace MbDevelopment.Greenmaster.BotanicalWebService.Handlers.Commands.Taxonomy.Kingdom;

public class UpdateKingdomHandler : IRequestHandler<UpdateKingdomCommand, KingdomDto>
{
    private readonly IRepository<Core.Taxonomy.TaxonKingdom> _repository;
    private readonly IHashids _hashids;
    private readonly KingdomMapper _mapper;

    public UpdateKingdomHandler(IRepository<Core.Taxonomy.TaxonKingdom> repository, IHashids hashids)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _hashids = hashids ?? throw new ArgumentNullException(nameof(hashids));
        _mapper = new KingdomMapper(hashids) ?? throw new ArgumentNullException(nameof(hashids));
    }

    public async Task<KingdomDto> Handle(UpdateKingdomCommand request, CancellationToken cancellationToken)
    {
        //validation in pipeline
        var decodedId = _hashids.DecodeSingle(request.Id);
        var kingdom = await _repository.GetAsync(k => k.Id == decodedId, cancellationToken);
        if (kingdom == null) throw new KeyNotFoundException($"Kingdom with id {request.Id} not found");
        
        UpdateModel(kingdom, request);

        _repository.Update(kingdom);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return _mapper.ToDto(kingdom)!;
    }

    private static void UpdateModel(Core.Taxonomy.TaxonKingdom kingdom, UpdateKingdomCommand request)
    {
        kingdom.LatinName = request.Name;
        kingdom.Description = request.Description;
    }
}
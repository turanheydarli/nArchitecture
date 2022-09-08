using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModelByDynamic;

public class GetListModelByDynamicQueryHandler:IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
{
    private readonly IMapper _mapper;
    private readonly IModelRepository _modelRepository;

    public GetListModelByDynamicQueryHandler(IMapper mapper, IModelRepository modelRepository)
    {
        _mapper = mapper;
        _modelRepository = modelRepository;
    }

    public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,include:
            m => m.Include(c => c.Brand),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize, cancellationToken: cancellationToken);
        
        ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
        return mappedModels;
    }
}
﻿using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModel;

public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Model> models = await _modelRepository.GetListAsync(include:
            m => m.Include(c => c.Brand),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize, cancellationToken: cancellationToken);

        ModelListModel model = _mapper.Map<ModelListModel>(models);

        return model;
    }
}
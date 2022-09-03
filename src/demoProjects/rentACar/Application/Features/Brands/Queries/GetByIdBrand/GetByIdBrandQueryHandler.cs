using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly BrandBusinessRules _brandBusinessRules;

    public GetByIdBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        _brandBusinessRules = brandBusinessRules;
    }

    public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        Brand brand = await _brandRepository.GetAsync(b => b.Id == request.Id);

        _brandBusinessRules.BrandShouldExistWhenRequested(brand);

        BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);

        return brandGetByIdDto;
    }
}
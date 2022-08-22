using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<CreatedBrandDto>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
            
            Brand brand = _mapper.Map<Brand>(request);
            Brand createBrand = await _brandRepository.AddAsync(brand);
            CreatedBrandDto createdBrand = _mapper.Map<CreatedBrandDto>(createBrand);

            return createdBrand;
        }
    }
}
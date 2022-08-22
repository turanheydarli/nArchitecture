using Application.Features.Brands.Dtos;
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
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            
            Brand brand = _mapper.Map<Brand>(request);
            Brand createBrand = await _brandRepository.AddAsync(brand);
            CreatedBrandDto createdBrand = _mapper.Map<CreatedBrandDto>(createBrand);

            return createdBrand;
        }
    }
}
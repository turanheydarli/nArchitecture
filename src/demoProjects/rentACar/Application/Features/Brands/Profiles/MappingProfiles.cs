﻿using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBrandCommand, Brand>().ReverseMap();
        CreateMap<CreatedBrandDto, Brand>().ReverseMap();
        
        CreateMap<IPaginate<Brand>, BrandListModel>().ReverseMap();
        CreateMap<BrandListDto, Brand>().ReverseMap();
        
        CreateMap<BrandGetByIdDto, Brand>().ReverseMap();
    }
}
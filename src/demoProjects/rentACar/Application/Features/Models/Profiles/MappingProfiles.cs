﻿using Application.Features.Models.DTOs;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, ModelListDto>()
            .ForMember(c => c.BrandName, opt => opt.MapFrom(p => p.Brand.Name)).ReverseMap();
        CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();
    }
}
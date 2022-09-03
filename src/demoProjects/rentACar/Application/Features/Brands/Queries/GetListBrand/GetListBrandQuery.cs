using Application.Features.Brands.Models;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.Brands.Queries.GetListBrand;

public class GetListBrandQuery : IRequest<BrandListModel>
{
    public PageRequest PageRequest { get; set; }
}
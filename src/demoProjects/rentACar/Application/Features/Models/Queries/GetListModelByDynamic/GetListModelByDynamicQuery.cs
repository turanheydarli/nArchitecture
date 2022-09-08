using Application.Features.Models.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;

namespace Application.Features.Models.Queries.GetListModelByDynamic;

public class GetListModelByDynamicQuery :IRequest<ModelListModel>
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }
}
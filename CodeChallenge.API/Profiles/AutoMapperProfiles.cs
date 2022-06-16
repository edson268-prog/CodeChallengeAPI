using AutoMapper;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;

namespace CodeChallenge.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            ////CreateMap<ORIGEN, DESTINO> //PARA CONSULTA GET
            //CreateMap<Product, DtoProduct>();

            CreateMap<Product, DtoResponseProduct>()
                .ForMember(dto => dto.Name, ent => ent.MapFrom(x => x.Name))
                .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
                .ForMember(dto => dto.AgeRestriction, ent => ent.MapFrom(x => x.AgeRestriction))
                .ForMember(dto => dto.Company, ent => ent.MapFrom(x => x.Company))
                .ForMember(dto => dto.Price, ent => ent.MapFrom(x => x.Price))
                .ForMember(dto => dto.SoldOut, ent => ent.MapFrom(x => x.SoldOut))
                .ForMember(dto => dto.ProductType, ent => ent.MapFrom(x => x.ProductType.Description));

            CreateMap<DtoProduct, Product>()
                .ForMember(dto => dto.Name, ent => ent.MapFrom(x => x.Name))
                .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
                .ForMember(dto => dto.AgeRestriction, ent => ent.MapFrom(x => x.AgeRestriction))
                .ForMember(dto => dto.Company, ent => ent.MapFrom(x => x.Company))
                .ForMember(dto => dto.Price, ent => ent.MapFrom(x => x.Price))
                .ForMember(dto => dto.ProductTypeId, ent => ent.MapFrom(x => x.ProductTypeId))
                .ReverseMap(); //PARA QUE FUNCIONE DE MANERA CONTRARIA TAMBIEN

            CreateMap<ProductType, DtoResponseProductType>()
                .ForMember(dto => dto.Name, ent => ent.MapFrom(x => x.Name))
                .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description));

            CreateMap<DtoProductType, ProductType>()
                .ForMember(dto => dto.Name, ent => ent.MapFrom(x => x.Name))
                .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
                .ReverseMap();
        }
    }
}

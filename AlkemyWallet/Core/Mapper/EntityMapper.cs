using AlkemyWallet.Core.Models;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper

{

    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            CreateMap<Catalogue, CatalogueDTO>().ReverseMap();
            CreateMap<Catalogue, CatalogueForCreationDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserByIdDTO>().ReverseMap();
            CreateMap<User, UserForCreatoionDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
        }
    }
}


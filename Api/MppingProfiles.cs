using Api.DTOs;
using Core.Entities;
using AutoMapper;
namespace Api
{
    public class MppingProfiles :Profile
    {
        public MppingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>();
        }
    }
}

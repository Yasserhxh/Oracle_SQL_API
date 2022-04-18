using AutoMapper;
using Domain.Models;
using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Models mapping
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<COMPTEUR_H, COMPTEUR_HModel>();
        }
    }
}

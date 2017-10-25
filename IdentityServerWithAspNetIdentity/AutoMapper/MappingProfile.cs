using AutoMapper;
using IdentityServer4.Models;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Models.ClientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<IdentityServer4Client, IdentityServer4ClientViewModel>();
            CreateMap<Client, IdentityServer4Client>();
        }
    }
}

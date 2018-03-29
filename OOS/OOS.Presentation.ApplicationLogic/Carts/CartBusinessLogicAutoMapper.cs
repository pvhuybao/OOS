using AutoMapper;
using OOS.Domain.Carts.Models;
using OOS.Presentation.ApplicationLogic.Carts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Carts
{
    public class CartBusinessLogicAutoMapper : Profile
    {
        public CartBusinessLogicAutoMapper()
        {
            CreateMap<CreateCartRequest, Cart>();            
            CreateMap<EditCartRequest, Cart>();            
            CreateMap<Cart, CreateCartResponse>();
            CreateMap<Cart, EditCartResponse>();
        }
    }
}

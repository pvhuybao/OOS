using OOS.Domain.Carts.Models;
using OOS.Presentation.ApplicationLogic.Carts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Carts
{
    public interface ICartBusinessLogic
    {
        CreateCartResponse CreateCart(CreateCartRequest request);
        void DeleteCart(string id);

        EditCartResponse EditCart(string id, EditCartRequest request);

        List<Cart> GetCart();

        Cart GetCartById(string id);
    }
}

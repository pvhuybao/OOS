using OOS.Domain.Products.Models;
using OOS.Presentation.ApplicationLogic.Common;
using OOS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Products.Messages
{
    public class GetProductExtraCategoryNameResponse : ResponseBase
    {
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IdCategory { get; set; }

        public string CategoryName { get; set; }

        public ProductStatus? Status { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public int Discount { get; set; }

        public List<ProductTail> ProductTails { get; set; }

        ///Base on list ProductTail

        public int MinPrice{get;set;}

        public int MaxPrice{get;set;}

        public int TotalQuantity{get;set;}

        public string BasicImage{get;set;}
        ///End
        
        public void CalculateProductValues()
        {
            if (ProductTails.Count==0)
                return;
            //set default
            MinPrice = MaxPrice = ProductTails[0].Price;
            BasicImage = ProductTails[0].Image;
            foreach(var p in ProductTails)
            {
                //minprice
                MinPrice=MinPrice<p.Price?MinPrice: p.Price;
                //maxprice
                MaxPrice = MaxPrice > p.Price ? MaxPrice : p.Price;
                //totalquantity
                TotalQuantity += p.Quantity;
            }
        }
    }
}

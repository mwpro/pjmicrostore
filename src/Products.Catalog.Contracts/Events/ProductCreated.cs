using System;
using System.Collections.Generic;
using System.Text;
using Products.Catalog.Contracts.ApiModels;

namespace Products.Catalog.Contracts.Events
{
    public class ProductCreatedEvent
    {
        public ProductDto ProductDetails { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }

    public class ProductUpdatedEvent
    {
        public ProductDto ProductDetails { get; set; }
        public DateTime UpdateDateUtc { get; set; }
    }
}

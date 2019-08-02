﻿using System;
using Products.Catalog.Contracts.ApiModels;

namespace Products.Catalog.Contracts.Events
{
    public class ProductUpdatedEvent
    {
        public ProductDto ProductDetails { get; set; }
        public DateTime UpdateDateUtc { get; set; }
    }
}

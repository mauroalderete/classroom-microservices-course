﻿namespace Api.Gateway.Models.Catalog.Commands
{
    public class ProductCreateCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

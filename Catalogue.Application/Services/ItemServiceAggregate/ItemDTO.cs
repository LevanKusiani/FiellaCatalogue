﻿namespace Catalogue.Application.Services.ItemServiceAggregate
{
    public class ItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}

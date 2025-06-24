using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.API.Domain.Aggregates
{
    public class Consumer
    {
        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public string? ImageUrl { get; private set; }

        public Consumer(string name, string description, string? imageUrl, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            UserId = userId;
        }
    }
}

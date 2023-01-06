using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Tags.Queries.GetTags
{
    public class TagDto : IMapFrom<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Food_Drink_Id { get; set; }
    }
}
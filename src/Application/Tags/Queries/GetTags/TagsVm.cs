using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Tags.Queries.GetTags
{
    public class TagsVm
    {
        public IList<TagDto> TagDtos { get; set; } = new List<TagDto>();
    }
}
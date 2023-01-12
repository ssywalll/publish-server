using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Tags.Queries.GetTags
{
    public class TagsVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<TagDto> Data { get; set; } = new List<TagDto>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services.Interface
{
    public interface ITagService
    {
        Task SaveTagAsync(Tag tag);

        Task<List<Tag>> GetAllTagsAsync();
    }
}

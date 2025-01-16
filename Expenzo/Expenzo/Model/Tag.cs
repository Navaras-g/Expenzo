using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenzo.Model
{
    public class Tag
    {

        public int TagId { get; set; }


        public string TagName { get; set; }

        public Tag(int tagId, string tagName)
        {
            TagId = tagId;
            TagName = tagName;
        }

        public Tag()
        {
        }

    }
}

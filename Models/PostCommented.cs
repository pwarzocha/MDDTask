using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDDConsoleApp.Models
{
    public class PostCommented
    {
        public PostUncommented? Post { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

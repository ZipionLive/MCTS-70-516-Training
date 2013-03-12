using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson2
{
    public class Child
    {
        public string id { get; set; }
        public string attr { get; set; }
        public List<GrandChild> children { get; set; }
    }

    public class GrandChild
    {
        public string id { get; set; }
        public string attr { get; set; }
        public string parent { get; set; }
    }
}

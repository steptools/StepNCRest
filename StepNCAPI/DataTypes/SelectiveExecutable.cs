using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepNCAPI.DataTypes
{
    class SelectiveExecutable : ParentExecutable
    {
        public String name { get; set; }
        public string type = "selective";
        public long id { get; set; }
        public List<Executable> children { get; set; }
    }
}

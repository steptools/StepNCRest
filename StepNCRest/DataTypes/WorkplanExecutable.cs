using StepNCRest.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepNCRest.Modules
{
    public class WorkplanExecutable : ParentExecutable
    {
        public String name { get; set; }
        public long id { get; set; }
        public string type = "workplan";
        public List<Executable> children { get; set; }
    }
}

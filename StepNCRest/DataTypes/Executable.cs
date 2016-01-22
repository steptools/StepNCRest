using StepNCRest.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepNCRest.DataTypes
{
    public interface Executable
    {
        string name { get; set; }
        long id { get; set; }
    }

    public interface ParentExecutable : Executable
    {
        List<Executable> children { get; set; }
    }

    // TODO remove this eventually (once all types are implemented)
    public class GenericExecutable : Executable
    {
        public string name { get; set; }
        public long id { get; set; }
    }

    public class ExecutableFactory
    {
        public static Executable fromId(STEPNCLib.Finder finder, long wpid)
        {
            Executable ret;
            if (finder.IsWorkplan(wpid))
            {
                WorkplanExecutable wpe = new WorkplanExecutable();
                wpe.children = finder.GetNestedExecutableAll(wpid).Select((long id) =>
                {
                    return ExecutableFactory.fromId(finder, id);
                }).ToList<Executable>();
                ret = wpe;
            }
            else if (finder.IsSelective(wpid))
            {
                SelectiveExecutable se = new SelectiveExecutable();
                se.children = finder.GetNestedExecutableAll(wpid).Select((long id) =>
                {
                    return ExecutableFactory.fromId(finder, id);
                }).ToList<Executable>();
                ret = se;
            }
            else
            {
                ret = new GenericExecutable();
            }

            ret.id = wpid;
            ret.name = finder.GetExecutableName(wpid);

            return ret;
        }
    }

}

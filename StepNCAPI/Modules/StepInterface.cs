using StepNCAPI.DataTypes;
using System;
using System.Collections.Generic;

namespace StepNCAPI.Modules
{
    public class StepInterface
    {
        STEPNCLib.Finder finder;
        WorkplanExecutable rootWorkplan = null;


        public StepInterface(string path)
        {
            finder = new STEPNCLib.Finder();

            finder.Open238(path);
        }

        public WorkplanExecutable GetMainWorkplan()
        {
            if (rootWorkplan == null) rootWorkplan = (WorkplanExecutable) ExecutableFactory.fromId(finder, finder.GetMainWorkplan());
            return rootWorkplan;
        }

    }
}

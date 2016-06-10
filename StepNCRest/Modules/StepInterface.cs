using StepNCRest.DataTypes;
using System;
using System.Collections.Generic;

namespace StepNCRest.Modules
{
    public class StepInterface
    {
        STEPNCLib.Finder finder;
        STEPNCLib.AptStepMaker apt;
        STEPNCLib.MachineState machinestate;
        WorkplanExecutable rootWorkplan = null;


        public StepInterface(string path)
        {
            apt = new STEPNCLib.AptStepMaker();
            apt.Open238(path);
            finder = new STEPNCLib.Finder();
            finder.Open238(path);
            STEPNCLib.MachineState.Init();
            machinestate = new STEPNCLib.MachineState(path);
        }

        public string GetKeyState()
        {
            return machinestate.GetKeystateJSON();
        }
        public string GetDeltaState()
        {
            return machinestate.GetDeltaJSON();
        }
        public string GetStateGeom()
        {
            return machinestate.GetGeometryJSON();
        }
        public string GetStateGeom(string uuid,STEPNCLib.MachineState.GeomType geomtype)
        {
            return machinestate.GetGeometryJSON(uuid, geomtype);
        }
        public int StepState()
        {
            return machinestate.AdvanceState();
        }
        public int SetStateNextWS()
        {
            return machinestate.nextWS();
        }
        public int SetStateWS(UInt32 wsid)
        {
            return machinestate.switchWS(wsid);
        }
        public WorkplanExecutable GetMainWorkplan()
        {
            if (rootWorkplan == null) rootWorkplan = (WorkplanExecutable) ExecutableFactory.fromId(finder, finder.GetMainWorkplan());
            return rootWorkplan;
        }
        public Executable GetSpecificWorkplan(long wpid)
        {
            return ExecutableFactory.fromId(finder, wpid);
        }

    }
}

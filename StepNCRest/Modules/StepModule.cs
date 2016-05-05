using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StepNCRest.Modules
{
    public class StepModule : NancyModule
    {
        public static Dictionary<String, StepInterface> stepInterfaces = new Dictionary<String, StepInterface>();
        private static System.Threading.Mutex mut = new System.Threading.Mutex();

        private delegate Response CB(StepInterface si);
        private static Response Mutexify(string id,CB action)
        {
            mut.WaitOne();
            var si = GetStepInterface(id);
            var rtn = action(si);
            mut.ReleaseMutex();
            return rtn;
        }
        public StepModule(){

            Get["/projects/{id}/plan"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti=>
                    {
                        return Response.AsJson(sti.GetMainWorkplan());
                    });
            };

            Get["/projects/{id}/plan/{wsid}"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti =>
                    {
                        DataTypes.GenericExecutable rtn = sti.GetSpecificWorkplan(parameters.wsid);
                        return Response.AsJson(rtn);
                    });
            };

            Get["/projects/{id}/geometry"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti =>
                    {
                        return Response.AsText(sti.GetStateGeom());
                    });
            };

            Get["/projects/{pid}/geometry/{type}/{eid}"] = parameters =>
            {
                return Mutexify((string)parameters.pid,
                    sti =>
                    {
                        STEPNCLib.MachineState.GeomType typ = STEPNCLib.MachineState.GeomType.NONE;
                        if ((string)parameters.type == "shell")
                            typ = STEPNCLib.MachineState.GeomType.MESH;
                        else if ((string)parameters.type == "annotation")
                            typ = STEPNCLib.MachineState.GeomType.POLYLINE;
                        if (typ == STEPNCLib.MachineState.GeomType.NONE)
                            return Response.AsText("");
                        return Response.AsText(sti.GetStateGeom((string)parameters.eid, typ));
                    });
            };

            Get["/projects/{id}/keystate"] = paramters =>
            {
                return Mutexify((string)paramters.id,
                    sti =>
                    {
                        return Response.AsText(sti.GetKeyState());
                    });
            };

            Get["/projects/{id}/deltastate"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti =>
                    {
                        return Response.AsText(sti.GetDeltaState());
                    });
            };

            Get["/projects/{id}/step"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti =>
                    {
                        int resp = sti.StepState();
                        if (resp == 0)
                            return Response.AsText("OK");
                        else if (resp == 1)
                            return Response.AsText("SWITCH");
                        else
                            return Response.AsText("ERROR");
                    });
            };
            Get["/projects/{id}/workingstep/{step}"] = parameters =>
            {
                return Mutexify((string)parameters.id,
                    sti =>
                    {
                        int resp = -1;
                        if ((string)parameters.step == "next")
                            resp = sti.SetStateNextWS();
                        else
                            resp = sti.SetStateWS((uint)parameters.step);
                        if (resp == 0)
                            return Response.AsText("OK");
                        else return Response.AsText("ERROR");
                    });
            };
        }

        public static StepInterface GetStepInterface(string id)
        {
            if (stepInterfaces.ContainsKey(id)) return stepInterfaces[id];
            StepInterface stepInterface = new StepInterface(Path.Combine(Properties.Settings.Default.projectPath, id, "model/model.stpnc"));
            stepInterfaces.Add(id, stepInterface);
            return stepInterface;
        }
    }
}

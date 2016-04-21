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

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
        public Dictionary<String, StepInterface> stepInterfaces = new Dictionary<String, StepInterface>();

        public StepModule(){

            Get["/projects/{id}/plan"] = parameters =>
            {
                var si = GetStepInterface((string)parameters.id);
                return Response.AsJson(si.GetMainWorkplan());
            };

        }

        public StepInterface GetStepInterface(string id)
        {
            if (stepInterfaces.ContainsKey(id)) return stepInterfaces[id];
            StepInterface stepInterface = new StepInterface(Path.Combine(Properties.Settings.Default.projectPath, id, "model/model.stpnc"));
            stepInterfaces.Add(id, stepInterface);
            return stepInterface;
        }
    }
}

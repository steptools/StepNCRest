using Nancy;

namespace StepNCAPI.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return "This is the StepNC API Server, please see the README for the endpoints";
            };
        }
    }
}
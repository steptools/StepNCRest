using Nancy;
using Nancy.Conventions;
using Nancy.Responses;
using System;
using System.Configuration;
using System.IO;

namespace StepNCRest.Modules
{
    public class FileModule : NancyModule
    {
        public FileModule()
        {
            if (!GenericFileResponse.SafePaths.Contains(Properties.Settings.Default.projectPath))
                GenericFileResponse.SafePaths.Add(Properties.Settings.Default.projectPath);

            // Returns list of projects
            Get["/projects"] = parameters =>
            {
                return Response.AsJson(Project.GetAll());
            };

            Get["/projects/{id}"] = parameters =>
            {
                return Response.AsJson(Project.Get((string)parameters.id));
            };
        }
    }
}

using Nancy;
using Nancy.Conventions;
using Nancy.Responses;
using System;
using System.Configuration;
using System.IO;

namespace StepNCAPI.Modules
{
    public class FileModule : NancyModule
    {
        public FileModule()
        {
            GenericFileResponse.SafePaths.Add(Properties.Settings.Default.projectPath);
            //StaticContentConventionBuilder.AddDirectory("/files", Properties.Settings.Default.projectPath, null);
            // Returns list of projects
            Get["/projects"] = parameters =>
            {
                return Response.AsJson(Project.GetAll());
            };
            Get["/project/{id}"] = parameters =>
            {
                return Response.AsJson(Project.Get((string)parameters.id));
            };
            //Get["/project/{id}/file/(?<path>[^.]+\\..*)"] = parameters =>
            Get["/project/{id}/file/{path*}"] = parameters =>
            {
                String id = parameters.id, path = parameters.path, ext = parameters.ext;
                return Response.AsFile(Project.Get(id).GetFilePath(path+"."+ext));
            };
        }
    }
}

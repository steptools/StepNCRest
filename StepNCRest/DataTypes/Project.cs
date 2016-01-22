using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StepNCRest.Modules
{
    public class ProjectConfigFile
    {
        public String name;
    }

    public class Project
    {
        public String path;
        public String cadjsPath;
        public String modelPath;
        public String id;
        public String name;
        public bool hasCadjs;
        public bool hasModel;

        public string GetFilePath(string relativePath)
        {
            return Path.Combine(path, relativePath);
        }

        public static Project FindFromPath(string path)
        {
            string modelPath = path + "\\model";
            string cadjsPath = path + "\\cadjs";

            var proj = new Project();

            proj.path = path;
            proj.id = new DirectoryInfo(path).Name;
            proj.hasCadjs = Directory.Exists(cadjsPath);
            if (proj.hasCadjs) proj.cadjsPath = Path.Combine("/files", proj.id, "cadjs").Replace("\\","/");
            proj.hasModel = Directory.Exists(modelPath);
            if (proj.hasCadjs) proj.cadjsPath = Path.Combine("/files", proj.id, "model").Replace("\\", "/");

            var serializer = new JavaScriptSerializer();
            var configFileContents = File.ReadAllText(Path.Combine(path, "config.json"));
            var configFile = serializer.Deserialize<ProjectConfigFile>(configFileContents);

            proj.name = configFile.name;

            return proj;
        }

        public static Project Get(string id)
        {
            return FindFromPath(Path.Combine(Properties.Settings.Default.projectPath, id));
        }

        public static List<Project> GetAll()
        {
            string[] projectPaths = Directory.GetDirectories(Properties.Settings.Default.projectPath);
            return projectPaths.Select(path => FindFromPath(path)).ToList();
        }
    }
}

using Nancy;
using Nancy.Conventions;

namespace StepNCAPI
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("files", Properties.Settings.Default.projectPath));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
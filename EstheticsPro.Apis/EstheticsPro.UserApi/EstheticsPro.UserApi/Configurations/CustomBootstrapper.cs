using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace EstheticsPro.UserApi.Configurations
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.AfterRequest.AddItemToEndOfPipeline(x=>x.Response
                .WithHeaders("Access-Control-Allow-Origin", "*")
                .WithHeaders("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept"));
        }
    }
}
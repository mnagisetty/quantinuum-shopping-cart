namespace ShoppingCartService
{
    using Microsoft.AspNetCore.Builder;
    using Nancy.Owin;
    using Nancy.Configuration;
    using Nancy;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(options => options.Bootstrapper = new TracingBootstrapper()));
        }
    }

    public class TracingBootstrapper : Nancy.DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment env)
        {
            env.Tracing(enabled: true, displayErrorTraces: true);            
        }
    } 
}

using MintPlayer.AspNetCore.Hsts;
using MintPlayer.AspNetCore.SpaServices.Extensions;
using MintPlayer.AspNetCore.SpaServices.Prerendering;
using MintPlayer.AspNetCore.SpaServices.Routing;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSpaStaticFilesImproved(configuration => configuration.RootPath = "ClientApp/dist");
builder.Services.AddSpaPrerenderingService<SsrDemo.Services.SpaPrerenderingService>();
builder.WebHost.UseKestrelHttpsConfiguration();

var app = builder.Build();

app.UseImprovedHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

if (!builder.Environment.IsDevelopment())
{
    app.UseSpaStaticFilesImproved();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// Allows us to customize the regexes to match the lines received from Angular CLI
app.UseSpaImproved(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    spa.UseSpaPrerendering(options =>
    {
        options.BootModuleBuilder = builder.Environment.IsDevelopment() ? new AngularPrerendererBuilder(npmScript: "build:ssr", @"Build at\:", 1) : null;
        options.BootModulePath = $"{spa.Options.SourcePath}/dist/server/main.js";
        options.ExcludeUrls = new[] { "/sockjs-node" };
    });
    if (builder.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start", cliRegexes: [new Regex(@"Local\:\s+(?<openbrowser>https?\:\/\/(.+))")]);

        // When you want to start Angular CLI manually with `npm start`
        //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});


app.Run();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    string htmlContent = System.IO.File.ReadAllText("wwwroot/index.html");
    return Results.Content(htmlContent, "text/html");
});
app.MapGet("/script.js", () =>
{
    string jscontent = System.IO.File.ReadAllText("wwwroot/script.js");
    return Results.Content(jscontent, "text/js");
});
app.MapGet("/style.css", () =>
{
    string cssContent = System.IO.File.ReadAllText("wwwroot/style.css");
    return Results.Content(cssContent, "text/css");
});

app.Run();

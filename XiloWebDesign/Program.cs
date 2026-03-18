var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();   // sirve index.html por defecto en "/"
app.UseStaticFiles();    // sirve styles.css, script.js, images/

app.Run();

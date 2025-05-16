using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://keycloaksso:8080/realms/sso"; // URL Keycloak
    options.ClientId = "client1";
    options.ClientSecret = "OOFuMAwf7UulmjU9Bt4SlpcGqDHAYrcx";
    options.ResponseType = "code";

    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.Scope.Add("openid");
    options.Scope.Add("profile");

    options.TokenValidationParameters.NameClaimType = "name";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo("/home/app/.aspnet/DataProtection-Keys")).SetApplicationName("mvcapp3");

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(8081);
    //o.ListenAnyIP(8443, listeno =>
    //{
    //    listeno.UseHttps("/https/aspnetapp.pfx", "MvcApp3Pass");
    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

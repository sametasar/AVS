using AVSGLOBAL.Class;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Class.Dal;
using AVSGLOBAL.Class.Global;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddTransient<IUser, Cls_User>();
builder.Services.AddTransient<ITokenService, Cls_TokenService>();

Cls_Settings.DefaultConnection = builder.Configuration["Settings:DefaultConnection"];
Cls_Settings.DefaultEmail = builder.Configuration["Settings:DefaultEmail"];
Cls_Settings.DefaultEmailPassword = builder.Configuration["Settings:DefaultEmailPassword"];
Cls_Settings.SMTPPort = builder.Configuration["Settings:SMTPPort"];
Cls_Settings.JWTKEY = builder.Configuration["Settings:JWTKEY"];
Cls_Settings.JWTISSUER = builder.Configuration["Settings:ISSUER"];
Cls_Settings.JWTAUDIENCE = builder.Configuration["Settings:AUDIENCE"];


builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Cls_Settings.JWTISSUER,
                   ValidAudience = Cls_Settings.JWTISSUER,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY))
               };
           });


//builder.Services.AddAuthentication(auth =>
//{
//    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//       .AddJwtBearer(options =>
//       {
//           options.TokenValidationParameters = new TokenValidationParameters
//           {
//               ValidateIssuer = true,
//               ValidateAudience = true,
//               ValidateLifetime = true,
//               ValidateIssuerSigningKey = true,
//               ValidIssuer = builder.Configuration["Jwt:Issuer"],
//               ValidAudience = builder.Configuration["Jwt:Issuer"],
//               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//           };
//       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseSession();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }

    await next();

});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPageReact}/{id?}");

app.Run();

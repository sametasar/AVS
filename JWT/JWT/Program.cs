using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWT.Interface;
using JWT.Class.GlobalClass;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = "AVS API", Version = "v1" });
});

Cls_Settings.DefaultConnection = builder.Configuration["Settings:DefaultConnection"];
Cls_Settings.DefaultEmail = builder.Configuration["Settings:DefaultEmail"];
Cls_Settings.DefaultEmailPassword = builder.Configuration["Settings:DefaultEmailPassword"];
Cls_Settings.SMTPPort = builder.Configuration["Settings:SMTPPort"];
Cls_Settings.JWTKEY = builder.Configuration["Settings:JWTKEY"];
Cls_Settings.JWTISSUER = builder.Configuration["Settings:ISSUER"];
Cls_Settings.JWTAUDIENCE = builder.Configuration["Settings:AUDIENCE"];

/// <summary>
/// Web uygulamamýzda token ne kadar süre devrede kalacak bilgisi!
/// </summary>
/// <returns></returns>
Cls_Settings.TokenExpireMinute = Convert.ToDouble(builder.Configuration["Settings:TokenExpireMinute"]);

//Cls_Jwt
builder.Services.AddAuthentication(x => {

    //Middleware yazýyoruz, bizim sistemimiz üzerinde çalýþan servislere Authentication (Kimlik Doðrulama Sistemini) ne þekilde çalýþtýracaðýný tanýmlýyoruz.

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(y => {

    y.RequireHttpsMetadata = false;
    y.SaveToken = true;
    y.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY)),
        ValidateIssuer = false,
        ValidateAudience = false


    };
    //Buradada Bearer mekanizmasýnýn ayarlarýný yapýyoruz. Yani yukarýdaki Authentication yapýsýný burada belirttiðim ayarlara göre ekleyeceksin!

});

builder.Services.AddSingleton<IJWT>(new Cls_Jwt(builder.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AVS API v1"));
    
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

//Önce otantike
app.UseAuthentication();

//sonra doðrulamalar
app.UseAuthorization();

app.MapControllers();

app.Run();

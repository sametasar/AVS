using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWT.Interface;
using JWT.Class.GlobalClass;
using Microsoft.AspNetCore.HttpOverrides;
using JWT.Class.Dal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region GLOBAL VARÝABLE

Cls_Settings.DefaultConnection = builder.Configuration["Settings:DefaultConnection"];
Cls_Settings.DefaultEmail = builder.Configuration["Settings:DefaultEmail"];
Cls_Settings.DefaultEmailPassword = builder.Configuration["Settings:DefaultEmailPassword"];
Cls_Settings.SMTPPort = builder.Configuration["Settings:SMTPPort"];
Cls_Settings.JWTKEY = builder.Configuration["Settings:JWTKEY"];
Cls_Settings.JWTISSUER = builder.Configuration["Settings:ISSUER"];
Cls_Settings.JWTAUDIENCE = builder.Configuration["Settings:AUDIENCE"];

/// <summary>
/// String þifrelemede kullanýlan key
/// </summary>
Cls_Settings.DefaultPasswordKey = builder.Configuration["Settings:DefaultPasswordKey"];

/// <summary>
/// Web uygulamamýzda token ne kadar süre devrede kalacak bilgisi!
/// </summary>
/// <returns></returns>
Cls_Settings.TokenExpireMinute = Convert.ToDouble(builder.Configuration["Settings:TokenExpireMinute"]);

Cls_Settings.SelectDatabseEngine = builder.Configuration["Settings:SelectDatabseEngine"];

//Ana Web Servisi Burada Belirtiyoruz! Tüm Gelen Requestleri Buraya Aktarabiliriz! Extar güvenli bir yapý inþaa edilmek istenirse.
//Ana servisin ip adresi bu sayede yalnýzca bizim tarafýmýzdan bilinecek istenildiði durumda ip trafiði deðiþtirilebilinecek.
//Cls_Settings.MAIN_WEB_SERVICE = builder.Configuration["Settings:MAIN_WEB_SERVICE"];

/* #region  DEFAULT MS SQL CONNECTION */
Cls_DefaultMsSql.UserName = builder.Configuration.GetSection("MSSQL").GetSection("USER").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultMsSql.Password = builder.Configuration.GetValue<string>("MSSQL:PASSWORD");
Cls_DefaultMsSql.Database = builder.Configuration.GetValue<string>("MSSQL:DATABASE");
Cls_DefaultMsSql.Server = builder.Configuration.GetValue<string>("MSSQL:SERVER");
/* #endregion */


/* #region  DEFAULT SQL LITE CONNECTION */
Cls_DefaultSqlLite.Directory = builder.Configuration.GetSection("SQLLITE").GetSection("Directory").Value;
Cls_DefaultSqlLite.DataSource = builder.Configuration.GetSection("SQLLITE").GetSection("DataSource").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultSqlLite.Version = builder.Configuration.GetValue<int>("SQLLITE:Version");
Cls_DefaultSqlLite.DefaultTimeout = builder.Configuration.GetValue<int>("SQLLITE:DefaultTimeout");
Cls_DefaultSqlLite.FailIfMissing = builder.Configuration.GetValue<bool>("SQLLITE:FailIfMissing");
Cls_DefaultSqlLite.Password = builder.Configuration.GetValue<string>("SQLLITE:Password");
Cls_DefaultSqlLite.Readonly = builder.Configuration.GetValue<bool>("SQLLITE:Readonly");
/* #endregion */

/* #region  DEFAULT MY SQL CONNECTION */
Cls_DefaultMySql.UserName = builder.Configuration.GetSection("MYSQL").GetSection("USER").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultMySql.Password = builder.Configuration.GetValue<string>("MYSQL:PASSWORD");
Cls_DefaultMySql.Database = builder.Configuration.GetValue<string>("MYSQL:DATABASE");
Cls_DefaultMySql.Server = builder.Configuration.GetValue<string>("MYSQL:SERVER");
/* #endregion */


/* #region  DEFAULT POSTGRE SQL CONNECTION */
Cls_DefaultPostgreSql.UserName = builder.Configuration.GetSection("POSTGRESQL").GetSection("USER").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultPostgreSql.Password = builder.Configuration.GetValue<string>("POSTGRESQL:PASSWORD");
Cls_DefaultPostgreSql.Database = builder.Configuration.GetValue<string>("POSTGRESQL:DATABASE");
Cls_DefaultPostgreSql.Server = builder.Configuration.GetValue<string>("POSTGRESQL:SERVER");
/* #endregion */


/* #region  DEFAULT ORACLE SQL CONNECTION */
Cls_DefaultOracle.UserName = builder.Configuration.GetSection("ORACLE").GetSection("USER").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultOracle.Password = builder.Configuration.GetValue<string>("ORACLE:PASSWORD");
Cls_DefaultOracle.Database = builder.Configuration.GetValue<string>("ORACLE:DATABASE");
Cls_DefaultOracle.Server = builder.Configuration.GetValue<string>("ORACLE:SERVER");
/* #endregion */



/* #region  DEFAULT MONGODB CONNECTION */
Cls_DefaultMongodb.UserName = builder.Configuration.GetSection("MONGODB").GetSection("USER").Value; //  Configuration["MSSQLUSER"];
Cls_DefaultMongodb.Password = builder.Configuration.GetValue<string>("MONGODB:PASSWORD");
Cls_DefaultMongodb.Database = builder.Configuration.GetValue<string>("MONGODB:DATABASE");
Cls_DefaultMongodb.Server = builder.Configuration.GetValue<string>("MONGODB:SERVER");
/* #endregion */

#endregion


// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    Enm_Database_Type DatabaseType = Enm_Database_Type.SqlLite;

    foreach (Enm_Database_Type foo in Enum.GetValues(typeof(Enm_Database_Type)))
    {
        if (foo.GetHashCode().ToString() == Cls_Settings.SelectDatabseEngine)
        {
            DatabaseType = foo;
        }
    }

    switch (DatabaseType)
    {
        case Enm_Database_Type.SqlServer:
            options.UseSqlServer(Cls_Tools.DefaultSqlServerConnectionString());
            break;

            case Enm_Database_Type.SqlLite:
            options.UseSqlite(Cls_Tools.DefaultSqliteConnectionString());
            break;

        default:
            options.UseSqlite(Cls_Tools.DefaultSqliteConnectionString());
            break;
    }

}
);


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = "AVS API", Version = "v1" });
});


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



builder.WebHost.UseKestrel
  (options =>
  {
      options.Limits.MaxRequestBodySize = long.MaxValue;
      options.Limits.MaxRequestBufferSize = long.MaxValue;
      options.Limits.MaxResponseBufferSize = long.MaxValue;
      options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(300);
      options.Limits.MaxConcurrentConnections = long.MaxValue;
      //options.AllowResponseHeaderCompression = true;

  });



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

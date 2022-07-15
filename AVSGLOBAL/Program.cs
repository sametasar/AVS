using AVSGLOBAL.Class;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AVSGLOBAL.Class.Dal;
using AVSGLOBAL.Class.GlobalClass;


var builder = WebApplication.CreateBuilder(args);


#region GLOBAL VARİABLE

Cls_Settings.DefaultConnection = builder.Configuration["Settings:DefaultConnection"];
Cls_Settings.DefaultEmail = builder.Configuration["Settings:DefaultEmail"];
Cls_Settings.DefaultEmailPassword = builder.Configuration["Settings:DefaultEmailPassword"];
Cls_Settings.SMTPPort = builder.Configuration["Settings:SMTPPort"];
Cls_Settings.JWTKEY = builder.Configuration["Settings:JWTKEY"];
Cls_Settings.JWTISSUER = builder.Configuration["Settings:ISSUER"];
Cls_Settings.JWTAUDIENCE = builder.Configuration["Settings:AUDIENCE"];

/// <summary>
/// String şifrelemede kullanılan key
/// </summary>
Cls_Settings.DefaultPasswordKey = builder.Configuration["Settings:DefaultPasswordKey"];

/// <summary>
/// Web uygulamamızda token ne kadar süre devrede kalacak bilgisi!
/// </summary>
/// <returns></returns>
Cls_Settings.TokenExpireMinute = Convert.ToDouble(builder.Configuration["Settings:TokenExpireMinute"]);

Cls_Settings.SelectDatabseEngine = builder.Configuration["Settings:SelectDatabseEngine"];

//Ana Web Servisi Burada Belirtiyoruz! Tüm Gelen Requestleri Buraya Aktarabiliriz! Extar güvenli bir yapı inşaa edilmek istenirse.
//Ana servisin ip adresi bu sayede yalnızca bizim tarafımızdan bilinecek istenildiği durumda ip trafiği değiştirilebilinecek.
Cls_Settings.MAIN_WEB_SERVICE = builder.Configuration["Settings:MAIN_WEB_SERVICE"];

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




builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddTransient<IUser, Cls_User>();
builder.Services.AddTransient<ITokenService, Cls_TokenService>();

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

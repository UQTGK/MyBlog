using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SqlSugarIOC
//注入 ORM
builder.Services.AddSqlSugar(new IocConfig()
{
    //ConnectionString = builder.Configuration.GetConnectionString("SqlConn"), //数据库连接串
    ConnectionString = builder.Configuration["SqlConn"],
    //ConnectionString = "server=.;uid=sa;pwd=haosql;database=SQLSUGAR4XTEST",
    DbType = IocDbType.MySql,   //数据库类型
    //DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true//自动释放
});
#endregion

builder.Services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
builder.Services.AddScoped<IBlogNewsService, BlogNewsService>();

builder.Services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
builder.Services.AddScoped<ITypeInfoService, TypeInfoService>();

builder.Services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
builder.Services.AddScoped<IWriterInfoService, WriterInfoService>();

//博客教程是这么写的
//public static class IOCExtend
//{
    //public static IServiceCollection AddCustomIOC(this IServiceCollection services)
    //{
        //services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
        //services.AddScoped<IBlogNewsService, BlogNewsService>();

        //services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
        //services.AddScoped<ITypeInfoService, TypeInfoService>();

        //services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
        //services.AddScoped<IWriterInfoService, WriterInfoService>();
        //return services;
    //}
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

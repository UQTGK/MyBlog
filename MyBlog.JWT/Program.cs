using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;

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
builder.Services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
builder.Services.AddScoped<IWriterInfoService, WriterInfoService>();

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

using MyLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BookDb>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(opt =>
{
    opt.LowercaseQueryStrings = true;
    opt.LowercaseUrls = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

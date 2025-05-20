var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Swagger �al���yor mu");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
Console.WriteLine("uygulama ba�lat�ld� ");
app.Run();

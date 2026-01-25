var builder = WebApplication.CreateBuilder(args);

// Agregamos los servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- AQUÍ ESTÁ EL TRUCO: ACTIVAR PERMISOS (CORS) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()  // Permitir cualquier origen
              .AllowAnyHeader()  // Permitir cualquier encabezado
              .AllowAnyMethod(); // Permitir GET, POST, PUT, DELETE
    });
});
// ---------------------------------------------------

var app = builder.Build();

// Configuración del entorno
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- AQUÍ APLICAMOS EL PERMISO ---
app.UseCors("PermitirTodo");
// ---------------------------------

app.UseAuthorization();

app.MapControllers();

app.Run();
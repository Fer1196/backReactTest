# Prueba Back Fernando

- Al levantar el frontend en Program.cs verificar que este la dirección del Front

``` js
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
``` 




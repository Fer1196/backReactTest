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
- Todas las APIS están aquí, al no específicar nombres se los puso con los métodos convenientes
  ![image](https://github.com/user-attachments/assets/0c8fab05-fc20-4e16-abb1-906a3174df46)




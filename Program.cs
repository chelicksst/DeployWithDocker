namespace DeployWithDocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем сервисы
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Добавляем CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowClient", policy =>
                {
                    policy.WithOrigins("https://client-1vew.onrender.com")  // Разрешаем запросы только с вашего нового клиента
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Конфигурируем конвейер HTTP запросов
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                var referer = context.Request.Headers["Referer"].ToString();
                if (!referer.StartsWith("https://client-1vew.onrender.com"))
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync("Access denied");
                    return;
                }
                await next();
            });

            // Включаем CORS
            app.UseCors("AllowClient");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

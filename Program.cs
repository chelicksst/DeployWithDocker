namespace DeployWithDocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ��������� �������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ��������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")  // ��������� ������� � ������ ���������� �������
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // ������������� �������� HTTP ��������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // �������� CORS
            app.UseCors("AllowLocalhost");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

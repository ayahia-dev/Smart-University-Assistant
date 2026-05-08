using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartUniversity.Application.DTOs.Auth;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Application.Services;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure;
using SmartUniversity.Infrastructure.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ================== Services ==================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentSubmissionRepository, AssignmentSubmissionRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuizSubmissionRepository, QuizSubmissionRepository>();
builder.Services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IStudentGradeRepository, StudentGradeRepository>();
builder.Services.AddScoped<IFacultyChecker, FacultyChecker>();
builder.Services.AddScoped<ICourseStudentQuery, CourseStudentQuery>();
builder.Services.AddScoped<ICourseChecker, CourseChecker>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IAssignmentSubmissionService, AssignmentSubmissionService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<QuizSubmissionService>();
builder.Services.AddScoped<FacultyService>();
builder.Services.AddScoped<AiIntegrationService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<LectureService>();
builder.Services.AddScoped<StudentGradeService>();
builder.Services.AddScoped<ILectureQuery, LectureQuery>();



builder.Services.AddHttpClient<AiService>();

// Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ================== Build ==================

var app = builder.Build();

// Seed admin
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 🟦 Admin
    if (!context.Users.Any(u => u.Email == "admin@smartuni.com"))
    {
        var admin = new User(
            "Admin User",
            "admin@smartuni.com",
            "11111111111111",
            BCrypt.Net.BCrypt.HashPassword("123456"),
            "Admin",
            1
        );

        context.Users.Add(admin);
        context.SaveChanges();
    }
}

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    // ❗ منع التكرار لو شغلت الكود تاني
//    if (context.Users.Any(u => u.Email.StartsWith("student") || u.Email.StartsWith("doctor")))
//        return;

//    var users = new List<User>();

//    var names = new[]
//    {
//        "Ahmed Yahiya", "Mohamed Hassan", "Ali Mahmoud", "Omar Khaled",
//        "Youssef Ahmed", "Hassan Ali", "Mahmoud Samy", "Mostafa Adel",
//        "Karim Nabil", "Tarek Ibrahim",
//        "Sara Mohamed", "Mona Ahmed", "Nour Ali", "Aya Hassan",
//        "Fatma Mahmoud", "Huda Ibrahim", "Salma Khaled", "Dina Samy",
//        "Yara Mostafa", "Menna Nabil"
//    };

//    int facultyCount = 6;

//    // 🟩 Students (40)
//    for (int i = 0; i < 40; i++)
//    {
//        var name = names[i % names.Length];
//        var email = $"student{i}@uni.com";
//        var nationalId = (10000000000000 + i).ToString(); // 🔥 unique

//        var user = new User(
//            name,
//            email,
//            nationalId,
//            BCrypt.Net.BCrypt.HashPassword("123456"),
//            "Student",
//            (i % facultyCount) + 1
//        );

//        users.Add(user);
//    }

//    // 🟦 Doctors (10)
//    for (int i = 0; i < 10; i++)
//    {
//        var name = "Dr. " + names[i % names.Length];
//        var email = $"doctor{i}@uni.com"; // ✔ صح
//        var nationalId = (20000000000000 + i).ToString(); // 🔥 range مختلف

//        var user = new User(
//            name,
//            email,
//            nationalId,
//            BCrypt.Net.BCrypt.HashPassword("123456"),
//            "Doctor",
//            (i % facultyCount) + 1
//        );

//        users.Add(user);
//    }

//    context.Users.AddRange(users);
//    context.SaveChanges();
//}


// ================== Middleware ==================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();

// ✅ CORS هنا (بعد Build وقبل Auth)
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
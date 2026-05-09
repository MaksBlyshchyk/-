# HRReserveSystem

Курсовий проєкт на тему "Система управління кадровим резервом".

## Технології

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 8
- SQLite
- Bootstrap

## Запуск

1. Переконайтесь, що встановлено .NET SDK 8:

   ```powershell
   dotnet --version
   ```

2. Відновіть залежності:

   ```powershell
   dotnet restore
   ```

3. Запустіть застосунок:

   ```powershell
   dotnet run
   ```

4. Відкрийте URL, який покаже консоль, наприклад `http://localhost:5000`.

SQLite база `hrreserve.db` створюється в корені проєкту автоматично під час запуску через EF Core migrations.

## Основні можливості

- Dashboard з кількістю кандидатів, вакансій, заявок і співбесід.
- CRUD для кандидатів.
- CRUD для вакансій.
- Заявки, які пов'язують кандидата з вакансією.
- Співбесіди для заявок.
- Відгуки після співбесід з оцінкою та рекомендацією.
- Оцінка soft skills кандидата.

## Структура проєкту

- `Models/` - сутності предметної області: `Candidate`, `Vacancy`, `Application`, `Interview`, `InterviewFeedback`, `SoftSkillAssessment`.
- `Data/ApplicationDbContext.cs` - EF Core контекст і налаштування SQLite-моделі.
- `Migrations/` - початкова міграція для створення таблиць.
- `Controllers/` - MVC-контролери для Dashboard і CRUD-операцій.
- `Views/` - Razor Views для списків, створення, редагування, деталей і видалення.
- `Views/Shared/_Layout.cshtml` - спільний layout з навігацією.
- `appsettings.json` - connection string `DefaultConnection` для SQLite.

## Важливі примітки

- Проєкт використовує тільки пакети EF Core версії `8.0.17`.
- У `Program.cs` використовується `app.UseStaticFiles()`.
- `MapStaticAssets()` і `WithStaticAssets()` не використовуються.

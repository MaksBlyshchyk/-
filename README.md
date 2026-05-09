# HRReserveSystem

HRReserveSystem - курсовий ASP.NET Core MVC проєкт на тему "Система управління кадровим резервом".

## Опис системи

Система призначена для рекрутерів і HR-фахівців, які ведуть базу кандидатів, вакансій, заявок, співбесід, відгуків та оцінок soft skills. Застосунок допомагає бачити поточний стан кадрового резерву, відстежувати статуси кандидатів і швидко знаходити релевантних людей для майбутніх вакансій.

## Стек технологій

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 8
- SQLite
- Bootstrap

## Функціональні можливості

- Dashboard з ключовими показниками:
  - кількість кандидатів;
  - кількість вакансій;
  - кількість заявок;
  - кількість співбесід;
  - кількість прийнятих кандидатів;
  - кількість відхилених кандидатів;
  - останні 5 кандидатів;
  - останні 5 вакансій.
- CRUD для кандидатів.
- CRUD для вакансій.
- CRUD для заявок, які пов'язують кандидата з вакансією.
- CRUD для співбесід.
- CRUD для відгуків після співбесід.
- CRUD для оцінок soft skills.
- Bootstrap UI з картками, таблицями, кнопками та badges для статусів.
- Автоматичне створення SQLite бази через EF Core migrations.
- Seed data, які додаються тільки якщо база порожня.

## Структура проєкту

- `Controllers/` - MVC-контролери для Dashboard, About і CRUD-операцій.
- `Data/ApplicationDbContext.cs` - EF Core контекст бази даних.
- `Data/SeedData.cs` - тестові дані для першого запуску.
- `Migrations/` - EF Core міграція для створення структури SQLite БД.
- `Models/` - сутності предметної області.
- `ViewModels/` - моделі для представлення даних у Views.
- `Views/` - Razor Views для сторінок застосунку.
- `Views/Shared/_Layout.cshtml` - спільний layout з навігаційним меню.
- `wwwroot/` - статичні файли: CSS, JS, Bootstrap, jQuery.
- `appsettings.json` - налаштування, зокрема connection string для SQLite.

## Структура БД

### Candidates

- `Id`
- `FullName`
- `Email`
- `Phone`
- `Skills`
- `ExperienceYears`
- `CreatedAt`

### Vacancies

- `Id`
- `Title`
- `Description`
- `Requirements`
- `SalaryMin`
- `SalaryMax`
- `Status`
- `CreatedAt`

### Applications

- `Id`
- `CandidateId`
- `VacancyId`
- `Status`
- `AppliedAt`

Зв'язки:

- `Applications.CandidateId` -> `Candidates.Id`
- `Applications.VacancyId` -> `Vacancies.Id`

### Interviews

- `Id`
- `ApplicationId`
- `InterviewDate`
- `InterviewType`
- `Result`
- `Notes`

Зв'язок:

- `Interviews.ApplicationId` -> `Applications.Id`

### InterviewFeedbacks

- `Id`
- `InterviewId`
- `Comment`
- `Score`
- `Recommendation`
- `CreatedAt`

Зв'язок:

- `InterviewFeedbacks.InterviewId` -> `Interviews.Id`

### SoftSkillAssessments

- `Id`
- `CandidateId`
- `Communication`
- `Teamwork`
- `Responsibility`
- `StressResistance`
- `Leadership`
- `OverallComment`

Зв'язок:

- `SoftSkillAssessments.CandidateId` -> `Candidates.Id`

## Інструкція запуску

1. Перевірити .NET SDK:

   ```powershell
   dotnet --version
   ```

   Потрібна версія .NET SDK 8.x.

2. Відновити NuGet-пакети:

   ```powershell
   dotnet restore
   ```

3. Запустити застосунок:

   ```powershell
   dotnet run
   ```

4. Відкрити URL з консолі, наприклад:

   ```text
   http://localhost:5000
   ```

SQLite файл `hrreserve.db` створюється в корені проєкту автоматично під час запуску.

## Команди міграцій

Додати нову міграцію:

```powershell
dotnet ef migrations add НазваМіграції
```

Застосувати міграції до бази:

```powershell
dotnet ef database update
```

Видалити останню міграцію, якщо вона ще не застосована до БД:

```powershell
dotnet ef migrations remove
```

Для цього проєкту варто використовувати EF Core CLI 8.x, щоб не змішувати інструменти з .NET 10.

## Що реалізовано

- ASP.NET Core MVC застосунок на .NET 8.
- Підключення SQLite через EF Core 8.
- Повна структура моделей для HR-системи.
- Початкова EF Core міграція.
- Автоматичне застосування міграцій при старті.
- Seed data:
  - 3 кандидати;
  - 3 вакансії;
  - 3 заявки;
  - 2 співбесіди;
  - 2 відгуки;
  - 2 оцінки soft skills.
- CRUD-сторінки для всіх основних сутностей.
- Dashboard з HR-метриками.
- About сторінка з описом системи.
- Bootstrap-інтерфейс з адаптивною навігацією, картками, таблицями, badges і кнопками.

## Що можна додати в майбутньому

- Авторизацію та ролі користувачів.
- Окремі ролі для рекрутера, HR-менеджера та адміністратора.
- Фільтрацію кандидатів за навичками, досвідом і статусом.
- Пошук вакансій за зарплатою та вимогами.
- Експорт кандидатів у CSV або Excel.
- Завантаження резюме кандидата.
- Календар співбесід.
- Email-нагадування про майбутні співбесіди.
- Аналітичні графіки для HR-метрик.

## Обмеження

- Проєкт використовує тільки NuGet-пакети EF Core версії `8.0.17`.
- У `Program.cs` використовується `app.UseStaticFiles()`.
- `MapStaticAssets()` і `WithStaticAssets()` не використовуються.

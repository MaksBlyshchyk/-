# HRReserveSystem

HRReserveSystem - курсовий ASP.NET Core MVC проєкт на тему "Система управління кадровим резервом".

## Опис проєкту

Система призначена для рекрутерів та HR-фахівців. Вона допомагає вести базу кандидатів, вакансій, заявок, співбесід, відгуків та оцінок soft skills. На Dashboard видно стан кадрового резерву, етапи відбору, статуси вакансій і найближчі співбесіди.

## Стек

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 8
- SQLite
- Bootstrap
- Cookie authentication без ASP.NET Identity

## Авторизація

Після запуску відкрийте застосунок і увійдіть через сторінку Login.

Основний тестовий користувач:

- Login: `admin`
- Password: `admin123`
- Role: `Admin`

Додаткові демо-користувачі для перевірки ролей:

| Login | Password | Role |
| --- | --- | --- |
| `admin` | `admin123` | Admin |
| `recruiter` | `recruiter123` | Recruiter |
| `interviewer` | `interviewer123` | Interviewer |

## Ролі користувачів

- Admin: повний доступ до всіх CRUD-сторінок.
- Recruiter: кандидати, вакансії, заявки, співбесіди, відгуки та soft skills.
- Interviewer: перегляд співбесід і додавання feedback.

CRUD-сторінки захищені від неавторизованих користувачів через cookie authentication і атрибути `[Authorize]`.

## Функції

- Dashboard:
  - кількість кандидатів;
  - кількість вакансій;
  - кількість заявок;
  - кількість співбесід;
  - кількість прийнятих кандидатів;
  - кількість відхилених кандидатів;
  - останні 5 кандидатів;
  - останні 5 вакансій;
  - найближчі співбесіди.
- CRUD для кандидатів.
- CRUD для вакансій.
- CRUD для заявок.
- CRUD для співбесід.
- CRUD для відгуків після співбесід.
- CRUD для оцінок soft skills.
- Пошук кандидатів за ПІБ, email і навичками.
- Фільтр кандидатів за мінімальним досвідом.
- Пошук вакансій за назвою.
- Фільтр вакансій за статусом.
- Фільтр заявок за статусом відбору.
- Bootstrap UI: cards, badges, красиві таблиці, однакові кнопки, адаптивна верхня навігація.
- Empty states для порожніх списків.
- Seed data для демонстрації на захисті.

## Структура проєкту

- `Controllers/` - MVC-контролери для Dashboard, Login/Logout і CRUD-операцій.
- `Data/ApplicationDbContext.cs` - EF Core контекст бази даних.
- `Data/SeedData.cs` - демонстраційні дані, які додаються тільки якщо база порожня.
- `Docs/CourseMaterials.md` - матеріали для курсової: ER-діаграма, Use Case, MVC, таблиці, інструкція користувача, висновки.
- `Migrations/` - EF Core migrations для SQLite.
- `Models/` - сутності предметної області.
- `Services/DemoUserService.cs` - тестові користувачі та ролі для cookie authentication.
- `ViewModels/` - ViewModel-класи для Dashboard і Login.
- `Views/` - Razor Views.
- `Views/Shared/_Layout.cshtml` - спільний layout з навігацією.
- `wwwroot/` - статичні файли CSS/JS і Bootstrap.

## Структура БД

### Candidates

Зберігає кандидатів кадрового резерву.

- `Id`
- `FullName`
- `Email`
- `Phone`
- `Skills`
- `ExperienceYears`
- `CreatedAt`

### Vacancies

Зберігає вакансії та їхні статуси.

- `Id`
- `Title`
- `Description`
- `Requirements`
- `SalaryMin`
- `SalaryMax`
- `Status`
- `CreatedAt`

### Applications

Зв'язує кандидата з вакансією та показує етап відбору.

- `Id`
- `CandidateId`
- `VacancyId`
- `Status`
- `AppliedAt`

### Interviews

Зберігає історію і план співбесід.

- `Id`
- `ApplicationId`
- `InterviewDate`
- `InterviewType`
- `Result`
- `Notes`

### InterviewFeedbacks

Зберігає коментарі, оцінки та рекомендації після співбесіди.

- `Id`
- `InterviewId`
- `Comment`
- `Score`
- `Recommendation`
- `CreatedAt`

### SoftSkillAssessments

Зберігає оцінки soft skills кандидата.

- `Id`
- `CandidateId`
- `Communication`
- `Teamwork`
- `Responsibility`
- `StressResistance`
- `Leadership`
- `OverallComment`

## Інструкція запуску

1. Переконайтесь, що встановлено .NET SDK 8.x:

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

4. Відкрийте URL, який покаже консоль, наприклад:

   ```text
   http://localhost:5000
   ```

5. Увійдіть:

   ```text
   admin / admin123
   ```

SQLite файл `hrreserve.db` створюється автоматично в корені проєкту. Міграції застосовуються при старті застосунку.

## Команди міграцій

Створити нову міграцію:

```powershell
dotnet ef migrations add НазваМіграції
```

Застосувати міграції:

```powershell
dotnet ef database update
```

Видалити останню незастосовану міграцію:

```powershell
dotnet ef migrations remove
```

Використовуйте EF Core CLI 8.x. Не використовуйте .NET 10 пакети або інструменти для цього проєкту.

## Що реалізовано

- ASP.NET Core MVC застосунок на .NET 8.
- EF Core 8 + SQLite.
- Cookie authentication без ASP.NET Identity.
- Ролі Admin, Recruiter, Interviewer.
- Захист CRUD-сторінок від неавторизованих користувачів.
- Dashboard з HR-метриками.
- Пошук і фільтрація для кандидатів, вакансій і заявок.
- CRUD для всіх основних сутностей.
- Seed data:
  - кілька кандидатів;
  - кілька вакансій;
  - кілька заявок;
  - співбесіди;
  - відгуки;
  - оцінки soft skills.
- About сторінка.
- Матеріали для курсової у `Docs/CourseMaterials.md`.

## Що можна додати в майбутньому

- Повноцінну ASP.NET Core Identity авторизацію.
- Збереження користувачів і ролей у БД.
- Завантаження резюме кандидатів.
- Експорт даних у Excel/CSV.
- Календар співбесід.
- Email-нагадування.
- Графіки HR-аналітики.
- Пагінацію великих таблиць.
- Журнал змін статусів кандидатів.

## Важливі обмеження

- Проєкт використовує `net8.0`.
- NuGet-пакети EF Core мають версію `8.0.17`.
- У `Program.cs` використовується `app.UseStaticFiles()`.
- `MapStaticAssets()` і `WithStaticAssets()` не використовуються.

# HRReserveSystem

Курсовий проєкт для варіанту №26: **"Система управління кадровим резервом (HR-система)"**.

## Опис

HRReserveSystem - веб-застосунок для рекрутерів, який дозволяє вести кадровий резерв: базу кандидатів і резюме, вакансії, заявки, історію співбесід, відгуки інтерв'юерів, оцінки soft skills та облік рекрутерів.

Система явно покриває вимоги варіанту №26:

- база кандидатів;
- база резюме кандидатів;
- база вакансій зі статусами;
- етапи відбору через заявки;
- історія співбесід;
- відгуки інтерв'юерів;
- оцінка soft skills;
- база рекрутерів;
- інструмент для щоденної роботи рекрутера.

## Стек

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 8
- SQLite
- Bootstrap
- Cookie authentication без ASP.NET Identity

## Авторизація

Система використовує cookie authentication. Користувачі зберігаються в таблиці `Recruiters`.

Тестові користувачі:

| Login | Password | Role |
| --- | --- | --- |
| `admin` | `admin123` | Admin |
| `recruiter` | `recruiter123` | Recruiter |
| `interviewer` | `interviewer123` | Interviewer |

Після входу користувач перенаправляється на Dashboard.

## Ролі

- Admin: повний доступ до всіх сторінок, включно з керуванням рекрутерами.
- Recruiter: Candidates, Vacancies, Applications, Interviews.
- Interviewer: Interviews, Feedbacks, SoftSkillAssessments.

CRUD-сторінки захищені атрибутами `[Authorize]`.

## Сторінки системи

- Login: вхід у систему.
- Logout: вихід із системи.
- AccessDenied: повідомлення про відсутність прав.
- Dashboard: загальна HR-аналітика.
- Candidates: база кандидатів і резюме.
- Vacancies: вакансії та їхні статуси.
- Applications: етапи відбору кандидатів.
- Interviews: історія та план співбесід з рекрутером.
- InterviewFeedbacks: відгуки інтерв'юерів і рекомендації.
- SoftSkillAssessments: оцінка soft skills кандидатів.
- Recruiters: база рекрутерів і користувачів системи.
- About: короткий опис мети системи.

## Dashboard

На головній сторінці показано:

- кількість кандидатів;
- кількість вакансій;
- кількість заявок;
- кількість співбесід;
- кількість рекрутерів;
- кількість прийнятих кандидатів;
- кількість відхилених кандидатів;
- останні кандидати;
- останні вакансії;
- найближчі співбесіди.

## Пошук і фільтри

- Candidates: пошук за ПІБ, email, навичками; фільтр за мінімальним досвідом.
- Vacancies: пошук за назвою; фільтр за статусом.
- Applications: фільтр за етапом відбору.
- Interviews: фільтр за типом і результатом.

## Структура БД

### Candidates

- `Id`
- `FullName`
- `Email`
- `Phone`
- `Skills`
- `ResumeSummary`
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

### Interviews

- `Id`
- `ApplicationId`
- `RecruiterId`
- `InterviewDate`
- `InterviewType`
- `Result`
- `Notes`

### InterviewFeedbacks

- `Id`
- `InterviewId`
- `RecruiterId`
- `Comment`
- `Score`
- `Recommendation`
- `CreatedAt`

### SoftSkillAssessments

- `Id`
- `CandidateId`
- `Communication`
- `Teamwork`
- `Responsibility`
- `StressResistance`
- `Leadership`
- `OverallComment`

### Recruiters

- `Id`
- `FullName`
- `Email`
- `Login`
- `Password`
- `Role`
- `CreatedAt`

## Структура проєкту

- `Controllers/` - MVC-контролери.
- `Models/` - моделі предметної області.
- `Data/ApplicationDbContext.cs` - EF Core DbContext.
- `Data/SeedData.cs` - демо-дані.
- `Migrations/` - EF Core migrations.
- `Views/` - Razor Views.
- `ViewModels/` - ViewModels для Dashboard і Login.
- `Services/DemoUserService.cs` - перевірка користувачів із таблиці Recruiters.
- `Docs/CourseMaterials.md` - матеріали для курсової.
- `wwwroot/` - CSS, JS, Bootstrap.

## Запуск

1. Перевірити .NET SDK 8:

   ```powershell
   dotnet --version
   ```

2. Відновити пакети:

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

5. Увійти:

   ```text
   admin / admin123
   ```

SQLite база `hrreserve.db` створюється автоматично. Міграції застосовуються при старті застосунку.

## Команди міграцій

Створити міграцію:

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

Для цього проєкту потрібно використовувати EF Core CLI 8.x.

## Що реалізовано

- ASP.NET Core MVC застосунок на .NET 8.
- EF Core 8 + SQLite.
- Cookie authentication.
- Ролі Admin, Recruiter, Interviewer.
- Повна сутність Recruiter.
- Зв'язок Recruiter з Interviews.
- Зв'язок Recruiter з InterviewFeedbacks.
- База резюме через `Candidate.ResumeSummary`.
- CRUD для кандидатів, вакансій, заявок, співбесід, відгуків, soft skills і рекрутерів.
- Dashboard з HR-метриками.
- Пошук і фільтри.
- Bootstrap UI з контрастними таблицями, cards, badges і empty states.
- Seed data для захисту.

## Що можна додати в майбутньому

- Хешування паролів замість демо-поля `Password`.
- ASP.NET Core Identity.
- Завантаження файлів резюме.
- Пагінацію таблиць.
- Експорт у Excel/CSV.
- Календар співбесід.
- Email-нагадування.
- Графіки HR-аналітики.

## Обмеження

- Проєкт використовує `net8.0`.
- NuGet-пакети EF Core мають версію `8.0.17`.
- У `Program.cs` використовується `app.UseStaticFiles()`.
- `MapStaticAssets()` і `WithStaticAssets()` не використовуються.
- .NET 10 пакети не використовуються.

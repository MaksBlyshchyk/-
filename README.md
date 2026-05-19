# HRReserveSystem

Курсовий проєкт за варіантом №26: **Система управління кадровим резервом (HR-система)**.

## Опис

HRReserveSystem - це ASP.NET Core MVC веб-застосунок для рекрутерів, адміністраторів та інтерв'юерів. Система допомагає вести базу кандидатів, резюме, вакансій, заявок, співбесід, відгуків інтерв'юерів та оцінок soft skills.

Проблема, яку вирішує система: рекрутеру потрібно бачити історію взаємодії з кандидатом, етап відбору, статус вакансії, результати співбесід і якість soft skills в одному місці.

## Стек технологій

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 8
- SQLite
- Bootstrap
- Cookie Authentication без ASP.NET Identity

У проєкті не використовуються .NET 10 пакети, `MapStaticAssets()` і `WithStaticAssets()`. У `Program.cs` використовується `app.UseStaticFiles()`.

## Авторизація

Користувачі зберігаються в таблиці `Recruiters`. `DemoUserService` перевіряє логін або email і пароль з БД, тобто це навчальна демо-авторизація без ASP.NET Identity.

Тестові користувачі:

| Login | Password | Role |
| --- | --- | --- |
| `admin` | `admin123` | Admin |
| `recruiter` | `recruiter123` | Recruiter |
| `interviewer` | `interviewer123` | Interviewer |

Публічна реєстрація не передбачена. Користувачів створює адміністратор через розділ **«Рекрутери»**. На сторінці Login немає кнопки створення акаунта.

Права доступу:

- Admin: повний доступ до всіх сторінок.
- Recruiter: Candidates, Vacancies, Applications, Interviews.
- Interviewer: Interviews, InterviewFeedbacks, SoftSkillAssessments.
- Неавторизований користувач: Login і About; CRUD-сторінки захищені через `[Authorize]`.

## Можливості

- Dashboard з кількістю кандидатів, вакансій, заявок, співбесід, рекрутерів, прийнятих і відхилених кандидатів.
- Останні 5 кандидатів, останні 5 вакансій, найближчі співбесіди.
- CRUD для Candidates, Vacancies, Applications, Interviews, InterviewFeedbacks, SoftSkillAssessments, Recruiters.
- Пошук кандидатів за ПІБ, email і навичками.
- Фільтр кандидатів за мінімальним досвідом.
- CSV export кандидатів із урахуванням поточного пошуку та фільтра досвіду.
- Пошук вакансій за назвою та фільтр за статусом.
- Фільтр заявок за етапом відбору.
- Фільтр співбесід за типом і результатом.
- Відображення середнього soft skills score на Dashboard, у списку оцінок і в деталях кандидата.
- Bootstrap UI: cards, badges, читабельні таблиці, однакові кнопки, empty states.
- Фінальний UI-pass: посилено контраст таблиць і статусів, `warning`/`info` badges мають темний текст, Soft Skills показує оцінки через читабельні score badges.
- Світла й темна тема інтерфейсу з перемикачем у navbar; вибір теми зберігається в `localStorage`.
- Покращений HR dashboard: м'які тіні, акуратні cards, адаптивні таблиці, контрастні badges і єдиний стиль форм.
- Desktop layout із лівим sidebar, активним пунктом меню, профілем користувача та швидким Logout.
- Mobile-friendly layout: верхня панель, bottom navigation для основних розділів і offcanvas-меню "Ще".
- Dashboard містить KPI-карти, останніх кандидатів, останні вакансії, найближчі співбесіди, статистику заявок і вакансій за статусами.

## Структура проєкту

- `Models/` - сутності предметної області.
- `Data/ApplicationDbContext.cs` - EF Core контекст.
- `Data/SeedData.cs` - демо-дані та нормалізація старих записів.
- `Controllers/` - MVC контролери.
- `Views/` - Razor Views.
- `ViewModels/` - моделі для Login і Dashboard.
- `Services/DemoUserService.cs` - перевірка demo-login з таблиці `Recruiters`.
- `Migrations/` - міграції SQLite.
- `Docs/CourseMaterials.md` - матеріали для захисту: ER, Use Case, MVC, таблиці, інструкція.
- `wwwroot/` - Bootstrap, CSS, JS.

## Структура БД

### Candidates

- `Id`
- `FullName`
- `Email`
- `Phone`
- `Skills`
- `ExperienceYears`
- `ResumeFilePath`
- `ResumeSummary`
- `CreatedAt`

### Vacancies

- `Id`
- `Title`
- `Description`
- `Requirements`
- `SalaryMin`
- `SalaryMax`
- `Status`: `Open`, `Paused`, `Closed`
- `CreatedAt`

### Applications

- `Id`
- `CandidateId`
- `VacancyId`
- `Status`: `New`, `Screening`, `Interview`, `TestTask`, `Offer`, `Hired`, `Rejected`
- `AppliedAt`

### Interviews

- `Id`
- `ApplicationId`
- `RecruiterId`
- `InterviewDate`
- `InterviewType`: `HR`, `Technical`, `Final`
- `Result`: `Pending`, `Passed`, `Failed`
- `Notes`

### InterviewFeedbacks

- `Id`
- `InterviewId`
- `RecruiterId`
- `Comment`
- `Score`
- `Recommendation`: `Hire`, `Maybe`, `Reject`
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

Оцінки soft skills мають валідацію від 1 до 10. У views показується середній бал.

### Recruiters

- `Id`
- `FullName`
- `Email`
- `Login`
- `Password`
- `Role`: `Admin`, `Recruiter`, `Interviewer`
- `CreatedAt`

## Зв'язки

- Candidate має багато Applications.
- Vacancy має багато Applications.
- Application має Candidate, Vacancy і багато Interviews.
- Interview має Application, Recruiter і багато Feedbacks.
- InterviewFeedback має Interview і Recruiter.
- SoftSkillAssessment має Candidate.
- Recruiter має Interviews і InterviewFeedbacks.

## Seed data

Якщо HR-таблиці порожні, система додає:

- 3 рекрутери;
- 5 кандидатів;
- 3 вакансії;
- 5 заявок;
- 3 співбесіди;
- 3 відгуки;
- 3 оцінки soft skills.

Якщо база вже містить HR-дані, seed не дублює записи, а тільки гарантує наявність demo-користувачів і нормалізує старі статуси до актуальних значень.

## Запуск

```bash
dotnet restore
dotnet build
dotnet run
```

Після запуску відкрийте URL з консолі, зазвичай `http://localhost:5000` або порт із `launchSettings.json`.

SQLite база створюється автоматично. У `Program.cs` виконується `Database.Migrate()`, тому міграції застосовуються під час запуску.

## Команди міграцій

```bash
dotnet ef migrations add НазваМіграції
dotnet ef database update
dotnet ef migrations remove
```

## Як перевірити систему

1. Відкрити `/Account/Login`.
2. Увійти як `admin / admin123`.
3. Перевірити Dashboard: `/`.
4. Перевірити перемикач світлої/темної теми. Обрана тема зберігається в `localStorage` і не скидається після оновлення сторінки.
5. Відкрити основні сторінки:
   - `/Candidates`
   - `/Vacancies`
   - `/Applications`
   - `/Interviews`
   - `/InterviewFeedbacks`
   - `/SoftSkillAssessments`
   - `/Recruiters`
   - `/Home/About`
6. Для кожної сутності перевірити Index, Create, Edit, Details, Delete.
7. У розділі `/Candidates` перевірити пошук, фільтр за досвідом і кнопку CSV export.
8. На desktop перевірити sidebar, на mobile - bottom navigation і offcanvas-меню.
9. Увійти як `recruiter` і перевірити, що Feedbacks, Soft Skills і Recruiters недоступні.
10. Увійти як `interviewer` і перевірити доступ тільки до Interviews, Feedbacks і Soft Skills.

## Реалізовано за варіантом №26

- База кандидатів.
- База резюме через `ResumeFilePath` і `ResumeSummary`.
- CSV export бази кандидатів.
- Вакансії зі статусами.
- Заявки як етапи відбору.
- Історія співбесід.
- Відгуки інтерв'юерів.
- Оцінка soft skills.
- Таблиця рекрутерів і ролі користувачів.
- Dashboard для рекрутера.

## Відомі обмеження

- Авторизація демонстраційна: паролі зберігаються у полі `Password`, без хешування.
- Публічна реєстрація не передбачена: користувачів створює адміністратор системи через розділ «Рекрутери».
- Завантаження PDF не реалізовано: `ResumeFilePath` зберігає шлях або назву файлу резюме.
- Немає email-сповіщень і календарної інтеграції.
- Немає повноцінного ASP.NET Identity, reset password і реєстрації користувачів.

## Що можна додати в майбутньому

- ASP.NET Identity з хешуванням паролів.
- Завантаження PDF-резюме.
- Коментарі рекрутера до кожного етапу відбору.
- Інтеграцію з календарем для співбесід.
- Розширену аналітику soft skills і pipeline-воронку.

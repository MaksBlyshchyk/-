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
- ASP.NET Core Identity з cookie authentication

У проєкті не використовуються .NET 10 пакети, `MapStaticAssets()` і `WithStaticAssets()`. У `Program.cs` використовується `app.UseStaticFiles()`.

## Авторизація

Користувачі предметної області зберігаються в таблиці `Recruiters`, а вхід у систему виконується через ASP.NET Core Identity. Під час seed/demo-ініціалізації для рекрутерів створюються Identity-користувачі, ролі `Admin`, `Recruiter`, `Interviewer` і хешовані паролі.

Поле `Recruiters.Password` залишене для навчальної демонстрації та швидкого заповнення тестових акаунтів на сторінці Login. Для фактичного входу використовується Identity password hash у таблицях `AspNetUsers`.

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
- Завантаження файлу резюме кандидата у форматах PDF, DOC або DOCX до 5 MB.
- Експорт кандидатів у Excel/CSV із урахуванням поточного пошуку та фільтра досвіду.
- Пошук вакансій за назвою та фільтр за статусом.
- Фільтр заявок за етапом відбору.
- Коментар рекрутера до заявки: причина статусу або наступний крок відбору.
- Фільтр співбесід за типом і результатом.
- Календар співбесід за датами з кандидатами, вакансіями, типом, результатом і відповідальним рекрутером.
- Пряме відкриття співбесіди в Google Calendar та експорт у `.ics` файл для Google Calendar або Outlook Calendar.
- Email-сповіщення при створенні або оновленні співбесіди. Якщо SMTP не налаштований, повідомлення зберігаються у локальній папці `EmailOutbox`.
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
- `Services/DemoUserService.cs` - список demo-користувачів для сторінки Login.
- `Services/IdentityRecruiterSyncService.cs` - синхронізація `Recruiters` з ASP.NET Identity.
- `Services/EmailNotificationService.cs` - SMTP або локальний outbox для email-сповіщень.
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
- `RecruiterComment`
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

## Email-сповіщення

Після створення або оновлення співбесіди система формує email для кандидата і призначеного рекрутера.

За замовчуванням реальна відправка вимкнена:

```json
"Email": {
  "Enabled": false,
  "Host": "",
  "Port": 587,
  "UseSsl": true,
  "UserName": "",
  "Password": "",
  "FromEmail": "no-reply@hrreserve.local",
  "FromName": "HR Reserve System",
  "OutboxPath": "EmailOutbox",
  "RedirectAllTo": ""
}
```

У такому режимі повідомлення не приходять на пошту, а зберігаються як `.txt` файли у папці `EmailOutbox`. Це зручно для демонстрації без паролів від поштового сервісу.

Щоб листи реально приходили, потрібно вказати SMTP-дані у `appsettings.json` або user secrets: `Enabled=true`, `Host`, `Port`, `UserName`, `Password`, `FromEmail`. Для Gmail зазвичай потрібен App Password, а не звичайний пароль акаунта.

Для захисту зручно заповнити `RedirectAllTo` своєю поштою. Тоді всі demo-сповіщення прийдуть на одну адресу, навіть якщо в кандидатів або рекрутерів записані навчальні email.

Безпечний спосіб налаштувати SMTP локально - через user secrets, щоб пароль не потрапив у Git:

```bash
dotnet user-secrets init
dotnet user-secrets set "Email:Enabled" "true"
dotnet user-secrets set "Email:Host" "smtp.gmail.com"
dotnet user-secrets set "Email:Port" "587"
dotnet user-secrets set "Email:UseSsl" "true"
dotnet user-secrets set "Email:UserName" "your-email@gmail.com"
dotnet user-secrets set "Email:Password" "your-app-password"
dotnet user-secrets set "Email:FromEmail" "your-email@gmail.com"
dotnet user-secrets set "Email:RedirectAllTo" "your-email@gmail.com"
```

Для Gmail значення `Email:Password` має бути саме App Password. Після цього перезапустіть `dotnet run` і створіть або оновіть співбесіду.

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
7. У розділі `/Candidates` перевірити пошук, фільтр за досвідом і кнопку експорту в Excel/CSV.
8. На desktop перевірити sidebar, на mobile - bottom navigation і offcanvas-меню.
9. Увійти як `recruiter` і перевірити, що Feedbacks, Soft Skills і Recruiters недоступні.
10. Увійти як `interviewer` і перевірити доступ тільки до Interviews, Feedbacks і Soft Skills.

## Реалізовано за варіантом №26

- База кандидатів.
- База резюме через `ResumeFilePath`, `ResumeSummary` і завантаження файлів PDF/DOC/DOCX.
- Експорт бази кандидатів у Excel/CSV.
- Вакансії зі статусами.
- Заявки як етапи відбору з коментарем рекрутера.
- Історія співбесід.
- Календар співбесід, пряме відкриття події в Google Calendar та `.ics` export для імпорту в календар.
- Email-сповіщення про створення та оновлення співбесід.
- Відгуки інтерв'юерів.
- Оцінка soft skills.
- Таблиця рекрутерів і ролі користувачів.
- Dashboard для рекрутера.

## Відомі обмеження

- Авторизація використовує ASP.NET Identity і хешовані паролі. Поле `Recruiters.Password` залишене як навчальне/demo-поле для швидкого показу тестових акаунтів.
- Публічна реєстрація не передбачена: користувачів створює адміністратор системи через розділ «Рекрутери».
- Завантаження резюме реалізовано як локальне збереження файлів у `wwwroot/uploads/resumes`; хмарне сховище не використовується.
- Email-сповіщення потребують SMTP-конфігурації для реальної відправки. Без SMTP вони зберігаються в `EmailOutbox` для демонстрації.
- Немає reset password і самостійної реєстрації користувачів.

## Що можна додати в майбутньому

- Reset password, підтвердження email та самостійну реєстрацію користувачів.
- OAuth/API-синхронізацію з Google Calendar або Outlook Calendar без ручного підтвердження події.
- Розширену аналітику soft skills і pipeline-воронку.

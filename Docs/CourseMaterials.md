# Матеріали для курсової роботи HRReserveSystem

## Тема

Система управління кадровим резервом.

## Мета системи

Створити веб-застосунок для рекрутерів, який дозволяє вести базу кандидатів, вакансій, заявок, співбесід, відгуків та оцінок soft skills.

## Постановка задачі

Варіант №26 передбачає створення HR-системи для управління кадровим резервом. Проблема полягає в тому, що рекрутеру потрібно зберігати резюме кандидатів, бачити історію співбесід, фіксувати відгуки інтерв'юерів і оцінювати soft skills в одному робочому інструменті.

Система має підтримувати базу резюме, етапи відбору, відгуки інтерв'юерів, статуси вакансій, рольовий доступ і демонстраційні облікові записи для перевірки.

## Опис предметної області

Предметна область охоплює процес роботи рекрутера з кадровим резервом: кандидат подає заявку на вакансію, заявка проходить етапи відбору, для заявки плануються співбесіди, інтерв'юер залишає відгук, а soft skills кандидата оцінюються за кількома критеріями. Рекрутери системи мають різні ролі: адміністратор керує всіма даними, рекрутер веде вакансії та кандидатів, інтерв'юер працює зі співбесідами, відгуками та оцінками soft skills.

## ER-діаграма бази даних

```mermaid
erDiagram
    CANDIDATES ||--o{ APPLICATIONS : "подає"
    VACANCIES ||--o{ APPLICATIONS : "має"
    APPLICATIONS ||--o{ INTERVIEWS : "проходить"
    INTERVIEWS ||--o{ INTERVIEW_FEEDBACKS : "отримує"
    CANDIDATES ||--o{ SOFT_SKILL_ASSESSMENTS : "оцінюється"
    RECRUITERS ||--o{ INTERVIEWS : "проводить"
    RECRUITERS ||--o{ INTERVIEW_FEEDBACKS : "залишає"

    CANDIDATES {
        int Id PK
        string FullName
        string Email
        string Phone
        string Skills
        string ResumeFilePath
        string ResumeSummary
        int ExperienceYears
        datetime CreatedAt
    }

    VACANCIES {
        int Id PK
        string Title
        string Description
        string Requirements
        decimal SalaryMin
        decimal SalaryMax
        string Status
        datetime CreatedAt
    }

    APPLICATIONS {
        int Id PK
        int CandidateId FK
        int VacancyId FK
        string Status
        datetime AppliedAt
    }

    INTERVIEWS {
        int Id PK
        int ApplicationId FK
        int RecruiterId FK
        datetime InterviewDate
        string InterviewType
        string Result
        string Notes
    }

    INTERVIEW_FEEDBACKS {
        int Id PK
        int InterviewId FK
        int RecruiterId FK
        string Comment
        int Score
        string Recommendation
        datetime CreatedAt
    }

    SOFT_SKILL_ASSESSMENTS {
        int Id PK
        int CandidateId FK
        int Communication
        int Teamwork
        int Responsibility
        int StressResistance
        int Leadership
        string OverallComment
    }

    RECRUITERS {
        int Id PK
        string FullName
        string Email
        string Login
        string Password
        string Role
        datetime CreatedAt
    }
```

## Use Case діаграма

```mermaid
flowchart LR
    Admin["Admin"]
    Recruiter["Recruiter"]
    Interviewer["Interviewer"]

    Login(("Увійти в систему"))
    Dashboard(("Переглянути Dashboard"))
    ManageCandidates(("Керувати кандидатами"))
    ManageVacancies(("Керувати вакансіями"))
    ManageApplications(("Керувати заявками"))
    ManageInterviews(("Планувати співбесіди"))
    ViewInterviews(("Переглядати співбесіди"))
    AddFeedback(("Керувати відгуками"))
    ManageSoftSkills(("Оцінювати soft skills"))

    Admin --> Login
    Admin --> Dashboard
    Admin --> ManageCandidates
    Admin --> ManageVacancies
    Admin --> ManageApplications
    Admin --> ManageInterviews
    Admin --> AddFeedback
    Admin --> ManageSoftSkills

    Recruiter --> Login
    Recruiter --> Dashboard
    Recruiter --> ManageCandidates
    Recruiter --> ManageVacancies
    Recruiter --> ManageApplications
    Recruiter --> ManageInterviews

    Interviewer --> Login
    Interviewer --> Dashboard
    Interviewer --> ViewInterviews
    Interviewer --> AddFeedback
    Interviewer --> ManageSoftSkills
```

## Архітектура MVC

Проєкт побудований за архітектурним шаблоном MVC:

- Model: класи `Candidate`, `Vacancy`, `Application`, `Interview`, `InterviewFeedback`, `SoftSkillAssessment` описують структуру даних і правила валідації.
- View: Razor Views відображають Dashboard, Login, About і CRUD-сторінки.
- Controller: контролери приймають HTTP-запити, працюють з EF Core через `ApplicationDbContext` і повертають відповідні Views.
- Data layer: `ApplicationDbContext` відповідає за доступ до SQLite, а `SeedData` створює демонстраційні дані.
- Authentication layer: `AccountController` і `DemoUserService` реалізують cookie authentication без ASP.NET Identity.

## Опис таблиць БД

| Таблиця | Призначення |
| --- | --- |
| `Candidates` | Зберігає кандидатів кадрового резерву, їхні контакти, навички та досвід. |
| `Vacancies` | Зберігає вакансії, вимоги, зарплатний діапазон і статус вакансії. |
| `Applications` | Пов'язує кандидата з вакансією та показує етап відбору. |
| `Interviews` | Зберігає історію та план співбесід за заявками. |
| `InterviewFeedbacks` | Зберігає оцінки, коментарі та рекомендації після співбесід. |
| `SoftSkillAssessments` | Зберігає оцінку soft skills кандидата за ключовими критеріями. |
| `Recruiters` | Зберігає користувачів системи, їх логіни, ролі та зв'язки зі співбесідами й відгуками. |

## Ролі користувачів

| Роль | Доступ |
| --- | --- |
| Admin | Повний доступ до всіх сторінок і CRUD-операцій. |
| Recruiter | Кандидати, вакансії, заявки та співбесіди. |
| Interviewer | Співбесіди, відгуки інтерв'юерів та оцінки soft skills. |

## Скріншоти роботи системи

Після запуску проєкту скріншоти зберігаються в папці `Docs/screenshots/`.

![Login](screenshots/login.png)

![Dashboard](screenshots/dashboard.png)

![Candidates](screenshots/candidates.png)

![Interviews](screenshots/interviews.png)

![Soft Skills](screenshots/soft-skills.png)

## Сценарій демонстрації

1. Запустити `dotnet restore`, `dotnet build`, `dotnet run`.
2. Увійти як `admin / admin123` і показати Dashboard, статистику статусів, середній soft skills score та всі CRUD-розділи.
3. У розділі "Кандидати" показати пошук, фільтр за досвідом, деталі кандидата із середнім soft skills score та CSV export.
4. У розділі "Вакансії" показати фільтр за статусом, у "Заявках" - фільтр за етапом відбору, у "Співбесідах" - фільтр за типом і результатом.
5. Увійти як `recruiter / recruiter123` і показати доступ тільки до Candidates, Vacancies, Applications, Interviews.
6. Увійти як `interviewer / interviewer123` і показати доступ до Interviews, InterviewFeedbacks, SoftSkillAssessments без доступу до кандидатів, вакансій, заявок і рекрутерів.

## Інструкція користувача

1. Запустити застосунок командою `dotnet run`.
2. Відкрити URL з консолі.
3. Увійти під одним з демо-користувачів:
   - `admin` / `admin123`
   - `recruiter` / `recruiter123`
   - `interviewer` / `interviewer123`
4. На Dashboard переглянути кількість кандидатів, вакансій, заявок, співбесід, прийнятих і відхилених кандидатів та середній soft skills score.
5. У розділі "Кандидати" додати або знайти кандидата за ПІБ, email, навичками чи досвідом, а також експортувати список у CSV.
6. У розділі "Вакансії" додати вакансію або відфільтрувати список за статусом.
7. У розділі "Заявки" відстежити етапи відбору кандидатів.
8. У розділі "Співбесіди" переглянути історію та найближчі співбесіди.
9. У розділі "Відгуки" додати оцінку і рекомендацію після співбесіди.
10. У розділі "Soft Skills" зафіксувати оцінку комунікації, командної роботи, відповідальності, стресостійкості та лідерства.

## Висновки

У результаті розроблено веб-застосунок, який відповідає темі кадрового резерву: система містить базу кандидатів, вакансії, заявки, історію співбесід, відгуки, оцінки soft skills і статуси відбору. Рекрутер отримує єдиний інтерфейс для ведення HR-процесу, а ролі користувачів обмежують доступ відповідно до обов'язків.

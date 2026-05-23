# Отчёт по практической работе №2

**Дисциплина:** работа с моделями данных, Entity Framework Core (Code First)  
**Выполнил:** Кобец Кирилл  
**Вариант:** 2  

---

## 1. Предметная область

Выбрана предметная область **«Блог»**: автор публикует **записи** (`BlogPost`), пользователи оставляют **комментарии** (`Comment`) к записям.

**Количество сущностей:** 2 (`BlogPost`, `Comment`).  
**Связь:** один ко многим (1:N) — у одной записи много комментариев.

---

## 2. Ограничения модели

| Ограничение | Реализация |
|-------------|------------|
| Первичные ключи | `Id` у обеих сущностей |
| Обязательные поля | `Title`, `Slug`, `AuthorName`, `Text` — `[Required]` |
| Длина строк | `[StringLength]` и Fluent API `HasMaxLength` |
| Уникальность | Уникальный индекс на `Slug` (Fluent API) |
| Имена столбцов | `[Column("...")]` |
| Не хранится в БД | `[NotMapped]` — свойство `Preview` у `BlogPost` |
| Внешний ключ | `Comment.PostId` → `BlogPost.Id`, каскадное удаление |

---

## 3. Использованные команды миграций

```powershell
cd G:\unik\cross\2\BlogEfCore
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Альтернатива: `dotnet run` из `G:\unik\cross\2\BlogEfCore` — миграции применяются в `Program.cs` через `Database.MigrateAsync()`.

---

## 4. Скриншоты (вставить в Word)

> Сделайте снимки экрана и вставьте в итоговый `.docx` вместо заглушек ниже.

### 4.1. Код сущностей

- `BlogEfCore/Models/BlogPost.cs` — аннотации `[Key]`, `[Required]`, `[StringLength]`, `[Column]`, `[NotMapped]`.
- `BlogEfCore/Models/Comment.cs`.

### 4.2. DbContext и Fluent API

- `BlogEfCore/Data/BlogDbContext.cs` — `DbSet<>`, `OnConfiguring`, `OnModelCreating`.

### 4.3. Миграции

- Папка `BlogEfCore/Migrations/` — файл `*_InitialCreate.cs`.
- Вывод консоли: `dotnet ef database update` без ошибок.

### 4.4. Структура базы данных

- DB Browser for SQLite: открыть `BlogEfCore\blog_variant2.db` (файл рядом с `Program.cs`).
- Или: `sqlite3 blog_variant2.db ".schema"` из каталога `BlogEfCore`.

---

## 5. Результат выполнения

База данных SQLite создаётся автоматически. Таблицы соответствуют модели Code First. Демонстрационная запись и комментарий добавляются при первом запуске приложения.

**ФИО в тестовых данных:** Кобец Кирилл (автор комментария в seed-данных).

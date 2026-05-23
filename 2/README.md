# Практическая работа №2 — Entity Framework Core (Code First)

**Студент:** Кобец Кирилл  
**Вариант:** 2  
**Предметная область:** блог (записи и комментарии)

## Состав решения

| Компонент | Описание |
|-----------|----------|
| `BlogPost` | Запись блога: заголовок, slug, текст, дата публикации |
| `Comment` | Комментарий к записи (связь 1:N с `BlogPost`) |
| `BlogDbContext` | Контекст EF Core, `DbSet<>`, конфигурация Fluent API |
| СУБД | SQLite (`blog_variant2.db`) |

## Ограничения и конфигурация

- **Data Annotations:** `[Key]`, `[Required]`, `[StringLength]`, `[Column]`, `[Table]`, `[NotMapped]` (свойство `Preview`).
- **Fluent API:** связь 1:N `Post → Comments`, каскадное удаление, уникальный индекс по `Slug`, индекс по `PostId`, имена таблиц и ограничения длины полей.

## Команды миграций

Выполнять из каталога `G:\unik\cross\2\BlogEfCore`:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Либо запуск приложения (миграции применяются автоматически):

```bash
dotnet run --project BlogEfCore
```

## Проверка базы данных

После `dotnet run` файл `blog_variant2.db` появится в каталоге проекта — рядом с `Program.cs` (`BlogEfCore/`).

Просмотр структуры (если установлен `sqlite3`):

```bash
sqlite3 blog_variant2.db ".schema"
sqlite3 blog_variant2.db "SELECT * FROM posts;"
sqlite3 blog_variant2.db "SELECT * FROM comments;"
```

В Visual Studio: **SQL Server Object Explorer** не нужен для SQLite; можно использовать расширение **SQLite** или DB Browser for SQLite.

## Отчёт

Текст отчёта для сдачи: [REPORT.md](REPORT.md). В Word-документ вставьте скриншоты кода и структуры БД по разделам отчёта.

## Критерии (максимум 8 баллов)

1. Модель + Data Annotations + Fluent API + уникальный индекс — **4 балла**
2. DbContext + миграции + созданная БД — **3 балла**
3. Две связанные сущности 1:N через Fluent API — **1 балл (бонус)**

# Практическая работа №3 — ASP.NET Core Minimal API

**Студент:** Кобец Кирилл  
**Вариант:** 2  
**Предметная область:** блог (модель данных из практической №2)

## Состав решения

| Компонент | Описание |
|-----------|----------|
| `BlogApi` | Веб-приложение ASP.NET Core (Minimal API) |
| `BlogDbContext` | EF Core, SQLite, миграции |
| `IDataService` / `BlogDataService` | Собственный сервис обработки данных |
| `GET /api/data` | JSON из БД через DbContext и сервис |
| `GET /api/config` | Пользовательские настройки из `IConfiguration` |

## Запуск

```bash
cd 3/BlogApi
dotnet run
```

Приложение: http://localhost:5197  
Проверка: http://localhost:5197/api/data , http://localhost:5197/api/config

## Конфигурация

Файл `appsettings.json`:

- `ConnectionStrings:BlogDb` — SQLite
- `AppSettings:AppName`, `Version`, `MaxItems`

## Отчёт

Подробный текстовый отчёт: [report-text-ru.txt](report-text-ru.txt)

## Критерии (максимум 8 баллов)

1. GET `/api/data` + дополнительный `/api/config` — **2+ балла**
2. Endpoint конфигурации — **1 балл**
3. `appsettings.json`, `IConfiguration`, `GetConnectionString` — **2 балла**
4. DI: интерфейс, регистрация Scoped, внедрение в MapGet — **2 балла**
5. EF Core, `AddDbContext`, JSON из БД — **1 балл**

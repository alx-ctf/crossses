# Публикация на GitHub (отдельный репозиторий)

Локальный репозиторий: `G:\unik\cross\2`, ветка `main`.

## 1. Войти в GitHub CLI (один раз)

```powershell
gh auth login
```

## 2. Создать репозиторий и отправить код

```powershell
cd G:\unik\cross\2
gh repo create pz2-kobets-ef --public --source=. --remote=origin --push --description "ПЗ2 EF Core, вариант 2, Кобец Кирилл"
```

## 3. Без gh (через сайт)

1. На https://github.com/new создайте пустой репозиторий (без README).
2. Выполните:

```powershell
cd G:\unik\cross\2
git remote add origin https://github.com/ВАШ_ЛОГИН/ИМЯ_РЕПО.git
git push -u origin main
```

## Важно

- Не добавляйте соавторов — в Contributors останетесь только вы.
- Репозиторий `cross` на диске: каждая практика в папке с **номером** (`2`, `3`, …).

# Технологии/Паттерны

### Авторизация/Аутентификация
1. JWT Bearer Token 
2. ASP .NET Identity

### Архитектура
1. MVC паттерн архитектуры
2. Разделение на слои 
(BLL, DAL, Domain, WebAPI)
3. Repository паттерн

### ORM 
1. EntityFrameworkCore

### Database
1. MSSQL

### Прочие технологии
1. Automapper
2. Swagger UI
3. LINQ




 
## Установка

Для установки правильной конфигурации проекта, вам нужно для начала назначить в обозревателе решений, в качестве запускаемого проекта проект Market.WebAPI.
Далее вам нужно зайти в файл appsettings.json, раскомментировать строку подключения к локальной базе данных, и законментировать строку подключения к облачной базе.

Далее в диспетчере пакетов NuGet вам нужно ввести команду:
```bash
update-database
```

## Важно
1. Был уже прописан Seed, для внесения 3-х сущностей пользователя в базу данных. Все они с разными ролями:

USERNAME/PASSWORD/ROLE/UserId
1. admin/masterpass/Admin/1
2. manager/masterpass/Manager/2
3. seller/masterpass/Seller/3

Так же хотел добавить, что на проекте стоит множество валидаций на всякий случай.
## Web Deploy 
Так же проект был задеплоен на бесплатный хостинг SmarterASP.
Вы можете перейти по ссылке: 
[Клик](http://devfrez-001-site1.itempurl.com/swagger/index.html)
 и затестировать.

## Описание функционала
### Авторизация
1. Для входа вам нужно перейти к эндпоинту, ввести нужные данные и получить в ответе accessToken для входа. Далее вам нужно вставить полученный токен в окно Authorize.
2. Вы можете посмотреть свой профиль и получить информацию об аккаунте в который вы вошли.

### Администратор
1. Создание пользователя, вы можете создать пользователя, так же вам нужно указать айди роли пользователя. Для того чтобы посмотреть какие роли есть в системе, вы можете использовать эндпоинт просмотра всех ролей.
2. Создание магазина вы можете создать магазин, указав какое у него будет имя и айди менеджера.
3. Вы можете посмотреть все магазины которые есть в базе.
4. Вы можете смотреть информацию о всех пользователях системы.

### Менеджер
1. Вы можете посмотреть все товары которые есть в магазине.
2. Вы можете добавлять, удалять, редактировать товары для магазина, к которому вы привязаны. Так же стоит проверка на уникальное наименование товара в рамках вашего магазина.
3. Вы можете добавлять, удалять продавцов из вашего магазина.
4. Вы можете смотреть информацию о всех пользователях системы.

### Продавец
1. Вы можете так же смотреть все товары магазина, в котором вы работаете.
2. Вы можете продавать товар, созданный менеджером. Если он конечно в наличии))

### P.S У всех типов ролей есть возможность посмотреть информацию о своем профиле.

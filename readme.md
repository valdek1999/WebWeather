WebWeather сервис для загрузки и отображения эксель файлов с данными о погоде
==============================


Требования к ПО
---------
Для работы локально потребуется docker или установленная бд Postgres 14+  
Конфигурация по умолчанию задана в appsettings.json, необходимые значения могут быть переопределены в переменных окружения.

Строки подключения(в appsetting.json):
-------------------------------------
* ConnectionStrings.DataWeatherContext - строка подключения к бд weather_db.  Для работы локально, необходимо установить локальный хост(127.0.0.1). Или для работы через docker-compose использовать хост local.postgres.ru.

Запуск проекта через Docker
---------------------------
Для локального запуска в docker-е, необходимо:  
1)С помощью терминала перейти в папку с проектом WebWeather  
2)Запустить команду: docker-compose -f "docker-compose.yml" up -d --build 
3)Перейти по локальному хосту http://localhost:5234/ или http://localhost:5233/
Или:   
1)Открыть решение в VisualStudio и запустить проект с помощью профиля docker-compose.

Возможности сервиса
----------------------------
* Загрузка архивов погодных условий в городе Москве в базу данных :ballot_box_with_check:
* Просмотр архивов погодных условий в городе Москве :ballot_box_with_check:
* Постраничная навигация на странице просмотра :ballot_box_with_check:
* Фильтрация по месяцам и годам :ballot_box_with_check:
* Сортировка погодных условий по возрастанию и убыванию дат :ballot_box_with_check:
* Возможность загружать несколько файлов за раз :ballot_box_with_check:
* Поп-ап уведомления о состоянии загрузки файлов в бд :black_square_button:  
* Валидация данных и уведомления об ошибках:ballot_box_with_check:

Пример работы сервиса
----------------------------

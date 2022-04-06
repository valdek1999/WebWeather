WebWeather сервис для загрузки и отображения эксель файлов с данными о погоде
==============================


Требования к ПО
---------
Для работы локально потребуется docker или установленная бд Postgres 14+  
Конфигурация по умолчанию задана в appsettings.json, необходимые значения могут быть переопределены в переменных окружения.

Строки подключения(в appsetting.json):
-------------------------------------
* ConnectionStrings.DataWeatherContext - строка подключения к бд weather_db.  Для работы локально, необходимо установить локальный хост(127.0.0.1). Или для работы через docker-compose использовать хост по умолчанию local.postgres.ru.

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
* Просмотр архивов погодных условий в городе Москве :ballot_box_with_check:
* Загрузка архивов погодных условий в городе Москве в базу данных :ballot_box_with_check:
* Валидация архива данных. Уведомления об ошибках валидации:ballot_box_with_check:
* Анимированные поп-ап уведомления о состоянии загрузки файлов в бд :black_square_button:
* Постраничная навигация на странице просмотра :ballot_box_with_check:
* Фильтрация по месяцам и годам :ballot_box_with_check:
* Сортировка погодных условий по возрастанию и убыванию дат :ballot_box_with_check:
* Возможность загружать несколько файлов за раз :ballot_box_with_check:  

Пример работы сервиса
----------------------------
![bandicam-2022-04-06-18-05-37-732](https://user-images.githubusercontent.com/17438672/162007552-41a07098-9e34-4ba5-b5a8-900cd3cf11b8.gif)
Пример уведомления об ошибках валидации
----------------------------
![bandicam-2022-04-06-18-07-20-007](https://user-images.githubusercontent.com/17438672/162007766-d8963967-e6fb-4960-84de-0fb6b027d523.gif)

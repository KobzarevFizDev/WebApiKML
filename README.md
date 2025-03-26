<h1 align="center">WebApiKml </h1>

<h2 align="left">Задача: </h2>
Необходимо создать приложение (ASP.NET Web API, C#). Должны быть реализованы следующие endpoints:

<br> 1. Получение всех элементов fields с полями (id, name, size, locations). Locations должен содержать структуру: {"Center":[lat,lng],"Polygon":[[lat,lng],.. ..,[lat,lng]]}, где lat – широта, lng – долгота

<br> 2. Получение площади поля (size) по идентификатору (id)

<br> 3. Получение расстояния в метрах от центра поля до точки, переданной во входном параметре (т.е. в запросе должны содержаться координаты точки и идентификатор поля, на выходе получаем расстояние в метрах)

<br> 4. Получение принадлежности точки к полям (т.е. лежит ли точка в контуре одного из полей). В запросе - координаты точки, на выходе идентификатор и название поля (id, name), в случае если точка находится в одном из контуров полей. В случае, если точка не принадлежит ни одному из контуров полей, возвращаем false

<h2 align="left">Решение: </h2> 
<br>Задача выполнена на net 8 
<br>Проект развернут с помощью Docker по адресу: **http://87.228.38.57:12300**

Get запрос на получение всех элементов fields с полями (id, name, size, locations)

![Interface](https://github.com/KobzarevFizDev/WebApiKml/raw/main/images/1.png)
Get запрос на получение площади поля (size) по идентификатору (id)

![Interface](https://github.com/KobzarevFizDev/WebApiKml/raw/main/images/2.png)

Get запрос на получение площади в метрах квадратных
![Interface](https://github.com/KobzarevFizDev/WebApiKml/raw/main/images/3.png)

Get запрос на получение расстояния до объекта в метрах
![Interface](https://github.com/KobzarevFizDev/WebApiKml/raw/main/images/4.png)

Get запрос. Проверка принадлежности точки
![Interface](https://github.com/KobzarevFizDev/WebApiKml/raw/main/images/5.png)

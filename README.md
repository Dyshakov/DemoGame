# DemoGame
## Видео геймплея
[![Watch the video](http://img.youtube.com/vi/J-FP1Vwp6-M/0.jpg)](https://youtu.be/J-FP1Vwp6-M)

## Hierarchy
Структура сцены проекта состоит из 5 частей:
* World
* Setup
* Managers
* Canvases
* Units
  
![image](https://github.com/user-attachments/assets/c415191d-7112-4b83-a770-b62d50008f85)


## Локация 
![image](https://github.com/user-attachments/assets/228053ed-cac7-409f-87ac-d0afe1ddb052)

![image](https://github.com/user-attachments/assets/411bc48c-dc18-49db-a24f-73a48740e450)


## UI
Интерфейс состоит из:
* MainInventoryGroup - панель инвентаря игрока
  ![image](https://github.com/user-attachments/assets/7353f8bc-a24e-48f9-a900-16cb39a6e5b6)
* Toolbar - панель быстрого доступа предметов<br>
  ![image](https://github.com/user-attachments/assets/d020289d-3c7d-4b86-be58-451cd9da4ce2)
* actionText - подсказка действия (открыть дверь, залутать игрока)<br>
![image](https://github.com/user-attachments/assets/a8c6e90f-4101-433a-be40-1625cadf27d0)
* LootInventory - инвентарь лутаемого бота<br>
![image](https://github.com/user-attachments/assets/e489807d-53e2-402f-b60b-a1c2b6e9f737)
* Stats - Статы персонажа (количество здоровья)<br>
![image](https://github.com/user-attachments/assets/a13d7014-fc8f-419f-a43e-3bd7e3adeda4)
* Hitoverlay - покраснение экрана при получении урона<br>
![image](https://github.com/user-attachments/assets/83de3dc6-031b-4f0f-a9ef-205d15aefcde)
* crosshair - перекрестие по центру экрана<br>
![image](https://github.com/user-attachments/assets/5a8bdd37-952c-4536-9ddb-193338e223a8)
* hitmarker - маркер попадания по врагу<br>
![image](https://github.com/user-attachments/assets/9cd7684c-0fc8-4b2c-9878-6da385663460)
<br>Общий вид структуры интерфейса<br>
![image](https://github.com/user-attachments/assets/704de67a-1bc4-42d3-b5f6-6d2b9cd3104d)

## Player
Компоненты игрока:<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/4947e7dd-41ec-4fc7-acd0-97bff35c0ea2)

* MOVE - скрипт который отвечает за движение игрока
* Player Health - скрипт который отвечает за здоровье игрока
* PlayerInput - отвечает за отслеживание нажатий клавиш кравиатуры
* PlayerLadderMove - отвечает за движение игрока по лестнице

## Оружие
Компоненты оружия: <br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/a2b18298-b331-41ce-bf83-4054bc4be935)

* Weapon - основной скрипт оружия который отвечает за стрельбу и перезарядку
* Weapon Animations - отвечает за анимацию оружия
* Weapon Recoil - отвечает за отдачу оружия

## Функционал

### Перезарядка
![reload](https://github.com/Dyshakov/DemoGame/assets/91851290/fb3f1fba-46e6-4094-b9d0-3df2c00929fd)

### Аптечка

![first aid](https://github.com/Dyshakov/DemoGame/assets/91851290/08ecb245-4708-4eec-b6b1-0b3bcd9d906b)

### Стрельба

![shooting](https://github.com/Dyshakov/DemoGame/assets/91851290/ab09b281-429d-44be-bbbe-f643d5912833)





# DemoGame
## Gameplay
[![Watch the video](http://img.youtube.com/vi/ivyqf5zdigs/0.jpg)](https://www.youtube.com/watch?v=ivyqf5zdigs)

## Hierarchy
Структура сцены проекта состоит из 5 частей:
* World
* Setup
* Managers
* Canvases
* Units
  
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/38373e30-f71b-4c92-bc89-c92af0bda2e5)

## Локация 
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/fbd7c666-a1dd-4576-b575-566d1d902e37)
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/73f31916-52a6-4bb1-b563-626265e28b02)

## UI
Интерфейс состоит из:
* MainInventoryGroup - панель инвентаря игрока
  ![image](https://github.com/Dyshakov/DemoGame/assets/91851290/41c3b64b-e954-47e1-9f19-0175fd3e14a5)
* Toolbar - панель быстрого доступа предметов<br>
  ![image](https://github.com/Dyshakov/DemoGame/assets/91851290/68c45818-24b9-4e66-9108-37ecbc29a3f5)
* actionText - подсказка действия (открыть дверь, залутать игрока)<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/3812bd96-4a70-4515-9a1c-cdb9e072b8be)
* LootInventory - инвентарь лутаемого бота<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/4a4a713a-f438-4d5e-8823-12e2bb9f1b24)
* Stats - Статы персонажа (количество здоровья)<br>
  ![image](https://github.com/Dyshakov/DemoGame/assets/91851290/80eb15bd-ed59-45a7-9377-e0ca631b80f5)
* Hitoverlay - покраснение экрана при получении урона<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/997aa188-0bc3-465c-ab64-5bd710ed3072)
* crosshair - перекрестие по центру экрана<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/36c87526-1060-4839-acf5-d92add7676c4)
* hitmarker - маркер попадания по врагу<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/87da270d-35e5-49fd-88cc-f171bfa3b35d)
Общая вид структуры интерфейса<br>
![image](https://github.com/Dyshakov/DemoGame/assets/91851290/96024c43-9161-4296-8116-b090ae92e5d0)

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





На работу ушло 9 часов. От трети до половины времени это работа с графикой внутри нейросети и фотошопа.
Для всех 3 уровней генерировал графику используя нейросети, в основном использовал davinci.ai
AI использовал только в точечных решениях например перемещение камеры по уровню либо же круговой вылет эффектов, т.к там много примитивной алгебры какую нейросеть решает шустрее. Архитектуру писал сам т.к нейрость её перегружает и оверинжинерит.

Видео геймплея - https://github.com/Swordmin/Puzzle-dev-test-2026-06-09_12-07-07
APK - https://drive.google.com/file/d/1ObAl-_tt0xQwFBSaRFzduZRJdVQMIxk7/view?usp=sharing

Промты для уровней использовал такие:

A cheerful colorful cartoon farm scene for children, 
flat illustration style, bright saturated colors, 
friendly animals: cow, pig, chicken, red barn in background, 
blue sky with white fluffy clouds, green meadow, sunflowers, 
clean simple shapes, kids book illustration, 
square format, white border, 

Cute cartoon underwater ocean scene for children's puzzle game, 
colorful fish, seahorse, starfish, coral reef, 
treasure chest, friendly octopus, bubbles, 
bright turquoise and blue water, flat vector style, 
vibrant colors, kids illustration, simple clean shapes, 
square format 

Adorable cartoon space scene for children's puzzle,
cute friendly rocket ship, smiling planets, stars, 
moon with face, astronaut teddy bear, colorful nebula,
flat illustration style, bright neon colors on dark blue background,
kids book art, simple bold shapes, 

Для функциональных частей кода использовал промты вроде:
"
private void Show() 
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject effectPartCreated = Instantiate(_effectPart, transform.position, Quaternion.identity);
        }
    }Мне нужно из этого сделать так чтобы объекты вылетали образуя круг вокруг коренного объекта, с небольшими погрешностями, потом пропадали Это всё через DOTween
    "

Из реализованных доп условий:
Добавил эффект вылетающих звезд при правильной постановке детальки.
Добавил прогресс бар уровня. 
Добавил переход между уровнями.
Добавил подсказку над нужной позицией для нужной детальки.

Адаптацию под разные ориентации не делал т.к для этого нужно было бы переписывать всё под 2 вида интерфейса. Растянуло бы срок сдачи в 2 раза. Не посчитал целесообразным. 

Некоторые решения в коде упростил в угоду реализации, поэтому эффекты не стал покрывать ObjectPool. Разносить остальные сущности на несколько разных и т.п


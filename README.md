# A game about piracy

"A game about piracy" es el nombre de mi prototipo para la primera Práctica de Evaluación Continua (PEC1) de la asignatura Programación de Videojuegos 2D del Máster Universitario en Diseño y Programación de Videojuegos de la UOC.

El objetivo de la práctica era desarollar un juego en 2D que imitara el sistema de duelos de insultos de "The Secret of Monkey Island" utilizando los conocimientos adquiridos en el estudio del primer módulo de la asignatura y realizando investigación por cuenta propia.

## Vídeo explicativo

[![Vídeo explicativo](https://img.youtube.com/vi/CoEJtddzxFE/maxresdefault.jpg)](https://youtu.be/CoEJtddzxFE)

## Versión jugable

El prototipo puede jugarse aquí:

[Juega a "A game of piracy" de Ragart en itch.io](https://itch.io/embed-upload/6748011?color=333333)

## Como jugar

El objetivo del juego es ganar tres asaltos en un duelo de insultos contra la CPU.

- En el turno del jugador, es necesario elegir un insulto de la lista de insultos disponibles usando el ratón.
    - Si el oponente acierta, gana el asalto y el jugador pierde el turno.
    - Si falla, el jugador gana el asalto y sigue su turno.
- En el turno del oponente, se escoge un insulto aleatorio y el jugador debe elegir una réplica de la lista de réplicas disponibles usando el ratón.
    - Si el jugador acierda, gana el asalto y el oponente pierde el turno.
    - Si falla, el oponente gana el asalto y sigue su turno.
- Cuando el jugador o el oponente han ganado tres asaltos, el juego finaliza.

## Desarrollo

A efectos de cumplir lo solicitado en las instrucciones, el prototipo incluye lo siguiente:

- Cinco **escenas**:
    - Créditos iniciales con la autoría del prototipo y la atribución del sistema de juego a LucasArts.
    - Introducción argumental que pone en contexto el motivo del duelo.
    - Menú principal con las opciones de iniciar la partida y salir del juego, así como con los créditos.
    - Escena de combate en la que se desarrolla el bucle del juego.
    - Final de partida que permite volver a jugar sin reiniciar el juego o volver al menú principal.
- Múltiples **scripts de C#** que implementan la lógica de juego, así como algunas opciones adicionales (por ejemplo, cambiar el cursor o ejecutar un diálogo entre personajes).
- Bucle del juego implementado mediante una **máquina de estados** compuesta por los estados siguientes:
    - **Begin:** configuración inicial del bucle de juego y determinación aleatoria del inicio de turno.
    - **PlayerTurn:** lógica del turno del jugador. Permite seleccionar un insulto y hace que el oponente escoja una réplica de manera aleatoria, pero con un 33% de probabilidades de acertar. Si el oponente no acierta, se mantiene el estado y se vuelve a iniciar el turno. De lo contrario, se pasa al estado EnemyTurn.
    - **EnemyTurn:** lógica del turno del oponente. Hace que el oponente escoja un insulto de manera aleatoria y permite seleccionar una réplica. Si el jugador acierta, se pasa al estado PlayerTurn. De lo contrario, se mantiene el estado y se vuelve a iniciar el turno.
    - **Won:** fin del juego cuando el jugador gana tres asaltos antes que el oponente. Muestra unos mensajes finales y redirige a la escena de final de partida.
    - **Lost:** fin del juego cuando el oponente gana tres asaltos antes que el jugador. Muestra unos mensajes finales y redirige a la escena de final de partida.
- Lista de insultos obtenida de un **fichero JSON** almacenado en Resources.
- **Elementos de sonido y visuales** en todas las escenas.
    - Para las transiciones entre animaciones, se han utilizado las máquinas de estado proporcionadas por el componente `Animator`.

Es importante mencionar que la pantalla final no muestra quién ha sido el ganador por una **elección de diseño**, ya que se muestra de manera agumental, a través de los diálogos de los personajes, en los estados Won y Lost de la escena de juego. De querer implementarse, se hubiese optado por una clase estática con el método `DontDestroyOnLoad()` que contuviera la información básica del juego que contiene actualmente la clase `CombatManager` (vidas del jugador y del oponente, estados, etc.).

## Problemas conocidos

- Las animaciones `Attack` y `Hit Taken` de jugador y oponente se ejecutan en ocasiones de manera desincronizada.
- El anclaje de los botones del menú principal y de la pantalla final hacen que el texto se desplace al cambiar de resolución.
- Al pulsar el botón `Escape`, se sale del modo a pantalla completa en vez de omitir la introducción, abrir el menú de pausa o cerrar los créditos.

## Créditos

### Monkey Island™
- Todos los elementos visuales pertenecientes a la saga Monkey Island™ son propiedad de Lucasfilm Games.

### Fuentes
- "Caslon Antique", de [Dieter Steffmann en 1001 Fonts](https://www.1001fonts.com/caslon-antique-font.html)
- "Onesize", de [Yuji Saiki en DaFont](https://www.dafont.com/onesize.font)

### Música
- "Battle" (tema del duelo), de [3xBlast en OpenGameArt](https://opengameart.org/content/glitchy-battle)
- "Danger streets" (tema de la introducción), de [shiru8bit en OpenGameArt](https://opengameart.org/content/8-bit-chiptune-danger-streets)
- "Nobody's Favorite" (tema del final de partida), de [Solomon Allen en OpenGameArt](https://opengameart.org/content/69-game-over-jingles-pack)
- "Step by step" (tema del menú principal), de [Cedfelt en OpenGameArt](https://opengameart.org/content/8-bit-music)

### Imágenes y animaciones
- "Hero Knight 2", de [Luiz Melo en Unity Asset Store](https://assetstore.unity.com/packages/2d/characters/hero-knight-2-168019)
- "Medieval Warrior Pack 2", de [Luiz Melo en Unity Asset Store](https://assetstore.unity.com/packages/2d/characters/medieval-warrior-pack-2-174788)

## Referencias

- "2D Animation in Unity (Tutorial)", de [Brackeys en YouTube](https://www.youtube.com/watch?v=hkaysu1Z-N8)
- "A Brief Anatomy of A Unity Project Folder", de [Jonathan Jenkins en Medium](https://medium.com/@jsj5909/a-brief-anatomy-of-a-unity-project-folder-563bd3f4ad40)
- "AddListener to OnPointerDown of Button instead of onClick", de [Stack Overflow](https://answers.unity.com/questions/1226851/addlistener-to-onpointerdown-of-button-instead-of.html)
- "EVENTS: How to Create a Flexible Dialogue System #7 (Unity)", de [Semag Games en YouTube](https://www.youtube.com/watch?v=psUy7inmJj0)
- "How to Code a Simple State Machine (Unity Tutorial)", de [Infallible Code en YouTube](https://www.youtube.com/watch?v=G1bd75R10m4)
- "How to Organize Project Asset Folders | 2D Game Development in Unity 5.6", de [Chris' Tutorials en YouTube](https://www.youtube.com/watch?v=FcdESnWsoBE)
- "How to make a Dialogue System in Unity", de [Brackeys en YouTube](https://www.youtube.com/watch?v=_nRzoTzeyxU)
- "How to make a Dialogue System with Choices in Unity2D | Unity + Ink tutorial 2021", de [Trever Mock en YouTube](https://www.youtube.com/watch?v=vY0Sk93YUhA)
- "Introduction to Sprite Animations", de [Unity](https://learn.unity.com/tutorial/introduction-to-sprite-animations)
- "Scrollable Menu in Unity with button or key controller", de [Pav Creations](https://pavcreations.com/scrollable-menu-in-unity-with-button-or-key-controller/)
- "State Machine Basics", de [Unity](https://docs.unity3d.com/Manual/StateMachineBasics.html)
- "Text.CrossFadeAlpha not working?", de [StackOverflow](https://answers.unity.com/questions/1004041/textcrossfadealpha-not-working.html)
- "TextMesh Pro - Outline & Soft Shadow", de [Zolran en YouTube](https://www.youtube.com/watch?v=kfODpwcNsoE)
- "The Ultimate Guide to Custom Cursors in Unity", de [James Xu en Wintermute Digital](https://wintermutedigital.com/post/2020-01-29-the-ultimate-guide-to-custom-cursors-in-unity/)
- "Turn-Based Combat in Unity", de [Brackeys en YouTube](https://www.youtube.com/watch?v=_1pz_ohupPs)
- "Unity: Trigger an animation through code", de [Timo Schmid en Medium](https://guilbomadev.medium.com/unity-trigger-an-animation-through-code-5242aef3a578) 
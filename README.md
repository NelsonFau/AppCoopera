Proyecto Coopera

Descripción

El proyecto consiste en desarrollar una aplicación web multijugador cooperativa en ASP.NET Core .NET 8 MVC.


Flujo bajo

Los jugadores entran al sitio web.

Al comenzar una partida, se generan metas de recursos: madera, piedra y comida.

Cada jugador aporta recursos presionando botones o resolviendo minijuegos (según la versión).

El juego termina cuando se alcanzan todos los recursos requeridos.

Se muestran los resultados de la partida (recursos por jugador, totales y tiempo).

Cualquier jugador puede iniciar una nueva partida.

Estados principales

Jugando: los jugadores pueden ingresar su nombre y recolectar recursos.

Presentando resultados: se muestran los resultados de la última partida y la opción de empezar otra.

El desarrollo se dividirá en etapas incrementales, asegurando siempre un producto funcional que luego se expande.





Instalación

Clonar el repositorio:

git clone https://github.com/NelsonFau/AppCoopera.git


Coopera/
│
├── src/                
│   ├── juegos/         # Minijuegos individuales
│   ├── recursos/       # Manejo de recursos (vidas, monedas, objetos)
│   ├── partidas/       # Guardado y carga de partidas
│   └── utils/          # Funciones auxiliares
│
├── assets/             # Imágenes, sonidos, y otros recursos multimedia
├── README.md           # Documentación del proyecto
└── requirements.txt    # Dependencias (si aplica)

Contribución

Gabriel Larrosa y Nelson Álvarez

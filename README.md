# IRON HEART IN THE SHADOWS

---

- **Integrantes**:

  - [Stephan Brommer Gutiérrez](mailto:alu0101493497@ull.edu.es)
  - [Aday Cuesta Correa](mailto:alu0101483887@ull.edu.es)
  - [Sofía De Fuentes Rosella](mailto:alu0101480619@ull.edu.es)

---

## Aspectos clave del uso de la aplicación

El uso de la aplicación, desarrollada en Unity, requiere un entorno mínimo de hardware para garantizar su instalación y operación. A continuación, se detallan los requisitos y recomendaciones técnicas, considerando el tamaño máximo proyectado del aplicativo (1 GB) y los componentes necesarios para una experiencia óptima.

### 1. Dispositivo Móvil Compatible

 - **Sistema Operativo**: Android 8.0 (Oreo) o superior, garantizando soporte para aplicaciones modernas y optimización del rendimiento gráfico.
 
 - **Espacio de Almacenamiento**: La aplicación, incluyendo texturas, modelos y otros activos, puede requerir hasta 2 GB de almacenamiento total. Es imprescindible que el dispositivo móvil cuente con al menos 8 GB de espacio libre disponible para permitir instalación, ejecución y posibles actualizaciones futuras.
 
 - **Memoria RAM**: Se recomienda un dispositivo con al menos 3 GB de RAM para ejecutar el proyecto sin problemas, especialmente si la aplicación utiliza elementos gráficos avanzados o física compleja.
   
 - **GPU y CPU**: Un procesador de gama media o superior, con soporte para gráficos Vulkan, garantizará una tasa de fotogramas fluida y tiempos de carga razonables.

### 2. Controlador Externo Compatible

El manejo del personaje principal se realiza mediante un controlador Bluetooth externo o por cable. Los modelos recomendados incluyen:

 - **Mando de Xbox**: Como Xbox One y Xbox Series X|S.
 
 - **Mando de PlayStation**: DualShock 4 y DualSense (PlayStation 4 y 5).
   
La conexión se realiza directamente al dispositivo móvil mediante emparejamiento Bluetooth o por cable como ya se mencionó, sin necesidad de configuraciones adicionales en la mayoría de los casos. Este programa se encarga de la detección automática del controlador y mapea los botones de entrada según las configuraciones estándar del dispositivo.

### 3. Rendimiento y Configuración

 - **Instalación**

   - La aplicación se entrega como un archivo APK, diseñado para una instalación directa desde almacenamiento interno o externo.
   
   - En dispositivos con soporte para tarjetas SD, se recomienda instalar la aplicación en la memoria interna para un acceso más rápido.
     
 - **Manejo del Personaje**

   - El esquema de control está preconfigurado para aprovechar los botones y joysticks del controlador. Esto incluye:
   
     - Joystick izquierdo para movimiento.
    
     - Joystick derecho para movimiento de la cámara.
     
     - Botones para interacciones específicas.
     
    - Unity facilita la integración de controladores mediante Input System o Input Manager, permitiendo una respuesta precisa y fluida en la jugabilidad.

### 4. Conectividad Opcional

- Aunque la aplicación no requiere conexión a Internet para su ejecución, se recomienda para descargas de actualizaciones o funcionalidades adicionales en el futuro, como contenido descargable o ajustes en tiempo real.

### Beneficio Técnico

Este diseño garantiza que el usuario pueda disfrutar de una experiencia de juego inmersiva sin necesidad de hardware adicional más allá del dispositivo móvil y un mando. La optimización de los recursos en Unity asegura que la aplicación se mantenga por debajo de los 6 GB de almacenamiento total, maximizando la calidad visual y el rendimiento incluso en dispositivos de gama media. Esto minimiza los costos asociados al hardware y reduce las barreras de entrada para los usuarios.

---

## Hitos de programación y su relación con los contenidos impartidos

Durante el desarrollo del proyecto, se han implementado varias técnicas y patrones de diseño que reflejan los contenidos impartidos en el curso. A continuación, se desglosan los hitos alcanzados y su relación con los conceptos clave aprendidos:

### 1. Uso del Patrón Observador para Gestión de Eventos

- **Descripción**: Se implementaron scripts notificadores siguiendo el patrón observador para manejar eventos en el juego. En este patrón, un "notificador" (sujeto) emite eventos que son captados por múltiples "suscriptores" (observadores) que reaccionan en consecuencia.
  
- **Aplicación en el Proyecto**

  - Los scripts notificadores actúan como intermediarios entre los eventos generados por el entorno (como entrada o salida de zonas, interacción con objetos) y los comportamientos de los objetos que deben responder a estos eventos.
  
  - Por ejemplo:

    - Zona de interacción: Notificadores que detectan cuándo el jugador entra o sale de un área específica, desencadenando cambios como la reproducción de sonidos o animaciones.
    
    - Interacción con objetos: Al interactuar con un objeto, el notificador informa a los suscriptores (otros componentes o sistemas) para actualizar su estado o realizar acciones.
      
- **Relación con los Contenidos**: El patrón observador es un enfoque eficiente para desacoplar componentes, promoviendo un diseño más modular y flexible.

### 2. Capas de Interacción para VR

- **Descripción**: Se diseñaron capas específicas para objetos interactuables, que permiten al usuario interactuar con ellos de manera intuitiva en un entorno de realidad virtual (VR).

- **Aplicación en el Proyecto**

  - Interacción Visual Mejorada:
  
    - Los objetos interactuables tienen un sistema de cambio de brillo o resaltado, que los distingue del resto del entorno y mejora la experiencia de usuario.
    
    - Este cambio visual permanece activo en todo momento, permitiendo una fácil identificación de elementos clave. Al apuntar o mirar directamente a estos objetos en VR, la retícula del Cardboard se amplía automáticamente, destacando su presencia y facilitando la interacción.
    
  - Capas Personalizadas: Los objetos se agrupan en capas específicas que filtran cuáles pueden ser interactuados, asegurando que solo los elementos relevantes respondan a las entradas del usuario.

- **Relación con los Contenidos**: Este sistema está basado en principios de diseño centrados en el usuario, aprendidos durante el curso, y aprovecha el uso de máscaras de capa para gestionar la interacción en VR.

### 3. Convenios de Diseño en VR para Mejorar la Experiencia del Usuario

- **Descripción**: Se aplicaron principios y convenciones específicas para evitar el mareo por movimiento (motion sickness) y mejorar la comodidad del usuario en VR.
  
- **Aplicación en el Proyecto**

  - Movimiento Suave de Cámara: La cámara está configurada para evitar movimientos bruscos y aceleraciones repentinas, manteniendo una transición suave al moverse o girar.

  - Interacciones Estables: Se han optimizado las transiciones entre estados, como cambios de vista o enfoques de cámara, para que sean graduales y naturales.
  
  - Objetos Interactuables Claros: El uso de resaltes y efectos visuales reduce la carga cognitiva del usuario, ayudándole a identificar fácilmente elementos interactuables sin necesidad de explorar de forma exhaustiva.

- **Relación con los Contenidos**: Estas prácticas están alineadas con los estándares de diseño VR impartidos, que priorizan la comodidad y accesibilidad del usuario.

### 4. Técnicas de Iluminación y Feedback Visual

- **Descripción**: Para mejorar la inmersión y la experiencia del usuario, se implementaron elementos visuales que proporcionan retroalimentación inmediata durante las interacciones.

- **Aplicación en el Proyecto**
  
  - Los objetos interactuables cambian de estado visual (como un brillo adicional o cambio de color) al estar dentro del campo de visión del usuario o al apuntarlos con un controlador.
  
  - Estos efectos no solo mejoran la interacción, sino que también cumplen un rol narrativo al guiar al jugador hacia los elementos relevantes.

- **Relación con los Contenidos**: Este enfoque se basa en los principios de UX en entornos interactivos, destacando la importancia de la retroalimentación visual y auditiva.

## Aspectos destacados de la aplicación desarrollada

**1.- Temática y Experiencia Inmersiva**: La demo se centra en sumergir al jugador en un mundo oscuro y amenazante, donde una invasión alienígena ha devastado el entorno. La misión principal consiste en explorar el escenario en busca del "Corazón sw Hierro", el único artefacto capaz de revertir la presencia de los invasores. A través de una combinación de efectos visuales, sonoros y narrativos, la demo crea una atmósfera opresiva e intrigante, perfecta para los amantes del género de terror.

**2.- Diseño de Interacciones**: Aunque no incluye puzzles complejos, la demo pone énfasis en la interacción con objetos clave del entorno. Elementos como puertas, objetos brillantes y zonas específicas invitan al jugador a explorar y descubrir detalles ocultos, reforzando la narrativa del juego.

**3.- Demostración Técnica**: Esta demo sirve como una prueba del concepto técnico y artístico detrás del juego completo. Se han implementado controles optimizados para VR mediante un mando, efectos visuales dinámicos para guiar al jugador, y un diseño narrativo que introduce los elementos centrales del mundo de juego.

**4.- Ambientación Visual y Sonora**:

  - Visual: Uso de iluminación dramática para crear contrastes que dirigen la atención y refuerzan el suspense.
  
  - Sonora: Una banda sonora minimalista y efectos ambientales realistas como ruidos alienígenas que aumentan la tensión y la inmersión.

**5.- Enfoque en Realidad Virtual (VR)**: La compatibilidad con dispositivos VR como Google Cardboard permite al jugador experimentar la demo desde una perspectiva más inmersiva. Gracias a los controles intuitivos y a las transiciones suaves, la experiencia es accesible y cómoda incluso para usuarios con poca experiencia en VR.

---

## Integración de sensores en interfaces multimodales

En el desarrollo de la aplicación, no se ha hecho uso de sensores como giroscopios, acelerómetros o detectores de proximidad, ya que su implementación no se alinea con los objetivos ni con la experiencia de usuario planteada para el proyecto.

### Justificación de la No Implementación de Sensores

  - **1.- Plataforma de Realidad Virtual (Cardboard) y Control por Mando**
  
    - El proyecto se ha diseñado para ser compatible con Google Cardboard y un mando (Xbox o PlayStation) como dispositivos principales de interacción. Esto permite un control más preciso del personaje mediante el joystick, lo que es esencial para mantener una experiencia de usuario fluida y predecible en un entorno de terror.
  
    - Aunque utilizamos el giroscopio del Cardboard para controlar la orientación de la cámara, no consideramos necesario incorporar otros sensores como acelerómetros para interactuar. El uso de estos elementos adicionales podría generar incomodidad en los jugadores sin aportar un valor significativo en el contexto del juego.
    
  - **2.- Temática de Terror y Control de Movimiento**
  
    - En un juego de terror, el diseño se enfoca en crear una experiencia inmersiva pero controlada, donde el jugador tiene pleno dominio sobre sus acciones y desplazamientos.
  
    - Aunque utilizamos el giroscopio del Cardboard para controlar la orientación de la cámara, evitar el uso de otros sensores dependientes de movimientos físicos, como los acelerómetros, fue una decisión consciente, ya que:
  
     - Podría incrementar la sensación de mareo (motion sickness), afectando la comodidad del jugador.
      
     - Disminuiría la precisión en el control, especialmente en momentos críticos del juego en los que el jugador necesita reaccionar con rapidez.
      
     - Podría romper la inmersión si los sensores no responden con la precisión necesaria, afectando la experiencia de juego.
      
  - **3.- Simplicidad y Focalización del Proyecto**
  
    - Dado que el proyecto se encuentra en una etapa inicial, hemos optado por soluciones más directas y confiables, como el uso del mando para interactuar y desplazarse.
    
    - La integración de sensores añadiría una complejidad innecesaria al desarrollo, desviando el enfoque de los elementos centrales del juego, como la ambientación, narrativa y diseño interactivo.

---

## Demostración visual de la ejecución (GIF animado)

## Acta de acuerdos del grupo respecto al trabajo en equipo

En el desarrollo del proyecto, se estableció una distribución clara de responsabilidades entre los miembros del equipo, permitiendo una colaboración eficiente y efectiva. A continuación, se detalla la contribución de cada integrante según las áreas principales del proyecto:

### Aday - Implementación de Controles y Animaciones

Aday se encargó de todo lo relacionado con la configuración y programación de los controles del mando, asegurando una integración fluida con el sistema de movimiento del personaje y el manejo de la cámara. Su trabajo incluyó:

  - El diseño e implementación de controles adaptados al uso de mandos de Xbox y PlayStation.
  
  - Ajustes en la cámara para garantizar transiciones suaves y un movimiento adecuado en primera persona, especialmente en un entorno de terror donde la inmersión es clave.
  
  - Integración y refinamiento de animaciones específicas vinculadas a las acciones del jugador, como caminar, correr y transiciones entre estados.
    
### Sofía - Desarrollo de Mecánicas del Juego

Sofía asumió la mayor parte del trabajo relacionado con las mecánicas centrales del juego, contribuyendo significativamente a la jugabilidad general. Entre sus logros destacan:

  - **Sistema de Vidas**: Creación de un sistema funcional que gestiona la salud del jugador y permite un control dinámico sobre el progreso y los desafíos del juego.
  
  - **Inventario**: Implementación de un sistema de inventario que permite al jugador recoger, gestionar y utilizar objetos esenciales para avanzar en la historia.
  
  - **Interacciones con el Entorno**: Desarrollo de mecánicas como la apertura de puertas y la recogida de elementos, asegurando que fueran intuitivas y fluidas para los jugadores.
  
  - Optimización de estas funcionalidades para integrarlas con los demás sistemas del proyecto.
    
### Stephan - Gestión de Assets y Eventos Narrativos

Stephan centró su esfuerzo en los elementos artísticos y narrativos del juego, así como en la ambientación de los eventos clave que dan forma a la experiencia de terror. Sus aportes incluyen:

  - **Búsqueda y Selección de Assets**: Identificación y adaptación de personajes, objetos y escenarios que se alinean con la historia y la atmósfera del juego.
  
  - **Eventos de Animación**: Configuración de secuencias de sustos y persecuciones, diseñadas para maximizar el impacto emocional en el jugador.
  
  - **Sonido y Ambientes**: Selección y edición de efectos sonoros que refuercen la narrativa y aumenten la inmersión, como ruidos de enemigos y música de fondo en escenas clave.
  
  - Configuración de zonas específicas para eventos de persecución, ajustando los tiempos y comportamientos de los NPC para generar tensión y desafío.
    
### Control de Versiones y Colaboración en Paralelo

Para optimizar la productividad y facilitar la colaboración sin interferir entre las tareas de los miembros del equipo, se utilizó el control de versiones de Unity. El proyecto se dividió en tres ramas principales, una para cada área de trabajo de los integrantes:

  - Aday trabajó en la rama de **Controles y Animaciones**
  
  - Sofía se encargó de la rama de **Mecánicas del Juego**
  
  - Stephan gestionó la rama de **Assets y Eventos Narrativos**
    
Este enfoque permitió que cada miembro pudiera avanzar en paralelo, sin conflictos de integración, y facilitó la consolidación de cambios al final de cada etapa del desarrollo. De esta manera, se logró un flujo de trabajo eficiente y organizado, asegurando la calidad y coherencia del proyecto.

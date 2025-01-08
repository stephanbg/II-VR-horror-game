# II-VR-horror-game

---

- **Integrantes**:

  - [Stephan Brommer Gutiérrez](mailto:alu0101493497@ull.edu.es)
  - [Aday Cuesta Correa](mailto:alu0101483887@ull.edu.es)
  - [Sofía De Fuentes Rosella](mailto:alu0101480619@ull.edu.es)

---

## Aspectos clave del uso de la aplicación

El uso de la aplicación, desarrollada en Unity, requiere un entorno mínimo de hardware para garantizar su instalación y operación. A continuación, se detallan los requisitos y recomendaciones técnicas, considerando el tamaño máximo proyectado del aplicativo (6 GB) y los componentes necesarios para una experiencia óptima.

### 1. Dispositivo Móvil Compatible

 - **Sistema Operativo**: Android 8.0 (Oreo) o superior, garantizando soporte para aplicaciones modernas y optimización del rendimiento gráfico.
 
 - **Espacio de Almacenamiento**: La aplicación, incluyendo texturas, modelos y otros activos, puede requerir hasta 6 GB de almacenamiento total. Es imprescindible que el dispositivo móvil cuente con al menos 8 GB de espacio libre disponible para permitir instalación, ejecución y posibles actualizaciones futuras.
 
 - **Memoria RAM**: Se recomienda un dispositivo con al menos 3 GB de RAM para ejecutar el proyecto sin problemas, especialmente si la aplicación utiliza elementos gráficos avanzados o física compleja.
   
 - **GPU y CPU**: Un procesador de gama media o superior, con soporte para gráficos Vulkan, garantizará una tasa de fotogramas fluida y tiempos de carga razonables.

### 2. Controlador Externo Compatible

El manejo del personaje principal se realiza mediante un controlador Bluetooth externo. Los modelos recomendados incluyen:

 - **Mando de Xbox**: Compatible con los modelos Bluetooth, como Xbox One y Xbox Series X|S.
 
 - **Mando de PlayStation**: DualShock 4 y DualSense (PlayStation 4 y 5), ambos con soporte Bluetooth.
   
La conexión se realiza directamente al dispositivo móvil mediante emparejamiento Bluetooth, sin necesidad de configuraciones adicionales en la mayoría de los casos. Unity se encarga de la detección automática del controlador y mapea los botones de entrada según las configuraciones estándar del dispositivo.

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

Este diseño garantiza que el usuario pueda disfrutar de una experiencia de juego inmersiva sin necesidad de hardware adicional más allá del dispositivo móvil y un mando Bluetooth. La optimización de los recursos en Unity asegura que la aplicación se mantenga por debajo de los 6 GB de almacenamiento total, maximizando la calidad visual y el rendimiento incluso en dispositivos de gama media. Esto minimiza los costos asociados al hardware y reduce las barreras de entrada para los usuarios.

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
    
    - Este cambio visual se activa al apuntar o mirar directamente al objeto en VR, facilitando la identificación de elementos clave.
    
  - Capas Personalizadas: Los objetos se agrupan en capas específicas que filtran cuáles pueden ser interactuados, asegurando que solo los elementos relevantes respondan a las entradas del usuario.

- **Relación con los Contenidos**: Este sistema está basado en principios de diseño centrados en el usuario, aprendidos durante el curso, y aprovecha el uso de raycasts y máscaras de capa para gestionar la interacción en VR.

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

## Integración de sensores en interfaces multimodales

## Demostración visual de la ejecución (GIF animado)

## Acta de acuerdos del grupo respecto al trabajo en equipo

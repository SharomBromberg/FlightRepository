BACKEND: FlightApp

Desarrollado en .Net8, en vscode, se depura desde el controlador o capa de presentación se maneja swagger para probarlo.

Capa de Aplicacion: Esta capa es la encargada de manejar la logica de los viajes y el cambio de moneda

1.  JourneyService:

    Logica de los viajes:

         -Inicializar los vuelos desde un archivo JSON y guardarlos en una base de datos si aún no existen. ( InitializeFlightsAsync)
         -Obtener todos los vuelos.(GetAllFlights)
         -Encontrar rutas de vuelo desde un origen a un destino, permitiendo o no escalas.(FindRoutes)
         -Obtener vuelos de ida desde un origen a un destino en una moneda específica, permitiendo o no escalas.(GetOneWayFlights)
         -Obtener vuelos de ida y vuelta desde un origen a un destino en una moneda específica, permitiendo o no escalas.
              (GetRoundTripFlights)

    Para realizar estas tareas, JourneyService depende de ICurrencyService para convertir los precios de los vuelos a diferentes monedas, y de IJourneyRepository para interactuar con la base de datos.

2.  CurrencyService:
    Logica relacionada con el cambio de moneda:
    -Convertir una cantidad de una moneda a otra utilizando una API externa.
    -Obtener todas las monedas disponibles utilizando la misma API externa.
    Para realizar estas tareas, se utilizó RestSharp para hacer solicitudes HTTP a la API, y JsonConvert para deserializar las respuestas de la API.

Capa de Dominio: Contiene las entidades y modelos que representan los conceptos clave de la aplicación

1.  FlightEntity: Esta es una entidad que representa un vuelo en la base de datos. Contiene información sobre el transportista del
    vuelo, el número de vuelo, el origen, el destino y el precio del vuelo.

    - También tiene un Id que se utiliza como clave primaria en la base de datos, este para garantizar que cada fila sea única y evitar duplicación de datos así como mantener la integridad de estos, para organizar los datos

2.  Flight: Este es un modelo que representa un vuelo en la lógica de negocio de la aplicación. Contiene un objeto Transport que a
    su vez contiene información sobre el transportista y el número de vuelo. También tiene propiedades para el origen, el destino y el precio del vuelo.
    Además, tiene un método Clone que crea una copia del objeto Flight, para mejorar la eficiencia y establecer todas las propiedades en las consultas.
3.  Journey: Este es un modelo que representa un viaje en la lógica de negocio de la aplicación. Un viaje consta de una lista de
    vuelos, y tiene propiedades para el origen, el destino y el precio total del viaje.
4.  Transport: Este es un modelo que representa el transporte de un vuelo. Contiene información sobre el transportista del vuelo y el número de vuelo.

Capa de infraestructura: Interacción con la base de datos

1.  FlightContext: Esta es la clase de contexto de Entity Framework que se utiliza para interactuar con la base de datos. Contiene un
    DbSet para FlightEntity, que representa la tabla de vuelos en la base de datos.

2.  IJourneyRepository: Esta es una interfaz que define los métodos que un repositorio de viajes debe implementar. En este caso, los
    métodos son GetFlights, que debe devolver una lista de todos los vuelos, y SaveFlights, que debe guardar una lista de vuelos en la base de datos.

3.  JourneyRepository: Esta es una implementación concreta de IJourneyRepository. Utiliza FlightContext para interactuar con la base
    de datos. En el método GetFlights, utiliza el método ToListAsync de Entity Framework para obtener todos los vuelos de la base de datos de forma asíncrona. En el método SaveFlights, utiliza el método AddRange para agregar una lista de vuelos al DbSet de vuelos, y luego el método SaveChangesAsync para guardar los cambios en la base de datos de forma asíncrona.

4.  markets.json: Data necesarioa para la aplicación.

- los datos de los vuelos se almacenan en la base de datos la primera vez que se buscan, y luego se recuperan de la base de datos en las búsquedas subsiguientes.

Capa de presentación: interactúa directamente con el usuario, controlador de API, la configuración de la aplicación y las migraciones de la base de datos.

1.  Program.cs: Configuración de los servicios que la aplicación va a utilizar, como los repositorios, los servicios de la aplicación,
    el contexto de la base de datos y las políticas de CORS. configuración del pipeline de solicitudes HTTP, que incluye la redirección HTTPS, el uso de CORS, el enrutamiento y la autorización.

2.  FlightController: El controlador de la API expone endpoints para obtener vuelos. Tiene dos métodos GetFlights. El primero
    devuelve todos los vuelos disponibles. El segundo acepta parámetros para el origen, el destino, la moneda, el tipo de vuelo (ida y vuelta o solo ida) y si se permiten paradas. Dependiendo del tipo de vuelo, llama a diferentes métodos en IJourneyService para obtener los vuelos correspondientes.

3.  flights.db: Este es el archivo de la base de datos SQLite que la aplicación utiliza para almacenar los datos de los vuelos. Se
    interactúa con esta base de datos a través de FlightContext y IJourneyRepository. Contiene una tabla de vuelos.
4.  Migraciones: Gestionan los cambios en la estructura de la base de datos. Cada vez que se hace un cambio en el modelo de datos, se
    crea una nueva migración que describe cómo aplicar ese cambio a la base de datos. Estas migraciones se aplican a la base de datos al iniciar la aplicación.

FONTEND: Angular 16
Depuración ng serve
hay que instalar node modules: npm i

1. Journey-List:

   Estos son los archivos del componente principal de la aplicación. El
   archivo HTML define la estructura del componente, el archivo SCSS define los estilos y el archivo TS define la lógica.

   -- journey-lis.ts: journeys, flights, origins, destinations, currencies, origin, destination, currency, allowStops, type, displayFlights, displayJourneys: son las propiedades del componente. Almacenan los datos que el componente necesita para funcionar, como los viajes y los vuelos disponibles, los valores seleccionados por el usuario en el formulario y si se deben mostrar los vuelos o los viajes.

- ngOnInit(): Este es un método del ciclo de vida de Angular que se llama cuando se inicializa el componente. En este caso, se utiliza para obtener todos los vuelos disponibles.
- getAllFlights(): Este método llama al servicio JourneyService para obtener todos los vuelos disponibles. También extrae los orígenes y destinos únicos de los vuelos para usarlos en el formulario.
- displayAllFlights(): Este método llama a getAllFlights() y luego establece displayFlights en true y displayJourneys en false, lo que hace que la aplicación muestre todos los vuelos y oculte los viajes.
- getFlights(): Este método llama al servicio JourneyService para obtener los vuelos que coinciden con los criterios seleccionados por el usuario en el formulario.
- displaySearchedFlights(): Este método verifica que el usuario haya rellenado todos los campos del formulario. Si es así, llama a getFlights() y luego establece displayJourneys en true y displayFlights en false, lo que probablemente hace que la aplicación muestre los viajes y oculte los vuelos. Si el usuario no ha rellenado todos los campos, muestra una notificación de advertencia.

-- journey-list.html:
este archivo contiene dos secciones principales

- Sección de búsqueda: Esta sección permite al usuario seleccionar opciones para buscar vuelos. Los usuarios pueden seleccionar el origen, el destino, la moneda y el tipo de vuelo. También pueden optar por permitir paradas. Hay dos botones: uno para buscar vuelos basados en las opciones seleccionadas y otro para mostrar todos los vuelos disponibles.
- Sección de resultados: Esta sección muestra los resultados de la búsqueda de vuelos. Si el usuario ha seleccionado mostrar todos los vuelos, se muestra una tabla con todos los vuelos disponibles. Si el usuario ha realizado una búsqueda, se muestran las opciones de viaje disponibles. Cada opción de viaje incluye un resumen del viaje y una lista de los vuelos que componen el viaje. Si no se encontraron viajes, se muestra un mensaje indicando que no se encontraron vuelos.

-- journey-list.scss : - El selector \* aplica estilos a todos los elementos, estableciendo la fuente en 'Roboto' y ocultando el desbordamiento horizontal.

- .search y .result son clases que se aplican a las secciones de búsqueda y resultados respectivamente. Dentro de estas clases, se
  definen estilos para varios elementos y subelementos, como h1, button, label, etc.

- Los bloques &**routes, &**box, &**select, &**buttons, &**option son ejemplos de anidación en SCSS y se refieren a elementos con
  clases como .search**routes, .search**box, .search**select, .search**buttons, .result**option, etc.

- Responsive &media queries: Los bloques @media (max-width: 768px) y @media (min-width: 768px) contienen reglas de estilo que se aplican solo cuando la anchura de la ventana del navegador es menor o mayor que 768px respectivamente.

2. app.module.ts: Este es el módulo principal de la aplicación. Los módulos en Angular agrupan componentes, servicios y otras piezas de código con funcionalidad relacionada, estos incluyen formsmodule, toastr para mostrar alertas, comonmodule y también tiene declarado el componente

3. environment/environment.ts: Este archivo contiene la configuración específica del entorno: apiUrl: 'http://localhost:5253',

4. interfaces/: Este directorio contiene las interfaces TypeScript que definen la forma de los datos en la aplicación: flightInterface, transportInterface, journeyInterface

5. services/journey.service.ts: Este archivo define un servicio Angular que probablemente se encarga de obtener los datos de los viajes del backend.
   -La propiedad apiUrl se inicializa con la URL de la API desde el entorno.
   -El constructor inyecta una instancia de HttpClient que se utiliza para hacer solicitudes HTTP.
   -getAllFlights() es un método que devuelve un Observable de un array de FlightInterface. Hace una solicitud GET a la API para obtener todos los vuelos.
   -getFlights() es un método que toma varios parámetros y devuelve un Observable de un array de JourneyInterface. Hace una solicitud GET a la API para obtener vuelos que coinciden con los parámetros proporcionados. Utiliza el operador map de RxJS para transformar la respuesta antes de devolverla.

Tiempo total de desarrollo: 3 días se completó el proyecto con los requerimientos necesarios del test.

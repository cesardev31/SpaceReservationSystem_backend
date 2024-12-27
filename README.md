# SpaceReservationSystem

## Descripción

SpaceReservationSystem es una aplicación para gestionar reservas de espacios. Proporciona una API para crear, leer, actualizar y eliminar reservas.

## Tecnologías Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 9.0
- PostgreSQL
- Swagger para documentación de API
- xUnit para pruebas unitarias

## Estructura del Proyecto

```
SpaceReservationSystem/
├── src/
│   ├── SpaceReservation.API/                # API REST y controladores
│   │   ├── Controllers/                     # Controladores de API
│   │   ├── Properties/                      # Configuraciones de lanzamiento
│   │   ├── appsettings.json                 # Configuración de la aplicación
│   │   ├── Program.cs                       # Configuración de inicio
│   │   ├── Startup.cs                       # Configuración de servicios
│   ├── SpaceReservation.Application/        # Lógica de negocio
│   │   ├── Data/                            # Contexto de datos
│   │   ├── Services/                        # Servicios de aplicación
│   │   ├── Migrations/                      # Migraciones de base de datos
│   ├── SpaceReservation.Domain/             # Entidades y reglas de negocio
│   │   ├── Entities/                        # Clases de entidad
│   ├── SpaceReservation.Infrastructure/     # Implementaciones de infraestructura
└── tests/
    └── SpaceReservation.UnitTests/          # Pruebas unitarias
```

## Configuración del Proyecto

### Requisitos Previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Configuración de la Base de Datos

1. Asegúrate de que PostgreSQL esté instalado y en ejecución.
2. Crea una base de datos llamada `SpaceReservation`.
3. Actualiza la cadena de conexión en `src/SpaceReservation.API/appsettings.json` con tus credenciales de PostgreSQL.

### Migraciones de Base de Datos

Ejecuta los siguientes comandos para aplicar las migraciones:

```bash
dotnet ef database update --project src/SpaceReservation.Application --startup-project src/SpaceReservation.API
```

### Ejecución del Proyecto

Para ejecutar el proyecto, usa el siguiente comando:

```bash
dotnet run --project src/SpaceReservation.API
```

Accede a la documentación de la API en [http://localhost:5191/swagger](http://localhost:5191/swagger).

## Pruebas

Para ejecutar las pruebas unitarias, usa el siguiente comando:

```bash
dotnet test
```

## Contribuciones

Las contribuciones son bienvenidas. Por favor, sigue los pasos a continuación para contribuir:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -m 'Añadir nueva funcionalidad'`).
4. Haz push a la rama (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.

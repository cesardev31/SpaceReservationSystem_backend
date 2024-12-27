# Sistema de Reserva de Espacios

Sistema de gestión de reservas de espacios desarrollado con .NET 8 y arquitectura limpia.

## 🚀 Tecnologías

- .NET 8
- Entity Framework Core
- PostgreSQL
- xUnit para pruebas
- Docker

## 📁 Estructura del Proyecto

SpaceReservationSystem/
├── src/
│ ├── SpaceReservation.API/ # API REST y controladores
│ ├── SpaceReservation.Application/ # Lógica de negocio
│ ├── SpaceReservation.Domain/ # Entidades y reglas de negocio
│ └── SpaceReservation.Infrastructure/ # Implementaciones de infraestructura
└── tests/
└── SpaceReservation.UnitTests/ # Pruebas unitarias

## 🛠️ Instalación

1. Clona el repositorio

```bash
git clone https://github.com/tuusuario/SpaceReservationSystem.git
```

2. Inicia los servicios con Docker

```bash
docker-compose up -d
```

3. Ejecuta las migraciones

```bash
dotnet ef database update --project src/SpaceReservation.Application
```

4. Inicia la aplicación

```bash
dotnet run --project src/SpaceReservation.API
```

## 🧪 Pruebas

Ejecuta las pruebas unitarias:

```bash
dotnet test tests/SpaceReservation.UnitTests/SpaceReservation.UnitTests.csproj
```

## 📝 API Endpoints

- `GET /api/reservations` - Obtener todas las reservas
- `GET /api/reservations/{id}` - Obtener una reserva por ID
- `POST /api/reservations` - Crear una nueva reserva
- `PUT /api/reservations/{id}` - Actualizar una reserva
- `DELETE /api/reservations/{id}` - Eliminar una reserva

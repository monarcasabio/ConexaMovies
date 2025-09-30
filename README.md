ConexaMovies API
API RESTful para la gestión de películas, construida con .NET 8 y ASP.NET Core, siguiendo principios de Arquitectura Limpia.

Descripción General
ConexaMovies API es un servicio que permite a los usuarios registrarse, autenticarse y gestionar una colección de películas. Incluye un sistema de roles para controlar el acceso a las operaciones y un servicio en segundo plano que sincroniza automáticamente las películas de Star Wars desde la SWAPI (The Star Wars API).

Características
Autenticación y Autorización:

Registro y login de usuarios.

Autenticación basada en JSON Web Tokens (JWT).

Sistema de roles (Admin y Regular) para proteger los endpoints.

Gestión de Películas (CRUD):

Los administradores pueden crear, actualizar y eliminar películas.

Los usuarios autenticados pueden consultar la lista de películas y ver sus detalles.

Sincronización Automática:

Un servicio en segundo plano se ejecuta periódicamente para obtener las películas de la SWAPI y añadirlas a la base de datos local.

Arquitectura Limpia:

Separación de responsabilidades en capas: Domain, Application, Infrastructure y WebApi.

Validación y Manejo de Errores:

Validación de solicitudes con FluentValidation.

Middleware global para la gestión centralizada de excepciones.

Base de Datos:

Uso de Entity Framework Core con SQLite.

Las migraciones se aplican automáticamente al iniciar la aplicación.

Documentación de API:

Documentación interactiva generada con Swagger (OpenAPI).

Tecnologías Utilizadas
Backend: .NET 8, ASP.NET Core

Base de Datos: Entity Framework Core, SQLite

Autenticación: JWT Bearer

Hashing de Contraseñas: BCrypt.Net-Next

Validación: FluentValidation.AspNetCore

Testing: xUnit, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing

Documentación: Swashbuckle.AspNetCore (Swagger)

Documentación de la API (Swagger)
Una vez que la aplicación esté en ejecución, puedes acceder a la documentación interactiva de Swagger en la siguiente URL:

https://localhost:5001/swagger

Desde aquí se puede explorar y probar todos los endpoints disponibles.

Autenticación
La API utiliza un esquema de autenticación Bearer Token (JWT). Para acceder a los endpoints protegidos, primero se debe obtener un token.

Registrar un nuevo usuario:

Usar el endpoint POST /api/auth/register para crear un nuevo usuario con el rol "Regular".

Iniciar sesión:

Usa el endpoint POST /api/auth/login con las credenciales. La respuesta incluirá un accessToken.

Autorizar las solicitudes:

En Swagger, hacer clic en el botón "Authorize" e introducir token con el formato Bearer {token}.

Usuario Administrador
El sistema incluye un usuario administrador por defecto para facilitar las pruebas de los endpoints protegidos:

Usuario: admin

Contraseña: Admin123!

Se puede usar estas credenciales en el endpoint POST /api/auth/login para obtener un token con permisos de administrador.

Configuración de Pruebas
Las pruebas unitarias se pueden ejecutar en cualquier momento. Sin embargo, para las pruebas de integración, la configuración actual requiere que la aplicación principal se esté ejecutando de forma independiente, ya que las pruebas apuntan a una instancia real de la API.

Importante: Antes de ejecutar las pruebas de integración, asegurarse de haber iniciado la WebApi y que esté disponible en https://localhost:5001.

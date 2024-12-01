# api-store-net
API store with .NET 8

# Proyecto: Aplicación con Entity Framework y SQL Server

Se adjunta el código fuente de la aplicación completa.
Abrir el proyecto buscando el archivo product-api.sln
El proyecto usa **Entity Framework**, **SQL Server** y las herramientas proporcionadas por Entity Framework. Una vez verificadas que estas herramientas estén instaladas, puedes correr las migraciones. Si es necesario, elimina todas las migraciones existentes en la carpeta `Migrations` y ejecuta los siguientes comandos:

```bash
Add-Migration "Nombre de la migración"
Update-Database
```

Si no probar directamente:

```bash
Update-Database
```

Patrones de diseño utilizados
Inyección de dependencias: Se implementó este patrón para evitar la reutilización de funcionalidades. Permite hacer llamados a la lógica necesaria sin implementarla repetidamente. Esta decisión se tomó por los beneficios que ofrece, como alta cohesión y bajo acoplamiento.
Repository: Este patrón se utilizó para centralizar la lógica de acceso a datos en una interfaz, evitando implementaciones dispersas en otras partes de la aplicación.
Unit of Work: Se implementó para garantizar que las operaciones dentro de una transacción se completen correctamente o se deshagan en caso de error.

Arquitectura utilizada:
Use una arquitectura sencilla, una arquitectura de capas para organizar el proyecto en Controllers, Services, Respositories y Entities. pense también por el tamaño de la aplicación y siempre teniendo en cuenta escalabilidad la eleg,í por la separación de responsabilidades, reutilización de métodos y pues en fácil de implementar.

Retos puntuales
- Un reto fue al final de la aplicación decidir que tipo de logs iba a usar en la aplicación, hice una implementación no muy profunda de logs, solo la realice en los controller, lo que hice fue llamar en el program.cs ya la funcionalidad que brinda .NET que es Logging e instancie esa variable en los controller para hacer su llamado en diferentes partes cuando se va a ejecutar la función, da un error o hay señales de un alerta. Por otra parte quise implementar un log más para manejo de errores en peticiones y cree un Middleware que dependiendo el error pues lo controla en la aplicación.

Dejo el link a Drive a la carpeta para ver el video de la implementación de un cambio en vivo durante el desarrollo del back
[Carpeta de video](https://drive.google.com/drive/folders/1SlClSMMwvhhT0IRCqzuWNI7RugHFFFNK?usp=sharing)

Adjunto también exactamente lo que reciben los Post de cada API ya que sale la entidad completa en cada uno, para que sea más sencillo hacer las pruebas:

Category:
```bash
{
  "name": "string"
}
```

Product:
```bash
{
  "name": "string",
  "price": 0,
  "categoryId": 0
}
```
User:
```bash
{
  "name": "string",
  "email": "string"
}
```
WishListItem:
```bash
{
  "id": 0,
  "productId": 0,
  "userId": 0
}
```

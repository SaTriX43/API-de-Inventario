# 📦 API de Inventario – ASP.NET Core (.NET 8)

API REST desarrollada con **ASP.NET Core Web API (.NET 8)** para el control de inventario mediante **movimientos de entrada y salida**, aplicando reglas de negocio reales y buenas prácticas de arquitectura backend.

---

## 🚀 Funcionalidades

### 📌 Gestión de Productos
- Crear productos
- Validación de nombres duplicados
- Manejo de precios y fechas de creación

### 📌 Movimientos de Inventario
- Registrar **entradas** de stock
- Registrar **salidas** de stock
- Validación para impedir salidas sin stock suficiente
- El stock **no se guarda**, se **calcula dinámicamente** a partir de los movimientos

### 📌 Consultas
- Obtener stock actual de un producto
- Historial de movimientos por producto
- Filtros:
  - Rango de fechas
  - Tipo de movimiento (Entrada / Salida)
- Paginación obligatoria en el historial

---

## 🧠 Reglas de Negocio
- No se permite registrar salidas si el stock es insuficiente
- El stock se calcula como:
  - **Entradas – Salidas**
- Las validaciones de negocio viven en la capa **Service**

---

## 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas:

Controllers
│
Services
│
Repositories (DAL)
│
Models (EF Core)

yaml
Copiar código

### Responsabilidades
- **Controllers**: manejo HTTP
- **Services**: lógica de negocio
- **Repositories**: acceso a datos
- **DTOs**: transporte de información
- **Middleware**: manejo global de errores

---

## 🧰 Tecnologías Utilizadas

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- Serilog
- Swagger / OpenAPI
- Result Pattern
- LINQ
- Middleware de manejo de errores

---

## 📄 Endpoints Principales

### Productos
- `POST /api/producto/crear-producto`

### Movimientos
- `POST /api/movimiento/crear-movimiento-entrada`
- `POST /api/movimiento/crear-movimiento-salida`
- `GET  /api/movimiento/obtener-stock-actual/{productoId}`
- `GET  /api/movimiento/obtener-historial/{productoId}`

---

## 🔍 Ejemplo de Query Params (Historial)

?page=1&pageSize=10
&fechaInicio=2025-12-01
&fechaFinal=2025-12-10
&tipoEntrada=true



---

## ⚙️ Configuración

1. Clonar el repositorio
2. Configurar la cadena de conexión en `appsettings.json`
3. Ejecutar migraciones
4. Ejecutar el proyecto

---

## 📌 Estado del Proyecto

✔️ Funcional  
✔️ Arquitectura limpia  
✔️ Enfocado en buenas prácticas backend  
✔️ Proyecto de aprendizaje con enfoque profesional  

---

## 👨‍💻 Autor

**Santiago**  
Backend Developer en formación (.NET)  
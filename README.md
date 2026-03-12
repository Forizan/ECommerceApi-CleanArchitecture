# E-Commerce API

This is a backend implementation for an E-Commerce platform built with **.NET 10**, structured around **Clean Architecture** principles.

The goal of this project was to build a scalable, maintainable API that handles real-world e-commerce scenarios like order processing, cart management, and secure authentication, while enforcing separation of concerns and type safety.

## 🛠 Tech Stack & Libraries

- **Core:** .NET 10, ASP.NET Core Web API
- **Data:** Entity Framework Core, PostgreSQL (Dockerized)
- **Auth:** JWT (JSON Web Tokens), BCrypt.Net
- **Validation:** FluentValidation
- **Logging:** Serilog (Structured logging)
- **Error Handling:** Global Exception Middleware (RFC 7807 Problem Details)

## 🏗 Architecture

The solution follows a strict separation of layers to keep the domain logic independent of external frameworks.

```text
ECommerceApi/
└── src/
    ├── Domain/         # Entities and interfaces (Core logic)
    ├── Application/    # Use cases, DTOs, Validators (CQRS/Services)
    ├── Infrastructure/ # EF Core Context, Migrations, Repositories
    └── Api/            # Controllers, Middleware, Entry point
```

## ✨ Key Features

- **Product Catalog:** Full CRUD capabilities with category association.
- **Shopping Cart:** Redis-ready cart logic (currently DB backed) for managing items and quantities.
- **Order Workflow:** Checkout process that converts cart items into finalized orders.
- **Security:**
  - Secure user registration & login.
  - Password hashing via BCrypt.
  - Role-based (or claim-based) resource protection.
- **Reliability:**
  - Unified error response structure.
  - Request data validation before hitting the business logic.

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- Docker (for PostgreSQL)

### Local Setup

1. **Clone and setup configuration**
   ```bash
   git clone https://github.com/yourusername/ECommerceApi.git
   cd ECommerceApi
   ```

2. **Spin up the database**
   The project includes a `docker-compose.yml` for PostgreSQL.
   ```bash
   docker-compose up -d
   ```

3. **Apply Migrations**
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/Api
   ```

4. **Run the Application**
   ```bash
   dotnet run --project src/Api
   ```
   API will be available at `http://localhost:5025`.
   Swagger UI: `http://localhost:5025/openapi/v1.json` (or `/swagger` if configured).

## 🔌 API Usage Examples

Here are some quick `curl` commands to test the flow without a frontend.

**1. Register a new user**
```bash
curl -X POST http://localhost:5025/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email": "dev@example.com", "password": "StrongPassword123!"}'
```

**2. Login & Get Token**
```bash
curl -X POST http://localhost:5025/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "dev@example.com", "password": "StrongPassword123!"}'
```

**3. Create a Product (Protected Route)**
*Replace `YOUR_TOKEN` with the JWT from the login response.*
```bash
curl -X POST http://localhost:5025/api/products \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name": "Gaming Monitor", "price": 4500, "stock": 5, "categoryId": 1}'
```

**4. Checkout Cart**
```bash
curl -X POST http://localhost:5025/api/orders/checkout/1 \
  -H "Authorization: Bearer YOUR_TOKEN"
```

## 📝 Configuration

Check `appsettings.json` to configure:
- `ConnectionStrings`: Default points to localhost PostgreSQL.
- `JwtSettings`: Ensure you change the `SecretKey` in production.
- `Serilog`: Adjust log levels for File/Console sinks.

---

### Author
[Forizan](https://github.com/Forizan)

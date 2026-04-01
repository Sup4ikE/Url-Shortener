# 🔗 URL Shortener

Fullstack web application for shortening URLs with authentication.

## 🚀 Tech Stack

### Backend

* ASP.NET Core Web API
* Entity Framework Core (Code First)
* PostgreSQL
* JWT Authentication

### Frontend

* Angular (latest)
* Angular Router
* HTTP Client

### Infrastructure

* Docker & Docker Compose
* Nginx (for frontend hosting & proxy)

---

## ✨ Features

* 🔐 User authentication (login)
* 🔗 Create short URLs
* 📊 View URL statistics
* 🔍 View detailed URL info
* 📁 URL management
* 🌐 SPA frontend with Angular
* 🔁 API proxy через Nginx

---

## 🏗️ Architecture

```
Frontend (Angular)  →  Nginx  →  ASP.NET API  →  PostgreSQL
```

* Angular працює як SPA
* Nginx:

  * віддає фронт
  * проксує `/api` на бекенд
* API працює з базою через EF Core

---

## 🐳 Run with Docker

### 1. Clone repo

```
git clone <your-repo-url>
cd UrlShortener
```

---

### 2. Run project

```
docker compose up --build
```

---

### 3. Open in browser

* Frontend: http://localhost:4200
* API: http://localhost:5005/swagger

---

## 🔑 Test Credentials

```
Login: admin
Password: admin
```

```
Login: user
Password: user
```

---

## 🧠 Database

* PostgreSQL
* Port: `5434`
* DB: `url_shortener`

### Migrations

```
dotnet ef database update \
  --project UrlShortener.Infrastructure \
  --startup-project UrlShortener.API
```

---

## 📂 Project Structure

```
UrlShortener/
│
├── UrlShortener.API           # ASP.NET Core Web API
├── UrlShortener.Infrastructure # EF Core, DbContext, migrations
├── frontend                   # Angular app
├── docker-compose.yml
```

---

## 🔥 Key Learnings

* Docker container orchestration
* EF Core migrations & seeding
* Angular SPA routing
* Nginx reverse proxy
* Debugging real-world issues:

  * caching problems
  * database connection issues
  * missing migrations

---

## 📌 Future Improvements

* 🔐 JWT refresh tokens
* 🧪 Unit tests
* 📊 Analytics dashboard
* 🌍 Deployment (VPS / cloud)
* 🧱 Clean Architecture refactor

---

## 👨‍💻 Author

Oleg Pona

---

## ⭐ Notes

This project was built as a fullstack learning project to practice real-world development scenarios including Docker, API design, and frontend integration.

# ⚡ SNIP — URL Platform

**SNIP** is a fullstack URL shortening platform with authentication, link management, and analytics.
Built as a real-world project to demonstrate backend, frontend, and DevOps skills.

---

## 🚀 Tech Stack

### 🧠 Backend

* ASP.NET Core Web API
* Entity Framework Core (Code First)
* PostgreSQL
* JWT Authentication

### 🎨 Frontend

* Angular (latest)
* Angular Router
* HTTP Client

### ⚙️ Infrastructure

* Docker & Docker Compose
* Nginx (reverse proxy & static hosting)

---

## ✨ Features

* 🔐 Authentication (login system)
* 🔗 Create short URLs
* 📊 Basic statistics tracking
* 🔍 View detailed link info
* 📁 Manage your URLs
* 🌐 SPA frontend (Angular)
* 🔁 Nginx API proxy

---

## 🏗️ Architecture

```
Angular (SPA) → Nginx → ASP.NET Core API → PostgreSQL
```

* Frontend served via Nginx
* API requests proxied through `/api`
* Backend handles business logic & persistence

---

## 🐳 Run with Docker

### 1. Clone repository

```
git clone https://github.com/your-username/snip-url-platform.git
cd snip-url-platform
```

### 2. Run project

```
docker compose up --build
```

### 3. Open in browser

* Frontend: http://localhost:4200
* Swagger: http://localhost:5005/swagger

---

## 🔑 Demo Credentials

```
admin / admin
user / user
```

---

## 🧠 Database

* PostgreSQL
* Port: `5434`
* Database: `url_shortener`

### Run migrations

```
dotnet ef database update \
  --project Snip.Infrastructure \
  --startup-project Snip.Api
```

---

## 📂 Project Structure

```
snip-url-platform/
│
├── UrlShortener.Api
├── UrlShortener.Core
├── UrlShortener.Infrastructure
├── UrlShortener.Tests
├── frontend
├── docker-compose.yml
```

---

## 📌 Future Improvements

* 🔐 Refresh tokens
* 📊 Advanced analytics dashboard
* ☁️ Cloud deployment (AWS / VPS)

---

## 👨‍💻 Author

**Oleg Pona**

---

## ⭐ About

This project was built as a hands-on fullstack application to simulate real production-like development workflows.

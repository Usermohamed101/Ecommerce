# MyApi

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-green)](https://www.docker.com/)
[![CI/CD](https://img.shields.io/badge/CI--CD-GitHub%20Actions-orange)](https://github.com/features/actions)

---

## Overview
**MyApi** is a fully-featured **Dockerized ASP.NET Core Web API** demonstrating advanced authentication, security, and payment integration.  
It is a professional-level portfolio project showing backend development, Docker deployment, and secure API practices.

---

## Key Features

### Authentication & Security
- JWT-based authentication
- **User registration and login**
- **Change password**
- **Email confirmation**
- **External login providers** (e.g., Google, Facebook)
- **Refresh tokens** for secure, long-lived sessions
- **Rate limiting** to protect APIs from abuse

### Payment Integration
- Fully integrated **FAWATERAK payment gateway**
- Handles payment verification and secure transactions

### API Features
- CRUD operations for main entities
- Database seeding for testing
- Full error handling and validation

### Technologies
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Docker & Docker Compose
- JWT Authentication & Refresh Tokens
- Rate Limiter Middleware
- FAWATERAK Payment Gateway

---

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Postman](https://www.postman.com/) (optional, for testing)

### Running the Project with Docker
1. Build and start containers:

```bash
docker-compose up --build

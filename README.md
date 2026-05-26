# 🌌 Hindsight — Missed Fortune Calculator

**Hindsight** is a brutal, time-travel personal finance tracker and dashboard that calculates exactly how much wealth you *missed out on* by not buying assets at specific points in history. 

Featuring a sleek, dark cyberpunk user interface, it provides a vivid look into alternate-reality wealth generation by dynamically combining real-time and historical market streams.

---

## 🏗️ High-Level Architecture

Hindsight is built as a split-cloud, high-performance full-stack application designed to scale efficiently across optimal edge and container networks.

* **Frontend:** A streaming serverless application deployed on **Cloudflare Pages/Workers** for instant global delivery, low-latency rendering, and native routing alignment.
* **Backend:** A production-hardened, clean-architecture Web API built with **.NET 10** and containerized with **Docker**, hosted on **Render**.
* **Database:** A zero-config **SQLite** database instance running within the container sandbox, featuring automated startup schema migrations.
* **Market Feed:** Powered via the secure **CoinGecko Free Demo API** infrastructure.

---

## 🛠️ Tech Stack Matrix

### Frontend : https://github.com/RonitRaaj/time-fortune
* **Runtime & Package Manager:** Bun (v1.3.4+)
* **Core Framework:** React 19 & TypeScript
* **Routing Engine:** TanStack Router & TanStack Start
* **State Management:** TanStack React Query (v5)
* **Styling Engine:** Tailwind CSS (v4) & Radix UI Primitives
* **Iconography:** Lucide React

### Backend & Infrastructure
* **Engine:** .NET 10 (ASP.NET Core Web API)
* **Data Access:** Entity Framework Core (EF Core)
* **Containerization:** Docker (Multi-stage production build configuration)
* **Documentation:** Swagger / OpenAPI Integration

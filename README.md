# Restaurant Order System
Ein Full-Stack Bestellsystem für Restaurants: Gäste bestellen direkt am Tisch über eine Web-App, 
das Personal verwaltet eingehende Bestellungen über eine Admin-Oberfläche.

## Features
-  Digitale Speisekarte, gruppiert nach Kategorien
-  Warenkorb mit Mengensteuerung
-  Tischbezogene Bestellungen (Jeder Tisch hat seine eigene URL, gedacht für QR-Code-Zugriff)
-  Admin-Übersicht für eingehende Bestellungen mit Status-Verwaltung (Offen - In Zubereitung - Serviert - Bezahlt)
-  Rest-api mit vollem CRUD für Kategorien - Gerichte - Tische - Bestellungen und Bestellpositionen

## Tech-Stack
Backend
-  ASP.NET Core Web API (.NET 10)
-  Entity Framework Core mit SQLite
-  Swagger / OpenAPI zur API-Dokumentation
rontend
-  SvelteKit (Svelte 5, TypeScript)
-  Ich hab mir selbst eine Lösung gebaut (mit Svelte5 eingebauten Werkzeugen namens 'Runes'), um zu verwalten, was im Warenkorb liegt, statt eine fertige externe Bib zu nutzen."

## PJ-Struktur
```
RestaurantApp/
├── backend/
│   └── RestaurantOrderSystem/
│       ├── Controllers/
│       ├── Models/
│       └── Data/
└── frontend/
    └── src/
        └── routes/
```

## DatenModell
<img width="800" height="500" alt="image" src="https://github.com/user-attachments/assets/437f7590-4a68-4907-9a28-04c2ef7df5eb" />

Eine (Mehrere) Bestellung (Order) ist einem Tisch zugeordnet und besteht aus mehreren Bestellpositionen (OrderItem), die jeweils auf ein Gericht (MenuItem) verweisen.

## Setup & Start
### voraussetzungen 
-  .NET SDK 10
-  Node.js

### Backend starten
-  cd backend/RestaurantOrderSystem
-  dotnet run
Das Backend läuft danach unter http://localhost:5072.
Die API-Dokumentation ist unter http://localhost:5072/swagger erreichbar.

### Frontend starten
-  cd frontend
-  npm install
-  npm run dev
Das Frontend läuft danach unter http://localhost:5173.

### Anwendung nutzen 
-  Speisekarte (Gast-Ansicht): http://localhost:5173/?tisch=1
-  Admin-Ansicht. http://localhost:5173/admin

### Api-Endpoints
Alle Endpoints unter /api/{Resource}, mit vollem CRUD (GET, GET/{id}, POST, PUT/{id}, DELETE/{id}):
-  /api/Categories
-  /api/MenuItems
-  /api/Tables
-  /api/Orders
-  /api/OrderItems

## Hintergrund

Dieses Projekt ist im Rahmen meiner Ausbildung an der HTL Grieskirchen (Zweig Informatik) entstanden, 
als praktisches Full-Stack-Projekt zur Vertiefung von ASP.NET Core, Entity Framework Core und modernem Frontend-Development mit Svelte.






















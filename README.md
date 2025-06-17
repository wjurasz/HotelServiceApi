
# Hotel Reservation System – API



## 🚀 Instalacja i uruchomienie  
Aby uruchomić aplikację, wykonaj poniższe kroki:

### 1️⃣ Sklonuj repozytorium
```bash
git clone https://github.com/wjurasz/HotelServiceApi
```

### 2️⃣ Wykonaj migracje dla projektów .Storage
```bash
add-migration nazwa
update-database
```

## 🎯 Technologie  
- C#  
- ASP.NET Core  
- Entity Framework Core  
- SQL Server  
- API REST  
- Dependency Injection  
- HttpClient  

## ✨ Funkcje  
- Zarządzanie klientami (CRUD: Create, Read, Update, Delete)
- Rezerwacje z możliwością dodania promocji
- Synchronizacja z bazą danych za pomocą Entity Framework
- Możliwość użycia promocji przy rezerwacjach
- Interfejs do obsługi klientów, promocji i rezerwacji  
- Symulacja rezerwacji z możliwością zarządzania statusem (oczekująca, potwierdzona, anulowana)  

### Endpointy API:

1. **Klienci**:
    - `GET /client` – Lista wszystkich klientów
    - `GET /client/{id}` – Pobierz klienta po identyfikatorze
    - `POST /client` – Utwórz nowego klienta
    - `PUT /client/{id}` – Zaktualizuj dane klienta
    - `PATCH /client/{id}` – Częściowa aktualizacja klienta
    - `DELETE /client/{id}` – Usuń klienta

2. **Promocje**:
    - `GET /api/promotions` – Lista wszystkich promocji
    - `GET /api/promotions/{code}` – Pobierz promocję po kodzie
    - `POST /api/promotions` – Dodaj nową promocję
    - `DELETE /api/promotions/{id}` – Usuń promocję

3. **Rezerwacje**:
    - `GET /reservations` – Lista wszystkich rezerwacji
    - `GET /reservations/{id}` – Pobierz rezerwację po identyfikatorze
    - `POST /reservations` – Tworzenie nowej rezerwacji
    - `PATCH /reservations/{id}/confirm` – Potwierdzenie rezerwacji
    - `PATCH /reservations/{id}/cancel` – Anulowanie rezerwacji

## 🛠 Przykłady użycia:

- Tworzenie nowego klienta:
    ```bash
    POST /client
    {
        "FirstName": "Przykładowy",
        "LastName": "Klient",
        "Email": "p.klient@email.com",
        "PhoneNumber": "123456789"
    }
    ```

- Rezerwacja z promocją:
    ```bash
    POST /reservations
    {
        "ClientId": 1,
        "StartDate": "2025-07-01",
        "EndDate": "2025-07-07",
        "PromotionId": 1
    }
    ```

### Pliki:

- `ClientController.cs`: Obsługuje operacje CRUD na klientach.
- `ReservationController.cs`: Obsługuje operacje CRUD na rezerwacjach.
- `PromotionController.cs`: Obsługuje operacje CRUD na promocjach.
- `ClientService.cs`: Logika biznesowa związana z klientami.
- `ReservationService.cs`: Logika biznesowa związana z rezerwacjami.
- `PromotionService.cs`: Logika biznesowa związana z promocjami.


### Baza danych:
Projekt korzysta z SQL Server do przechowywania danych o klientach, rezerwacjach i promocjach.

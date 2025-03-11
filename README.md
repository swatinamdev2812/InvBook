# InvBook

InvBook is a .NET Core 8 Web API designed for inventory booking. It follows the CQRS pattern with MediatR for handling requests and AutoMapper for object mapping. The application is containerized using Docker and uses SQLite as the database.

## Features
- **Inventory Management**:
  - Get all inventory items
  - Create a new inventory item
  - Delete an inventory item by ID
  - Delete all inventory items
- **Member Management**:
  - Create a member
  - Get all members
  - Get a member by ID
  - Delete a member by ID
- **Inventory Booking**:
  - `/bookings/book` - Book an inventory item.
  - **Response:** `"Booking successful with Booking Id: {booking.Id}"`
  - `/bookings/cancel` - Cancel a booking using the booking ID.
  - `/bookings/by-date` - Get bookings by date.
  - `/bookings/member/{memberId}` - Get bookings by member ID.
  - A maximum of **2 active bookings per user**.
  - Only available inventory items can be booked.
- **File Upload Service**:
  - Upload inventory items to be stored in the `InventoryItems` table.
- **Logging & Error Handling**:
  - Integrated **Serilog** for structured logging.
  - Custom **Exception Handling Middleware** to manage API errors.
- **Docker Support**: The application is containerized for easy deployment.

## Technologies Used

- **.NET Core 8**
- **CQRS Pattern** (with MediatR)
- **AutoMapper**
- **SQLite**
- **Docker**
- **Serilog** (Logging)
- **Exception Handling Middleware**

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Setup & Run

1. **Clone the repository**

   ```sh
   git clone <repository-url>
   cd InvBook
   ```

2. **Build & Run with Docker**

   ```sh
   docker build -t invbook .
   docker run -d -p 8080:8080 -v sqlite_data:/app/data --name invbook-container invbook
   ```

3. **Run Without Docker**

   ```sh
   dotnet build
   dotnet run
   ```

## API Endpoints

### Inventory Endpoints

- `GET /api/inventory/getall` - Get all inventory items
- `POST /api/inventory/create` - Add a new inventory item
- `DELETE /api/inventory/delete/{id}` - Delete an inventory item by ID
- `DELETE /api/inventory/deleteall` - Delete all inventory items

### Member Endpoints

- `POST /api/members/create` - Create a new member
- `GET /api/members/getall` - Get all members
- `GET /api/members/{id}` - Get a member by ID
- `DELETE /api/members/delete/{id}` - Delete a member by ID

### Booking Endpoints

- `POST /api/bookings/book` - Book an inventory item
  - **Request Body:** `{ "memberId": 1, "inventoryItemId": 2 }`
  - **Response:** `"Booking successful with Booking Id: {booking.Id}"`
  - **Rules:** Max 2 active bookings per user, only available items can be booked.
- `POST /api/bookings/cancel` - Cancel a booking
  - **Request Body:** `{ "bookingId": 5 }`
  - **Response:** `{ "message": "Booking cancelled successfully" }`
- `GET /api/bookings/by-date` - Get bookings by date
- `GET /api/bookings/member/{memberId}` - Get bookings by member ID

### Upload Service

- `POST /api/upload` - Upload inventory items (file-based import)

## Contact

For any inquiries, reach out via GitHub issues.


# Sweet Crumbs Bakery Management (ASP.NET Core MVC)

Starter application built with **ASP.NET Core 8 MVC, Entity Framework Core, SQLite and ASP.NET Core Identity**.

## Included
- Landing page and product catalogue
- Shopping cart with quantity updates
- Checkout and order persistence
- Printable order receipt (`Print order`)
- Payment page marked as demo only
- Customer reviews and contact form
- Admin product create/edit/delete and order list (role-protected)
- Identity login and registration pages

## Run locally
1. Install the **.NET 8 SDK**.
2. Open a terminal in this folder.
3. Run:
   ```bash
   dotnet restore
   dotnet run
   ```
4. Open the local URL printed by `dotnet run`.

The SQLite database `bakery.db` is created automatically on first run, with sample bakery products.

## Important setup notes
- **Admin access:** create a user using Register, then assign the `Admin` role to that user. For a development database, one option is to temporarily seed an admin user/role in `Program.cs`; do not hard-code production passwords. The app creates the `Admin` and `Customer` roles, but it intentionally does not create a default admin account.
- **Payments:** the included button is a demonstration only and does not process real money. For real payments, integrate a provider such as Razorpay or Stripe using server-side order creation, signature/webhook verification, and provider test keys stored in User Secrets/environment variables. Never trust a client-side “payment successful” redirect.
- **EF Core schema:** this starter uses `EnsureCreated()` for quick local setup. For production or ongoing schema changes, replace it with EF Core migrations (`dotnet ef migrations add InitialCreate`, then `dotnet ef database update`).
- Replace sample contact details and image URLs before publishing.
- Add rate limiting, anti-spam protection, HTTPS, secure secrets, and production error handling before deployment.

## Project structure
- `Controllers/` MVC controllers
- `Models/` EF Core entities
- `Data/ApplicationDbContext.cs` Identity + application database
- `Views/` Razor pages
- `wwwroot/css/site.css` styles

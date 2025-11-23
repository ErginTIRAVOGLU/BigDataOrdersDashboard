# Copilot Instructions for BigDataOrdersDashboard

## Project Overview
This is an ASP.NET Core MVC application for managing and visualizing product and category data in a SQL Server database. The architecture follows standard MVC patterns, with Entity Framework Core for data access and Razor views for UI.

## Key Components
- **Controllers**: Business logic and routing (`Controllers/`)
  - `ProductController`, `CategoryController`, `HomeController`, etc.
- **Entities**: Data models (`Entities/`)
  - `Product`, `Category` (with navigation properties)
- **DbContext**: Database access (`Context/BigDataOrdersDbContext.cs`)
- **Views**: Razor pages for UI (`Views/`)
  - Organized by feature (e.g., `Product/ProductList.cshtml`)
- **Configuration**: Connection strings and logging (`appsettings.json`, `appsettings.Development.json`)
- **Static Assets**: JS/CSS/images in `wwwroot/`

## Data Flow
- Controllers interact with `BigDataOrdersDbContext` to query/update entities.
- Views receive model objects from controllers for rendering.
- Product/category CRUD operations are handled via controller actions and corresponding views.

## Developer Workflows
- **Build**: Use Visual Studio or run `dotnet build BigDataOrdersDashboard.csproj` in the project directory.
- **Run**: Use Visual Studio or `dotnet run --project BigDataOrdersDashboard.csproj`.
- **Database**: Connection string is in `appsettings.json` under `DefaultConnection`. Uses SQL Server.
- **Entity Framework**: Migrations and updates should use EF Core CLI (`dotnet ef ...`).
- **Debugging**: Standard ASP.NET Core debugging tools apply. Errors route to `/Home/Error`.

## Project-Specific Patterns
- **ViewBag for Dropdowns**: Country/category dropdowns are populated via `ViewBag` in controller actions.
- **Pagination**: Product list uses simple pagination (page size 50, see `ProductController.ProductList`).
- **Validation**: Model validation is handled via `[HttpPost]` actions and `ModelState.IsValid`.
- **Image URLs**: Products have a `ProductImageUrl` property for image display.
- **Category Navigation**: `Category` entity has a collection of `Product` entities.

## Integration Points
- **CKEditor**: Included in `wwwroot/assets/plugins/ckeditor/` for rich text editing.
- **Entity Framework Core**: Used for all data access.
- **SQL Server**: Main database backend.

## Conventions
- **File/Folder Naming**: Follows feature-based organization (e.g., `Views/Product/`, `Controllers/ProductController.cs`).
- **Nullable Reference Types**: Enabled in project settings.
- **Static Asset Mapping**: Uses custom `MapStaticAssets()` extension in `Program.cs`.

## Example: Adding a New Product
1. Add logic to `ProductController.CreateProduct` (GET/POST).
2. Update `Views/Product/CreateProduct.cshtml` for the form.
3. Ensure `Product` entity and `BigDataOrdersDbContext` are updated if new fields are needed.

## References
- Main entry: `Program.cs`
- Data context: `Context/BigDataOrdersDbContext.cs`
- Entities: `Entities/Product.cs`, `Entities/Category.cs`
- Views: `Views/Product/`, `Views/Category/`
- Static assets: `wwwroot/assets/`

---
For further details, see controller and view files for specific feature implementations. Follow existing patterns for CRUD, validation, and dropdown population.

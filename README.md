# Modular Monolith Sandbox

Doing RiverBooks!

## Books Database Migrations

- Change to `RiverBooks.Web` directory
- Run the following to **add** a migration (replace `Initial` with your migration name):

```
dotnet ef migrations add Initial -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```

- Update the physical database with the following:

```
dotnet ef database update
```

- To update the Testing database:

```
dotnet ef database update -- --environment Testing
```

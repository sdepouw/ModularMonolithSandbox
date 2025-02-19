# Modular Monolith Sandbox

Doing RiverBooks!

## Books Database Migrations

- Change to `RiverBooks.Web` directory
- Run the following to **add** a migration (replace `Initial` with your migration name):

```
dotnet ef migrations add Initial -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o Infrastructure/Data/Migrations
```

- Update the physical database with the following:

```
dotnet ef database update -c BookDbContext
```

- To update the Testing database:

```
dotnet ef database update -c BookDbContext -- --environment Testing
```

## Users Database Migrations

- Change to `RiverBooks.Web` directory
- Run the following to **add** a migration (replace `InitialUsers` with your migration name):

```
dotnet ef migrations add InitialUsers -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Infrastructure/Data/Migrations
```

- Update the physical database with the following:

```
dotnet ef database update -c UsersDbContext
```

- To update the Testing database:

```
dotnet ef database update -c UsersDbContext -- --environment Testing
```

## OrderProcessing Database Migrations

- Change to `RiverBooks.Web` directory
- Run the following to **add** a migration (replace `Initial_OrderProcessing` with your migration name):

```
dotnet ef migrations add Initial_OrderProcessing -c OrderProcessingDbContext -p ..\RiverBooks.OrderProcessing\RiverBooks.OrderProcessing.csproj -s .\RiverBooks.Web.csproj -o Infrastructure/Data/Migrations
```

- Update the physical database with the following:

```
dotnet ef database update -c OrderProcessingDbContext
```

- To update the Testing database:

```
dotnet ef database update -c OrderProcessingDbContext -- --environment Testing
```

## OrderProcessing Materialized View Support - Redis

Requires Docker. Run the following command:

```
docker run --name my-redis -p 6379:6379 -d redis
```

Confirm that it is running with:

```
docker ps
```

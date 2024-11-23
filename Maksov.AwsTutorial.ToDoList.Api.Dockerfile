# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY ["Maksov.AwsTutorial.ToDoList.Api/Maksov.AwsTutorial.ToDoList.Api.csproj", "Maksov.AwsTutorial.ToDoList.Api/"]
COPY ["Maksov.AwsTutorial.ToDoList.BLL/Maksov.AwsTutorial.ToDoList.BLL.csproj", "Maksov.AwsTutorial.ToDoList.BLL/"]
COPY ["Maksov.AwsTutorial.ToDoList.BLL.Implementation/Maksov.AwsTutorial.ToDoList.BLL.Implementation.csproj", "Maksov.AwsTutorial.ToDoList.BLL.Implementation/"]

# Restore dependencies
RUN dotnet restore ./Maksov.AwsTutorial.ToDoList.Api/Maksov.AwsTutorial.ToDoList.Api.csproj

# Copy all project files
COPY . ./
RUN dotnet publish ./Maksov.AwsTutorial.ToDoList.Api -c Release -o publish --no-restore

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Maksov.AwsTutorial.ToDoList.Api.dll"]

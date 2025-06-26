# Todo App — Full Stack (.NET Core API + HTML/CSS/JavaScript)

This is a simple full-stack Todo List application built using:

-  **Backend:** ASP.NET Core Web API
-  **Frontend:** HTML, CSS, JavaScript (Vanilla)

Users can:
-  Add a todo
-  View todos
-  Mark a todo as completed
-  Delete a todo

##  Prerequisites

- [.NET 6 or later SDK](https://dotnet.microsoft.com/download)
- A web browser (Chrome, Firefox, Edge, etc.)
- Optional: [Swagger](https://swagger.io/) for testing API

###  Backend Setup

1. Open the `Backend/` folder in your terminal or IDE.
2. Restore and build the project:
   ```bash 
   dotnet restore
   dotnet build
   dotnet run

3. Go to the TodoFrontend/ directory
- Open index.html in a browser directly, or use Live Server (VS Code).
- The frontend JS (in app.js) communicates with the API at http://localhost:5022/api/todo.
// const apiUrl = "http://localhost:5022/api/todo";

// const form = document.getElementById("todo-form");
// const todoList = document.getElementById("todo-list");

// form.addEventListener("submit", async (e) => {
//   e.preventDefault();

//   const newTodo = {
//     title: document.getElementById("title").value.trim(),
//     description: document.getElementById("description").value.trim(),
//     dueDate: document.getElementById("dueDate").value,
//     priority: parseInt(document.getElementById("priority").value)
//   };

//   console.log("Sending:", JSON.stringify(newTodo));

//   try {
//     const res = await fetch(apiUrl, {
//       method: "POST",
//       headers: {
//         "Content-Type": "application/json"
//       },
//       body: JSON.stringify(newTodo)
//     });

//     const data = await res.json();

//     if (!res.ok) {
//       throw new Error(data.message || "Failed to create todo");
//     }

//     alert(data.message || "Todo added successfully");
//     form.reset();
//     loadTodos();
//   } catch (err) {
//     alert("Error creating todo.");
//     console.error("Create Todo Error:", err);
//   }
// });

// async function loadTodos() {
//   try {
//     const res = await fetch(apiUrl);
//     const result = await res.json();

//     const todos = result.data || [];

//     todoList.innerHTML = "";
//     todos.forEach((todo) => {
//       const li = document.createElement("li");
//       li.innerHTML = `
//         <strong>${todo.title}</strong><br/>
//         ${todo.description}<br/>
//         Due: ${new Date(todo.dueDate).toLocaleDateString()}<br/>
//         Priority: ${todo.priority} | Completed: ${todo.isComplete}
//         <hr/>
//       `;
//       todoList.appendChild(li);
//     });
//   } catch (err) {
//     console.error("Error loading todos:", err);
//   }
// }

// // Initial load
// loadTodos();
const apiUrl = "http://localhost:5022/api/todo";

const form = document.getElementById("todo-form");
const todoList = document.getElementById("todo-list");

form.addEventListener("submit", async (e) => {
  e.preventDefault();

  const newTodo = {
    title: document.getElementById("title").value.trim(),
    description: document.getElementById("description").value.trim(),
    dueDate: document.getElementById("dueDate").value,
    priority: parseInt(document.getElementById("priority").value)
  };

  try {
    const res = await fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(newTodo)
    });

    const data = await res.json();

    if (!res.ok) throw new Error(data.message || "Failed to create todo");

    alert(data.message || "Todo added successfully");
    form.reset();
    loadTodos();
  } catch (err) {
    alert("Error creating todo.");
    console.error("Create Todo Error:", err);
  }
});

async function loadTodos() {
  try {
    const res = await fetch(apiUrl);
    const result = await res.json();

    const todos = result.data || [];

    todoList.innerHTML = "";
    todos.forEach((todo) => {
      const li = document.createElement("li");
      li.innerHTML = `
        <strong>${todo.title}</strong><br/>
        ${todo.description}<br/>
        Due: ${new Date(todo.dueDate).toLocaleDateString()}<br/>
        Priority: ${todo.priority}<br/>
        Completed: ${todo.isComplete}<br/>
        <button onclick="markComplete('${todo.id}')">Mark Complete</button>
        <button onclick="deleteTodo('${todo.id}')">Delete</button>
        <hr/>
      `;
      todoList.appendChild(li);
    });
  } catch (err) {
    console.error("Error loading todos:", err);
  }
}

async function markComplete(id) {
  try {
    const res = await fetch(`${apiUrl}/${id}/complete`, {
      method: "PUT"
    });

    const data = await res.json();

    if (!res.ok) throw new Error(data.message || "Failed to mark complete");

    alert(data.message || "Todo marked as complete");
    loadTodos();
  } catch (err) {
    alert("Error marking as complete.");
    console.error("Mark Complete Error:", err);
  }
}

async function deleteTodo(id) {
  try {
    const res = await fetch(`${apiUrl}/${id}`, {
      method: "DELETE"
    });

    const data = await res.json();

    if (!res.ok) throw new Error(data.message || "Failed to delete");

    alert(data.message || "Todo deleted");
    loadTodos();
  } catch (err) {
    alert("Error deleting todo.");
    console.error("Delete Todo Error:", err);
  }
}

// Initial load
loadTodos();

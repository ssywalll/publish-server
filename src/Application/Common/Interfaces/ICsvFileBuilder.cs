using CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus;
using CleanArchitecture.Application.TodoLists.Queries.ExportTodos;
namespace CleanArchitecture.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}

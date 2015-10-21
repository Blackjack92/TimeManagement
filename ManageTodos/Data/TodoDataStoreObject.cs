using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Data
{
    internal class TodoDataStoreObject : DataStoreObject<Todo>
    {
        protected override void SetProperties(List<Expression<Func<Todo, object>>> selector)
        {
            selector.Add(todo => todo.Id);
            selector.Add(todo => todo.Title);
            selector.Add(todo => todo.WorkingItems);
        }
    }
}

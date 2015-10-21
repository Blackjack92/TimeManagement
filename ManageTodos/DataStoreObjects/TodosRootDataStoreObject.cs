using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.DataStoreObjects
{
    internal class TodosRootDataStoreObject : DataStoreObject<TodosRoot>
    {
        protected override void SetProperties(List<Expression<Func<TodosRoot, object>>> selector)
        {
            selector.Add(root => root.Todos);
        }
    }
}

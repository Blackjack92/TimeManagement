using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.DataStoreObjects
{
    internal class WorkingItemsRootDataStoreObject : DataStoreObject<WorkingItemsRoot>
    {
        protected override void SetProperties(List<Expression<Func<WorkingItemsRoot, object>>> selector)
        {
            selector.Add(root => root.WorkingItems);
        }
    }
}

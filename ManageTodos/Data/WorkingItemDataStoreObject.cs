using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Data
{
    internal class WorkingItemDataStoreObject : DataStoreObject<WorkingItem>
    {
        protected override void SetProperties(List<Expression<Func<WorkingItem, object>>> selector)
        {
            selector.Add(workingItem => workingItem.Description);
        }
    }
}

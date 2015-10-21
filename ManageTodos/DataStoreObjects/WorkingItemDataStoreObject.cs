using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.DataStoreObjects
{
    internal class WorkingItemDataStoreObject : DataStoreObject<WorkingItem>
    {
        protected override void SetProperties(List<Expression<Func<WorkingItem, object>>> selector)
        {
            selector.Add(workingItem => workingItem.Id);
            selector.Add(workingItem => workingItem.Description);

            selector.Add(workingItem => workingItem.Start);
            selector.Add(workingItem => workingItem.End);
        }
    }
}

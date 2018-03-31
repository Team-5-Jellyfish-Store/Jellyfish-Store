using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineStore.Tests.Mocks
{
    public static class DbSetExtensions
    {
        public static Mock<IDbSet<T>> GetQueryableMockDbSet<T>(this List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<IDbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.Setup(m => m.Add(It.IsAny<T>())).Verifiable();
            return dbSet;
        }
    }
}

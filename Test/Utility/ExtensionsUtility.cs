using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Test.Utility
{
    public static class ExtensionsUtility
    {
        public static IDbSet<T> ToDbSet<T>(this IList<T> list) where T : class
        {
            /*http://msdn.microsoft.com/en-us/library/dn314429.aspx#queryTest */
            IQueryable<T> data = list.AsQueryable();
            var mockSet = new Mock<IDbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}

using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace SportManager.Repositories.Helper
{
    public static class DbContextHelper
    {
        public static DbSet<T> GetDbSet<T>(this DbContext context) where T : class
        {
            PropertyInfo pi = context.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>));
            if (pi == null) return null;
            return pi.GetValue(context, null) as DbSet<T>;
        }
    }
}
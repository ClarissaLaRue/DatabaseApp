using System;

namespace Database.Common.Interfaces
{
    public interface IFactory
    {
        object CreateInstance(Type targetType, object[] parameters);
    }
}
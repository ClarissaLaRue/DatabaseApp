using System;

namespace SportManager.Common.Interfaces
{
    public interface IFactory
    {
        object CreateInstance(Type targetType, object[] parameters);
    }
}
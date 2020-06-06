using System;
using Database.DataAccess.Entities.Helper;
using Database.DataAccess.Entities.Interfaces;

namespace Database.DataAccess.Entities.Base
{
    [Serializable]
    public class ModelBase : IModel
    {
        protected ModelBase()
        {
            InitializationHelper.Initialize(this);
        }

    }
}
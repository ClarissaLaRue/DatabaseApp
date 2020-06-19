using System;
using SportManager.DataAccess.Entities.Helper;
using SportManager.DataAccess.Entities.Interfaces;

namespace SportManager.DataAccess.Entities.Base
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
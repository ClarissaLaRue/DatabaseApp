using System;
using Database.DataAccess.Entities.Helper;
using Database.Models.Interfaces;

namespace Database.Models.ViewModel.Base
{
    [Serializable]
    public class ModelBase : IModel
    {
        #region Constructors

        protected ModelBase()
        {
            InitializationHelper.Initialize(this);
        }

        #endregion //Constructors

        public string FormatBoolean(bool? value, string nullText = "Unknown")
        {
            return value.HasValue ? (value.Value ? "Yes" : "No") : nullText;
        }
    }
}
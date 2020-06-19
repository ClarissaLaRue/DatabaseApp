using System;
using SportManager.Models.Interfaces;

namespace SportManager.Models.Base
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
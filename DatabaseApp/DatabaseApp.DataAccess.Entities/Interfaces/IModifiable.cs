﻿namespace Database.DataAccess.Entities.Interfaces
{
    public interface IModifiable : IModel
    {
        bool IsDirty { get; set; }
    }
}
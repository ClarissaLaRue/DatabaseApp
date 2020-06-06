using System;

namespace Database.DataAccess.Entities.Helper
{
    public class CascadeDeleteAttribute: Attribute
    {
        public CascadeDeleteAttribute():this(true)
        {
        }

        public CascadeDeleteAttribute(bool value)
        {
            Value = value;
        }
        public bool Value { get; set; }
    }
}
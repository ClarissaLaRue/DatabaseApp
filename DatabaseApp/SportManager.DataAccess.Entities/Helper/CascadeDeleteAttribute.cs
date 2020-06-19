using System;

namespace SportManager.DataAccess.Entities.Helper
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
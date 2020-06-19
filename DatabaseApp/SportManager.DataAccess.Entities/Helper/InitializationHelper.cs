using System.Reflection;
using SportManager.DataAccess.Entities.Interfaces;

namespace SportManager.DataAccess.Entities.Helper
{
    public static class InitializationHelper
    {
        public static void Initialize(object target)
        {
            foreach (PropertyInfo propertyInfo in target.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    var initAttrList = (InitializeAttribute[])propertyInfo.GetCustomAttributes(typeof(InitializeAttribute), false);
                    if (initAttrList.Length != 0)
                    {
                        object value = propertyInfo.GetValue(target, null);
                        foreach (InitializeAttribute initializeAttribute in initAttrList)
                        {
                            value = initializeAttribute.Init(propertyInfo.PropertyType, value);
                            propertyInfo.SetValue(target, value, null);
                        }
                    }
                    var defValueAttrList = (DefaultValueAttribute[])propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                    if (defValueAttrList.Length != 0)
                    {
                        object value = propertyInfo.GetValue(target, null);
                        foreach (DefaultValueAttribute defaultValueAttribute in defValueAttrList)
                        {
                            propertyInfo.SetValue(target, defaultValueAttribute.Value, null);
                        }
                    }
                }
            }

            var modifiable = target as IModifiable;
            if (modifiable != null)
            {
                var npc = target as INotifyPropertyChanged;

                if (npc != null)
                {
                    npc.PropertyChanged += (sender, e) =>
                    {
                        if (e.PropertyName != "IsDirty" && !((IModifiable)sender).IsDirty)
                        {
                            ((IModifiable)sender).IsDirty = true;
                        }
                    };
                }

                modifiable.IsDirty = false;
            }
        }
    }
}
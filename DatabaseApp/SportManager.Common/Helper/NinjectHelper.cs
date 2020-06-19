using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Parameters;

namespace SportManager.Common.Helper
{
    public static class NinjectHelper
    {
        public static IKernel Kernel { get;  set; }

        public static TObject Get<TObject>()
        {
            return (TObject)NinjectHelper.Kernel.GetService(typeof(TObject));
        }

        public static IEnumerable<TObject> GetAll<TObject>()
        {
            return Kernel.GetAll(typeof(TObject), new IParameter[0]).Cast<TObject>();
        }

        public static bool IsDefined<TObject>()
        {
            return Kernel.GetBindings(typeof (TObject)).Any();
        }
    }
}
using System.Collections.Generic;

namespace GameOfThrones.Droid.Extensions
{
    public static class BindingsExtensions
    {
        public static void DetachAll(this List<GalaSoft.MvvmLight.Helpers.Binding> self)
        {
            self.ForEach((binding) => binding.Detach());

            self.Clear();
        }
    }
}

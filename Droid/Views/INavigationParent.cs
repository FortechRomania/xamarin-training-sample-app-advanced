using Android.Support.V7.Widget;
using GameOfThrones.Droid.Fragments;

namespace GameOfThrones.Droid.Views
{
    public interface INavigationParent
    {
        Toolbar Toolbar { get; }

        int NumberOfChildren { get; }

        NavigationChildFragment TopFragment { get; }

        void PresentFragment(NavigationChildFragment fragment);

        void PopToRoot();
    }
}

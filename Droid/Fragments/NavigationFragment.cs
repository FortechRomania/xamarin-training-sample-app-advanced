using System.Linq;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using GameOfThrones.Droid.Views;

using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace GameOfThrones.Droid.Fragments
{
    public class NavigationFragment : SupportFragment, INavigationParent
    {
        private const int RootFragmentCount = 1;

        public Toolbar Toolbar { get; private set; }

        public int NumberOfChildren => ChildFragmentManager.BackStackEntryCount + RootFragmentCount;

        public NavigationChildFragment RootFragment { get; private set; }

        public NavigationChildFragment TopFragment => ChildFragmentManager.Fragments.First() as NavigationChildFragment;

        public static NavigationFragment NewInstance(NavigationChildFragment rootFragment)
        {
            var navigationFragment = new NavigationFragment();
            navigationFragment.RootFragment = rootFragment;

            return navigationFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_navigation, container, false);
            Toolbar = root.FindViewById<Toolbar>(Resource.Id.toolbar);
            Toolbar.NavigationClick += HandleNavigationPressed;

            return root;
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            if (!ChildFragmentManager.Fragments.Any() && RootFragment != null)
            {
                PresentFragment(RootFragment);
            }
        }

        public void PresentFragment(NavigationChildFragment fragment)
        {
            var fragmentTransaction = ChildFragmentManager.BeginTransaction();

            fragmentTransaction.Replace(Resource.Id.navigation_content_frame, fragment);
            fragmentTransaction.SetTransition(SupportFragmentTransaction.TransitFragmentFade);

            if (ChildFragmentManager.Fragments.Any())
            {
                fragmentTransaction.AddToBackStack(null);
            }

            fragmentTransaction.Commit();
        }

        public void PopToRoot()
        {
            ChildFragmentManager.PopBackStackImmediate(null, Android.Support.V4.App.FragmentManager.PopBackStackInclusive);
        }

        private void HandleNavigationPressed(object sender, Toolbar.NavigationClickEventArgs e)
        {
            ChildFragmentManager.PopBackStackImmediate();
        }
    }
}

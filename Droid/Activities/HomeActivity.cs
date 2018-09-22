using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using GameOfThrones.Droid.Fragments;

namespace GameOfThrones.Droid.Activities
{
    [Activity(MainLauncher = true)]
    public partial class HomeActivity : AppCompatActivity
    {
        public enum HomeNavigationFragmentType
        {
            Books, Characters, Houses
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_home);

            BottomNavigationView.NavigationItemSelected += BottomNavigationItemSelected;
            BottomNavigationView.SelectedItemId = Resource.Id.menu_books;
        }

        private void BottomNavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_books:
                    LoadFragment(HomeNavigationFragmentType.Books);
                    break;
                case Resource.Id.menu_characters:
                    LoadFragment(HomeNavigationFragmentType.Characters);
                    break;
                case Resource.Id.menu_houses:
                    LoadFragment(HomeNavigationFragmentType.Houses);
                    break;
            }
        }

        private void LoadFragment(HomeNavigationFragmentType type)
        {
            var tag = type.ToString();
            var fragmentTransaction = SupportFragmentManager.BeginTransaction();

            if (!(SupportFragmentManager.FindFragmentByTag(tag) is NavigationFragment navigationFragment))
            {
                navigationFragment = NavigationFragment.NewInstance(CreateRootFragment(type));
                fragmentTransaction.Add(Resource.Id.ContentFrameLayout, navigationFragment, tag);
            }
            else
            {
                fragmentTransaction.Attach(navigationFragment);
            }

            var curFrag = SupportFragmentManager.PrimaryNavigationFragment;
            if (curFrag != null && curFrag != navigationFragment)
            {
                fragmentTransaction.Detach(curFrag);
            }

            fragmentTransaction.SetPrimaryNavigationFragment(navigationFragment);
            fragmentTransaction.SetReorderingAllowed(true);
            fragmentTransaction.CommitNow();
        }

        private NavigationChildFragment CreateRootFragment(HomeNavigationFragmentType type)
        {
            switch (type)
            {
                case HomeNavigationFragmentType.Books:
                    return BooksFragment.NewInstance();
                case HomeNavigationFragmentType.Characters:
                    return CharactersFragment.NewInstance();
                case HomeNavigationFragmentType.Houses:
                    return HousesFragment.NewInstance();
                default:
                    return null;
            }
        }
    }
}

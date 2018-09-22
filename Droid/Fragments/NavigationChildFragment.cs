using Android.Views;
using GameOfThrones.Droid.Views;

namespace GameOfThrones.Droid.Fragments
{
    public class NavigationChildFragment : Android.Support.V4.App.Fragment
    {
        public INavigationParent NavigationParent => (Activity as INavigationParent) ?? (ParentFragment as INavigationParent);

        public string Title { get; protected set; }

        public int? MenuId { get; protected set; }

        public bool ShouldHideNavigationBar { get; protected set; }

        public bool ShouldDisplayBackButton => NavigationParent.NumberOfChildren > 1;

        public override void OnResume()
        {
            base.OnResume();

            ConfigureToolbar();
        }

        protected void ConfigureToolbar()
        {
            if (ShouldHideNavigationBar)
            {
                NavigationParent.Toolbar.Visibility = ViewStates.Gone;

                return;
            }
            else
            {
                NavigationParent.Toolbar.Visibility = ViewStates.Visible;
            }

            NavigationParent.Toolbar.Title = Title;

            if (MenuId.HasValue)
            {
                NavigationParent.Toolbar.Menu.Clear();
                NavigationParent.Toolbar.InflateMenu(MenuId.Value);
            }
            else
            {
                NavigationParent.Toolbar.Menu.Clear();
            }

            if (ShouldDisplayBackButton)
            {
                NavigationParent.Toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_material);
            }
            else
            {
                NavigationParent.Toolbar.NavigationIcon = null;
            }
        }
    }
}

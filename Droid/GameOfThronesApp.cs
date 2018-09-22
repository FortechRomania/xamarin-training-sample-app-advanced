using System;
using Android.App;
using Android.Runtime;

namespace GameOfThrones.Droid
{
    [Application(AllowBackup = false)]
    public class GameOfThronesApp : Application
    {
        public GameOfThronesApp(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            DependencyLocator.Current.RegisterDependencies();
        }
    }
}

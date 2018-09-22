// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GameOfThrones.iOS.Views
{
    [Register ("HouseTableViewCell")]
    partial class HouseTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CoatOfArmsLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WordsLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CoatOfArmsLabel != null) {
                CoatOfArmsLabel.Dispose ();
                CoatOfArmsLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (WordsLabel != null) {
                WordsLabel.Dispose ();
                WordsLabel = null;
            }
        }
    }
}
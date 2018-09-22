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
    [Register ("CharacterTableViewCell")]
    partial class CharacterTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BornInformationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PlayedByLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BornInformationLabel != null) {
                BornInformationLabel.Dispose ();
                BornInformationLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (PlayedByLabel != null) {
                PlayedByLabel.Dispose ();
                PlayedByLabel = null;
            }
        }
    }
}
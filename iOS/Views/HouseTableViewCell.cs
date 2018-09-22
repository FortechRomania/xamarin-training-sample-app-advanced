using Foundation;
using GameOfThrones.ViewModels;
using System;
using UIKit;

namespace GameOfThrones.iOS.Views
{
    public partial class HouseTableViewCell : UITableViewCell
    {
        public HouseTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void Bind(HouseCellViewModel viewModel)
        {
            NameLabel.Text = viewModel.Name;
            WordsLabel.Text = viewModel.Words;
            CoatOfArmsLabel.Text = viewModel.CoatOfArms;
        }
    }
}
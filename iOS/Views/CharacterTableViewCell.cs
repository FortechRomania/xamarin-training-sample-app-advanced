using Foundation;
using GameOfThrones.ViewModels;
using System;
using UIKit;

namespace GameOfThrones.iOS.Views
{
    public partial class CharacterTableViewCell : UITableViewCell
    {
        public CharacterTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void Bind(CharacterCellViewModel viewModel)
        {
            NameLabel.Text = viewModel.NameAndNickname;
            BornInformationLabel.Text = viewModel.BornInformation;
            PlayedByLabel.Text = viewModel.PlayedBy;
        }
    }
}
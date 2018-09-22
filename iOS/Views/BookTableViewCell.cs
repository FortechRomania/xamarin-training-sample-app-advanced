using Foundation;
using GameOfThrones.ViewModels;
using System;
using UIKit;

namespace GameOfThrones.iOS.Views
{
    public partial class BookTableViewCell : UITableViewCell
    {
        public BookTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void Bind(BookCellViewModel viewModel)
        {
            NameLabel.Text = viewModel.Name;
            AuthorsLabel.Text = viewModel.Authors;
            NumberOfPagesLabel.Text = viewModel.NumberOfPages;
            ReleaseDateLabel.Text = viewModel.ReleaseDate;
        }
    }
}
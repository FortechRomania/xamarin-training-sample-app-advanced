using System;
using System.Collections.Generic;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using GameOfThrones.iOS.DataSources;
using GameOfThrones.iOS.Views;
using GameOfThrones.ViewModels;
using UIKit;

namespace GameOfThrones.iOS.ViewControllers
{
    public partial class BooksTableViewController : UITableViewController
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        private BooksViewModel _viewModel;

        public BooksTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _viewModel = DependencyLocator.Current.CreateBooksViewModel();
            _viewModel.ViewDidLoad();

            TableView.Source = new ObservingPlainTableViewSource<BookCellViewModel>(_viewModel.Books, BindBookTableCellView, nameof(BookTableViewCell));

            SetBindings();
        }

        private void SetBindings()
        {
            _bindings.Add(this.SetBinding(
                () => _viewModel.Title,
                () => Title
            ));
        }

        private void BindBookTableCellView(UITableViewCell cell, BookCellViewModel viewModel, NSIndexPath indexPath)
        {
            (cell as BookTableViewCell).Bind(viewModel);
        }
    }
}
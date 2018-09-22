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
    public partial class CharactersTableViewController : UITableViewController
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        private CharactersViewModel _viewModel;

        public CharactersTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _viewModel = DependencyLocator.Current.CreateCharactersViewModel();
            _viewModel.ViewDidLoad();

            var source = new ObservingPlainTableViewSource<CharacterCellViewModel>(_viewModel.Characters, BindCharacterTableViewCell, nameof(CharacterTableViewCell));
            source.WillDisplayCellDelegate = WillDisplayCell;
            TableView.Source = source;

            SetBindings();
        }

        private void SetBindings()
        {
            _bindings.Add(this.SetBinding(
                () => _viewModel.Title,
                () => Title
            ));
        }

        private void WillDisplayCell(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            _viewModel.WillDisplayCellAtIndex(indexPath.Row);
        }

        private void BindCharacterTableViewCell(UITableViewCell cell, CharacterCellViewModel viewModel, NSIndexPath indexPath)
        {
            (cell as CharacterTableViewCell).Bind(viewModel);
        }
    }
}
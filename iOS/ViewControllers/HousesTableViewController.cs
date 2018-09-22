using Foundation;
using GalaSoft.MvvmLight.Helpers;
using GameOfThrones.iOS.DataSources;
using GameOfThrones.iOS.Views;
using GameOfThrones.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;

namespace GameOfThrones.iOS.ViewControllers
{
    public partial class HousesTableViewController : UITableViewController
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        private HousesViewModel _viewModel;

        public HousesTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _viewModel = DependencyLocator.Current.CreateHousesViewModel();
            _viewModel.ViewDidLoad();

            var source = new ObservingPlainTableViewSource<HouseCellViewModel>(_viewModel.Houses, BindHouseTableViewCell, nameof(HouseTableViewCell));
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

        private void BindHouseTableViewCell(UITableViewCell cell, HouseCellViewModel viewModel, NSIndexPath indexPath)
        {
            (cell as HouseTableViewCell).Bind(viewModel);
        }
    }
}
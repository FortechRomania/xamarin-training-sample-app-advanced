using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using UIKit;

namespace GameOfThrones.iOS.DataSources
{
    public class ObservingPlainTableViewSource<TElement> : UITableViewSource, INotifyPropertyChanged
    {
        [Weak] private UITableView _tableView;

        private ObservableCollection<TElement> _dataSource = new ObservableCollection<TElement>();

        private TElement _selectedItem;

        public ObservingPlainTableViewSource(ObservableCollection<TElement> dataSource, Action<UITableViewCell, TElement, NSIndexPath> bindCellDelegate, string cellReuseIdentifier = "")
        {
            DataSource = dataSource;
            BindCellDelegate = bindCellDelegate;
            CellReuseIdentifier = cellReuseIdentifier;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TElement> DataSource
        {
            get => _dataSource;
            set
            {
                if (_dataSource != null)
                {
                    _dataSource.CollectionChanged -= HandleCollectionChanged;
                }

                _dataSource = value;

                if (_dataSource != null)
                {
                    _dataSource.CollectionChanged += HandleCollectionChanged;
                }

                TableView?.ReloadData();
            }
        }

        public UITableViewRowAnimation AddOrRemoveAnimation { get; set; } = UITableViewRowAnimation.None;

        public UITableViewRowAnimation UpdateAnimation { get; set; } = UITableViewRowAnimation.None;

        public Action<UITableViewCell, TElement, NSIndexPath> BindCellDelegate { get; set; }

        public string CellReuseIdentifier { get; set; }

        public Func<NSIndexPath, string> CellIdentifierDelegate { get; set; }

        public Action<UIScrollView> DraggingStartedDelegate { get; set; }

        public Action<UIScrollView> ScrolledDelegate { get; set; }

        public Func<UITableView, NSIndexPath, nfloat> HeightForRowDelegate { get; set; }

        public Action<UITableView, UITableViewCell, NSIndexPath> WillDisplayCellDelegate { get; set; }

        public nfloat? EstimatedRowHeight { get; set; }

        public TElement SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value))
                {
                    return;
                }

                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShouldClearSelectionInstantly { get; set; } = true;

        private UITableView TableView
        {
            get
            {
                return _tableView;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cellReuseIdentifier = CellIdentifierDelegate?.Invoke(indexPath) ?? CellReuseIdentifier;

            var cell = tableView.DequeueReusableCell(cellReuseIdentifier);

            BindCellDelegate(cell, DataSource[indexPath.Row], indexPath);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            _tableView = tableview;

            return DataSource?.Count ?? 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            SelectedItem = DataSource[indexPath.Row];

            if (ShouldClearSelectionInstantly)
            {
                SelectedItem = default(TElement);
                tableView.DeselectRow(indexPath, true);
            }
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            SelectedItem = default(TElement);
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return HeightForRowDelegate?.Invoke(tableView, indexPath) ?? UITableView.AutomaticDimension;
        }

        [Export("tableView:estimatedHeightForRowAtIndexPath:")]
        public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return HeightForRowDelegate?.Invoke(tableView, indexPath) ?? EstimatedRowHeight ?? UITableView.AutomaticDimension;
        }

        [Export("scrollViewDidScroll:")]
        public override void Scrolled(UIScrollView scrollView)
        {
            ScrolledDelegate?.Invoke(scrollView);
        }

        [Export("scrollViewWillBeginDragging:")]
        public override void DraggingStarted(UIScrollView scrollView)
        {
            DraggingStartedDelegate?.Invoke(scrollView);
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            WillDisplayCellDelegate?.Invoke(tableView, cell, indexPath);
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var count = e.NewItems.Count;
                        var paths = new NSIndexPath[count];

                        for (var i = 0; i < count; i++)
                        {
                            paths[i] = NSIndexPath.FromRowSection(e.NewStartingIndex + i, 0);
                        }

                        TableView?.InsertRows(paths, AddOrRemoveAnimation);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        var count = e.OldItems.Count;
                        var paths = new NSIndexPath[count];

                        for (var i = 0; i < count; i++)
                        {
                            var index = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                            paths[i] = index;
                        }

                        TableView?.DeleteRows(paths, AddOrRemoveAnimation);
                    }

                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        var count = e.OldItems.Count;

                        for (var i = 0; i < count; i++)
                        {
                            var fromIndex = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                            var toIndex = NSIndexPath.FromRowSection(e.NewStartingIndex + i, 0);

                            TableView?.MoveRow(fromIndex, toIndex);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        var count = e.OldItems.Count;
                        var paths = new NSIndexPath[count];

                        for (var i = 0; i < count; i++)
                        {
                            var index = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                            paths[i] = index;
                        }

                        TableView?.ReloadRows(paths, UpdateAnimation);
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        TableView?.ReloadData();
                    }

                    break;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

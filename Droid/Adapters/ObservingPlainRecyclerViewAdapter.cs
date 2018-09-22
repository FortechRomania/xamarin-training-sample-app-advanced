using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Android.Support.V7.Widget;
using Android.Views;

namespace GameOfThrones.Droid.Adapters
{
    public class ObservingPlainRecyclerViewAdapter<TElement> : RecyclerView.Adapter, IItemSelectedListener<TElement>, INotifyPropertyChanged
    {
        private const int FooterViewType = 10;
        private ObservableCollection<TElement> _dataSource = new ObservableCollection<TElement>();
        private List<RecyclerView.ViewHolder> _viewHolders = new List<RecyclerView.ViewHolder>();
        private RecyclerView _owningRecyclerview;

        private TElement _selectedItem;

        public ObservingPlainRecyclerViewAdapter(
            ObservableCollection<TElement> dataSource,
            Func<ViewGroup, int, RecyclerView.ViewHolder> createViewHolderDelegate,
            Action<RecyclerView.ViewHolder, TElement, int> bindViewHolderDelegate)
        {
            DataSource = dataSource;
            CreateViewHolderDelegate = createViewHolderDelegate;
            BindViewHolderDelegate = bindViewHolderDelegate;
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
                    NotifyDataSetChanged();
                }
            }
        }

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

                if (ShouldClearSelectionInstantly)
                {
                    _selectedItem = default(TElement);
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ShouldClearSelectionInstantly { get; set; } = true;

        public Func<ViewGroup, int, RecyclerView.ViewHolder> CreateViewHolderDelegate { get; set; }

        public Action<RecyclerView.ViewHolder, TElement, int> BindViewHolderDelegate { get; set; }

        public Func<ViewGroup, int, RecyclerView.ViewHolder> CreateFooterDelegate { get; set; }

        public Action<RecyclerView.ViewHolder> BindFooterDelegate { get; set; }

        public Func<TElement, int> ItemViewTypeMapping { get; set; } = (any) => 0;

        public bool HasFooter => CreateFooterDelegate != null && BindFooterDelegate != null;

        public override int ItemCount => HasFooter ? _dataSource.Count + 1 : _dataSource.Count;

        public override int GetItemViewType(int position)
        {
            return position >= _dataSource.Count ? FooterViewType : ItemViewTypeMapping.Invoke(_dataSource[position]);
        }

        public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
        {
            base.OnAttachedToRecyclerView(recyclerView);

            _owningRecyclerview = recyclerView;
        }

        public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
        {
            base.OnDetachedFromRecyclerView(recyclerView);

            _owningRecyclerview = null;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder viewHolder;

            if (viewType == FooterViewType)
            {
                viewHolder = CreateFooterDelegate?.Invoke(parent, viewType);
            }
            else
            {
                viewHolder = CreateViewHolderDelegate.Invoke(parent, viewType);
            }

            if (viewHolder is ISelectableViewHolder<TElement> selectableViewHolder)
            {
                selectableViewHolder.ItemSelectedListener = this;
            }

            _viewHolders.Add(viewHolder);

            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (GetItemViewType(position) == FooterViewType)
            {
                BindFooterDelegate?.Invoke(holder);
            }
            else
            {
                BindViewHolderDelegate.Invoke(holder, _dataSource[position], position);
            }
        }

        public void OnItemSelected(TElement element)
        {
            SelectedItem = element;
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);

                        if (e.NewStartingIndex == FocusedPosition())
                        {
                            _owningRecyclerview.ScrollToPosition(e.NewStartingIndex);
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
                    }

                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        var count = e.OldItems.Count;

                        for (var i = 0; i < count; i++)
                        {
                            NotifyItemMoved(e.OldStartingIndex + i, e.NewStartingIndex + i);

                            if (e.OldStartingIndex == FocusedPosition())
                            {
                                _owningRecyclerview.ScrollToPosition(e.OldStartingIndex);
                            }
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        NotifyItemRangeChanged(e.OldStartingIndex, e.OldItems.Count);
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        NotifyDataSetChanged();
                    }

                    break;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int? FocusedPosition()
        {
            if (_owningRecyclerview?.GetLayoutManager() is LinearLayoutManager linearLayoutManager)
            {
                return linearLayoutManager.FindFirstVisibleItemPosition();
            }

            return null;
        }
    }
}

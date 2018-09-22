using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace GameOfThrones.Collections
{
    public class SmartObservableCollection<TElement> : ObservableCollection<TElement>
    {
        public SmartObservableCollection()
        {
        }

        public SmartObservableCollection(IEnumerable<TElement> collection) : base(collection)
        {
        }

        public SmartObservableCollection(List<TElement> list) : base(list)
        {
        }

        public void AddRange(IEnumerable<TElement> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            RaiseResetEvent();
        }

        public void UpdateExistingItems(IEnumerable<TElement> range)
        {
            foreach (var item in range)
            {
                var index = Items.IndexOf(item);

                if (index != -1)
                {
                    Items[index] = item;
                }
            }

            RaiseResetEvent();
        }

        public void Reset(IEnumerable<TElement> range)
        {
            Items.Clear();

            AddRange(range);
        }

        private void RaiseResetEvent()
        {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

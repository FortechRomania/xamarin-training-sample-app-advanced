using System.Collections.Generic;
using Android.Arch.Lifecycle;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using GalaSoft.MvvmLight.Helpers;
using GameOfThrones.ViewModels;
using GameOfThrones.Droid.Extensions;
using GameOfThrones.Droid.Adapters;
using GameOfThrones.Droid.ViewHolders;

using MvvmLightBinding = GalaSoft.MvvmLight.Helpers.Binding;

namespace GameOfThrones.Droid.Fragments
{
    public partial class BooksFragment : NavigationChildFragment
    {
        private readonly List<MvvmLightBinding> _bindings = new List<MvvmLightBinding>();

        private BooksViewModel _viewModel;

        private RecyclerView _recyclerView;

        public static BooksFragment NewInstance()
        {
            return new BooksFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewModel = ViewModelProviders.Of(this).Get(DependencyLocator.Current.CreateBooksViewModel);
            _viewModel.ViewDidLoad();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fragment_books, container, false);

            _recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.books_recyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            _recyclerView.SetAdapter(new ObservingPlainRecyclerViewAdapter<BookCellViewModel>(_viewModel.Books, CreateCellViewHolder, BindCellViewHolder));

            SetBindings();

            return rootView;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            _bindings.DetachAll();
        }

        private void SetBindings()
        {
            _bindings.Add(this.SetBinding(
                () => _viewModel.Title,
                () => Title
            ));
        }

        private RecyclerView.ViewHolder CreateCellViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cell_book, parent, false);
            return new BookViewHolder(view);
        }

        private void BindCellViewHolder(RecyclerView.ViewHolder viewHolder, BookCellViewModel viewModel, int position)
        {
            (viewHolder as BookViewHolder).Bind(_viewModel.Books[position]);
        }
    }
}

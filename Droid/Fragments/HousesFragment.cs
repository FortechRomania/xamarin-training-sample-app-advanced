using System.Collections.Generic;
using Android.Arch.Lifecycle;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using GameOfThrones.Droid.Adapters;
using GameOfThrones.Droid.Extensions;
using GameOfThrones.Droid.ViewHolders;
using GameOfThrones.ViewModels;

using MvvmLightBinding = GalaSoft.MvvmLight.Helpers.Binding;

namespace GameOfThrones.Droid.Fragments
{
    public class HousesFragment : NavigationChildFragment
    {
        private readonly List<MvvmLightBinding> _bindings = new List<MvvmLightBinding>();

        private HousesViewModel _viewModel;

        private RecyclerView _recyclerView;

        public static HousesFragment NewInstance()
        {
            return new HousesFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewModel = ViewModelProviders.Of(this).Get(DependencyLocator.Current.CreateHousesViewModel);
            _viewModel.ViewDidLoad();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fragment_houses, container, false);

            _recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.houses_recyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            _recyclerView.SetAdapter(new ObservingPlainRecyclerViewAdapter<HouseCellViewModel>(_viewModel.Houses, CreateCellViewHolder, BindCellViewHolder));

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
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cell_house, parent, false);
            return new HouseViewHolder(view);
        }

        private void BindCellViewHolder(RecyclerView.ViewHolder viewHolder, HouseCellViewModel viewModel, int position)
        {
            (viewHolder as HouseViewHolder).Bind(_viewModel.Houses[position]);

            _viewModel.WillDisplayCellAtIndex(position);
        }
    }
}

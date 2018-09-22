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
    public class CharactersFragment : NavigationChildFragment
    {
        private readonly List<MvvmLightBinding> _bindings = new List<MvvmLightBinding>();

        private CharactersViewModel _viewModel;

        private RecyclerView _recyclerView;

        public static CharactersFragment NewInstance()
        {
            return new CharactersFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewModel = ViewModelProviders.Of(this).Get(DependencyLocator.Current.CreateCharactersViewModel);
            _viewModel.ViewDidLoad();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fragment_characters, container, false);

            _recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.characters_recyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            _recyclerView.SetAdapter(new ObservingPlainRecyclerViewAdapter<CharacterCellViewModel>(_viewModel.Characters, CreateCellViewHolder, BindCellViewHolder));

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
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cell_character, parent, false);
            return new CharacterViewHolder(view);
        }

        private void BindCellViewHolder(RecyclerView.ViewHolder viewHolder, CharacterCellViewModel viewModel, int position)
        {
            (viewHolder as CharacterViewHolder).Bind(_viewModel.Characters[position]);

            _viewModel.WillDisplayCellAtIndex(position);
        }
    }
}

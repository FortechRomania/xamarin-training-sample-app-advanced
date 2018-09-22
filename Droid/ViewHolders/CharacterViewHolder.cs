using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GameOfThrones.ViewModels;

namespace GameOfThrones.Droid.ViewHolders
{
    public class CharacterViewHolder : RecyclerView.ViewHolder
    {
        private TextView _nameTextView;
        private TextView _bornTextView;
        private TextView _playedByTextView;

        public CharacterViewHolder(View view) : base(view)
        {
            _nameTextView = view.FindViewById<TextView>(Resource.Id.character_name_textView);
            _bornTextView = view.FindViewById<TextView>(Resource.Id.character_born_textView);
            _playedByTextView = view.FindViewById<TextView>(Resource.Id.character_playedBy_textView);
        }

        public void Bind(CharacterCellViewModel viewModel)
        {
            _nameTextView.Text = viewModel.NameAndNickname;
            _bornTextView.Text = viewModel.BornInformation;
            _playedByTextView.Text = viewModel.PlayedBy;
        }
    }
}

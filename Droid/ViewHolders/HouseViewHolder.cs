using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GameOfThrones.ViewModels;

namespace GameOfThrones.Droid.ViewHolders
{
    public class HouseViewHolder : RecyclerView.ViewHolder
    {
        private TextView _nameTextView;
        private TextView _wordsTextView;
        private TextView _coatOfArmsTextView;

        public HouseViewHolder(View view) : base(view)
        {
            _nameTextView = view.FindViewById<TextView>(Resource.Id.house_name_textView);
            _wordsTextView = view.FindViewById<TextView>(Resource.Id.house_words_textView);
            _coatOfArmsTextView = view.FindViewById<TextView>(Resource.Id.house_coat_of_arms_textView);
        }

        public void Bind(HouseCellViewModel viewModel)
        {
            _nameTextView.Text = viewModel.Name;
            _wordsTextView.Text = viewModel.Words;
            _coatOfArmsTextView.Text = viewModel.CoatOfArms;
        }
    }
}

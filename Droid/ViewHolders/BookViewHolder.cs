using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GameOfThrones.ViewModels;

namespace GameOfThrones.Droid.ViewHolders
{
    public class BookViewHolder : RecyclerView.ViewHolder
    {
        private TextView _nameTextView;
        private TextView _authorsTextView;
        private TextView _pagesTextView;
        private TextView _releaseDateTextView;

        public BookViewHolder(View view) : base(view)
        {
            _nameTextView = view.FindViewById<TextView>(Resource.Id.book_name_textView);
            _authorsTextView = view.FindViewById<TextView>(Resource.Id.book_authors_textView);
            _pagesTextView = view.FindViewById<TextView>(Resource.Id.book_pages_textView);
            _releaseDateTextView = view.FindViewById<TextView>(Resource.Id.book_releaseDate_textView);
        }

        public void Bind(BookCellViewModel viewModel)
        {
            _nameTextView.Text = viewModel.Name;
            _authorsTextView.Text = viewModel.Authors;
            _pagesTextView.Text = viewModel.NumberOfPages;
            _releaseDateTextView.Text = viewModel.ReleaseDate;
        }
    }
}

namespace GameOfThrones.Droid.Adapters
{
    public interface ISelectableViewHolder<TElement>
    {
        IItemSelectedListener<TElement> ItemSelectedListener { get; set; }
    }
}

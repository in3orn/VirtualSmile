namespace Common
{
    public interface IDynamicItem<in TData>
    {
        void Init(TData data);
    }
}
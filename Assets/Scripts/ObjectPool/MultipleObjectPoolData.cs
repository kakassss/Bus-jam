public struct MultipleObjectPoolData<T>
{
    public int ID;
    public T Prefab;
    public MultipleObjectPoolData(T prefab, int id)
    {
        Prefab = prefab;
        ID = id;
    }
}
namespace GarbageManager.Model.Result.Interfaces
{
    public interface IResultWithData<T> : IResult
    {
        T GetData { get; }
    }
}

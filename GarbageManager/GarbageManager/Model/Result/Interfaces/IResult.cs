namespace GarbageManager.Model.Result.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }

        ResultStatus Status { get; }

        string Message { get; set; }
    }
}

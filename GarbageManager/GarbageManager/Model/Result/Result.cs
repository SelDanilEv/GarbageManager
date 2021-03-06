using GarbageManager.Model.Result.Interfaces;
using System.Linq;

namespace GarbageManager.Model.Result
{
    public class Result<T> : IResultWithData<T>
    {
        public Result()
        {
            Status = ResultStatus.Success;
        }

        public Result(T data) : this()
        {
            Data = data;
        }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public T Data { private get; set; }

        public T GetData => Data;

        public bool IsSuccess => Status == ResultStatus.Success;

        public static Result<T> SuccessResult() => new Result<T>() { Status = ResultStatus.Success };
        public static Result<T> WarningResult() => new Result<T>() { Status = ResultStatus.Warning };
        public static Result<T> ErrorResult() => new Result<T>() { Status = ResultStatus.Error };

        public static Result<T> SuccessResult(T data) => new Result<T>() { Status = ResultStatus.Success, Data = data };

        public static Result<T> ErrorResult(string errMessage) => new Result<T>() { Status = ResultStatus.Error, Message = errMessage };

        public Result<T> BuildMessage(params string[] messages)
        {
            var template = messages[0];
            messages = messages.Skip(1).ToArray();
            Message = string.Format(template, messages);
            return this;
        }
    }

    public class Result : Result<string>
    {

    }
}

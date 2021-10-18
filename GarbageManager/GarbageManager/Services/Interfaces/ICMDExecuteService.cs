using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface ICMDExecuteService
    {
        IResult ExecuteCMD(string executeString);
    }
}

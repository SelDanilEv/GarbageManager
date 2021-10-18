using GarbageManager.Consts;
using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace GarbageManager.Services
{
    class CMDExecuteService : ICMDExecuteService
    {
        public IResult ExecuteCMD(string executeString)
        {
            try
            {
                Process.Start(executeString);
            }
            catch (Exception e)
            {
                return Result.ErrorResult(e.Message);
            }

            return Result.SuccessResult();
        }
    }
}

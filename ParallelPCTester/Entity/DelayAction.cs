using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal class DelayAction : IAction
{
    public static int[] CheatTimes = [
        3000    //存证时升降对准
        ,1500   //存证时无条件下沉
        ];
    private int delayms;
    public string Description => $"阻塞延时等待{delayms}毫秒";

    public DelayAction(int ms)
    {
        this.delayms = ms;
    }

    public async Task<(ErrorType Error, bool Finished)> PoolOnce()
    {
        await Task.Delay(delayms);
        return (ErrorType.NoError, true);
    }

    public async Task<ErrorType> PreExecute()
    {
        return ErrorType.NoError;
    }

    public void SetEnvironment(ActionEnvironment env)
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal class DelayAction : IAction
{
    public static int[] CheatTimes = [
        5    //0.存证时升降对准
        ,5   //1.存证时无条件下沉
        ,5   //2.取证时半开、小旋转、下沉三合一动作
        ,5    //3.存证或取证时丝杆抽出格口
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

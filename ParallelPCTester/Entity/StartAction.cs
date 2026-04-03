using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal class StartAction : IAction
{
    public string Description => "任务开始";

    public async Task<(ErrorType Error, bool Finished)> PoolOnce()
    {
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

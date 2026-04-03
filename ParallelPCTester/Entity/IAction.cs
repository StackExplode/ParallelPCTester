using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal interface IAction
{
    public string Description { get; }
    public void SetEnvironment(ActionEnvironment env);
    public Task<ErrorType> PreExecute();
    public Task<(ErrorType Error, bool Finished)> PoolOnce();
    public virtual async Task<ErrorType> PostExecute() { return ErrorType.NoError; }
}

using ParallelPCTester.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParallelPCTester.Entity;
using static ParallelPCTester.BLL.MotorParamBuilder;

namespace ParallelPCTester.App;
internal class BatchStoreAction_Cheat : ActionBuilder
{
    public BatchStoreAction_Cheat(int row, int col, bool iscard, bool istop) : base(row, col, iscard, istop) 
    { }

    protected override void BuildActions()
    {
        throw new NotImplementedException();

    }
}

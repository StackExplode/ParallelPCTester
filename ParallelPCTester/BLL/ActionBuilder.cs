using ParallelPCTester.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.BLL;
internal abstract class ActionBuilder
{
    private DAGNode root;
    public DAGNode Root => root;
    protected abstract void BuildActions();
    protected ushort row, col;
    protected bool iscard, istop;

    public ActionBuilder(int row, int col, bool iscard, bool istop)
    {
        this.row = (ushort)row;
        this.col = (ushort)col;
        this.iscard = iscard;
        this.istop = istop;
        root = new DAGNode(new StartAction());
        this.BuildActions();
    }


}

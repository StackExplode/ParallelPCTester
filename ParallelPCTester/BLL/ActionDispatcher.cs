using ParallelPCTester.Driver;
using ParallelPCTester.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.BLL;
internal class ActionDispatcher
{
    private DAGNode root;
    private ActionEnvironment env;
    private LinkedList<DAGNode> runningQueue = new LinkedList<DAGNode>();
    private LinkedListNode<DAGNode> Current;

    public ActionDispatcher(DAGNode root, ActionEnvironment env)
    {
        this.root = root;
        this.env = env;
        runningQueue.AddLast(root);
        Current = runningQueue.First!;
    }


    public async Task<bool> Run()
    {
        //Thread.CurrentThread.Priority = ThreadPriority.Highest;
        while (runningQueue.Count > 0)
        {
            var action = Current.Value.Action;
            
            var rst = await action.PoolOnce();
            if (rst.Error != ErrorType.NoError)
            {
                Logger.PrintResult(rst.Error, "", $"执行{action.Description}过程出错！");
                return false;
            }
            var last = Current;
            if (rst.Finished)
            {
                Logger.AppendLine();
                Logger.AppendLine($"执行{action.Description}过程完毕！", "green");
                await action.PostExecute();
                last.Value.Finish();
                foreach (var child in last.Value.GetReadyChildren())
                {
                    Logger.AppendLine();
                    Logger.AppendLine($"开始执行任务：{child.Action.Description}", "blue");
                    child.Action.SetEnvironment(env);
                    var rst2 = await child.Action.PreExecute();
                    if (rst2 != ErrorType.NoError)
                    {
                        Logger.PrintResult(rst2, "", $"准备执行{child.Action.Description}过程出错！");
                        return false;
                    }
                    runningQueue.AddLast(child);
                }
                runningQueue.Remove(last);
            }
            Current = Current.Next ?? runningQueue.First!;
            await Task.Delay(env.QuerySpan);
        }

        return true;
    }

}

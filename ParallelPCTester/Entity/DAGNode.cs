using ParallelPCTester.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal class DAGNode
{
    private int remainingDependencies = 0;
    public string Name { get; set; }
    public IAction @Action { get; set; }
    public List<DAGNode> Children { get; set; } = new List<DAGNode>();
    public bool IsReadyToGo => remainingDependencies == 0;

    public void AddDependencies(params DAGNode[] dependencies)
    {
        foreach (var dep in dependencies)
        {
            dep.AddChildren(this);
        }
    }

    public void AddChildren(params DAGNode[] children)
    {
        foreach (var child in children)
        {
            Children.Add(child);
            child.remainingDependencies++;
        }
    }

    public void AddChildren(params IAction[] actions)
    {
        foreach (var action in actions)
        {
            var child = new DAGNode(action);
            Children.Add(child);
            child.remainingDependencies++;
        }
    }

    public void AddChildren(params (string name, IAction act)[] children)
    {
        foreach (var (name, act) in children)
        {
            var child = new DAGNode(name, act);
            Children.Add(child);
            child.remainingDependencies++;
        }
    }

    public void Finish()
    {
        foreach (var child in Children)
        {
            child.remainingDependencies--;
        }
    }

    public IEnumerable<DAGNode> GetReadyChildren()
    {
        foreach (var child in Children)
        {
            if (child.IsReadyToGo)
                yield return child;
        }
    }


    public DAGNode(string name, IAction action)
    {
        Name = name;
        Action = action;
    }

    public DAGNode(IAction action)
    {
        Name = action.Description;
        Action = action;
    }

    public DAGNode(string name, MotorActionParam param)
    {
        Name = name;
        Action = new MotorAction(param);
    }

    public DAGNode(MotorActionParam param)
    {
        Action = new MotorAction(param);
        Name = Action.Description;    
    }

}

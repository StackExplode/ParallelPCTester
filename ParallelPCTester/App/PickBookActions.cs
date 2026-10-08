using ParallelPCTester.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParallelPCTester.Entity;
using static ParallelPCTester.BLL.MotorParamBuilder;

namespace ParallelPCTester.App;
internal class PickBookActions : ActionBuilder
{
    public PickBookActions(int row, int col, bool iscard, bool istop) : base(row, col, iscard, istop)
    {
    }
    protected override void BuildActions()
    {
        var step1 = new DAGNode(MoveTo(MotorAddress.BigRotate, col, iscard, istop));
        var step2 = new DAGNode(LiftFastMove(row, iscard, istop));
        var step3 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.LaserPos));
        Root.AddChildren(step1, step3, step2);

        var step4 = new DAGNode(LiftAim(row, iscard,istop));
        step4.AddDependencies(step1, step2, step3);

        var step5 = new DAGNode(LiftSink(iscard));
        var step6 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.CasePos));
        var step7 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.HalfOpen));
        step4.AddChildren(step6, step5, step7);

        var step8 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.BookStore));
        step8.AddDependencies(step5, step6, step7);

        var step9 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.Close));
        step9.AddDependencies(step8);

        var step10 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.FullBack));
        step10.AddDependencies(step9);

        var step11 = new DAGNode(MoveToSpec(MotorAddress.Lift,LiftSpecPosType.Exit));
        var step12 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.DoorPos));
        step10.AddChildren(step12, step11);
        
        var step13 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead ,ScrewLeadPosType.BookExit));
        step13.AddDependencies(step11, step12);

        var step14 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.BigOpen));
        step14.AddDependencies(step13);

        var step15 = new DAGNode(Roll(MotorAddress.Roll, RollType.Out));
        step15.AddDependencies(step14);

        var step16 = new DAGNode(MoveToSpec(MotorAddress.Lift, LiftSpecPosType.BookEntrance));
        var step17 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.BookEntrance));
        step15.AddChildren(step16, step17);
    }
}

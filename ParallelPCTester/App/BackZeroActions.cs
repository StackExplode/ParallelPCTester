using ParallelPCTester.BLL;
using ParallelPCTester.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ParallelPCTester.BLL.MotorParamBuilder;

namespace ParallelPCTester.App;
internal class BackZeroActions : ActionBuilder
{
    public BackZeroActions(int row, int col, bool iscard, bool istop) : base(row, col, iscard, istop)
    {
        
    }

    protected override void BuildActions()
    {
        var step1 = new DAGNode(BackZero(MotorAddress.ScrewLead));
        Root.AddChildren(step1);

        var step2 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.BigOpen));
        step1.AddChildren(step2);

        var step3 = new DAGNode(BackZero(MotorAddress.SmallRotate));
        step2.AddChildren(step3);

        var step4 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.DoorPos));
        step3.AddChildren(step4);

        var step5 = new DAGNode(BackZero(MotorAddress.SmallRotate));
        step4.AddChildren(step5);

        var step6 = new DAGNode(BackZero(MotorAddress.Lift));
        step5.AddChildren(step6);

        var step7 = new DAGNode(BackZero(MotorAddress.BigRotate));
        step6.AddChildren(step7);

        var step8 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.DoorPos));
        step7.AddChildren(step8);

        var step9 = new DAGNode(MoveToSpec(MotorAddress.Lift,LiftSpecPosType.BookEntrance));
        step8.AddChildren(step9);

        var step10 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.BookEntrance));
        step9.AddChildren(step10);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelPCTester.BLL;
using ParallelPCTester.Entity;
using static ParallelPCTester.BLL.MotorParamBuilder;

namespace ParallelPCTester.App;
internal class StoreCardActions_Cheat : ActionBuilder
{
    public StoreCardActions_Cheat(int row, int col, bool iscard, bool istop) : base(row, col, iscard, istop)
    {
    }
    protected override void BuildActions()
    {
        var step1 = new DAGNode(MoveTo(MotorAddress.BigRotate, col, iscard, istop));
        
        var step0_1 = new DAGNode(MoveToSpec(MotorAddress.Lift, LiftSpecPosType.CardEntrance));
        var step0_2 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.CardEntrance));

        Root.AddChildren(step0_1, step0_2, step1);

        var step2 = new DAGNode(Roll(MotorAddress.Roll, RollType.CardIn));
        step2.AddDependencies(step0_1, step0_2);

        var step3 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.Close));
        step3.AddDependencies(step2);

        var step4 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.FullBack));
        step4.AddDependencies(step3);

        var step5 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.LaserPos));
        var step6 = new DAGNode(LiftFastMove(row, iscard, istop));
        step4.AddChildren(step5, step6);

        var step7 = new DAGNode(LiftAim(row, iscard, istop));
        step7.AddDependencies(step1, step5, step6);

        var step_cheat1 = new DAGNode(new DelayAction(DelayAction.CheatTimes[0]));
        step_cheat1.AddDependencies(step1, step5, step6);

        var step8 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.CasePos));
        step8.AddDependencies(step_cheat1);

        var step9 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.CardStore));
        step9.AddDependencies(step8);

        var step10 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.MicroOpen));
        step10.AddDependencies(step9);

        var step_cheat2 = new DAGNode(new DelayAction(DelayAction.CheatTimes[1]));
        step_cheat2.AddDependencies(step10);

        var step11 = new DAGNode(LiftSink(iscard));
        step11.AddDependencies(step10);

        var step12 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.FullBack));
        step12.AddDependencies(step_cheat2);

        var step_cheat3 = new DAGNode(new DelayAction(DelayAction.CheatTimes[3]));
        step_cheat3.AddDependencies(step_cheat2);

        var step13 = new DAGNode(MoveToSpec(MotorAddress.SmallRotate, SmallRoatePosType.DoorPos));
        var step14 = new DAGNode(MoveToSpec(MotorAddress.Lift, LiftSpecPosType.BookEntrance));
        var step15 = new DAGNode(ClawAction(MotorAddress.Claw, ClawActionType.BigOpen));
        step_cheat3.AddChildren(step13, step14, step15);

        var step16 = new DAGNode(MoveToSpec(MotorAddress.ScrewLead, ScrewLeadPosType.BookEntrance));
        step16.AddDependencies(step15);
    }
}

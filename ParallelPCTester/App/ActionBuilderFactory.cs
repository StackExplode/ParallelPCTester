using ParallelPCTester.BLL;
using ParallelPCTester.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.App;
internal static class ActionBuilderFactory
{
    public static ActionBuilder CreateActionBuilder(MissionType type, int row, int col)
    {
        bool iscard = type.HasFlag(MissionType.IsCard);
        bool istop = type.HasFlag(MissionType.IsTop);
        bool ispick = type.HasFlag(MissionType.IsPick);
        bool isbackzero = type.HasFlag(MissionType.IsBackZero);

        bool ischeat = type.HasFlag(MissionType.IsCheat);

        switch ((isbackzero, ispick, iscard))
        {
            case (true, _, _):
                return new BackZeroActions(row, col, iscard, istop);
            case (false, false, false):
                if(ischeat)
                    return new StoreBookActions_Cheat(row, col, iscard, istop);
                else
                    return new StoreBookActions(row, col, iscard, istop);
            case (false, true, false):
                if(ischeat)
                    return new PickBookActions_Cheat(row, col, iscard, istop);
                else
                    return new PickBookActions(row, col, iscard, istop);
            case (false, false, true):
                if (ischeat)
                    return new StoreCardActions_Cheat(row, col, iscard, istop);
                else
                    return new StoreCardActions(row, col, iscard, istop);
            case (false, true, true):
                if (ischeat)
                    return new PickCardActions_Cheat(row, col, iscard, istop);
                else
                    return new PickCardActions(row, col, iscard, istop);

            default:
                throw new NotImplementedException("The mission type is not implemented");
        }
    }
}

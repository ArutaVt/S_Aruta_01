using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Data
{
    public class Totalization
    {
        public enum SyukeiType
        {
            GAME,
            IN,
            OUT
        }

        public static void Reset()
        {
            ddmModeDatas = new ObservableCollection<DdmModeData>()
            {
                new DdmModeData { DdmStsName = "Nml", In = 0, Out = 0, Game = 0 },
                new DdmModeData { DdmStsName = "BnsWait", In = 0, Out = 0, Game = 0 },
                new DdmModeData { DdmStsName = "Bns", In = 0, Out = 0, Game = 0 },
                new DdmModeData { DdmStsName = "Rush", In = 0, Out = 0, Game = 0 },
                new DdmModeData { DdmStsName = "AtBns", In = 0, Out = 0, Game = 0 },
            };

            ddmModeMoves = new ObservableCollection<DdmModeMove>()
            {
                new DdmModeMove{DdmStsName = "Nml", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
                new DdmModeMove{DdmStsName = "BnsWait", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
                new DdmModeMove{DdmStsName = "Bns", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
                new DdmModeMove{DdmStsName = "Rush", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
                new DdmModeMove{DdmStsName = "AtBns", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
            };
        }

        public static ObservableCollection<DdmModeData> ddmModeDatas = new ObservableCollection<DdmModeData>()
        {
            new DdmModeData { DdmStsName = "Nml", In = 0, Out = 0, Game = 0 },
            new DdmModeData { DdmStsName = "BnsWait", In = 0, Out = 0, Game = 0 },
            new DdmModeData { DdmStsName = "Bns", In = 0, Out = 0, Game = 0 },
            new DdmModeData { DdmStsName = "Rush", In = 0, Out = 0, Game = 0 },
            new DdmModeData { DdmStsName = "AtBns", In = 0, Out = 0, Game = 0 },
        };

        public static ObservableCollection<DdmModeMove> ddmModeMoves = new ObservableCollection<DdmModeMove>()
        {
            new DdmModeMove{DdmStsName = "Nml", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
            new DdmModeMove{DdmStsName = "BnsWait", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
            new DdmModeMove{DdmStsName = "Bns", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
            new DdmModeMove{DdmStsName = "Rush", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
            new DdmModeMove{DdmStsName = "AtBns", Nml = 0, BnsWait = 0, Bns = 0, Rush = 0, AtBns = 0 },
        };

        public static void SyukeiDdmMove(DDMMODE startMode, DDMMODE endMode)
        {
            switch (endMode)
            {
                case DDMMODE.Nml:
                    ddmModeMoves[(int)startMode].Nml++;
                    break;
                case DDMMODE.Bns:
                    ddmModeMoves[(int)startMode].Bns++;
                    break;
                case DDMMODE.BnsWait:
                    ddmModeMoves[(int)startMode].BnsWait++;
                    break;
                case DDMMODE.Rush:
                    ddmModeMoves[(int)startMode].Rush++;
                    break;
                case DDMMODE.AtBns:
                    ddmModeMoves[(int)startMode].AtBns++;
                    break;
                default:
                    break;
            }
        }

    }

    public class DdmModeData
    {
        public string DdmStsName { get; set; }
        public UInt64 In { get; set; }
        public UInt64 Out { get; set; }
        public UInt64 Game { get; set; }
    }

    public class DdmModeMove
    {
        public string DdmStsName { get; set; }
        public UInt64 Nml { get; set; }
        public UInt64 BnsWait { get; set; }
        public UInt64 Bns { get; set; }
        public UInt64 Rush { get; set; }
        public UInt64 AtBns { get; set; }
    }

}

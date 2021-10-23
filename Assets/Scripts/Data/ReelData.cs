using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    class ReelData
    {
        public enum Zugara
        {
            WhiteSeven,
            RedSeven,
            Bar,
            Homura,
            Cherry,
            Suika,
            Replay,
            BellA,
            BellB,
            Blank,
            MAX
        }

        public static Zugara[] LeftReelArray = new Zugara[] { 
            Zugara.Suika,           // 1
            Zugara.BellA,           // 2
            Zugara.Replay,          // 3
            Zugara.WhiteSeven,      // 4
            Zugara.Suika,           // 5
            Zugara.BellA,           // 6
            Zugara.Replay,          // 7
            Zugara.Bar,             // 8
            Zugara.Cherry,          // 9
            Zugara.Suika,           // 10
            Zugara.BellA,           // 11
            Zugara.Replay,          // 12
            Zugara.Suika,           // 13
            Zugara.Homura,          // 14
            Zugara.Homura,          // 15
            Zugara.BellB,           // 16
            Zugara.Replay,          // 17
            Zugara.Suika,           // 18
            Zugara.RedSeven,        // 19
            Zugara.BellB,           // 20
            Zugara.Replay           // 21
        };

        public static Zugara[] CenterReelArray = new Zugara[] {
            Zugara.Cherry,          // 1
            Zugara.Replay,          // 2
            Zugara.Suika,           // 3
            Zugara.WhiteSeven,      // 4
            Zugara.BellB,           // 5
            Zugara.Bar,             // 6
            Zugara.Replay,          // 7
            Zugara.BellA,           // 8
            Zugara.Homura,          // 9
            Zugara.Blank,           // 10
            Zugara.Suika,           // 11
            Zugara.Replay,          // 12
            Zugara.BellA,           // 13
            Zugara.RedSeven,        // 14
            Zugara.Blank,           // 15
            Zugara.Suika,           // 16
            Zugara.Replay,          // 17
            Zugara.BellA,           // 18
            Zugara.Cherry,          // 19
            Zugara.Replay,          // 20
            Zugara.BellA            // 21
        };

        public static Zugara[] RightReelArray = new Zugara[] {
            Zugara.Suika,           // 1
            Zugara.Replay,          // 2
            Zugara.BellA,           // 3
            Zugara.WhiteSeven,      // 4
            Zugara.Suika,           // 5
            Zugara.Replay,          // 6
            Zugara.BellA,           // 7
            Zugara.Bar,             // 8
            Zugara.RedSeven,        // 9
            Zugara.BellB,           // 10
            Zugara.Replay,          // 11
            Zugara.BellA,           // 12
            Zugara.Blank,           // 13
            Zugara.Suika,           // 14
            Zugara.Replay,          // 15
            Zugara.BellA,           // 16
            Zugara.Homura,          // 17
            Zugara.Homura,          // 18
            Zugara.Replay,          // 19
            Zugara.BellA,           // 20
            Zugara.Blank            // 21
        };

        public static int getZugaraComa(ReelData.Zugara zugara, ReelController.Reel_ID reel, int stopComa)
        {
            int suberi = -1;
            ReelData.Zugara[] workArray;

            switch (reel)
            {
                case ReelController.Reel_ID.Left:
                    workArray = ReelData.LeftReelArray;
                    break;
                case ReelController.Reel_ID.Center:
                    workArray = ReelData.CenterReelArray;
                    break;
                case ReelController.Reel_ID.Right:
                    workArray = ReelData.RightReelArray;
                    break;
                default:
                    workArray = ReelData.LeftReelArray;
                    Debug.LogError("不正なリール指定です。");
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                int coma = stopComa - i;
                if (coma < 0) coma += Mn.leftReel.MaxComa;
                if (workArray[coma] == zugara)
                {
                    suberi = i;
                    break;
                }
            }

            return suberi;
        }
    }
}

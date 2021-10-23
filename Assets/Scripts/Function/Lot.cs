namespace Sim.Function
{
	class Lot
	{

		/// <summary>
		/// 条件装置抽せん
		/// </summary>
		public FRT_CODE LotFrtCode()
		{
			DdmMain.frtData.Lot();      // 条件装置抽せん
			FRT_CODE result = (FRT_CODE)FrtData.ResultFrtItem.lowResult;
			return result;
		}
		/// <summary>
		/// 通常時ボーナス抽せん
		/// </summary>
		public static HIT_NOT L1()
		{
			int Label = 0;
			int TableRow = (int)DdmVariable.Settei; // 設定
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (HIT_NOT)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 前兆G数抽せん
		/// </summary>
		public static ADD_GAME L2()
		{
			int Label = 1;
			int TableRow = 0;
			int TableCol = 0;
			int OffsetNum = 0;
			return (ADD_GAME)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 準備中RUSH抽せん
		/// </summary>
		public static HIT_NOT L3()
		{
			int Label = 2;
			int TableRow = (int)DdmVariable.Settei; // 設定
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (HIT_NOT)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// ボーナス中RUSH抽せん
		/// </summary>
		public static HIT_NOT L4()
		{
			int Label = 3;
			int TableRow = (int)DdmVariable.Settei; // 設定
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (HIT_NOT)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 初期継続ランク昇格抽せん
		/// </summary>
		public static RUSH_STS L5()
		{
			int Label = 4;
			int TableRow = 0;
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.AtMode;    // 継続率
			return (RUSH_STS)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// RUSH中継続ランク昇格抽せん
		/// </summary>
		public static RUSH_STS L6()
		{
			int Label = 5;
			int TableRow = 0;
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (RUSH_STS)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 継続ランクボーナス時RUSHストック抽せん
		/// </summary>
		public static HIT_NOT L7()
		{
			int Label = 6;
			int TableRow = 0;
			int TableCol = 0;
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (HIT_NOT)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 状態移行抽せん
		/// </summary>
		public static NML_STS L8()
		{
			int Label = 7;
			int TableRow = 0;
			int TableCol = (int)DdmVariable.NmlSts; // 状態
			int OffsetNum = (int)DdmVariable.FrtCode;   // 条件装置
			return (NML_STS)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

		/// <summary>
		/// 初期継続率抽せん
		/// </summary>
		public static RUSH_MODE L9()
		{
			int Label = 8;
			int TableRow = 0;
			int TableCol = 0;
			int OffsetNum = 0;
			return (RUSH_MODE)DdmMain.allDdmData.Lot(Label, TableRow, TableCol, OffsetNum);
		}

	}
}

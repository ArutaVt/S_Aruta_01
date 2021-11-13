using System;

namespace Sim
{
	public class DdmVariable
	{
		public static AutoMakeCode.Enum.Status startMnSts;
		public static Int16 YuuriGame; //	有利区間G数
		public static Int16 MYCounter; //	MYカウンター
		public static SETTEI Settei;   //	設定値
		public static FRT_CODE FrtCode;    //	条件装置
		public static DDMMODE DdmMode; //	出玉状態
		public static DDMMODE StartDdmMode;    //	開始時出玉状態
		public static DDMMODE EndDdmMode;  //	終了時出玉状態
		public static Int16 HighGame;  //	高確率G数
		public static Int16 BnsGame;   //	ボーナスゲーム数
		public static NML_STS NmlSts;  //	状態
		public static Int16 Stock; //	継続ストック
		public static RUSH_MODE AtMode;    //	継続率
		public static RUSH_STS AtSts;  //	継続ランク
		public static Int16 ZenchoGame;    //	前兆G数
		public static Int16 RushGame;  //	RushG数

		public static bool RushStartFlg;		// Rush開始フラグ
		public static FREEZE_TYPE FreezeType;   // フリーズタイプ
		public static bool FreezeFlg;           // フリーズ発生フラグ

		public static UInt64 SBIG;				// SBIG
		public static UInt64 ABIG;				// ABIG
		public static UInt64 RB;				// RB
		public static UInt64 In;				// In枚数
		public static UInt64 Out;				// Out枚数
		public static UInt64 BnsOut;			// Out枚数
		public static UInt64 TotalGames;		// 総ゲーム数
		public static UInt64 TotalNmlGames;		// 総通常ゲーム数
		public static UInt64 NmlGame;           // 通常消化G数
		public static UInt64 HaveCoin;          // 持ちコイン
		public static int BnsGet;				// ボーナス中獲得枚数

		public static int[,] deme = new int[3, 3];

		// リールデータ
		public static int[,] reel;
	}
	public enum DDMMODE
	{
		Nml = 0,    //	通常
		BnsWait = 1,    //	ボーナス準備
		Bns = 2,    //	ボーナス
		Rush = 3,   //	Rush
		AtBns = 4,  //	AT中ボーナス
	}
	public enum SETTEI
	{
		Settei1 = 0,    //	設定１
		Settei2 = 1,    //	設定２
		Settei3 = 2,    //	設定３
		Settei4 = 3,    //	設定４
		Settei5 = 4,    //	設定５
		Settei6 = 5,    //	設定６
	}
	public enum ADD_GAME
	{
		Add_0G = 0, //	0G
		ADD_1G = 1, //	1G
		ADD_3G = 3, //	3G
		ADD_5G = 5, //	5G
		ADD_7G = 7, //	7G
		ADD_9G = 9, //	9G
		ADD_12G = 12,   //	12G
		ADD_13G = 13,   //	13G
		ADD_14G = 14,   //	14G
		ADD_15G = 15,   //	15G
		ADD_16G = 16,   //	16G
	}
	public enum HIT_NOT
	{
		Not = 0,    //	非当せん
		Hit = 1,    //	当せん
	}
	public enum FRT_CODE
	{
		Chance = 0, //	チャンス目
		KyoChe = 1, //	強チェリー
		JakuChe = 2,    //	弱チェリー
		Suika = 3,  //	スイカ
		HomuraReplay = 4,   //	ほむら揃い
		RegReplay = 5,  //	REGリプレイ
		BigReplay = 6,  //	BIGリプレイ
		SBigReplay = 7, //	SBIGリプレイ
		Bell_123 = 8,   //	押し順ベル123
		Bell_132 = 9,   //	押し順ベル132
		Bell_213 = 10,  //	押し順ベル213
		Bell_231 = 11,  //	押し順ベル231
		Bell_321 = 12,  //	押し順ベル321
		Bell_312 = 13,  //	押し順ベル312
		Hazure = 14,    //	ハズレ
	}
	public enum NML_STS
	{
		Low = 0,    //	低確率
		Nml = 1,    //	通常
		High = 2,   //	高確率
	}
	public enum RUSH_STS
	{
		End = 0,    //	終了
		ReStart = 1,    //	継続
		Bonus = 2,  //	ボーナス
	}
	public enum RUSH_MODE
	{
		_50 = 0,    //	0.5
		_60 = 1,    //	0.6
		_70 = 2,    //	0.7
		_80 = 3,    //	0.8
		_90 = 4,    //	0.9
	}

	public enum FREEZE_TYPE
	{
		None,
		LongFreeze,
		RushStart,
	}
}

//
// ブリッジ歯式判定
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.BuiData
{
	/// <summary>
	/// ブリッジ歯式判定
	/// </summary>
	public static class BridgeChecker
    {
        // 定数 //////////////////////////////////////////////////////

        /// <summary>
        /// 歯式テーブル行番号種別
        /// </summary>
        private enum BuiTableLineType
        {
            /// <summary>永久歯</summary>
            Eikyushi = 0,
            /// <summary>乳歯</summary>
            Nyushi = 1,
            /// <summary>支台歯</summary>
            Shidaishi = 2,
            /// <summary>健全支台歯</summary>
            LiveShidaishi = 3,
            /// <summary>増歯</summary>
            Zoshi = 4,
            /// <summary>分割歯など</summary>
            Bunkatsushi = 5,
            /// <summary>欠損歯</summary>
            Kessonshi = 6,
            /// <summary>鈎破損</summary>
            Kohason = 7,
            /// <summary>隙</summary>
            Geki = 8,
            /// <summary>過剰歯</summary>
            Kajoshi = 9,
        };

        /// <summary>支台歯ﾏｰｸ</summary>
        private const char CHAR_SHIDAISHI = 'O';

        /// <summary>健全支台歯ﾏｰｸ</summary>
        private const char CHAR_LIVE_SHIDAISHI = '@';

        /// <summary>欠損歯ﾏｰｸ</summary>
        private const char CHAR_KESSONSHI = 'X';

        /// <summary>分割歯などのﾏｰｸ</summary>
        private const char CHAR_BUNKATSUSHI = '\'';

        /// <summary>隙ﾏｰｸ</summary>
        private const char CHAR_GEKI = '^';

        /// <summary>過剰歯ﾏｰｸ</summary>
        private const char CHAR_ZOSHI = 'V';

        /// <summary>部位 左顎</summary>
        private const int LEFT_GAKU_NUM = 1;

        /// <summary>部位 右顎</summary>
        private const int RIGHT_GAKU_NUM = (-1);

        /// <summary>部位 上顎</summary>
        private const int UPPER_GAKU_NUM = 0;

        /// <summary>部位 下顎</summary>
        private const int LOWER_GAKU_NUM = 1;

        /// <summary>属性 ダミー歯</summary>
        private const int ATTRIBUTE_DUMMY = 0;

        /// <summary>属性 ダミー歯(隙)</summary>
        private const int ATTRIBUTE_DUMMY_GEKI = 1;

        /// <summary>属性 支台歯</summary>
        private const int ATTRIBUTE_SHIDAISHI = 2;

        /// <summary>支台歯属性 端</summary>
        private const int AS_SIDE = 0;

        /// <summary>支台歯属性 中間</summary>
        private const int SHIDAISHI_ATTRIBUTE_MIDDLE = 1;

        /// <summary>標準ブリッジ</summary>
        private const int BRIGE_STANDARD = 0;

        /// <summary>分割抜歯後のブリッジ</summary>
        private const int BRIDGE_DIVISION = 1;

        /// <summary>延長ブリッジ</summary>
        private const int BRIDGE_EXTENSION = 2;

        /// <summary>歯番ビットデータリスト</summary>
        private static byte[] TOOTH_NUM_BIT_LIST = new byte[8] { 0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080 };

        /// <summary>エラーメッセージ1</summary>
        private const string ERROR_MESAAGE_1 = "ブリッジの歯番が不適切です";

        /// <summary>エラーメッセージ2</summary>
        private const string ERROR_MESAAGE_2 = "ブリッジにダミーがありません";

        /// <summary>エラーメッセージ3</summary>
        private const string ERROR_MESAAGE_3 = "ブリッジの歯番が連続していません";

        /// <summary>エラーメッセージ4</summary>
        private const string ERROR_MESAAGE_4 = "ブリッジは２歯以上の支台歯が必要です";

        /// <summary>エラーメッセージ5</summary>
        private const string ERROR_MESAAGE_5 = "ダミーの連続が長すぎます";

        /// <summary>エラーメッセージ6</summary>
        private const string ERROR_MESAAGE_6 = "分割ブリッジに不適当な歯番があります";

        /// <summary>エラーメッセージ7</summary>
        private const string ERROR_MESAAGE_7 = "延長ブリッジに不適当な歯番があります";

        /// <summary>エラーメッセージ8</summary>
        private const string ERROR_MESAAGE_8 = "延長ブリッジは２歯以上の支台歯が必要です";

        /// <summary>エラーメッセージ9</summary>
        private const string ERROR_MESAAGE_9 = "強度不足です";

        /// <summary>エラーメッセージ10</summary>
        private const string ERROR_MESAAGE_10 = "7番の支台歯が必要です";

        /// <summary>エラーメッセージ11</summary>
        private const string ERROR_MESAAGE_11 = "上顎の１根のみの支台歯は、不適切です";

        /// <summary>エラーメッセージ12</summary>
        private const string ERROR_MESAAGE_12 = "上顎の頬側２根が残った場合、口蓋根のダミーは必要ありません";

        /// <summary>エラーメッセージ13</summary>
        private const string ERROR_MESAAGE_13 = "右側支台歯の負担能力不足です";

        /// <summary>エラーメッセージ14</summary>
        private const string ERROR_MESAAGE_14 = "左側支台歯の負担能力不足です";

        /// <summary>エラーメッセージ15</summary>
        private const string ERROR_MESAAGE_15 = "事前承認が必要です";

        /// <summary>エラーメッセージ16</summary>
        private const string ERROR_MESAAGE_16 = "分割部の健全支台歯は、不適切です";

        /// <summary>歯番指数</summary>
        private static readonly decimal[,] TOOTH_NUM_EXPONTENT = new decimal[2, 8]
        {
            { 2, 1, 5, 4, 4, 6, 6, 4 },	    // 上顎指数
            { 1, 1, 5, 4, 4, 6, 6, 4 },		// 下顎指数
        };


        // メンバ変数 //////////////////////////////////////////////////////

        /// <summary>
        /// ブリッジ計算式
        /// </summary>
        private static string CalcString;

        /// <summary>
        /// 連続欠損歯数
        /// </summary>
        private static int KessonBuiContinuCount;

        /// <summary>
        /// ブリッジメンバ数
        /// </summary>
        private static int BrigeMemberCount;

        /// <summary>
        /// 近心延長ダミー
        /// </summary>
        private static bool IsNearDummy;

        /// <summary>
        /// 歯番連続性
        /// </summary>
        private static bool IsContinuBui;

        /// <summary>
        /// 
        /// </summary>
        private static BrigeBuiInfo[] BrigeBuiList;

        /// <summary>
        /// 歯式バッファ
        /// </summary>
        private static string[] BuiStrings;


        // メソッド //////////////////////////////////////////////////////

        /// <summary>
        /// ブリッジが保険認可かどうか
        /// </summary>
        /// <param name="bui">部位</param>
        /// <param name="calcString">計算式</param>
        /// <param name="message">メッセージ</param>
        /// <returns>
        /// -1 : ブリッジ歯式でない
        ///  0 : 保険認可
        ///  1 : 保険で認められない
        /// </returns>
        public static int EnableHokenBridge(BuiExp bui, ref string calcString, ref string message)
        {
            calcString = string.Empty;
            message = string.Empty;

            // 初期化
            Init();
            InitBuiStrings();

            // チェックデータ作成
            BuiCheckData checkData = CreateCheckData(bui);

            // チェック
            return EnableHokenBridge(checkData, bui, ref calcString, ref message);
        }

        /// <summary>
        /// 計算式を作成する
        /// </summary>
        /// <param name="resister">抵抗力</param>
        /// <param name="fatigue">疲労 ()</param>
        /// <param name="fatigueSuppliment">補足疲労</param>
        private static void CreateCalcString(decimal resister, decimal fatigue, decimal fatigueSuppliment)
        {
            decimal answer = resister - fatigue - fatigueSuppliment;
            CalcString = string.Format("{0:0.0} = {1:0.0} - ({2:0.0} + {3:0.0})", answer, resister, fatigue, fatigueSuppliment);
        }

        /// <summary>
        /// 分割歯番チェック<br/>
        /// ※この関数では、分割歯番の検査だけチェックしている<br/>
        /// ※歯番化隙は無視する <br/>
        /// 6番か7番ならOK それ以外はNG 
        /// </summary>
        /// <remarks>
        /// [イ].a<br/>
        /// 第1(6番),第2(7番)大臼歯を分割抜歯してブリッジの支台歯とすることは｢ブリッジの<br/>
        /// 適応症と設計｣(1992年版)に定める｢歯根を分割抜去した大臼歯に対するブリッジ｣の<br/>
        /// 項を参照し、残った歯冠,歯根の状態が歯科医学的に適切な場合に限り認められる<br/>
        /// なお、上顎第2大臼歯の遠心頬側根抜歯, 下顎第2大臼歯の遠心根抜歯の場合の<br/>
        /// 延長ダミーは認められない
        /// </remarks>
        /// <returns>true：保険で認められている(6番か7番)、保険で認められていない分割歯番が存在する</returns>
        private static bool IsEnableBunkatsuBui()
        {
            for (int i = 0; i < BrigeMemberCount - 1; i++)
            {
                int nextIndex = i + 1;
                if (BrigeBuiList[i].Part == BrigeBuiList[nextIndex].Part && BrigeBuiList[i].ToothNum == BrigeBuiList[nextIndex].ToothNum)
                {
                    // 次の部位が同じ歯番
                    if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI && BrigeBuiList[nextIndex].Attribute != ATTRIBUTE_DUMMY_GEKI)
                    {
                        // 両方とも隙でない
                        if (BrigeBuiList[i].ToothNum != 6 && BrigeBuiList[i].ToothNum != 7)
                        {
                            // ６番 or ７番でない
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 上顎の分割ブリッジの形
        /// </summary>
        /// <remarks>
        /// 上顎の場合 1根のみの支台歯は認められない<br/>
        /// 例) ⑥６６, ６⑥６, ６６⑥<br/>
        /// <br/>
        /// 上顎の頬側 2根が残った場合、口蓋根のダミーは必要とされない<br/>
        /// 例) ⑥６⑥, ⑦７⑦<br/>
        /// </remarks>
        /// <param name="message">メッセージ</param>
        /// <returns>true：保険で認められている、false：保険で認められていない</returns>
        private static bool IsEableDivisionForm(ref string message)
        {
            message = string.Empty;

            for (int i = 0; i < BrigeMemberCount; i++)
            {
                // 6番のチェック /////////
                if (BrigeBuiList[i].ToothNum == 6 && BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    int targetIndex = i + 1;
                    int targetNextIndex = i + 2;
                    if (targetIndex < BrigeMemberCount && targetNextIndex < BrigeMemberCount)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 6 && BrigeBuiList[targetNextIndex].ToothNum == 6)
                        {
                            // 両方とも６番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // ダミー歯
                                if (BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                                {
                                    message = ERROR_MESAAGE_11;
                                    return false;
                                }
                                if (BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_SHIDAISHI)
                                {
                                    message = ERROR_MESAAGE_12;
                                    return false;
                                }
                            }
                        }
                    }

                    targetIndex = i - 1;
                    targetNextIndex = i + 1;
                    if (targetIndex >= 0 && targetNextIndex < BrigeMemberCount)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 6 && BrigeBuiList[targetNextIndex].ToothNum == 6)
                        {
                            // 両方とも６番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY && BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // 両方ともダミー歯
                                message = ERROR_MESAAGE_11;
                                return false;
                            }
                        }
                    }

                    targetIndex = i - 1;
                    targetNextIndex = i - 2;
                    if (targetNextIndex >= 0)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 6 && BrigeBuiList[targetNextIndex].ToothNum == 6)
                        {
                            // 両方とも６番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY && BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // 両方ともダミー歯
                                message = ERROR_MESAAGE_11;
                                return false;
                            }
                        }
                    }
                }

                // 7番のチェック /////////
                if (BrigeBuiList[i].ToothNum == 7 && BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    int targetIndex = i + 1;
                    int targetNextIndex = i + 2;
                    if (targetIndex < BrigeMemberCount && targetNextIndex < BrigeMemberCount)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 7 && BrigeBuiList[targetNextIndex].ToothNum == 7)
                        {
                            // 両方とも７番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // ダミー歯
                                if (BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                                {
                                    message = ERROR_MESAAGE_11;
                                    return false;
                                }
                                if (BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_SHIDAISHI)
                                {
                                    message = ERROR_MESAAGE_12;
                                    return false;
                                }
                            }
                        }
                    }

                    targetIndex = i - 1;
                    targetNextIndex = i + 1;
                    if (targetIndex >= 0 && targetNextIndex < BrigeMemberCount)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 7 && BrigeBuiList[targetNextIndex].ToothNum == 7)
                        {
                            // 両方とも７番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY && BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // 両方ともダミー歯
                                message = ERROR_MESAAGE_11;
                                return false;
                            }
                        }
                    }

                    targetIndex = i - 1;
                    targetNextIndex = i - 2;
                    if (targetNextIndex >= 0)
                    {
                        // チェック部位が存在
                        if (BrigeBuiList[targetIndex].ToothNum == 7 && BrigeBuiList[targetNextIndex].ToothNum == 7)
                        {
                            // 両方とも７番
                            if (BrigeBuiList[targetIndex].Attribute == ATTRIBUTE_DUMMY && BrigeBuiList[targetNextIndex].Attribute == ATTRIBUTE_DUMMY)
                            {
                                // 両方ともダミー歯
                                message = ERROR_MESAAGE_11;
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 連続欠損歯数チェック
        /// </summary>
        /// <remarks>
        /// [ｱ].1.(2)<br/>
        /// 連続欠損の場合は2歯までとする。<br/>
        /// 但し、中側切歯(1,2番)については連続4歯欠損まで認める。
        /// </remarks>
        /// <returns>true：保険で認められている、false：連続欠損歯数 (保険で認められていない)</returns>
        private static bool IsEnableContinueBuiCount()
        {
            int maxCount = 4;
            int count = 0;
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    // 支台歯を初期化ﾄﾘｶﾞにする
                    count = 0;
                    maxCount = 4;
                }
                else
                {
                    if (BrigeBuiList[i].ToothNum > 2)
                    {
                        // 中側切歯以外ならmax変更
                        maxCount = 2;
                    }
                    count++;
                }

                if (count > maxCount)
                {
                    // 判定
                    KessonBuiContinuCount = count;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 符号化歯番の作成
        /// </summary>
        /// <remarks>
        /// 符号化 {左顎(+), 右顎(-)}<br/>
        /// 右顎のみ 0原点化を行なう
        /// </remarks>
        /// <param name="index">インデックス</param>
        /// <returns>符号化歯番</returns>
        private static int GetSigdNumber(int index)
        {
            int number = BrigeBuiList[index].ToothNum;

            if (BrigeBuiList[index].Part == RIGHT_GAKU_NUM)
            {
                // 0原点化
                number -= 1;
            }

            return number * BrigeBuiList[index].Part;   // 符号付
        }

        /// <summary>
        /// 指定された位置の次の符号化歯番を返す
        /// </summary>
        /// <remarks>
        /// 次が無い時はｶﾚﾝﾄの符号化歯番を返す<br/>
        /// 隙は無視される
        /// </remarks>
        /// <param name="index">インデックス</param>
        /// <returns>符号化歯番</returns>
        private static int GetNextSigdNumber(int index)
        {
            for (int i = index + 1; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI)
                {
                    // 隙は無視
                    return GetSigdNumber(i);    // 符号化歯番
                }
            }
            return GetSigdNumber(index);        // 符号化歯番
        }

        /// <summary>
        /// ブリッジの形状検査 (palette独自の検査)
        /// </summary>
        /// <remarks>
        /// ブリッジは基本的に支台歯, ダミー歯が相互に連続している必要がある<br/>
        /// ただしダミー歯及び支台歯の1歯欠落は認める<br/>
        /// 隙は、欠落のカウントとは無関係にした<br/>
        /// </remarks>
        /// <returns>tue：ブリッジの形になっている(連続)、false：ブリッジの形になっていない(非連続)</returns>
        private static bool IsEnableBrigeForm()
        {
            if (BrigeMemberCount < 2)
            {
                // 前処理により､こうなる事はない
                return false;
            }
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI)
                {
                    // 隙は無視
                    int sa = GetSigdNumber(i) - GetNextSigdNumber(i);
                    if (sa > 2)
                    {
                        // 2歯欠落
                        return false;
                    }
                    if (sa > 1)
                    {
                        // 1歯欠落
                        IsContinuBui = true;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 隙の歯番化(配置チェック付)
        /// </summary>
        /// <remarks>
        /// --- 隙の扱い --<br/>
        ///   1-1間 . 1番, 1-2間 . 2番, 2-3間 . 2番, 3-4間 . 4番<br/>
        ///   4-5間 . 4番, 5-6間 . 5番, 6-7間 . 6番, 7-8間 . 7番,<br/>
        /// <br/>
        /// この時点で隙の配置不適が判明した歯式は NG とする
        /// </remarks>
        /// <param name="gakuNum">顎指定(LEFT_GAKU_NUM or RIGHT_GAKU_NUM)</param>
        /// <param name="rightToothNum">右歯番</param>
        /// <param name="leftToothNum">左歯番</param>
        /// <param name="subToothNum">補助歯番(gakuの値による)</param>
        /// <returns>1～8：隙の歯番、負整数：隙の歯番化の結果が不適切</returns>
        private static int ConvertGekiNumber(int gakuNum, int rightToothNum, int leftToothNum, int subToothNum)
        {
            if (gakuNum == LEFT_GAKU_NUM)
            {
                // 左顎
                switch (leftToothNum)
                {
                    // 左０番
                    case 0:
                        switch (rightToothNum)
                        {
                            // 右１、２番
                            case 1:
                            case 2:
                                return 2;

                            // 右３、４番
                            case 3:
                            case 4:
                                return 4;

                            // 右５番
                            case 5:
                                return rightToothNum;

                            // 右０、６、７、８番
                            case 0:
                            case 6:
                            case 7:
                            case 8:
                            default:
                                return -1;
                        }

                    // 左１番
                    case 1:
                        if (rightToothNum == 0)
                        {
                            return (subToothNum < 3) ? 1 : -1;
                        }
                        return -1;

                    // 左２番
                    case 2:
                        if (rightToothNum == 0)
                        {
                            // 右０番
                            if (subToothNum < 2)
                            {
                                return 2;
                            }
                        }
                        if (rightToothNum == 1)
                        {
                            return 2;
                        }
                        return -1;

                    // 左３番
                    case 3:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum >= 0 && rightToothNum < 3) ? 2 : -1;

                    // 左４番
                    case 4:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum == 0 || rightToothNum == 3) ? 4 : (rightToothNum == 2) ? 2 : -1;

                    // 左５番
                    case 5:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum == 0 || rightToothNum == 3 || rightToothNum == 4) ? 5 : -1;

                    // 左６番
                    case 6:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum == 0 || rightToothNum == 4 || rightToothNum == 5) ? 5 : -1;

                    // 左７番
                    case 7:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum == 5 || rightToothNum == 6) ? rightToothNum : -1;

                    // 左８番
                    case 8:
                        if (subToothNum > 0)
                        {
                            return -1;
                        }
                        return (rightToothNum == 6 || rightToothNum == 7) ? rightToothNum : -1;
                    default:
                        return -1;
                }
            }
            else
            {
                // 右顎
                switch (leftToothNum)
                {
                    // 左０番
                    case 0:
                        switch (rightToothNum)
                        {
                            // 右１番
                            case 1:
                                return (subToothNum == 1 || subToothNum == 2) ? 1 : -1;

                            // 右２番
                            case 2:
                                if (subToothNum > 1)
                                {
                                    return -1;
                                }
                                return 2;

                            // 右３番
                            case 3:
                                if (subToothNum > 0)
                                {
                                    return -1;
                                }
                                return 2;

                            // 右３、４番
                            case 4:
                            case 5:
                                if (subToothNum > 0)
                                {
                                    return -1;
                                }
                                return rightToothNum;

                            // 右６番
                            case 6:
                                if (subToothNum > 0)
                                {
                                    return -1;
                                }
                                return 5;

                            // 右０、７、８番
                            case 0:
                            case 7:
                            case 8:
                            default:
                                return -1;
                        }

                    // 左１番
                    case 1:
                        return (rightToothNum == 0 || rightToothNum == 2 || rightToothNum == 3) ? 2 : -1;

                    // 左２番
                    case 2:
                        return (rightToothNum == 0 || rightToothNum == 3 || rightToothNum == 4) ? 2 : -1;

                    // 左３番
                    case 3:
                        return (rightToothNum == 0 || rightToothNum == 4 || rightToothNum == 5) ? 4 : -1;

                    // 左４番
                    case 4:
                        return (rightToothNum == 0 || rightToothNum == 5 || rightToothNum == 6) ? 4 : -1;

                    // 左５番
                    case 5:
                        return (rightToothNum == 0 || rightToothNum == 6 || rightToothNum == 7) ? 5 : -1;

                    // 左６番
                    case 6:
                        return (rightToothNum == 7 || rightToothNum == 8) ? 6 : -1;

                    // 左７番
                    case 7:
                        return (rightToothNum == 8) ? 7 : -1;

                    // 左８番
                    case 8:
                    default:
                        return -1;
                }
            }
        }

        /// <summary>
        /// ブリッジ･チェック要素配列初期化
        /// </summary>
        /// <remarks>
        /// 前処理で歯種の構成(永久歯, 支台歯)を絞り込んである<br/>
        /// 隙の2歯以上連続は不可とする<br/>
        /// </remarks>
        /// <param name="gakuNum">顎指定(LEFT_GAKU_NUM or RIGHT_GAKU_NUM)</param>
        /// <param name="brigeString">ブリッジデータ文字列</param>
        /// <returns>正整数：ブリッジメンバ数、負整数：隙の歯番化の結果が不適切、ゼロ：ブリッジではない</returns>
        private static int InitBrigeFactor(int gakuNum, string brigeString)
        {
            // 初期化
            BrigeMemberCount = 0;
            IsNearDummy = false;
            IsContinuBui = false;

            int gaku = 0;
            int rightIndex = 0;
            int leftIndex = 0;
            int rightToothNum = 0;
            int leftToothNum = 0;
            int subToothNum = 0;
            for (int j = 0; j < brigeString.Length; j++)
            {
                char datByte = brigeString[j];
                switch (datByte)
                {
                    // 左顎 
                    case 'H':
                    case 'K':
                        gaku = LEFT_GAKU_NUM;
                        break;

                    // 右顎 
                    case 'I':
                    case 'J':
                        gaku = RIGHT_GAKU_NUM;
                        break;

                    // 永久歯番
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                        BrigeBuiList[BrigeMemberCount].Part = gaku;
                        BrigeBuiList[BrigeMemberCount].ToothNum = (int)(datByte - '0');
                        rightIndex = brigeString[j - 1];
                        if (rightIndex == CHAR_SHIDAISHI || rightIndex == CHAR_LIVE_SHIDAISHI)
                        {
                            // 支台歯
                            BrigeBuiList[BrigeMemberCount].Attribute = ATTRIBUTE_SHIDAISHI;
                        }
                        else
                        {
                            // 支台歯以外はダミー歯
                            BrigeBuiList[BrigeMemberCount].Attribute = ATTRIBUTE_DUMMY;
                        }
                        BrigeBuiList[BrigeMemberCount].ShidaishiAttribute = SHIDAISHI_ATTRIBUTE_MIDDLE;
                        BrigeBuiList[BrigeMemberCount].Resister = BrigeBuiList[BrigeMemberCount].Fatigue = BrigeBuiList[BrigeMemberCount].FatigueSuppliment = 0;
                        BrigeMemberCount++;
                        break;

                    // 隙
                    case CHAR_GEKI:
                        // 初期化		 
                        rightIndex = j;
                        leftIndex = j;
                        rightToothNum = 0;
                        leftToothNum = 0;
                        subToothNum = 0;

                        BrigeBuiList[BrigeMemberCount].Part = gaku;
                        leftIndex--;
                        if (brigeString[leftIndex] >= '1' && brigeString[leftIndex] <= '8')
                        {
                            // １～８番
                            leftToothNum = (int)(brigeString[leftIndex] - '0');
                        }

                        if (brigeString[leftIndex] == CHAR_GEKI)
                        {
                            // 隙が連続 
                            return -1;
                        }

                        do
                        {
                            // 支台歯, 分割歯ﾏ-ｸ分ｽｷｯﾌﾟ 
                            rightIndex++;
                        } while (rightIndex < brigeString.Length && (brigeString[rightIndex] == CHAR_SHIDAISHI || brigeString[rightIndex] == CHAR_LIVE_SHIDAISHI || brigeString[rightIndex] == CHAR_BUNKATSUSHI));

                        if (rightIndex < brigeString.Length)
                        {
                            if (brigeString[rightIndex] >= '1' && brigeString[rightIndex] <= '8')
                            {
                                // １～８番
                                rightToothNum = (int)(brigeString[rightIndex] - '0');
                            }

                            if (brigeString[rightIndex] == CHAR_GEKI)
                            {
                                // 隙が連続
                                return -1;
                            }

                            if (gaku == LEFT_GAKU_NUM && rightToothNum == 0)
                            {
                                do
                                {
                                    rightIndex++;
                                    if (brigeString.Length <= rightIndex)
                                    {
                                        break;
                                    }

                                    if (brigeString[rightIndex] >= '1' && brigeString[rightIndex] <= '8')
                                    {
                                        subToothNum = (int)(brigeString[rightIndex] - '0');
                                        break;
                                    }
                                } while (rightIndex < brigeString.Length);
                            }
                        }

                        if (gaku == RIGHT_GAKU_NUM && leftToothNum == 0)
                        {
                            do
                            {
                                leftIndex--;
                                if (leftIndex < 0)
                                {
                                    break;
                                }
                                if (brigeString[leftIndex] >= '1' && brigeString[leftIndex] <= '8')
                                {
                                    subToothNum = (int)(brigeString[leftIndex] - '0');
                                    break;
                                }
                            } while (brigeString[leftIndex] != 'H' && brigeString[leftIndex] != 'K');
                        }

                        BrigeBuiList[BrigeMemberCount].ToothNum = ConvertGekiNumber(gaku, rightToothNum, leftToothNum, subToothNum);
                        if (BrigeBuiList[BrigeMemberCount].ToothNum < 1 || BrigeBuiList[BrigeMemberCount].ToothNum > 8)
                        {
                            // 隙の歯番化で不適隙を発見 
                            return -1;
                        }
                        BrigeBuiList[BrigeMemberCount].Attribute = ATTRIBUTE_DUMMY_GEKI;
                        BrigeBuiList[BrigeMemberCount].ShidaishiAttribute = SHIDAISHI_ATTRIBUTE_MIDDLE;
                        BrigeBuiList[BrigeMemberCount].Resister = BrigeBuiList[BrigeMemberCount].Fatigue = BrigeBuiList[BrigeMemberCount].FatigueSuppliment = 0;
                        BrigeMemberCount++;
                        break;
                    default:
                        break;
                }
            }

            // 両端が支台歯なら、支台歯の属性を(端 = AS_SIDE)に変更 
            if (BrigeBuiList[0].Attribute == ATTRIBUTE_SHIDAISHI)
            {
                BrigeBuiList[0].ShidaishiAttribute = AS_SIDE;
            }
            int i = BrigeMemberCount - 1;
            if (0 <= i)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    BrigeBuiList[i].ShidaishiAttribute = AS_SIDE;
                }
            }
            return BrigeMemberCount;
        }

        /// <summary>
        /// ブリッジ･タイプを調べる (その1)
        /// </summary>
        /// <remarks>
        /// 分割抜歯後のブリッジを区別<br/>
        /// 隙歯番の重複は無視し、それ以外はすべて分割ブリッジと見る<br/>
        /// 例) ⑤⑤, ⑤５, ６６など
        /// </remarks>
        /// <returns>分割抜歯後のブリッジ or 標準ブリッジ</returns>
        private static int GetBrigeType1()
        {
            for (int i = 0; i < BrigeMemberCount - 1; i++)
            {
                int nextIndex = i + 1;
                if (BrigeBuiList[i].Part == BrigeBuiList[nextIndex].Part
                    && BrigeBuiList[i].ToothNum == BrigeBuiList[nextIndex].ToothNum)
                {
                    if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI
                        && BrigeBuiList[nextIndex].Attribute != ATTRIBUTE_DUMMY_GEKI)
                    {
                        // 分割抜歯後のブリッジ
                        return BRIDGE_DIVISION;
                    }
                }
            }
            // 標準ブリッジ
            return BRIGE_STANDARD;
        }

        /// <summary>
        /// ブリッジ･タイプを調べる (その2)
        /// </summary>
        /// <returns>延長ブリッジ or 標準型のブリッジ</returns>
        private static int GetBrigeType2()
        {
            int index = BrigeMemberCount - 1;
            if (BrigeBuiList[0].Attribute == ATTRIBUTE_DUMMY
                || BrigeBuiList[0].Attribute == ATTRIBUTE_DUMMY_GEKI
                || BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY
                || BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY_GEKI)
            {
                // 延長ブリッジ
                return BRIDGE_EXTENSION;
            }
            // 標準型のブリッジ
            return BRIGE_STANDARD;
        }

        /// <summary>
        /// 延長歯番チェック
        /// </summary>
        /// <remarks>
        /// [ｱ].1.(3)<br/>
        /// 延長ブリッジは原則として認めない。<br/>
        /// 但し、第2大臼歯(7)が欠損している場合にのみ咬合状態及び支台歯の骨植状態を考慮して半歯程度のダミーを認める。<br/>
        /// なお、隣接歯の処置状況等からやむなく延長ブリッジを行なう場合、側切歯(2)<br/>
        /// 及び小臼歯1歯(4, 5)のみ摘要を可とする。<br/>
        ///  1 ：NG<br/>
        ///  2 ：OK<br/>
        ///  3 ：NG<br/>
        ///  4 ：OK<br/>
        ///  5 ：OK<br/>
        ///  6 ：6は分割延長の場合 OK, ただし遠心延長ダミーの場合、警告文出力<br/>
        ///      非分割延長では NG<br/>
        ///  7 ：7の遠心延長ダミーは NG, 非分割延長は OK (半歯ﾀﾞﾐ－)<br/>
        ///  8 ：NG<br/>
        /// <br/>
        /// 延長歯2歯連続は NG<br/>
        /// 両端が延長ダミーは NG<br/>
        /// 両端が延長ダミーでも 装置の歯数が5歯以上ならOK 
        /// </remarks>
        /// <returns>0：保険で認められている、-1：保険で認められていない、1：6番の分割延長ダミー</returns>
        private static int EnableExtensionBridge()
        {
            int ect = 0;
            for (int i = 0; i < 2; i++)
            {
                int index = 0;
                int otherIndex = 1;
                if (i != 0)
                {
                    index = BrigeMemberCount - 1;
                    otherIndex = BrigeMemberCount - 2;
                }

                if (BrigeBuiList[index].Attribute != ATTRIBUTE_SHIDAISHI)
                {
                    // 端は支台歯でない
                    // 1, 3, 8番はNG
                    if (BrigeBuiList[index].ToothNum == 1 || BrigeBuiList[index].ToothNum == 3 || BrigeBuiList[index].ToothNum == 8)
                    {
                        return -1;
                    }

                    // 6番
                    if (BrigeBuiList[index].ToothNum == 6)
                    {
                        if (BrigeBuiList[otherIndex].ToothNum != 6)
                        {
                            // 6番の分割延長でない
                            return -1;
                        }
                        else
                        {
                            if (i == 0 && BrigeBuiList[index].Part == LEFT_GAKU_NUM)
                            {
                                // 左顎 6番の遠心延長ダミー
                                return 1;
                            }
                            if (i != 0 && BrigeBuiList[index].Part == RIGHT_GAKU_NUM)
                            {
                                // 右顎 6番の遠心延長ダミー
                                return 1;
                            }
                            IsNearDummy = true;
                        }
                    }

                    // 7番
                    if (BrigeBuiList[index].ToothNum == 7 && BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY && BrigeBuiList[otherIndex].ToothNum == 7)
                    {
                        if (i == 0 && BrigeBuiList[index].Part == LEFT_GAKU_NUM)
                        {
                            // 左顎 7番の遠心延長ダミー
                            return -1;
                        }
                        if (i != 0 && BrigeBuiList[index].Part == RIGHT_GAKU_NUM)
                        {
                            // 右顎 7番の遠心延長ダミー
                            return -1;
                        }
                        IsNearDummy = true;
                    }
                    // 次も支台歯ではない
                    if (BrigeBuiList[otherIndex].Attribute != ATTRIBUTE_SHIDAISHI)
                    {
                        // 一端に延長ダミーが2歯以上
                        return -1;
                    }
                    ect++;
                }
            }

            if (ect == 2)
            {
                // 両端共に延長ダミー
                return (BrigeMemberCount > 4) ? 0 : -1;
            }
            return 0;
        }

        /// <summary>
        /// 延長ブリッジの支台歯数チェック
        /// </summary>
        /// <remarks>
        /// [ｱ].2.(2)
        /// 延長ブリッジの場合はR. Fの関係に関わらず、回転力を軽減させるため支台歯は2本以上とする
        /// </remarks>
        /// <returns>true：保険で認められている、false：保険で認められていない</returns>
        private static bool IsExtensionSupport()
        {
            int count = 0;
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    count++;
                }
            }
            return (count > 1) ? true : false;
        }

        /// <summary>
        /// 標準ブリッジの補足疲労(FS)を求める
        /// </summary>
        /// <remarks>
        /// ｶﾚﾝﾄﾎﾟｼﾞｼｮﾝから支台歯までの距離(歯数)をカウントし、小さい方の値を返す
        /// 補足疲労は前歯部(1,2,3)を含む2歯以上連続するダミーに加算される
        /// 前歯部連続欠損で隙が中心にあり、かつ隣接する左右顎の歯番が1でない場合の補足疲労は0にする
        /// </remarks>
        /// <param name="index">ｶﾚﾝﾄ欠損歯の位置</param>
        /// <returns>支台歯までの最少距離 = 補足疲労 (fatigue suppliment)</returns>
        private static int CalcStandardFS(int index)
        {
            bool isFromt = false;		// 前歯部を含んだら true
            int distanceLeft = 0;		// 左右支台歯までの距離
            int distanceRight = 0;

            // 左検査
            for (int i = index; i >= 0; i--)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {		// 支台歯に当ったらカウント終り
                    break;
                }
                else
                {
                    if (BrigeBuiList[i].ToothNum < 4)
                    {
                        // 前歯部ならtrue
                        isFromt = true;
                    }
                    // Add count left
                    distanceLeft++;
                }
            }

            // 右検査
            for (int i = index; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {		// 支台歯に当ったらカウント終り
                    break;
                }
                else
                {
                    if (BrigeBuiList[i].ToothNum < 4)
                    {
                        // 前歯部ならtrue
                        isFromt = true;
                    }
                    // Add count right
                    distanceRight++;
                }
            }

            // 前歯部の隙で
            if (BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY_GEKI && isFromt == true)
            {
                int fgap = index - 1;
                int rgap = index + 1;

                // 中心にあり
                if (0 <= fgap && fgap < BrigeBuiList.Length && rgap < BrigeBuiList.Length)
                {
                    if (BrigeBuiList[fgap].Part == LEFT_GAKU_NUM && BrigeBuiList[rgap].Part == RIGHT_GAKU_NUM)
                    {
                        // 1-1間でないなら
                        if (BrigeBuiList[fgap].ToothNum != 1 || BrigeBuiList[rgap].ToothNum != 1)
                        {
                            // 補足疲労はｾﾞﾛ
                            return 0;
                        }
                    }
                }
            }

            if (isFromt == false)
            {
                // 前歯部を含まない
                return 0;
            }

            if (distanceLeft == 1 && distanceRight == 1)
            {
                // 欠損歯は単数
                return 0;
            }
            // 欠損歯が複数連続
            return ((distanceLeft < distanceRight) ? distanceLeft : distanceRight);
        }

        /// <summary>
        /// 指定された大臼歯(6, 7番)の前後を調べ、同番の連続歯数と形状(ﾋﾞｯﾄﾊﾟﾀｰﾝ)を算出
        /// </summary>
        /// <param name="index">検査位置</param>
        /// <param name="byteValue">形状ﾋﾞｯﾄﾊﾟﾀｰﾝの格納先</param>
        /// <returns>連続歯数</returns>
        private static int GetByte67(int index, out byte byteValue)
        {
            int toothNum = BrigeBuiList[index].ToothNum;
            while (index >= 0 && BrigeBuiList[index].ToothNum == toothNum
                && (BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY || BrigeBuiList[index].Attribute == ATTRIBUTE_SHIDAISHI))
            {
                index--;
            }

            // 検索開始位置
            index++;
            int count = 0;
            byteValue = 0x00;
            byte cs = 0x01;
            while (BrigeBuiList[index].ToothNum == toothNum && index < BrigeMemberCount
                && (BrigeBuiList[index].Attribute == ATTRIBUTE_DUMMY || BrigeBuiList[index].Attribute == ATTRIBUTE_SHIDAISHI))
            {
                if (BrigeBuiList[index].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    byteValue |= cs;
                }
                cs <<= 1;
                count++;
                index++;
            }
            return count;
        }

        /// <summary>
        /// 大臼歯(6, 7番)の疲労 (fatigue)を算出する
        /// </summary>
        /// <param name="index">検査位置</param>
        /// <param name="gakuNum">顎指定</param>
        private static void CalcFatigue67(int index, int gakuNum)
        {
            byte byeValue;
            int count = GetByte67(index, out byeValue);

            // 判定
            switch (count)
            {
                case 1:
                    return;

                case 2:
                    if (byeValue == 0x00)
                    {
                        BrigeBuiList[index].Fatigue = TOOTH_NUM_EXPONTENT[gakuNum, (BrigeBuiList[index].ToothNum - 1)] / 2.0m;
                        return;
                    }
                    BrigeBuiList[index].Fatigue = 4.0m;
                    return;

                case 3:
                    BrigeBuiList[index].Fatigue = 4.0m;
                    return;
            }
        }

        /// <summary>
        /// 大臼歯(6, 7番)の抵抗力 (resister)を算出する
        /// </summary>
        /// <param name="index">検査位置</param>
        /// <param name="gakuNum">顎指定</param>
        private static void CalcResister67(int index, int gakuNum)
        {
            byte byeValue;
            int count = GetByte67(index, out byeValue);

            // 判定
            switch (count)
            {
                case 1:
                    return;

                case 2:
                    if (byeValue == 0x03)
                    {
                        if (gakuNum == UPPER_GAKU_NUM)
                        {
                            BrigeBuiList[index].Resister = 1.0m;
                        }
                        else
                        {
                            BrigeBuiList[index].Resister = 3.0m;
                        }
                        return;
                    }
                    BrigeBuiList[index].Resister = 2.0m;
                    return;

                case 3:
                    BrigeBuiList[index].Resister = 1.0m;
                    return;
            }
        }

        /// <summary>
        /// 中間支台歯の抵抗力算出
        /// </summary>
        /// <remarks>
        /// 支台歯の両端がダミーまたは隙ダミーであること(ただし分割支台歯の片割れを除く)
        /// 中間支台歯は、隙ダミーの歯番化値を考慮せず 左右いずれかが連続している場合、その支台歯の指数の 1/2 にする
        /// 中間支台歯が分割されている場合は、上記条件を満たす場合抵抗力を 1/2 にする
        /// </remarks>
        /// <param name="index">検査位置</param>
        private static void CalcIntermediate(int index)
        {

            // 左側検査
            int left = 999;
            for (int i = index - 1; i >= 0; i--)
            {
                if (BrigeBuiList[i].ToothNum == BrigeBuiList[index].ToothNum && BrigeBuiList[i].Attribute == BrigeBuiList[index].Attribute)
                {
                    // 分割支台歯
                    continue;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_DUMMY_GEKI)
                {
                    // 隙ダミー
                    left = 99;
                    continue;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_DUMMY)
                {
                    // ダミー
                    left = GetSigdNumber(i);
                    break;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    // 支台歯
                    if (left == 99)
                        // 隙ダミーあり
                        left = GetSigdNumber(i);
                    break;
                }
            }

            // 右側検査
            int right = 999;
            for (int i = index + 1; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].ToothNum == BrigeBuiList[index].ToothNum && BrigeBuiList[i].Attribute == BrigeBuiList[index].Attribute)
                {
                    // 分割支台歯
                    continue;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_DUMMY_GEKI)
                {
                    // 隙ダミー
                    right = 99;
                    continue;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_DUMMY)
                {
                    // ダミー
                    right = GetSigdNumber(i);
                    break;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI)
                {
                    // 支台歯
                    if (right == 99)
                    {
                        // 隙ダミーあり
                        right = GetSigdNumber(i);
                    }
                    break;
                }
            }
            if (left < 9 && right < 9)
            {
                // 判定 (8 to -7)
                int sigdNum = GetSigdNumber(index);
                if ((left - sigdNum) < 2 && (sigdNum - right) < 2)
                {
                    // 連続している
                    BrigeBuiList[index].Resister /= 2.0m; // 中間支台歯の抵抗力
                }
            }
        }

        /// <summary>
        /// 延長ブリッジの左右ﾊﾞﾗﾝｽ･チェックが不必要かどうかを調べる
        /// </summary>
        /// <returns>true：不必要、false：必要</returns>
        private static bool IsNotNeedBalance()
        {
            bool ls = false;
            bool md = false;
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI && !ls)
                {
                    ls = true;
                }
                if (BrigeBuiList[i].Attribute != ATTRIBUTE_SHIDAISHI && ls)
                {
                    md = true;
                }
                if (BrigeBuiList[i].Attribute == ATTRIBUTE_SHIDAISHI && md)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// [ｱ].2.(1) 計算共通部
        /// </summary>
        /// <param name="gakuNum">顎指定</param>
        private static void CommnCalc(int gakuNum)
        {
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                if (BrigeBuiList[i].Attribute != ATTRIBUTE_SHIDAISHI)
                {
                    // ダミー
                    // Fatigue of dummy
                    BrigeBuiList[i].Fatigue = TOOTH_NUM_EXPONTENT[gakuNum, (BrigeBuiList[i].ToothNum - 1)];
                    if (BrigeBuiList[i].ToothNum == 6 || BrigeBuiList[i].ToothNum == 7)
                    {
                        // 大臼歯
                        if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI)
                        {
                            // 非隙
                            CalcFatigue67(i, gakuNum);
                        }
                    }
                    // Fatigue suppliment of dummy
                    BrigeBuiList[i].FatigueSuppliment = (decimal)CalcStandardFS(i);
                }
                else
                {
                    // 支台歯
                    BrigeBuiList[i].Resister = TOOTH_NUM_EXPONTENT[gakuNum, (BrigeBuiList[i].ToothNum - 1)];
                    if (BrigeBuiList[i].ToothNum == 6 || BrigeBuiList[i].ToothNum == 7)
                    {
                        // 大臼歯
                        if (BrigeBuiList[i].Attribute != ATTRIBUTE_DUMMY_GEKI)
                        {
                            // 非隙
                            CalcResister67(i, gakuNum);
                        }
                    }
                    // Resister of intermediate support
                    if (BrigeBuiList[i].ShidaishiAttribute == SHIDAISHI_ATTRIBUTE_MIDDLE)
                    {
                        // 中間支台歯
                        CalcIntermediate(i);
                    }
                }
            }
        }

        /// <summary>
        /// 支台歯の負担能力検査
        /// </summary>
        /// <remarks>
        /// [ｱ].2.(3) 計算共通部
        ///	支台歯の負担能力は両側のﾊﾞﾗﾝｽを考慮して設計する。
        ///	すなわち、欠損の一側の支台歯の R の総計が、隣接するダミー部の F 及び FS の
        ///	総計の 1/3 以上であること。
        ///	</remarks>
        /// <param name="gakuNum">顎指定</param>
        /// <param name="message">メッセージ</param>
        /// <returns>true：：強度充分、false：強度不足</returns>
        private static bool IsBurigeFuka(int gakuNum, ref string message)
        {
            message = string.Empty;

            // 左側の検査
            int count = 0;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute != ATTRIBUTE_SHIDAISHI)
            {
                // 延長部の読み捨て
                count++;
            }

            decimal rt = 0m;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute == ATTRIBUTE_SHIDAISHI)
            {
                rt += BrigeBuiList[count].Resister;
                count++;
            }

            decimal rmt = 0m;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute != ATTRIBUTE_SHIDAISHI && count < BrigeMemberCount)
            {
                rmt += (BrigeBuiList[count].Fatigue + BrigeBuiList[count].FatigueSuppliment);
                count++;
            }

            // 右側の検査
            count = BrigeMemberCount - 1;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute != ATTRIBUTE_SHIDAISHI)
            {
                // 延長部の読み捨て
                count--;
            }

            decimal lt = 0m;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute == ATTRIBUTE_SHIDAISHI)
            {
                lt += BrigeBuiList[count].Resister;
                count--;
            }

            decimal lmt = 0m;
            while (0 <= count && count < BrigeBuiList.Length && BrigeBuiList[count].Attribute != ATTRIBUTE_SHIDAISHI)
            {
                lmt += (BrigeBuiList[count].Fatigue + BrigeBuiList[count].FatigueSuppliment);
                count--;
            }

            if ((lt * 3.0m) < lmt)
            {
                if (UPPER_GAKU_NUM == gakuNum)
                {
                    // 左側支台歯の負担能力不足
                    message = ERROR_MESAAGE_14;
                }
                else
                {
                    // 右側支台歯の負担能力不足
                    message = ERROR_MESAAGE_13;
                }
                return false;
            }
            if ((rt * 3.0m) < rmt)
            {
                if (UPPER_GAKU_NUM == gakuNum)
                {
                    // 右側支台歯の負担能力不足
                    message = ERROR_MESAAGE_13;
                }
                else
                {
                    // 左側支台歯の負担能力不足
                    message = ERROR_MESAAGE_14;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 延長ブリッジの強度検査
        /// </summary>
        /// <remarks>
        /// [ｱ].2.(1)
        /// 1)ブリッジの抵抗力(r)の判定方法
        ///   r = R - (F + FS)
        /// 2)延長ダミーにかかわる補足疲労
        ///   ダミーの指数の1/2
        /// </remarks>
        /// <param name="gakuNum">顎指定</param>
        /// <returns>true：：強度充分、false：強度不足</returns>
        private static bool IsStrongExtension(int gakuNum)
        {
            int count = BrigeMemberCount - 1;

            CommnCalc(gakuNum);
            if (BrigeBuiList[0].Attribute != ATTRIBUTE_SHIDAISHI)
            {
                // 7番は分割されていない場合 1/2
                if (BrigeBuiList[0].ToothNum == 7 && BrigeBuiList[0].ToothNum != BrigeBuiList[1].ToothNum)
                {
                    BrigeBuiList[0].Fatigue /= 2.0m;
                }
                BrigeBuiList[0].FatigueSuppliment = BrigeBuiList[0].Fatigue / 2.0m;
            }

            if (BrigeBuiList[count].Attribute != ATTRIBUTE_SHIDAISHI)
            {
                // 7番は分割されていない場合 1/2
                if (BrigeBuiList[count].ToothNum == 7 && BrigeBuiList[count].ToothNum != BrigeBuiList[count - 1].ToothNum)
                {
                    BrigeBuiList[count].Fatigue /= 2.0m;
                }
                BrigeBuiList[count].FatigueSuppliment = BrigeBuiList[count].Fatigue / 2.0m;
            }

            decimal resister = 0m;
            decimal fatigue = 0m;
            decimal fatigueSuppliment = 0m;
            for (int i = 0; i < BrigeMemberCount; i++)
            {
                resister += BrigeBuiList[i].Resister;
                fatigue += BrigeBuiList[i].Fatigue;
                fatigueSuppliment += BrigeBuiList[i].FatigueSuppliment;
            }

            // 計算式生成
            CreateCalcString(resister, fatigue, fatigueSuppliment);

            // 計算
            decimal answer = resister - fatigue - fatigueSuppliment;
            if (answer < 0m)
            {
                // 0未満は強度不足
                return false;
            }
            return true;
        }

        /// <summary>
        /// 標準ブリッジの強度検査<br/>
        /// [ｱ].2.(1) ～ [ｱ].2.(3)
        /// </summary>
        /// <param name="gakuNum">顎指定</param>
        /// <returns>true：強度充分、false：強度不足</returns>
        private static bool IsStrongStandard(int gakuNum)
        {
            CommnCalc(gakuNum);

            decimal resister = 0m;
            decimal fatigue = 0m;
            decimal fatigueSuppliment = 0m;

            for (int i = 0; i < BrigeMemberCount; i++)
            {
                resister += BrigeBuiList[i].Resister;
                fatigue += BrigeBuiList[i].Fatigue;
                fatigueSuppliment += BrigeBuiList[i].FatigueSuppliment;
            }

            // 計算式生成
            CreateCalcString(resister, fatigue, fatigueSuppliment);

            // 計算
            decimal answer = resister - fatigue - fatigueSuppliment;
            if (answer < 0m)
            {
                // 0未満は強度不足
                return false;
            }
            return true;
        }

        // 保険認可ブリッジかどうか調べる (ﾒｲﾝ部)
        //
        //	引  数：ul	：顎指定(PT_UPPER || PT_LOWER)
        //			dat	：ブリッジ･ﾃﾞｰﾀ
        //
        //	戻り値：0：保険で認められている
        //			1：保険で認められていない
        //			-1：引渡された歯式はブリッジではない
        /// <summary>
        /// 保険認可ブリッジかどうか調べる (ﾒｲﾝ部)
        /// </summary>
        /// <param name="gakuNum">顎指定</param>
        /// <param name="buiString">ブリッジ･ﾃﾞｰﾀ</param>
        /// <param name="message">メッセーｚい</param>
        /// <returns>0：保険で認められている、1：保険で認められていない、-1：引渡された歯式はブリッジではない</returns>
        private static int EnableHokenMainBridge(int gakuNum, string buiString, ref string message)
        {
            message = string.Empty;

            // ブリッジ･チェック要素配列初期化
            int resultFactor = InitBrigeFactor(gakuNum, buiString);
            if (resultFactor < 0)
            {
                // 隙の配置が不適切
                message = ERROR_MESAAGE_1;
                return 1;
            }
            else if (resultFactor == 0)
            {
                // ブリッジではない
                return -1;
            }

            // ブリッジ形状検査
            if (!IsEnableBrigeForm())
            {
                // ブリッジ歯番が非連続
                message = ERROR_MESAAGE_3;
                return 1;
            }

            // ブリッジ構成最少歯数以下
            if (BrigeMemberCount < 3)
            {
                // 延長ブリッジには支台歯2歯以上必要 [ｱ].2.(2)
                message = ERROR_MESAAGE_4;
                return 1;
            }

            // [ｱ].1.(2)連続欠損歯数チェック
            if (!IsEnableContinueBuiCount())
            {
                // 中側切歯は4歯連続可､それ以外は2歯連続可
                message = ERROR_MESAAGE_5;
                return 1;
            }

            // ブリッジのﾀｲﾌﾟを調べる
            if (GetBrigeType1() == BRIDGE_DIVISION)
            {
                // 分割抜歯なら
                if (!IsEnableBunkatsuBui())
                {
                    // 分割歯番は6|7 ? [ｲ].a
                    // 第1,第2大臼歯のみ分割抜歯が認められている
                    // 分割歯番不適当
                    message = ERROR_MESAAGE_6;
                    return 1;
                }
                if (gakuNum == UPPER_GAKU_NUM)
                {
                    // 上顎のみチェック
                    if (!IsEableDivisionForm(ref message))
                    {
                        // 形状チェック
                        return 1;
                    }
                }
            }

            if (GetBrigeType2() == BRIDGE_EXTENSION)
            {
                // 延長ブリッジならば
                int sw = EnableExtensionBridge();
                if (sw == -1)
                {
                    // [ｱ].1.(3)
                    // 延長ブリッジは(第2大臼歯), 側切歯, 小臼歯のいずれか1歯のみ認可
                    // 延長歯番不適当
                    message = ERROR_MESAAGE_7;
                    return 1;
                }

                if (!IsExtensionSupport())
                {
                    // [ｱ].2.(2)
                    // 延長ブリッジの支台歯数は2歯以上必要
                    // 延長ブリッジの支台歯数不足
                    message = ERROR_MESAAGE_8;
                    return 1;
                }

                if (!IsStrongExtension(gakuNum))
                {
                    // 延長ブリッジの強度不足
                    message = ERROR_MESAAGE_9;
                    return 1;
                }

                if (sw == 1)
                {
                    // 6番遠心延長ダミーあり
                    message = ERROR_MESAAGE_10;
                    return 1;
                }

                if (!IsNotNeedBalance())
                {
                    if (!IsBurigeFuka(gakuNum, ref message))
                    {
                        // 支台歯の負担能力検査
                        return 1;
                    }
                }
            }
            else
            {
                // 標準ブリッジ (含 分割標準)
                if (!IsStrongStandard(gakuNum))
                {
                    // 標準ブリッジの強度不足
                    message = ERROR_MESAAGE_9;
                    return 1;
                }

                // 支台歯の負担能力検査
                if (!IsBurigeFuka(gakuNum, ref message))
                {
                    return 1;
                }
            }

            if (IsNearDummy || IsContinuBui)
            {
                // 事前承認が必要
                message = ERROR_MESAAGE_15;
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 健全支台歯のチェック<br/>
        /// 6, 7番の分割抜歯ブリッジで残った歯根を健全支台歯にしていたらNG
        /// </summary>
        /// <param name="checkData"></param>
        /// <returns>true：良好、false：不良</returns>
        private static bool IsHealthfulSupport(BuiCheckData checkData)
        {
            uint mask = 0x60606060;

            uint eikyushi = checkData.GetUIntValue(BuiTableLineType.Eikyushi) & mask;
            uint shidaishi = checkData.GetUIntValue(BuiTableLineType.Shidaishi) & mask;
            uint libeShidaishi = checkData.GetUIntValue(BuiTableLineType.LiveShidaishi) & mask;
            if ((libeShidaishi & (eikyushi | shidaishi)) != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 部位文字列の初期化
        /// </summary>
        private static void InitBuiStrings()
        {
            BuiStrings[0] = "H";
            BuiStrings[1] = "I";
            BuiStrings[2] = "J";
            BuiStrings[3] = "K";
        }

        /// <summary>
        /// 部位情報からチェック用歯式文字列を作成する
        /// </summary>
        /// <remarks>
        /// --- 注意 ---<br/>
        ///    乳歯行の 'R'(5bit目) は ６と⑥<br/>
        ///             'S'(6bit目) は ７と⑦<br/>
        ///    が同時にあるとき使用されるが、ファイル記憶形式の歯式データに変換する際<br/>
        ///    強制的に 5,6,7Bit目 を 0に落とし"RST"が歯式文字列に含まれないようにした<br/>
        /// <br/>
        ///    又、この関数では、ﾋﾞｯﾄﾏｯﾌﾟﾃｰﾌﾞﾙに同一歯番のﾋﾞｯﾄが立っている場合の<br/>
        ///    位置情報(左右)が欠落している為、完全な歯式の復元はできません<br/>
        /// </remarks>
        /// <param name="bui">部位</param>
        private static void BuiExpToToothString(BuiExp bui)
        {
            // 部位文字列の初期化
            InitBuiStrings();

            List<SimpleTooth> upperToothList = new List<SimpleTooth>();
            List<SimpleTooth> lowerToothList = new List<SimpleTooth>();
            foreach (SimpleTooth tooth in bui.GetBuiObjectList())
            {
                if (tooth.IsUpper)
                {
                    // 上顎
                    upperToothList.Add(tooth);
                }
                else
                {
                    // 下顎
                    lowerToothList.Insert(0, tooth);
                }
            }

            // 文字列作成
            CreateToothString(upperToothList);
            CreateToothString(lowerToothList);
        }

        /// <summary>
        /// 部位の文字列作成
        /// </summary>
        /// <param name="toothList">部位リスト</param>
        private static void CreateToothString(List<SimpleTooth> toothList)
        {
            int[] pos = { 0, 1, 3, 2 };

            foreach (SimpleTooth tooth in toothList)
            {
                int jawIndex = 0;
                if (tooth.IsUpperRight)
                {
                    jawIndex = 0;
                }
                else if (tooth.IsUpperLeft)
                {
                    jawIndex = 1;
                }
                else if (tooth.IsLowerRight)
                {
                    jawIndex = 3;
                }
                else if (tooth.IsLowerLeft)
                {
                    jawIndex = 2;
                }

                if (tooth.Attribute == ToothAttribute.NormalNumber)
                {
                    // 通常歯番
                    if (tooth.IsEikyushi)
                    {
                        // 永久歯
                        var queryEikyushi = from x in Tooth.EIKYUSHI_TABLE where x.Position == tooth.Position select x.Str;
                        BuiStrings[pos[jawIndex]] += queryEikyushi.Single().ToString();
                    }
                    else
                    {
                        // 乳歯
                        var queryNyushi = from x in Tooth.NYUSHI_TABLE where x.Position == tooth.Position select x.Str;
                        BuiStrings[pos[jawIndex]] += queryNyushi.Single().ToString();
                    }
                }
                else
                {
                    var queryAttribute = from x in SimpleTooth.ATTRIBUTE_EXP_TABLE where x.Attribute == tooth.Attribute select x.Str;
                    BuiStrings[pos[jawIndex]] += queryAttribute.Single().ToString();
                    if (tooth.Attribute != ToothAttribute.Geki && tooth.Attribute != ToothAttribute.Kajoshi)
                    {
                        var queryEikyushi = from x in Tooth.EIKYUSHI_TABLE where x.Position == tooth.Position select x.Str;
                        BuiStrings[pos[jawIndex]] += queryEikyushi.Single().ToString();
                    }
                }
            }
        }

        // 保険認可ブリッジかどうか調べる
        //  0 : 保険で認められている
        //  1 : 保険で認められていない
        // -1 : 引渡された歯式はブリッジではない
        private static int EnableHokenBridge(BuiCheckData checkData, BuiExp bui, ref string calcString, ref string message)
        {
            calcString = string.Empty;
            message = string.Empty;

            // 健全支台歯のチェック
            if (!IsHealthfulSupport(checkData))
            {
                // 健全支台歯の不適切
                message = ERROR_MESAAGE_16;
                return 1;
            }

            // 支台歯(○),健全支台歯(◎)の論理和を採る
            uint shidaishiValue = checkData.GetUIntValue(BuiTableLineType.Shidaishi) | checkData.GetUIntValue(BuiTableLineType.LiveShidaishi);
            if (shidaishiValue == 0)
            {
                // 引数(map)はブリッジではない
                return -1;
            }

            // 乳歯, ▽, 増歯, 欠損歯, 鈎破損 が含まれていたら不可
            if (checkData.GetUIntValue(BuiTableLineType.Nyushi) != 0
                || checkData.GetUIntValue(BuiTableLineType.Kajoshi) != 0
                || checkData.GetUIntValue(BuiTableLineType.Zoshi) != 0
                || checkData.GetUIntValue(BuiTableLineType.Kessonshi) != 0
                || checkData.GetUIntValue(BuiTableLineType.Kohason) != 0)
            {

                // 不適当な歯番が存在する
                message = ERROR_MESAAGE_1;
                return 1;
            }
            if (checkData.GetUIntValue(BuiTableLineType.Eikyushi) == 0
                && checkData.GetUIntValue(BuiTableLineType.Geki) == 0
                && checkData.GetUIntValue(BuiTableLineType.Bunkatsushi) == 0)
            {
                // ブリッジにダミー(永久歯、隙、分割歯）がない
                message = ERROR_MESAAGE_2;
                return 1;
            }

            BuiExpToToothString(bui);

            // < 結果表示のルール >
            //・上顎の判定結果メッセージを戻すときは、上顎の計算式を戻す			
            //・下顎の判定結果メッセージを戻すときは、下顎の計算式を戻す
            //・以下の処理での、ブリッジではない(-1)と判定されるのは、歯式が入力されていないとき
            // -----------------------------------------------------------------------------------------------------------------------------
            //									| 下顎:ブリッジではない(-1)	| 下顎:保険で認められていない(1)| 下顎:保険で認められている(0)	|
            // -----------------------------------------------------------------------------------------------------------------------------
            // 上顎:ブリッジではない(-1)		|	下顎の判定結果			|	下顎の判定結果				|	下顎の判定結果				|
            // 上顎:保険で認められていない(1)	|	上顎の判定結果			|	上顎の判定結果				|	上顎の判定結果				|
            // 上顎:保険で認められている(0)		|	上顎の判定結果			|	下顎の判定結果				|	下顎の判定結果				|
            // -----------------------------------------------------------------------------------------------------------------------------

            // 上顎
            string upperStr = BuiStrings[0] + BuiStrings[1];

            int resultUpper = EnableHokenMainBridge(UPPER_GAKU_NUM, upperStr, ref message);
            if (1 == resultUpper)
            {
                calcString = CalcString;
                return 1;
            }
            else if (0 == resultUpper)
            {
                calcString = CalcString;
            }

            CalcString = string.Empty;

            // 下顎（下顎が判定されるのは、上顎が、保険で認められているか、ブリッジではない場合）
            string lowerStr = BuiStrings[3] + BuiStrings[2];
            int resultLower = EnableHokenMainBridge(LOWER_GAKU_NUM, lowerStr, ref message);
            if (1 == resultLower)
            {
                calcString = CalcString;
                return 1;
            }
            else if (0 == resultLower)
            {
                calcString = CalcString;
            }

            return 0;
        }

        /// <summary>
        /// 部位情報からチェックデータの作成
        /// </summary>
        /// <param name="bui">部位</param>
        /// <returns></returns>
        private static BuiCheckData CreateCheckData(BuiExp bui)
        {
            BuiCheckData checkData = new BuiCheckData();
            checkData.BuiByteList = new byte[10, 4];

            foreach (SimpleTooth tooth in bui.GetBuiObjectList())
            {
                // 格納列番号
                int columnIndex = 0;
                if (tooth.IsLowerLeft)
                {
                    columnIndex = 2;
                }
                else if (tooth.IsLowerRight)
                {
                    columnIndex = 3;
                }
                else if (tooth.IsUpperLeft)
                {
                    columnIndex = 1;
                }
                else if (tooth.IsUpperRight)
                {
                    columnIndex = 0;
                }

                // 格納歯番（永久歯・乳歯）
                var queryEikyushi = from x in Tooth.EIKYUSHI_TABLE where x.Position == tooth.Position select x.Str;
                int toothNum = int.Parse(queryEikyushi.Single().ToString());

                // 格納行番号
                BuiTableLineType lineIndex = BuiTableLineType.Eikyushi;
                if (tooth.IsEikyushi)
                {
                    // 永久歯
                    lineIndex = BuiTableLineType.Eikyushi;
                }
                else
                {
                    // 乳歯
                    lineIndex = BuiTableLineType.Nyushi;
                }

                if (tooth.Attribute == ToothAttribute.Shidaishi)
                {
                    // 支台歯
                    lineIndex = BuiTableLineType.Shidaishi;
                }
                else if (tooth.Attribute == ToothAttribute.KenzenShidaishi)
                {
                    // 健全支台歯
                    lineIndex = BuiTableLineType.LiveShidaishi;
                }
                else if (tooth.Attribute == ToothAttribute.EtcZoshi)
                {
                    // 増歯
                    lineIndex = BuiTableLineType.Zoshi;
                }
                else if (tooth.Attribute == ToothAttribute.EtcBunkatsushi)
                {
                    // 分割歯など
                    lineIndex = BuiTableLineType.Bunkatsushi;
                }
                else if (tooth.Attribute == ToothAttribute.EtcKessonshi)
                {
                    // 欠損歯
                    lineIndex = BuiTableLineType.Kessonshi;
                }
                else if (tooth.Attribute == ToothAttribute.EtcKohason)
                {
                    // 鉤破損
                    lineIndex = BuiTableLineType.Kohason;
                }
                else if (tooth.Attribute == ToothAttribute.Geki)
                {
                    // 隙
                    lineIndex = BuiTableLineType.Geki;
                }
                else if (tooth.Attribute == ToothAttribute.Kajoshi)
                {
                    // 過剰歯
                    lineIndex = BuiTableLineType.Kajoshi;
                }
                checkData.BuiByteList[(int)lineIndex, columnIndex] |= TOOTH_NUM_BIT_LIST[toothNum - 1];
            }
            return checkData;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private static void Init()
        {
            CalcString = string.Empty;
            KessonBuiContinuCount = 0;
            BrigeMemberCount = 0;
            IsNearDummy = false;
            IsContinuBui = false;
            BrigeBuiList = new BrigeBuiInfo[48];
            BuiStrings = new string[4];
        }


        // 内部構造体 //////////////////////////////////////////////////////

        /// <summary>
        /// ブリッジ部位情報
        /// </summary>
        private struct BrigeBuiInfo
        {
            /// <summary>
            /// 顎位置(左右)
            /// </summary>
            public int Part;

            /// <summary>
            /// 歯番
            /// </summary>
            public int ToothNum;

            /// <summary>
            /// 支台 | 欠損
            /// </summary>
            public int Attribute;

            /// <summary>
            /// 支台歯の属性(端 | 中間)
            /// </summary>
            public int ShidaishiAttribute;

            /// <summary>
            /// 抵抗力 (resister)
            /// </summary>
            public decimal Resister;

            /// <summary>
            /// 疲労 (fatigue)
            /// </summary>
            public decimal Fatigue;

            /// <summary>
            /// 補足疲労 (fatigue suppliment)
            /// </summary>
            public decimal FatigueSuppliment;
        }

        /// <summary>
        /// 歯式チェック用データ(320bit)
        /// </summary>
        private struct BuiCheckData
        {
            /// <summary>
            /// 格納用の部位データ（8 bit x 10 x 4）
            /// </summary>
            public byte[,] BuiByteList;

            /// <summary>
            /// 右上、左上、右下、左下の各種部位番号を返す
            /// </summary>
            /// <param name="enumType">歯式テーブル行番号種別</param>
            /// <returns>各種部位番号</returns>
            public uint GetUIntValue(BuiTableLineType enumType)
            {
                int index = (int)enumType;
                byte[] byteArray = { BuiByteList[index, 0], BuiByteList[index, 1], BuiByteList[index, 2], BuiByteList[index, 3] };
                return BitConverter.ToUInt32(byteArray, 0);
            }
        }
    }
}

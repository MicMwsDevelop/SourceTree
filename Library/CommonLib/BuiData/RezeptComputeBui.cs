//
// 電子レセプト記録用文字列変換
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.Common;

namespace CommonLib.BuiData
{
	public static class RezeptComputeBui
	{
        /// <summary>
        /// 電子レセプト記録文字列からBuiExpに変換
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static BuiExp ToBuiExpFromRezeptComputeString(this string str)
        {
            var shiftJIS = MultiByteStrings.GetShiftJISEncoding();
            var bytes = shiftJIS.GetBytes(str.ToCharArray());  // ShiftJisの文字列配列

            BuiExp resultBui = BuiExp.Empty;
            try
            {
                int byteLen = bytes.Length;
                if (0 < byteLen)
                {
                    if (0 == (byteLen % 6))
                    {
                        List<Bui> vlist = new List<Bui>();
                        List<Bui> urvlist = new List<Bui>();
                        List<Bui> ulvlist = new List<Bui>();
                        List<Bui> lrvlist = new List<Bui>();
                        List<Bui> llvlist = new List<Bui>();
                        string buf = str;
                        int index = 0;
                        while (index < byteLen)
                        {
                            byte[] temp = new byte[byteLen];
                            Array.Copy(bytes, index, temp, 0, 4);
                            string bui = shiftJIS.GetString(temp);
                            int buiNum = 0;
                            int.TryParse(bui, out buiNum);

                            temp = new byte[2];
                            Array.Copy(bytes, index + 4, temp, 0, 2);
                            string type = shiftJIS.GetString(temp);
                            int typeNum = 0;
                            int.TryParse(type, out typeNum);
                            switch (buiNum)
                            {
                                case 1000:
                                    {
                                        string shibangai = "口腔全体";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1001:
                                    {
                                        string shibangai = "上顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1002:
                                    {
                                        string shibangai = "下顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1003:
                                    {
                                        string shibangai = "右側上顎臼歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1004:
                                    {
                                        string shibangai = "上顎前歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1005:
                                    {
                                        string shibangai = "左側上顎臼歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1006:
                                    {
                                        string shibangai = "左側下顎臼歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1007:
                                    {
                                        string shibangai = "下顎前歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1008:
                                    {
                                        string shibangai = "右側下顎臼歯";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1010:
                                    {
                                        string shibangai = "右上顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1020:
                                    {
                                        string shibangai = "左上顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1030:
                                    {
                                        string shibangai = "左下顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                case 1040:
                                    {
                                        string shibangai = "右下顎歯列";
                                        switch (typeNum)
                                        {
                                            case 0:
                                                shibangai += "歯牙部";
                                                break;
                                            case 10:
                                                shibangai += "部";
                                                break;
                                            case 20:
                                                shibangai += "欠損部";
                                                break;
                                        }
                                        return new BuiExp(new StringBui(shibangai));
                                    }
                                default:
                                    {
                                        ToothNumber toothnum = GetRezeptComputeToothNumber(bui);
                                        if (toothnum != ToothNumber.None)
                                        {
                                            ToothAttribute toothAttr = GetRezeptComputeToothType(type);
                                            var tooth = new SimpleTooth(toothnum, toothAttr);
                                            // Ver1.099 【請求ファイルカルテコンバート】乳歯の歯番に支台歯の状態コードが記録されている場合に通常歯番としてコンバートを行う (Bug 17640)(2019/09/03 渡辺)
                                            if (tooth.IsNyushi && (toothAttr.HasFlag(ToothAttribute.Shidaishi) || toothAttr.HasFlag(ToothAttribute.KenzenShidaishi)))
                                            {
                                                tooth = tooth.SetAttribute(ToothAttribute.NormalNumber);
                                            }
                                            //vlist.Add(tooth);
                                            if (tooth.IsUpperRight)
                                            {
                                                urvlist.Add(tooth);
                                            }
                                            else if (tooth.IsUpperLeft)
                                            {
                                                ulvlist.Add(tooth);
                                            }
                                            else if (tooth.IsLowerRight)
                                            {
                                                lrvlist.Add(tooth);
                                            }
                                            else if (tooth.IsLowerLeft)
                                            {
                                                llvlist.Add(tooth);
                                            }
                                        }
                                    }
                                    break;
                            }
                            index += 6;
                        }
                        // 右側だけ永久歯と乳歯が反転する問題への応急処置
                        // C++とは異なるが、右側を正中から順に指定すれば可
                        vlist.AddRange(urvlist.Reverse<Bui>());     // 右上
                        vlist.AddRange(ulvlist);                    // 左上
                        vlist.AddRange(lrvlist.Reverse<Bui>());     // 右下
                        vlist.AddRange(llvlist);                    // 左下
                        resultBui = BuiExp.MakeByQuery(vlist);
                    }
                }
            }
            catch
            {

            }
            return resultBui;
        }

        /// <summary>
        /// BuiExpから電子レセプト記録文字列に変換
        /// </summary>
        /// <param name="bui"></param>
        /// <param name="kesson"></param>
        /// <returns></returns>
        public static string ToRezeptComputeString(this BuiExp bui, bool kesson)
        {
            StringBuilder sb = new StringBuilder();

            // 歯式を表示順にソート
            var disp = bui.ToDispOrder().OfType<SimpleTooth>();     // 歯式のみ処理する（歯番外部位の場合は空になる）

            // 過剰歯は出力しないように変更
            // 変更前
            // ▽3_| → 43_|
            // 3▽_| → 33_|
            // 1_|_▽1 → 1_|_11
            //   ↓
            // 変更後
            // ▽3_| → 3_|
            // 3▽_| → 3_|
            // 1_|_▽1 → 1_|_1

            bool kajo = false;
            kajo = disp.Any(x => x.Attribute == ToothAttribute.Kajoshi);
            var teeth = disp.Where(x => x.Attribute != ToothAttribute.Kajoshi);

            foreach (var th in teeth)
            {
                sb.Append(ToRezeptComputeString(th as Tooth, kesson, kajo));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 歯番を電子レセプト記録文字列に変換
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="kesson"></param>
        /// <param name="kajo"></param>
        private static string ToRezeptComputeString(Tooth tooth, bool kesson, bool kajo)
        {
            string ret = GetRezeptComputeToothNumber(tooth.Number).ToString();
            switch (tooth.Attribute)
            {
                case ToothAttribute.Shidaishi:
                    ret += "30";	// 支台歯
                    break;
                case ToothAttribute.KenzenShidaishi:
                    ret += "50";	// 便宜抜髄支台歯
                    break;
                case ToothAttribute.Geki:
                    // Ver3.24(2009/12/04):電子レセプト対応 正中隙の出力方法を修正(Bug 8243)
                    //   正中隙("１△｜１" or "１｜△１")は必ず右側の歯種コードで出力する。
                    //     記録条件仕様(歯科用) 3-(3)-オ 注3
                    // 1番近心隙が左側で入力されていたら、強制的に右側の歯種コードに変換する
                    // ただし、入力されている順番は変更しない(この関数で処理する限りは変更不能)

                    //  レセ電仕様上エラー(右側→左側の順に出力するルールに違反)となるケース
                    //     １｜１△１ → 101100103100101180103100
                    //     １｜Ａ△１ → 101100105100101180103100
                    //  こういう症例が無い事が前提。

                    //   問題ないであろうと思われるケース
                    //     ｜△２ → ｜△２(変化なし) 
                    //     １△｜１ → １△｜１(変化なし)
                    //     ｜△１ → △｜１
                    //     １｜△１ → １△｜１
                    //     １△｜△１ → １△△｜１
                    //     １｜△△１ → １△△｜１


                    if (tooth.Number == ToothNumber.UpperLeft1)
                    {	// 上顎正中隙(左上１番近心隙)
                        ret = GetRezeptComputeToothNumber(ToothNumber.UpperRight1).ToString();
                    }
                    else if (tooth.Number == ToothNumber.LowerLeft1)
                    {	// 下顎正中隙(左下１番近心隙)
                        ret = GetRezeptComputeToothNumber(ToothNumber.LowerRight1).ToString();
                    }
                    else if (tooth.Number == ToothNumber.UpperLeftA)
                    {	// 上顎正中隙(左上Ａ近心隙)
                        ret = GetRezeptComputeToothNumber(ToothNumber.UpperRightA).ToString();
                    }
                    else if (tooth.Number == ToothNumber.LowerLeftA)
                    {	// 下顎正中隙(左下Ａ近心隙)
                        ret = GetRezeptComputeToothNumber(ToothNumber.LowerRightA).ToString();
                    }
                    ret += "80";	// 部近心隙
                    break;
                default:
                    if (kesson)
                    {
                        if (tooth.IsEikyushi)
                        {
                            // 永久歯のみ状態コードを設定
                            ret += "20";	// 欠損歯
                        }
                        else
                        {
                            ret += "00";	// 現存歯
                        }
                    }
                    else if (kajo)
                    {
                        // 過剰歯
                        ret += "10";	// 部
                    }
                    else
                    {
                        ret += "00";	// 現存歯
                    }
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 歯番→レセプト電算データ歯番
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        private static int GetRezeptComputeToothNumber(ToothNumber tn)
        {
            switch (tn)
            {
                case ToothNumber.UpperRight8: return 1018;
                case ToothNumber.UpperRight7: return 1017;
                case ToothNumber.UpperRight6: return 1016;
                case ToothNumber.UpperRight5: return 1015;
                case ToothNumber.UpperRight4: return 1014;
                case ToothNumber.UpperRight3: return 1013;
                case ToothNumber.UpperRight2: return 1012;
                case ToothNumber.UpperRight1: return 1011;
                case ToothNumber.UpperLeft1: return 1021;
                case ToothNumber.UpperLeft2: return 1022;
                case ToothNumber.UpperLeft3: return 1023;
                case ToothNumber.UpperLeft4: return 1024;
                case ToothNumber.UpperLeft5: return 1025;
                case ToothNumber.UpperLeft6: return 1026;
                case ToothNumber.UpperLeft7: return 1027;
                case ToothNumber.UpperLeft8: return 1028;
                case ToothNumber.LowerRight8: return 1048;
                case ToothNumber.LowerRight7: return 1047;
                case ToothNumber.LowerRight6: return 1046;
                case ToothNumber.LowerRight5: return 1045;
                case ToothNumber.LowerRight4: return 1044;
                case ToothNumber.LowerRight3: return 1043;
                case ToothNumber.LowerRight2: return 1042;
                case ToothNumber.LowerRight1: return 1041;
                case ToothNumber.LowerLeft1: return 1031;
                case ToothNumber.LowerLeft2: return 1032;
                case ToothNumber.LowerLeft3: return 1033;
                case ToothNumber.LowerLeft4: return 1034;
                case ToothNumber.LowerLeft5: return 1035;
                case ToothNumber.LowerLeft6: return 1036;
                case ToothNumber.LowerLeft7: return 1037;
                case ToothNumber.LowerLeft8: return 1038;
                case ToothNumber.UpperRightE: return 1055;
                case ToothNumber.UpperRightD: return 1054;
                case ToothNumber.UpperRightC: return 1053;
                case ToothNumber.UpperRightB: return 1052;
                case ToothNumber.UpperRightA: return 1051;
                case ToothNumber.UpperLeftA: return 1061;
                case ToothNumber.UpperLeftB: return 1062;
                case ToothNumber.UpperLeftC: return 1063;
                case ToothNumber.UpperLeftD: return 1064;
                case ToothNumber.UpperLeftE: return 1065;
                case ToothNumber.LowerRightE: return 1085;
                case ToothNumber.LowerRightD: return 1084;
                case ToothNumber.LowerRightC: return 1083;
                case ToothNumber.LowerRightB: return 1082;
                case ToothNumber.LowerRightA: return 1081;
                case ToothNumber.LowerLeftA: return 1071;
                case ToothNumber.LowerLeftB: return 1072;
                case ToothNumber.LowerLeftC: return 1073;
                case ToothNumber.LowerLeftD: return 1074;
                case ToothNumber.LowerLeftE: return 1075;
            }
            return 0;
        }

        /// <summary>
        /// レセプト電算データ文字列から部位種別を取得
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static ToothAttribute GetRezeptComputeToothType(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                // 先頭の１バイトの内容で判断する
                if (str.StartsWith("3"))
                {
                    // 支台歯
                    return ToothAttribute.Shidaishi;
                }
                // 先頭の１バイトの内容で判断する
                if (str.StartsWith("5"))
                {
                    // 便宜抜髄支台歯
                    return ToothAttribute.KenzenShidaishi;
                }
                // 先頭の１バイトの内容で判断する
                if (str.StartsWith("8"))
                {
                    // 部近心隙
                    return ToothAttribute.Geki;
                }
                // ノーマル歯番
                return ToothAttribute.NormalNumber;
            }
            // 無効
            return ToothAttribute.None;
        }

        /// <summary>
        /// レセプト電算データ歯番→歯番
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static ToothNumber GetRezeptComputeToothNumber(string str)
        {
            int num = 0;
            int.TryParse(str, out num);
            switch (num)
            {
                case 1018: return ToothNumber.UpperRight8;
                case 1017: return ToothNumber.UpperRight7;
                case 1016: return ToothNumber.UpperRight6;
                case 1015: return ToothNumber.UpperRight5;
                case 1014: return ToothNumber.UpperRight4;
                case 1013: return ToothNumber.UpperRight3;
                case 1012: return ToothNumber.UpperRight2;
                case 1011: return ToothNumber.UpperRight1;
                case 1021: return ToothNumber.UpperLeft1;
                case 1022: return ToothNumber.UpperLeft2;
                case 1023: return ToothNumber.UpperLeft3;
                case 1024: return ToothNumber.UpperLeft4;
                case 1025: return ToothNumber.UpperLeft5;
                case 1026: return ToothNumber.UpperLeft6;
                case 1027: return ToothNumber.UpperLeft7;
                case 1028: return ToothNumber.UpperLeft8;
                case 1048: return ToothNumber.LowerRight8;
                case 1047: return ToothNumber.LowerRight7;
                case 1046: return ToothNumber.LowerRight6;
                case 1045: return ToothNumber.LowerRight5;
                case 1044: return ToothNumber.LowerRight4;
                case 1043: return ToothNumber.LowerRight3;
                case 1042: return ToothNumber.LowerRight2;
                case 1041: return ToothNumber.LowerRight1;
                case 1031: return ToothNumber.LowerLeft1;
                case 1032: return ToothNumber.LowerLeft2;
                case 1033: return ToothNumber.LowerLeft3;
                case 1034: return ToothNumber.LowerLeft4;
                case 1035: return ToothNumber.LowerLeft5;
                case 1036: return ToothNumber.LowerLeft6;
                case 1037: return ToothNumber.LowerLeft7;
                case 1038: return ToothNumber.LowerLeft8;
                case 1055: return ToothNumber.UpperRightE;
                case 1054: return ToothNumber.UpperRightD;
                case 1053: return ToothNumber.UpperRightC;
                case 1052: return ToothNumber.UpperRightB;
                case 1051: return ToothNumber.UpperRightA;
                case 1061: return ToothNumber.UpperLeftA;
                case 1062: return ToothNumber.UpperLeftB;
                case 1063: return ToothNumber.UpperLeftC;
                case 1064: return ToothNumber.UpperLeftD;
                case 1065: return ToothNumber.UpperLeftE;
                case 1085: return ToothNumber.LowerRightE;
                case 1084: return ToothNumber.LowerRightD;
                case 1083: return ToothNumber.LowerRightC;
                case 1082: return ToothNumber.LowerRightB;
                case 1081: return ToothNumber.LowerRightA;
                case 1071: return ToothNumber.LowerLeftA;
                case 1072: return ToothNumber.LowerLeftB;
                case 1073: return ToothNumber.LowerLeftC;
                case 1074: return ToothNumber.LowerLeftD;
                case 1075: return ToothNumber.LowerLeftE;
            }
            return ToothNumber.None;
        }
    }
}

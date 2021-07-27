//
// class BuiExpHelper -- BuiExp拡張メソッド定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.BuiData
{
	public static class BuiExpHelper
    {
        public static bool IsBridge(this BuiExp buiexp)
        {
            return buiexp.Contains(ToothAttribute.Shidaishi | ToothAttribute.KenzenShidaishi);
        }

        public static bool IsBridgeEx(this BuiExp source)
        {
            BuiExp buiexp = BuiExp.MakeByQuery(source.OfType<SimpleTooth>());
            if (buiexp.Contains(ToothAttribute.Shidaishi | ToothAttribute.KenzenShidaishi))
            {
                // 小児隙補綴の為の隙の位置を表すためだけの支台歯をブリッジ判定から除外する為に削除する
                if (buiexp.Contains(ToothAttribute.Geki))
                {
                    // 隙の隣の支台歯(隙1つにつき支台歯一種類)を削除
                    foreach (var geki in (buiexp & ToothAttribute.Geki))
                    {
                        ToothPosition enshinPos = geki.Position.Next(); // buiの遠心側位置
                        ToothPosition kinshinPos = geki.Position;   // buiの近心側位置
                        if ((buiexp & enshinPos).Contains(ToothAttribute.Shidaishi | ToothAttribute.KenzenShidaishi))
                        {
                            // 隙の遠心側に支台歯が存在する
                            // 遠心側に含まれる支台歯の内最初に見つかった属性を削除する。
                            buiexp -= (buiexp & enshinPos).First() as SimpleTooth;
                        }
                        else if ((buiexp & kinshinPos).Contains(ToothAttribute.Shidaishi | ToothAttribute.KenzenShidaishi))
                        {
                            buiexp -= (buiexp & kinshinPos).First() as SimpleTooth;
                        }
                    }
                }
            }

            // 隙の位置を表すための支台歯を除外した上で、支台歯とポンティックの両方を含む場合のみブリッジと判定する
            return buiexp.Contains(ToothAttribute.Shidaishi | ToothAttribute.KenzenShidaishi)
                   && buiexp.Contains(ToothAttribute.NormalNumber | ToothAttribute.Geki | ToothAttribute.EtcBunkatsushi);
        }

        /// <summary>
        /// 永久歯のみ
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：永久歯のみ、false：永久歯以外を含む</returns>
        public static bool IsAllAdultTooth(this BuiExp source)
        {
            return source.OfType<SimpleTooth>().All(x => x.IsEikyushi);
        }

        /// <summary>
        /// 乳歯のみ
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：乳歯のみ、false：乳歯以外を含む</returns>
        public static bool IsAllChildTooth(this BuiExp source)
        {
            return source.OfType<SimpleTooth>().All(x => x.IsNyushi);
        }

        /// <summary>
        /// 混合歯列かどうか
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：混合歯列、false：永久歯のみ、または乳歯のみ</returns>
        public static bool IsMixTooth(this BuiExp source)
        {
            if (!source.IsAllAdultTooth() && !source.IsAllChildTooth())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 上額を含むかどうか
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：上額を含む、false：含まない</returns>
        public static bool IsUpper(this BuiExp source)
        {
            var tooth = source.OfType<Tooth>();
            return tooth.Any(x => x.IsUpper);
        }

        /// <summary>
        /// 下額を含むかどうか
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：下額を含む、false：含まない</returns>
        public static bool IsLower(this BuiExp source)
        {
            var tooth = source.OfType<Tooth>();
            return tooth.Any(x => x.IsLower);
        }

        /// <summary>
        /// 上下額を含むかどうか
        /// </summary>
        /// <param name="source">部位</param>
        /// <returns>true：上下額を含む、false：含まない</returns>
        public static bool IsUpperAndLower(this BuiExp source)
        {
            if (source.IsUpper() && source.IsLower())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 部位の内容をBui.Compare()順(正規の順)に列挙する
        /// </summary>
        /// <returns>BuiExp内の部位の一般的なBui.Compare()順(正規の順)での列挙</returns>
        public static IEnumerable<Bui> ToNormalOrder(this BuiExp source)
        {
            Bui[] array = source.ToArray();
            Array.Sort<Bui>(array);
            return array;
        }

        /// <summary>
        /// 隙・過剰歯を同一ポジション内での一番内側に移動 
        /// </summary>
        /// <remarks>
        /// c++版の部位入力で△→３→遠心→△→４→近心と入力した時に本来と逆の順序で
        /// 入力されてしまうバグを、部位描画側で吸収してしまったものと思われる。
        /// 部位入力を直したとしてもそのままでは既存データが描画できないので、C#版でも同様に描画する。
        /// </remarks>
        /// <param name="targetJawTeeth"></param>
        /// <returns></returns>
        public static IEnumerable<Tooth> MoveToInnerGekiKajoshi(this IEnumerable<Tooth> targetJawTeeth)
        {
            var list = targetJawTeeth.ToList();

            // 隙と過剰歯を抽出
            var targets = list.Where(x => (x is SimpleTooth) && (ToothAttribute.Geki | ToothAttribute.Kajoshi).HasFlag(x.Attribute)).ToArray();

            // 抽出した隙と過剰歯を元のリストから削除
            list.RemoveAll(y => targets.Any(x => ReferenceEquals(x, y)));

            // 隙と過剰歯を遠心→近心の順で、同じToothPositionの歯番の先頭に挿入
            foreach (var target in targets.Reverse())
            {
                int insertTo = list.FindIndex(x => x.Position >= target.Position);
                if (insertTo == -1)
                {
                    // 挿入位置が見つからない
                    list.Add(target);
                }
                else
                {
                    list.Insert(insertTo, target);
                }
            }
            return list;
        }


        /// <summary>
        /// 部位を旧C++のGetBuiObjectListと同じ順序で取り出す
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Bui> GetBuiObjectList(this IEnumerable<Bui> source)
        {
            // 歯番を持つ部位とそれ以外に分割
            var other = source.Where(x => !(x is Tooth));   // 歯以外
            var teeth = source.OfType<Tooth>();   // 歯

            if(teeth.Count() > 0)
            {
                // 歯が含まれる

                // ブロックごとに分割して右側を逆転する
                var ur = teeth.Where(x => x.HalfJaw == HalfJaw.UpperRight).Reverse();
                var ul = teeth.Where(x => x.HalfJaw == HalfJaw.UpperLeft);
                var lr = teeth.Where(x => x.HalfJaw == HalfJaw.LowerRight).Reverse();
                var ll = teeth.Where(x => x.HalfJaw == HalfJaw.LowerLeft);

                teeth = ur.Concat(ul).Concat(lr).Concat(ll);
            }
            return other.Concat(teeth);
        }

        /// <summary>
        /// BuiExpの内容を一般的な表示順で列挙する(内部形式の順序を右側のみ逆順にする)
        /// </summary>
        /// <returns>BuiExp内の部位の一般的な表示順での列挙</returns>
        public static IEnumerable<Bui> ToDispOrder(this IEnumerable<Bui> source)
        {
            // 内包するBuiをTooth以外のインスタンスと、Toothの右上、左上、右下、左下に分ける
            var others = source.Where(x => !(x is Tooth));
            var teeth = source.OfType<Tooth>();

            // 右上、左上、右下、左下に分けて、隙と過剰歯は強制的に同一ポジション内の一番内側に移動する
            var ur = teeth.Where(x => x.Position.IsUpperRight()).MoveToInnerGekiKajoshi();
            var ul = teeth.Where(x => x.Position.IsUpperLeft()).MoveToInnerGekiKajoshi();
            var lr = teeth.Where(x => x.Position.IsLowerRight()).MoveToInnerGekiKajoshi();
            var ll = teeth.Where(x => x.Position.IsLowerLeft()).MoveToInnerGekiKajoshi();

            // Toothの右上、右下のみ逆順で結合
            return others.Concat(ur.Reverse()).Concat(ul).Concat(lr.Reverse()).Concat(ll);
        }
        
        /// <summary>
        /// BuiExpの内容を一般的な表示順で列挙する(内部形式の順序を右側のみ逆順にし、隙と過剰歯は同一ポジション内の一番内側へ強制的に移動)
        /// </summary>
        /// <remarks>
        /// ※MoveToInnerGekiKajoshiの注釈を参照
        /// </remarks>
        /// <returns>BuiExp内の部位の一般的な表示順での列挙</returns>
        public static IEnumerable<Bui> ToDispOrder(this BuiExp source)
        {
            return source.AsEnumerable().ToDispOrder();
        }

        /// <summary>
        /// 部位に含まれる全てのToothPositionを取得する
        /// </summary>
        /// <param name="source">対象部位</param>
        /// <returns>ToothPosition値を論理和で合成したもの</returns>
        public static ToothPosition GetToothPosition(this BuiExp source)
        {
            ToothPosition result = ToothPosition.None;
            var query = source.Where(x => x.HasToothPosition);
            foreach (var tooth in query)
            {
                result |= tooth.Position;
            }
            return result;
        }

        /// <summary>
        /// 部位に含まれる全てのToothNumberを取得する
        /// </summary>
        /// <param name="source">対象部位</param>
        /// <returns>ToothNumber値を論理和で合成したもの</returns>
        public static ToothNumber GetToothNumbers(this BuiExp source)
        {
            ToothNumber result = ToothNumber.None;
            var query = source.Where(x => x.HasToothNumber);
            foreach (var tooth in query)
            {
                result |= tooth.Number;
            }
            return result;
        }

        /// <summary>
        /// 部位に含まれる全てのToothAttributeを取得する
        /// </summary>
        /// <param name="source">対象部位</param>
        /// <returns>ToothNumber値を論理和で合成したもの</returns>
        public static ToothAttribute GetToothAttributes(this BuiExp source)
        {
            ToothAttribute result = ToothAttribute.None;
            var query = source.Where(x => x.HasToothAttribute);
            foreach (var tooth in query)
            {
                result |= tooth.Attribute;
            }
            return result;
        }

        /// <summary>
        /// 歯番外部位文字列を取得
        /// </summary>
        /// <param name="bui">対象部位</param>
        /// <param name="multi">複数の文字列部位をカンマで接続する(省略時、false指定時は先頭の文字列部位のみ)</param>
        /// <returns>
        /// 歯番外部位文字列。部位に文字列部位が含まれない場合は空文字列
        /// </returns>
        public static string GetShibangaiBuiString(this BuiExp bui, bool multi = false)
        {
            var stringBuis = bui.OfType<StringBui>();
            if (stringBuis.Count() == 0)
            {
                return string.Empty;
            }
            else
            {
                if (multi)
                {
                    return string.Join(",", stringBuis.Select(x => x.Expression));
                }
                else
                {
                    return stringBuis.First().Expression;
                }
            }
        }

        // 未作成機能について
        //   面についてはBuiExpではなく別に面クラス(またはヘルパークラス)を作成
        //    // 近心面を含むかどうか？
        //    friend TBool IsIncludeNearside(const DString& men);

        //    // 遠心面を含むかどうか？
        //    friend TBool IsIncludeFarside(const DString& men);

        //    // 隣接面を含むかどうか？
        //    friend TBool IsInlcudeComplicateSide(const DString& men);

        //    // 近心面と遠心面を含むかどうか？
        //    friend TBool IsInlcudeBothNearsideAndFarside(const DString& men);

        //  充填やインレーで複雑が算定出来るかどうか？は保険ルール上の問題なのでBuiExpでは取り扱わない
        //    // 充填やインレーで複雑が算定出来るかどうか？
        //    friend TBool IsComplicateKado(ToothPosition tn, const DString& men, const BuiExp& kesson);

        //  レセ電については、レセ電用ヘルパークラスを作成
        //    // レセプト電算データ文字列の取得
        //    // Ver3.20(2008/12/25):電子レセプト対応 電子レセプト出力の作成 旧GetRezeptComputeString()
        //    string ToEReceiptString(BuiExp kesson);
        //
        //    // レセプト電算データ文字列の設定
        //    // Ver3.20(2008/12/25):電子レセプト対応 電子レセプト出力の作成 旧SetRezeptComputeString()
        //    public static BuiExp FromEReceiptString(string str);

        //// データのシリアル化 → 必要ないのでは？
        //    // シリアライズ
        //    void Serialize(DBStream &bs);

        //    // デシリアライズ
        //    void DeSerialize(DBStream &bs);

    }
}

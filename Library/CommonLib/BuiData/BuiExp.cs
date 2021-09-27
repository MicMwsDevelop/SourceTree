//
// 部位表現(歯番や文字列表現部位等の単一部位の集合,旧DBuiExpクラスに相当)クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.BuiData
{
	/// <summary>
	/// オンメモリの部位表現形式(UBOXのDBuiExpに相当)を定義するクラス<br/>
	/// </summary>
	/// <remarks>
	/// 一旦初期化されたインスタンスの内部データは一切変更されない事が保障される
	/// </remarks>
	[Serializable]
    public class BuiExp : IEnumerable<Bui>, IEquatable<BuiExp>
    {
        /// <summary>
        /// 部位演算の動作モード
        /// </summary>
        public class OperationMode : ICloneable
        {
            /// <summary>
            /// 旧C++互換モードで動作させる。
            /// </summary>
            public bool IsCppCompatible { get; private set; }

            // IsCppCompatibleを作ったことにより下記のプロパティは廃止
            ///// <summary>
            ///// 演算により歯番部位とそれ以外の部位が混する事を許可しているかどうか(既定値false)
            ///// </summary>
            ///// <remarks>
            ///// falseに設定されている場合に、歯番部位とそれ以外の部位を混在させない為の方法は演算の種類ごとに異なる。
            ///// (paletteに準じる)
            ///// ただし、コンストラクタ等で歯番部位とそれ以外の部位が混ざった部位データを作る事は可能。
            ///// </remarks>
            //public bool AllowMixToothBuiAndOtherBui 
            //{
            //    get
            //    {
            //        // C++互換モードの場合は禁止。そうでない場合は許可
            //        return !IsCppCompatible;
            //    }
            //}

            public OperationMode(bool? isCppCompatible = null)
            {
                IsCppCompatible = isCppCompatible ?? false;   // デフォルトでは互換モード
            }

            public static OperationMode Override(bool? isCppCompatible = null)
            {
                return new OperationMode(isCppCompatible: isCppCompatible);
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        /// <summary>
        /// 部位演算の動作モード
        /// </summary>
        public static OperationMode Mode { get; set; }

        /// <summary>
        /// BuiExp文字列時の区切り文字
        /// </summary>
        private const string BUI_EXP_STRING_SEPARATOR = ",";

        /// <summary>
        /// 内包するBuiのリスト
        /// </summary>
        // private List<Bui> Contents = new List<Bui>(); // 初期状態では空リスト
        private Bui[] Contents;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        /// <remarks>
        /// デフォルトコンストラクタは公開しない。空の部位インスタンスが必要な場合はEmptyプロパティを使用する。
        /// </remarks>
        protected BuiExp()
        {
            Contents = new Bui[] { };
        }

        /// <summary>
        /// 指定したBui1個だけを要素に持つBuiExpを生成
        /// </summary>
        /// <param name="bui">格納する唯一の部位</param>
        public BuiExp(Bui bui)
        {
            Contents = new[] { bui };
        }

        /// <summary>
        /// staticフィールドの初期化(静的コンストラクタ)
        /// </summary>
        static BuiExp()
        {
            // 動作モード
            Mode = new OperationMode(isCppCompatible: true);    // C++互換モード(歯番部位とそれ以外(文字列部位)の混在は禁止)

            // 定義済み空の部位
            Empty = new BuiExp();

            // $パフォーマンス改善$
            
        }

        /// <summary>
        /// 内包する部位の数(非再帰)
        /// </summary>
        public int Count
        {
            get
            {
                return Contents.Length;
            }
        }

        /// <summary>
        /// 指定したインデックス位置にある部位を取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>部位</returns>
        public Bui this[int index]
        {
            get
            {
                return Contents[index];
            }
        }

        //--- public

        /// <summary>
        /// この部位表現が空かどうかを返す
        /// </summary>
        /// <returns>リストが空の場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// 歯のみで構成された部位かどうかを返す
        /// </summary>
        /// <returns>空部位、または、Toothのみで構成された部位の場合はtrue。それ以外の場合はfalseを返す</returns>
        public bool IsAllTooth
        {
            get
            {
                return Contents.All(x => x is Tooth);
            }
        }


        /// <summary>
        /// SimpleTooth(アトリビュート付きの歯)のみで構成された部位かどうかを返す
        /// </summary>
        /// <returns>空部位、または、SimpleToothのみで構成された部位の場合はtrue。それ以外の場合はfalseを返す</returns>
        public bool IsAllSimpleTooth
        {
            get
            {
                return Contents.All(x => x is SimpleTooth);
            }
        }

        /// <summary>
        /// StringBuiだけで構成された部位かどうかを返す
        /// </summary>
        /// <returns>空部位、または、StringBuiだけで構成された部位の場合はtrue。それ以外の場合はfalseを返す</returns>
        public bool IsAllStringBui
        {
            get
            {
                return Contents.All(x => x is StringBui);
            }
        }

        /// <summary>
        /// CPPのIsShibangaiBuiと同じ判別
        /// </summary>
        public bool IsShibangaiBui()
        {
            return !IsEmpty && IsAllStringBui;
        }

        /// <summary>
        /// 指定したBuiの列挙による初期化。同一ToothPosition内での順序も列挙の順序通りに格納される。
        /// </summary>
        /// <param name="query">初期化に使用する列挙</param>
        /// <returns>指定条件の内容で初期化した部位表現</returns>
        public static BuiExp MakeByQuery(IEnumerable<Bui> query)
        {
            var result = new List<Bui>();

            foreach (Bui bui in query)
            {
                int i = IndexOfToInsert(result, bui);
                if (i == -1)
                {
                    result.Add(bui);
                }
                else
                {
                    result.Insert(i, bui);
                }
            }
            return new BuiExp { Contents = result.ToArray<Bui>() };
        }

        /// <summary>
        /// Buiの既定の挿入位置を返す
        /// </summary>
        /// <param name="insertTooth">挿入部位</param>
        /// <returns>
        /// 挿入位置のインデックス。挿入位置が終端(挿入ではなく追加が必要)の場合は-1<br/>
        /// </returns>
        private static int IndexOfToInsert(List<Bui> contents, Bui insertTooth)
        {
            if (0 < contents.Count)
            {
                for (int i = 0; i < contents.Count; ++i)
                {
                    if (insertTooth.HasToothPosition)
                    {
                        // 歯番を持つ部位は、歯番を持たない部位よりは後。
                        // 歯番を持つ部位については同じ歯番ならその一番後(後位の歯番の前)に挿入する。
                        if (contents[i].HasToothPosition && insertTooth.Position < contents[i].Position)
                        {
                            // ToothPositionを持つ部位同士
                            // 後位の歯番を発見したのでその前に挿入
                            return i;
                        }
                    }
                    else if (insertTooth is StringBui)
                    {
                        if (contents[i].HasToothPosition)
                        {
                            // 文字列部位は、既存の文字列部位の最後・・・つまり最初の歯番を持つ部位の前に挿入
                            return i;
                        }
                    }
                    else
                    {
                        if (contents[i] is StringBui || contents[i].HasToothPosition)
                        {
                            // このメソッドで関係が定義されていない部位は、先頭部分の一番最後、
                            // つまり、文字列部位または歯番部位の直前に追加される
                            return i;
                        }
                    }
                }
                return -1; // 挿入位置が見つからなかったらresult == Count(挿入位置は終端)
            }
            else
            {
                // 部位が空なので追加
                return -1;
            }
        }

        /// <summary>
        /// 反復処理する列挙子を返す
        /// </summary>
        /// <returns>反復処理する列挙子</returns>
        public IEnumerator<Bui> GetEnumerator()
        {
            return Contents.Cast<Bui>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 指定されたBuiExpと内容が並び順も含めて同じかどうかを返す
        /// </summary>
        /// <param name="other">比較するBuiExp</param>
        /// <returns> 指定されたBuiExpと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool Equals(BuiExp other)
        {
            // オブジェクトがBuiExp
            BuiExp target = other as BuiExp;

            // 要素数が異なる
            if (Count != target.Count)
            {
                return false;
            }
            else
            {
                // 要素数が同じ
                if (Count == 0)
                {
                    // 両方とも空
                    return true;
                }
                else
                {
                    // 両方とも1個以上だが個数は一致している
                    for (int i = 0; i < Count; ++i)
                    {
                        if (!this[i].Equals(target[i]))
                        {
                            // 不一致が１つでもあればfalseとして終了
                            return false;
                        }
                    }
                    // 全て一致した
                    return true;
                }
            }
        }

        /// <summary>
        /// 指定されたBuiExpと内容が並び順も含めて同じかどうかを返す
        /// </summary>
        /// <param name="other">比較するBuiExp</param>
        /// <returns> 指定されたBuiExpと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public override bool Equals(Object other)
        {
            // オブジェクトがBuiExp
            if (other is BuiExp)
            {
                return this.Equals((BuiExp)other);
            }
            // オブジェクトがBuiExp以外
            else
            {
                return base.Equals(other);
            }
        }

        /// <summary>
        /// 指定した属性種別の「もの」を含んでいるかどうかを調べる
        /// </summary>
        /// <param name="attributes">ToothAttribute値または複数のToothAttribute値を|演算子で組み合わせた値</param>
        /// <returns>指定属性を含んでいる場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool Contains(ToothAttribute attributes)
        {
            return Contents.Any(bui => bui.HasToothAttribute && attributes.HasFlag(bui.Attribute));
        }

        /// <summary>
        /// 指定した位置の部位を含んでいるかどうかを調べる
        /// </summary>
        /// <param name="positions">ToothPosition値または複数のToothPosition値を|演算子で組み合わせた値</param>
        /// <returns>指定した位置の部位を含んでいる場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool Contains(ToothPosition positions)
        {
            return Contents.Any(bui => (bui.HasToothPosition && positions.HasFlag(bui.Position)));
        }

        /// <summary>
        /// 指定した歯番を含んでいるかどうかを調べる
        /// </summary>
        /// <param name="number">ToothNumberを指定</param>
        /// <returns>指定した歯番を含んでいる場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool Contains(ToothNumber number)
        {
            return Contents.OfType<Tooth>().Any(tooth => number.HasFlag(tooth.Number));
        }

        /// <summary>
        /// 指定した部位を含んでいるかどうかを調べる
        /// </summary>
        /// <param name="tooth">部位</param>
        /// <returns>指定した部位を含んでいる場合はtrue、それ以外の場合はfalseを返す</returns>
        public bool Contains(Bui bui)
        {
            return Contents.Any(x => x.Equals(bui));
        }


        /// <summary>
        /// 指定した単位毎に部位を分割する。単位に当てはまらない部位(余り)は切り捨てる<br/>
        /// ※旧take_out()関数の代替として使用する
        /// </summary>
        /// <remarks>
        /// 無条件に部位全体を１個の要素として返す旧C++互換動作の場合で内容が文字列部位の場合で、
        /// 現在の部位が歯番外部位(IsAllStringBui)の場合、さらに部位単位種別が文字列部位でもなく
        /// 両側、片側のどちらでもない場合には、現在の部位全体を１個の要素として格納して返す。
        /// </remarks>
        /// <param name="unitType">部位単位種別</param>
        /// <returns>部位配列</returns>
        public BuiExp[] Split(BuiUnitType unitType)
        {
            if (IsEmpty)
            {
                return new BuiExp[] { };
            }

            // 一口腔単位の場合は、無条件にこの部位全部を返す
            if (unitType == BuiUnitType.Mouth)
            {
                return new BuiExp[] { this };
            }

            // 無条件に部位全体を１個の要素として返す旧C++互換動作の場合で内容が文字列部位の場合で、現在の部位が歯番外部位(IsAllStringBui)の場合、
            // さらに部位単位種別が文字列部位でもなく両側、片側のどちらでもない場合には、現在の部位全体を１個の要素として格納して返す。
            if (Mode.IsCppCompatible
                && IsShibangaiBui()
                &&
                (
                    unitType == BuiUnitType.ToothPosition
                 || unitType == BuiUnitType.ToothNumber
                 || unitType == BuiUnitType.Tooth
                 || unitType == BuiUnitType.Kadomen
                 || unitType == BuiUnitType.OneThird
                 || unitType == BuiUnitType.Jaw
                 || unitType == BuiUnitType.Set
                )
            )
            {
                return new BuiExp[] { this };
            }

            var list = new List<BuiExp>();
            BuiExp work = this;

            switch (unitType)
            {
                // 1歯単位は部位の中の個々の物(歯に限る)
                // unitTypeが穿洞面毎の場合でもこのメソッドは歯毎と同じ動作をする。
                case BuiUnitType.Tooth:
                case BuiUnitType.Kadomen:
                    {
                        // この部位内の個々の歯を1個ずつ格納したBuiExpを作成してリストに格納
                        var query = from bui in Contents.OfType<Tooth>().ToDispOrder() select new BuiExp(bui);
                        list.AddRange(query);
                        break;
                    }

                // 片側（かたそく）
                case BuiUnitType.OneSide:
                    {
                        // 片方の側のみが存在する顎の部位のみ顎単位で取り出す
                        // 歯番を持たない部位は無視する
                        var query = from bui in Contents.OfType<Tooth>() group bui by bui.Position.GetJaw();
                        foreach (var r in query)
                        {
                            if (r.GroupBy(x => x.Position.GetHalfJaw()).Count() == 1)
                            {
                                list.Add(MakeByQuery(r));
                            }
                        }
                        break;
                    }

                // 両側（りょうそく）
                case BuiUnitType.BothSide:
                    // 両方の側が存在する顎の部位のみ顎単位で取り出す
                    // 歯番を持たない部位は無視する
                    {
                        var query = from bui in Contents.OfType<Tooth>() group bui by bui.Position.GetJaw();
                        foreach (var r in query)
                        {
                            if (r.GroupBy(x => x.Position.GetHalfJaw()).Count() == 2)
                            {
                                list.Add(MakeByQuery(r));
                            }
                        }
                        break;
                    }

                // １／３顎単位
                case BuiUnitType.OneThird:
                    {
                        var query = (from bui in Contents.OfType<Tooth>() group bui by bui.Position.GetOneThirdJaw()).OrderBy(g => g.Key);
                        foreach (var r in query)
                        {
                            list.Add(MakeByQuery(r));
                        }
                        break;
                    }


                // 顎(上顎、下顎)単位
                // 装置毎(単純に顎ごとに出力する)
                case BuiUnitType.Jaw:
                case BuiUnitType.Set:
                    {
                        var query = from bui in Contents.OfType<Tooth>() group bui by bui.Position.GetJaw();
                        foreach (var r in query)
                        {
                            list.Add(MakeByQuery(r));
                        }
                        break;
                    }

                // 歯番毎
                case BuiUnitType.ToothNumber:
                    {
                        var query = from bui in Contents.OfType<Tooth>().ToDispOrder() group bui by bui.Number;
                        foreach (var r in query)
                        {
                            list.Add(MakeByQuery(r));
                        }
                        break;
                    }
                // 歯の位置毎
                case BuiUnitType.ToothPosition:
                    {
                        var query = from bui in Contents.OfType<Tooth>().ToDispOrder() group bui by bui.Position;
                        foreach (var r in query)
                        {
                            list.Add(MakeByQuery(r));
                        }
                        break;
                    }
                // ひとつの文字列表現部位
                case BuiUnitType.StringBui:
                    {
                        // 個々のStringBuiを格納したBuiExpのインスタンスを作成してリストに格納
                        var query = from bui in Contents.OfType<StringBui>() select new BuiExp(bui);
                        list.AddRange(query);
                        break;
                    }
                default:
                    throw new NotImplementedException(string.Format("指定された部位単位種別{0}に対するSplit処理が定義されていない。", unitType.ToString()));
            }

            return list.ToArray<BuiExp>();
        }

        /// <summary>
        /// 指定インデックスの位置にあるオブジェクトを削除した新しいインスタンスを生成
        /// </summary>
        /// <param name="index">削除位置</param>
        /// <returns>変更済みの新しいインスタンス</returns>
        /// <exception cref="ArgumentException">存在しない削除位置を指定した場合に発生</exception>
        public BuiExp Remove(int index)
        {
            // 存在しない要素を指している場合はエラーとする
            if (index < 0 || !(index < Count))
            {
                throw new ArgumentException("indexが存在しない要素を指している");
            }

            var result = new List<Bui>();

            for (int i = 0; i < Count; ++i)
            {
                if (i != index)
                {
                    // 指定インデックス以外の部位を追加
                    result.Add(Contents[i]);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// Splitで分割された個々の部位について指定された二つの部位(unitTypeでsplitされた1つの要素でなければならない)が同じ同じ単位内の部位かどうかを判定
        /// </summary>
        /// <remarks>
        /// </remarks>
        private static bool IsSameUnitRestrictedToSplitResult(BuiExp unit1, BuiExp unit2, BuiUnitType unitType)
        {
            // 歯式の中の一つの物毎に数える単位、穿洞面毎
            // Attributeも判別する
            if (unitType == BuiUnitType.Tooth || unitType == BuiUnitType.Kadomen)
            {
                Bui bui1 = unit1.Single();
                Bui bui2 = unit2.Single();
                if (!(bui1 is Tooth) || !(bui2 is Tooth))
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                return bui1.Equals(bui2);
            }
            else if (unitType == BuiUnitType.ToothNumber)
            {
                // 歯番が一致するかどうか
                Bui bui1 = unit1.First();
                Bui bui2 = unit2.First();
                if (!bui1.HasToothNumber || !bui2.HasToothNumber)
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                return bui1.Number == bui2.Number;
            }
            else if (unitType == BuiUnitType.ToothPosition)
            {
                // 歯の位置が一致するかどうか
                Bui bui1 = unit1.First();
                Bui bui2 = unit2.First();
                if (!bui1.HasToothPosition || !bui2.HasToothPosition)
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                return bui1.Position == bui2.Position;
            }
            else if (unitType == BuiUnitType.OneThird)
            {
                // 1/3顎単位
                var tooth1 = unit1.First();
                var tooth2 = unit2.First();

                if (!tooth1.HasToothPosition || !tooth2.HasToothPosition)
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                return tooth1.Position.GetOneThirdJaw() == tooth2.Position.GetOneThirdJaw();
            }
            else if (unitType == BuiUnitType.Jaw || unitType == BuiUnitType.OneSide || unitType == BuiUnitType.BothSide)
            {
                // 顎(上顎、下顎)単位、片側（かたそく）、両側（りょうそく）
                var tooth1 = unit1.First();
                var tooth2 = unit2.First();

                if (!tooth1.HasToothPosition || !tooth2.HasToothPosition)
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                if (unitType == BuiUnitType.OneSide)
                {
                    return tooth1.Position.GetHalfJaw() == tooth2.Position.GetHalfJaw();
                }
                else
                {
                    return tooth1.Position.GetJaw() == tooth2.Position.GetJaw();
                }
            }
            else if (unitType == BuiUnitType.Set)
            {
                // 装置毎
                // 装置単位は顎単位で歯式が一致するかどうかで判定する
                // 二つの装置に重なる部分があるかどうか調べたい場合は
                // BuiUnitType.Toothを使う必要がある
                if (unit1.IsShibangaiBui() || unit2.IsShibangaiBui())
                {
                    // どちらかが歯番外部位の場合、装置単位での判定は不能なのでfalseを返す
                    return false;
                }
                return unit1 == unit2;
            }
            else if (unitType == BuiUnitType.Mouth)
            {
                // 口腔全体(部位全体)
                // 一口腔単位は患者単位の意味なので、部位が存在する限り
                // 常に交差する
                return true;
            }
            else if (unitType == BuiUnitType.StringBui)
            {
                // ひとつの文字列表現部位毎
                var str1 = unit1.First() as StringBui;
                var str2 = unit2.First() as StringBui;
                if (str1 == null || str2 == null)
                {
                    // 歯番外部位が含まれていた場合には起こりうる状態。
                    // 判定不能なのでfalseを返す
                    return false;
                }
                return str1.Equals(str2);
            }
            else
            {
                throw new ArgumentOutOfRangeException("不明なBuiUnitType");
            }
        }

        /// <summary>
        /// 指定単位毎に見て2つの部位に交差する部分があるかどうか
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>交差部分があるかどうか</returns>
        public static bool IsExistSameUnit(BuiExp left, BuiExp right, BuiUnitType unitType)
        {
            BuiExp[] leftContaints = left.Split(unitType);
            BuiExp[] rightContaints = right.Split(unitType);

            foreach (BuiExp leftUnit in leftContaints)
            {
                // rightContaintsのいずれすかのユニットがleftunitと同一かどうか
                if (rightContaints.Any(rightUnit => IsSameUnitRestrictedToSplitResult(leftUnit, rightUnit, unitType)))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 指定単位毎に見てbuiと交差する部分を抽出
        /// </summary>
        /// <param name="bui">抽出条件部位</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>交差部分があるかどうか</returns>
        public BuiExp GetCrossUnitByOldMethod(BuiExp bui, BuiUnitType unit)
        {
            var list = new List<Bui>();

            if (IsShibangaiBui() || bui.IsShibangaiBui())
            {
                // 右辺、左辺のどちらかが歯番外部位、または、両方とも歯番外部位の場合
                return this & bui;  // 互換動作の&演算子の動き
            }
            else
            {
                BuiExp[] leftContaints = this.Split(unit);
                BuiExp[] rightContaints = bui.Split(unit);

                foreach (BuiExp leftUnit in leftContaints)
                {
                    if (unit == BuiUnitType.Set)
                    {
                        // 装置の場合C++では装置単位の判定ではなく属性を含めて同一の歯があるかどうかで判断していた
                        // 交差部分としては、本当の交差部分ではなく左側の装置を追加する
                        if (rightContaints.Any(rightUnit => !((leftUnit & rightUnit).IsEmpty)))
                        {

                            list.AddRange(leftUnit);    // 左側(の装置)を追加
                        }
                    }
                    else if (unit == BuiUnitType.Tooth || unit == BuiUnitType.Kadomen)
                    {
                        // 歯番で見て交差する部分があるかどうか(C++に合わせた動作)
                        var leftNumbers = leftUnit.GetToothNumbers();
                        if (rightContaints.Any(x => (leftNumbers & x.GetToothNumbers()) != 0))
                        {
                            list.AddRange(leftUnit);
                        }
                    }
                    else
                    {
                        // rightContaintsのいずれかのユニットがleftunitと同一かどうか
                        if (rightContaints.Any(rightUnit => IsSameUnitRestrictedToSplitResult(leftUnit, rightUnit, unit)))
                        {
                            list.AddRange(leftUnit);
                        }
                    }
                }
                return BuiExp.MakeByQuery(list);
            }
        }

        /// <summary>
        /// 指定単位毎に見てbuiと交差する部分を抽出
        /// </summary>
        /// <param name="bui">抽出条件部位</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>交差部分があるかどうか</returns>
        /// <remarks>自費見積書発行で追加されたメソッド。C#移植直後では自費見積書発行専用のため、今後使う必要が出た場合は要相談</remarks>
        public BuiExp GetCrossUnit(BuiExp bui, BuiUnitType unit)
        {
            var list = new List<Bui>();

            BuiExp[] leftContaints = this.Split(unit);
            BuiExp[] rightContaints = bui.Split(unit);

            foreach (BuiExp leftUnit in leftContaints)
            {
                // rightContaintsのいずれかのユニットがleftunitと同一かどうか
                if (rightContaints.Any(rightUnit => IsSameUnitRestrictedToSplitResult(leftUnit, rightUnit, unit)))
                {
                    list.AddRange(leftUnit);
                }
            }
            return BuiExp.MakeByQuery(list);
        }

        // Ver1.067(2017/08/28)分割歯に単冠を作製する場合の支台築造（ファイバーポスト）の算定について(Bug xxxxx)
        /// <summary>
        /// 部位オブジェクトから重複歯番の部位のみ取得する
        /// </summary>
        /// <returns></returns>
        public BuiExp GetDuplicatedBui()
        {
            BuiExp duplicatedBui = BuiExp.Empty;

            // 永久歯・乳歯の全ての歯番をチェックする
            foreach (var bui in BuiExp.AllNormal)
            {
                BuiExp buiExp = new BuiExp(bui);

                // 自身の部位と比較用の歯番を照らし合わせる
                BuiExp work = this & buiExp;
                if (1 < work.Count)
                {
                    // 論理積の結果が２歯以上の場合は重複歯番
                    duplicatedBui |= work;
                }
            }
            return duplicatedBui;
        }

#if false
        /// <summary>
        /// (旧C++互換メソッド)指定単位毎に見て2つの部位に交差する部分があるかどうか(旧C++互換のメソッド)
        /// </summary>
        /// <remarks>
        /// 旧C++プログラムの同等メソッドと同様の動きにするために、unitTypeがKadomen,Tooth,Setの場合にToothNumberが指定されたとみなす。
        /// </remarks>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>交差部分があるかどうか</returns>
        public static bool IsExistSameUnitByOldMethod(BuiExp left, BuiExp right, BuiUnitType unitType)
        {
            if (unitType == BuiUnitType.Kadomen || unitType == BuiUnitType.Tooth || unitType == BuiUnitType.Set)
            {
                unitType = BuiUnitType.ToothNumber;
            }
            return IsExistSameUnit(left, right, unitType);
        }

        /// <summary>
        /// (旧C++互換メソッド)指定単位毎に見てbuiと交差する部分を抽出
        /// </summary>
        /// <remarks>
        /// 旧C++プログラムの同等メソッドと同様の動きにするために、unitTypeがKadomen,Tooth,Setの場合にToothNumberが指定されたとみなす。
        /// </remarks>
        /// <param name="bui">抽出条件部位</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>交差部分があるかどうか</returns>
        public BuiExp GetCrossUnitByOldMethod(BuiExp bui, BuiUnitType unitType)
        {
            if (unitType == BuiUnitType.Kadomen || unitType == BuiUnitType.Tooth || unitType == BuiUnitType.Set)
            {
                unitType = BuiUnitType.ToothNumber;
            }
            return GetCrossUnit(bui, unitType);
        }
#endif

        /// <summary>
        /// 指定された単位を一意に表すオブジェクトを取得
        /// </summary>
        /// <param name="unitBui">unitType毎にsplit済みの１部位</param>
        /// <param name="unitType">unitType</param>
        /// <returns>unitTypeを一意に識別するEqualsで同一性が識別可能、かつ、IComparable(大小比較可能)なobject</returns>
        private static object GetBuiUnitID(BuiExp unitBui, BuiUnitType unitType)
        {
            if (unitType == BuiUnitType.StringBui)
            {
                return unitBui;
            }
            else
            {
                // C++互換モードの場合はSplitの動作をtake_outに合わせたため、歯に関する単位を指定しても歯番外部位全体を１個の単位要素として出てきてしまう
                if (unitBui.IsShibangaiBui())
                {
                    return unitBui; // 歯番外部位は全体を一つの単位要素として扱う
                }
                else
                {

                    if (unitType == BuiUnitType.Kadomen || unitType == BuiUnitType.Tooth)
                    {
                        // 1歯毎の場合は、同じ歯番が複数ある場合はそれぞれそれぞれ１個と数える
                        return unitBui.Select(x => x.Number).ToArray();
                    }
                    else if (unitType == BuiUnitType.ToothNumber)
                    {
                        // 歯番毎の場合は、同じ歯番は１個と数える
                        return unitBui.First().Number;
                    }
                    else if (unitType == BuiUnitType.ToothPosition)
                    {
                        // 歯の位置毎の場合は、同じ位置は１個と数える
                        return unitBui.First().Position;
                    }
                    else if (unitType == BuiUnitType.Mouth)
                    {
                        // １口腔単位なので何でも良いから存在すれば全て同じとみなす
                        return 1;
                    }
                    else if (unitType == BuiUnitType.OneThird)
                    {
                        // 1/3顎を識別
                        return (unitBui.First() as Tooth).OneThirdJaw;
                    }
                    else if (unitType == BuiUnitType.OneSide)
                    {
                        // 上下顎それぞれの右側・左側(1/4顎)を識別
                        return (unitBui.First() as Tooth).HalfJaw;
                    }
                    else if (unitType == BuiUnitType.Jaw || unitType == BuiUnitType.BothSide || unitType == BuiUnitType.OneSide)
                    {
                        // 上顎・下顎を識別
                        return (unitBui.First() as Tooth).Jaw;
                    }
                    else if (unitType == BuiUnitType.Set)
                    {
                        return unitBui;
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }
        }

        /// <summary>
        /// 指定単位毎に見て2つの部位が全く同じかどうか
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <param name="unitType">比較単位</param>
        /// <returns>指定単位毎に見て2つの部位が全く同じかどうか</returns>
        public static bool EqualsByUnit(BuiExp left, BuiExp right, BuiUnitType unitType)
        {
            // 右側と左側に含まれる部位をunitType毎に分割し、unitType毎の同一性を識別可能なIDとなるオブジェクトを取得する
            var leftContaints = left.Split(unitType).Select(y => BuiExp.GetBuiUnitID(y, unitType)).ToArray();
            var rightContaints = right.Split(unitType).Select(y => BuiExp.GetBuiUnitID(y, unitType)).ToArray();

            // 左側の全てのunitが並び順も含めて右側の全てのunitと一致するかどうか
            if (leftContaints.Count() != rightContaints.Count())
            {
                return false;
            }
            else
            {
                for (int i = 0; i < leftContaints.Count(); ++i)
                {
                    var leftUnit = leftContaints[i];
                    var rightUnit = rightContaints[i];
                    if (leftUnit is BuiExp && rightUnit is BuiExp)
                    {
                        if (!leftUnit.Equals(rightUnit))
                        {
                            return false;
                        }
                    }
                    else if (leftUnit is IEnumerable<ToothNumber> && rightUnit is IEnumerable<ToothNumber>)
                    {
                        if (!(leftUnit as IEnumerable<ToothNumber>).SequenceEqual((rightUnit as IEnumerable<ToothNumber>)))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!leftUnit.Equals(rightUnit))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 右辺に含まれるBuiから左辺に含まれるBuiを指定単位毎に削除<br/>
        /// (旧nontypeRemove関数の代替)
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <param name="unitType">削除単位指定</param>
        /// <returns>変更済みの新しいインスタンス</returns>
        public static BuiExp RemoveByToothNumber(BuiExp left, BuiExp right)
        {
            var result = new List<Bui>();
            ToothNumber rightNumbers = right.GetToothNumbers(); // 含まれる歯番のFlags

            foreach (var bui in left)
            {
                if (!bui.HasToothNumber || !rightNumbers.HasFlag(bui.Number))
                {
                    // 歯番を持たないか、rightに含まれない歯番の部位のみresultに格納
                    result.Add(bui);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        ///  この部位のハッシュコードを返す
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            int hashCode = Contents.Length.GetHashCode();
            foreach (var x in Contents)
            {
                hashCode ^= x.GetHashCode();
            }
            return hashCode;
        }

        /// <summary>
        /// 部位文字列を返す<br/>
        /// (Object.ToStringをオーバーライドする)
        /// </summary>
        /// <returns>部位文字列</returns>
        public override string ToString()
        {
            if (IsEmpty)
            {
                // 部位が空の場合は空文字列を返す
                return string.Empty;
            }
            else
            {
                StringBuilder builder = new StringBuilder();

                foreach (var bui in Contents)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(BUI_EXP_STRING_SEPARATOR);
                    }
                    builder.Append(bui);
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// 部位文字列からBuiExpクラスに変換する
        /// </summary>
        /// <param name="source">部位文字列</param>
        /// <returns>BuiExpクラス</returns>
        public static BuiExp Parse(string source)
        {
            if (source.Length == 0)
            {
                return Empty;
            }
            else
            {
                var list = new List<Bui>();

                string[] strBuiList = source.Split(
                        new string[] { BUI_EXP_STRING_SEPARATOR }, StringSplitOptions.None);

                foreach (var strBui in strBuiList)
                {
                    if (string.IsNullOrEmpty(strBui))
                    {
                        throw new ApplicationException("部位文字列に空の要素が含まれている");
                    }
                    else if (strBui[0] == StringBui.HEADER_CHAR)
                    {
                        list.Add(StringBui.Parse(strBui));
                    }
                    // 歯番部位
                    else
                    {
                        list.Add(SimpleTooth.Parse(strBui));
                    }
                }
                return MakeByQuery(list);
            }
        }

        /// <summary>
        /// 等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>等値かどうか</returns>
        public static bool operator ==(BuiExp left, BuiExp right)
        {
            if (Object.ReferenceEquals(left, right))
            {
                // 同じインスタンスへの参照か、両方ともnullの場合
                return true;
            }
            else if (Object.ReferenceEquals(left, null) || Object.ReferenceEquals(right, null))
            {
                // どちらか一方がnull
                return false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// 不等値演算子
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>不等値ならtrue, 等値ならfalse</returns>
        public static bool operator !=(BuiExp left, BuiExp right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 加算演算子:二つのBuiExpの内容を既定の順序で連結した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>加算結果を表すBuiExpインスタンス</returns>
        /// <remarks>
        /// [OperationMode]
        /// [既定の順序]<br/>
        ///   同じ歯番同士の足し算は、右辺を左辺の外側へ挿入する<br/>
        ///   　[左上 12345] + [上顎 54321|123] = [上顎 54321|11223345]<br/>
        ///   　[下顎 (5)43(2)1|123] + [下顎 (4)32|(1)(3)] = [下顎 (5)(4)4332(2)1|1(1)23(3)]<br/>
        ///   <br/>
        ///   歯番が関係ない部位同士の連結は単に右辺の内容を左辺の終端に追加する
        /// </remarks>
        public static BuiExp operator +(BuiExp left, BuiExp right)
        {
            if (right.IsEmpty)
            {
                return left;
            }
            else
            {
                List<Bui> result = left.Contents.ToList();

                bool existTooth = left.Any(x => x is Tooth);
                bool existOther = left.Any(x => !(x is Tooth));

                foreach (Bui bui in right)
                {
                    if (Mode.IsCppCompatible)
                    {
                        // 旧C++互換の時は歯番部位と歯番以外の部位の混在を禁止する
                        if (bui is Tooth)
                        {
                            if (existOther)
                            {
                                continue;   // 既に歯番以外の部位があるのでこの部位はスキップする
                            }
                            else
                            {
                                existTooth = true;
                            }
                        }
                        else
                        {
                            if (existTooth)
                            {
                                continue;   // 既に歯番部位があるのでこの部位はスキップする
                            }
                            else
                            {
                                existOther = true;
                            }
                        }
                    }

                    int insertPos = IndexOfToInsert(result, bui);
                    if (insertPos == -1)
                    {
                        result.Add(bui);
                    }
                    else
                    {
                        result.Insert(insertPos, bui);
                    }
                }
                return MakeByQuery(result);
            }
        }

        /// <summary>
        /// 部位リストの合算処理
        /// ■注意点■
        /// この関数を使用した場合は必ず最後にMakeByQueryを実行すること
        /// </summary>
        /// <remarks>
        /// ループ処理で+演算子で合算するとMakeByQueryが何度も呼ばれて処理コストがかかるため、
        /// MakeByQueryを使用しない関数として作成
        /// </remarks>
        /// <param name="list">合算対象部位リスト</param>
        /// <param name="targetBui">対象部位</param>
        public static void SumBuiList(List<Bui> list, BuiExp targetBui)
        {
            if (targetBui.IsEmpty)
            {
                
            }
            else
            {
                bool existTooth = list.Any(x => x is Tooth);
                bool existOther = list.Any(x => !(x is Tooth));

                foreach (Bui bui in targetBui)
                {
                    if (Mode.IsCppCompatible)
                    {
                        // 旧C++互換の時は歯番部位と歯番以外の部位の混在を禁止する
                        if (bui is Tooth)
                        {
                            if (existOther)
                            {
                                continue;   // 既に歯番以外の部位があるのでこの部位はスキップする
                            }
                            else
                            {
                                existTooth = true;
                            }
                        }
                        else
                        {
                            if (existTooth)
                            {
                                continue;   // 既に歯番部位があるのでこの部位はスキップする
                            }
                            else
                            {
                                existOther = true;
                            }
                        }
                    }

                    list.Add(bui);
                }
            }
        }

        /// <summary>
        /// 減算演算子：左辺BuiExpから右辺BuiExpに含まれる部位を全て削除した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>減算結果の新しいBuiExpインスタンス</returns>
        /// <remarks>
        /// 左辺から、右辺に含まれる「もの」を除いた歯式を返す。<br/>
        /// 　[上顎 321|12345] - [上顎 54321|123] = [左上 45]<br/>
        /// 　[右上 333] - [右上 33] = [右上 3]<br/>
        /// 　[右上 33] - [右上 33] = []<br/>
        /// 　[右上 33] - [右上 333] = []
        /// </remarks>
        public static BuiExp operator -(BuiExp left, BuiExp right)
        {
            if (Mode.IsCppCompatible && ((left.IsShibangaiBui() || right.IsShibangaiBui()) && (!left.IsShibangaiBui() || !right.IsShibangaiBui())))
            {
                // 旧C++のロジックで動いている場合、どちらか片方だけが『文字列部位のみ』の場合は無条件に左辺を返す
                return left;
            }
            else
            {
                if (left.IsEmpty || right.IsEmpty)
                {
                    return left;
                }
                else
                {
                    var result = left.Contents.ToList();

                    foreach (Bui bui in right)
                    {
                        // 右辺にある部位と同じ左辺の部位を全て削除
                        while (result.Contains(bui))
                        {
                            result.Remove(bui);
                        }
                    }
                    return MakeByQuery(result);
                }
            }
        }

        /// <summary>
        /// 加算演算子：左辺BuiExpに右辺Buiを追加した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>加算結果を格納した新しいBuiExpインスタンス</returns>
        public static BuiExp operator +(BuiExp left, Bui right)
        {
            if (Mode.IsCppCompatible)
            {
                // 旧C++互換モードでは歯番とそれ以外の部位の混在が禁止されている
                if (right is Tooth)
                {
                    // rightは歯番
                    if (left.Any(x => !(x is Tooth)))
                    {
                        // leftに歯以外の部位が存在するので追加できない
                        return left;    // leftをそのまま返す
                    }
                }
                else
                {
                    // rightは歯番以外
                    if (left.Any(x => x is Tooth))
                    {
                        // leftに歯の部位が存在するので追加できない
                        return left;    // leftをそのまま返す
                    }
                }
            }

            List<Bui> result = left.Contents.ToList();

            int insertPos = IndexOfToInsert(result, right);
            if (insertPos == -1)
            {
                result.Add(right);
            }
            else
            {
                result.Insert(insertPos, right);
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 加算演算子：右辺Buiを唯一の要素に持つBuiExpに左辺BuiExpを追加した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値(SimpleTooth, StringBui等)</param>
        /// <param name="right">右辺値(BuiExp)</param>
        /// <returns>加算結果を格納した新しいBuiExpインスタンス</returns>
        public static BuiExp operator +(Bui left, BuiExp right)
        {
            var add = right.AsEnumerable();
            if (Mode.IsCppCompatible)
            {
                // 旧C++互換モードでは、歯番とそれ以外の部位の混在が禁止されている
                if (left is Tooth)
                {
                    // rightが歯番なので歯番のみを追加対象にする
                    add = right.OfType<Tooth>();
                }
                else
                {
                    // rightは歯番以外なので歯番以外のみを追加対象にする
                    add = right.Where(x => !(x is Tooth));
                }
            }

            List<Bui> result = new List<Bui>(new[] { left });
            foreach (Bui bui in add)
            {
                int insertPos = IndexOfToInsert(result, bui);
                if (insertPos == -1)
                {
                    result.Add(bui);
                }
                else
                {
                    result.Insert(insertPos, bui);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 減算演算子：左辺BuiExpから右辺Buiを全て削除した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>減算結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator -(BuiExp left, Bui right)
        {
            if (left.IsEmpty)
            {
                return left;
            }
            else
            {
                var result = left.Contents.ToList();

                // 右辺にある部位と同じ左辺の部位を全て削除
                while (result.Contains(right))
                {
                    result.Remove(right);
                }
                return MakeByQuery(result);
            }
        }

        /// <summary>
        /// 減算演算子：BuiExpから指定歯番を削除した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="position">歯番</param>
        /// <returns>減算結果の新しいBuiExpインスタンス</returns>
        /// <remarks>
        /// 歯式から該当する歯番を削除した歯式を返す<br/>
        /// 　[上顎 3321|12345] - [右上3] = [上顎 21|12345]
        /// </remarks>
        public static BuiExp operator -(BuiExp buiExp, ToothPosition positions)
        {
            var result = new List<Bui>();

            foreach (Bui bui in buiExp)
            {
                if (bui.HasToothPosition)
                {
                    if (!positions.HasFlag(bui.Position))
                    {
                        result.Add(bui);
                    }
                }
                else
                {
                    result.Add(bui);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 減算演算子：BuiExpから指定属性を削除した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="attributes">ToothAttribute値または複数のToothAttribute値を|演算子で組み合わせた値</param>
        /// <returns>減算結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator -(BuiExp buiExp, ToothAttribute attributes)
        {
            var result = new List<Bui>();

            foreach (Bui bui in buiExp)
            {
                if (bui.HasToothPosition)
                {
                    // この部位の属性が、指定されたattributesに含まれない場合のみ追加
                    if (!attributes.HasFlag(bui.Attribute))
                    {
                        result.Add(bui);
                    }
                }
                else
                {
                    result.Add(bui);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 減算演算子：BuiExpから指定した歯番を削除した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="number">削除対象の歯番を表すToothインスタンス</param>
        /// <returns>減算結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator -(BuiExp buiExp, ToothNumber numbers)
        {
            var result = new List<Bui>();

            foreach (Bui bui in buiExp)
            {
                if (bui.HasToothPosition)
                {
                    if (!numbers.HasFlag(bui.Number))
                    {
                        // 歯番が一致しないbuiのみ追加
                        result.Add(bui);
                    }
                }
                else
                {
                    result.Add(bui);
                }
            }
            return MakeByQuery(result);
        }


        /// <summary>
        /// 論理積演算子：左辺BuiExpと右辺の両方に含まれるBuiを抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>論理積結果の新しいBuiExpインスタンス</returns>
        /// <remarks>
        /// 左辺と右辺の両方に含まれる「もの」によって構成される歯式を返す。<br/>
        /// 　[左上 12345] & [上顎 54321|123] = [左上 123]<br/>
        /// 　[下顎 (5)43(2)1|123] & [下顎 (4)32|(1)(3)] = [右下 3]
        /// 並び順と個数は、左辺は保存されるが右辺での並び順と個数は無視される
        /// 　[下顎 (6)(6)5(4)(3)|] & [下顎 (6)55] = [下顎 (6)(6)5|]
        /// </remarks>
        public static BuiExp operator &(BuiExp left, BuiExp right)
        {
            if (Mode.IsCppCompatible && ((left.IsShibangaiBui() || right.IsShibangaiBui()) && (!left.IsShibangaiBui() || !right.IsShibangaiBui())))
            {
                // 旧C++のロジックで動いている場合、どちらか片方だけが『文字列部位のみ』の場合は無条件に左辺を返す
                return left;
            }
            else
            {
                if (left.IsEmpty || right.IsEmpty)
                {
                    return Empty;
                }
                else
                {
                    var result = new List<Bui>();

                    foreach (Bui bui in left)
                    {
                        // 左辺に含まれるBuiが右辺にも存在したら追加
                        if (right.Contains(bui))
                        {
                            result.Add(bui);
                        }
                    }
                    return MakeByQuery(result);
                }
            }
        }

        /// <summary>
        /// 論理積演算子：左辺BuiExpから右辺に指定した部位のみを抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="right">削除する部位</param>
        /// <returns>論理積結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator &(BuiExp buiExp, Bui right)
        {
            var result = new List<Bui>();

            foreach (Tooth bui in buiExp.OfType<Tooth>())
            {
                // 同じ部位のみ追加
                if (bui == right)
                {
                    result.Add(bui);
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 論理積演算子：左辺BuiExpから指定位置の歯を抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="positions">歯番</param>
        /// <returns>論理積結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator &(BuiExp buiExp, ToothPosition positions)
        {
            var result = new List<Bui>();

            var query = from x in buiExp where x.HasToothPosition select x;
            foreach (Bui bui in query)
            {
                // 指定された位置の部位のみ追加
                if (positions.HasFlag(bui.Position))
                {
                    result.Add(bui);
                }
            }

            return MakeByQuery(result);
        }

        /// <summary>
        /// 論理積演算子：左辺BuiExpから指定位置の歯を抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="positions">歯番</param>
        /// <returns>論理積結果の新しいBuiExpインスタンス</returns>
        public static BuiExp operator &(BuiExp buiExp, ToothNumber numbers)
        {
            var result = new List<Bui>();

            var query = from x in buiExp where x.HasToothNumber select x;
            foreach (Bui bui in query)
            {
                // 指定された位置の部位のみ追加
                if (numbers.HasFlag(bui.Number))
                {
                    result.Add(bui);
                }
            }

            return MakeByQuery(result);
        }

        /// <summary>
        /// 論理積演算子：左辺BuiExpから指定属性を抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="buiExp">部位表現クラス</param>
        /// <param name="attributes">属性</param>
        /// <returns>論理積結果の新しいBuiExpインスタンス</returns>
        /// <remarks>歯の属性を持たないクラスの部位は全て除外される</remarks>
        public static BuiExp operator &(BuiExp buiExp, ToothAttribute attributes)
        {
            var result = new List<Bui>();

            foreach (Bui bui in buiExp)
            {
                if (bui.HasToothAttribute)
                {
                    // 属性がattributesに含まれる場合のみ追加
                    if (attributes.HasFlag(bui.Attribute))
                    {
                        result.Add(bui);
                    }
                }
            }
            return MakeByQuery(result);
        }


        /// <summary>
        /// 論理和演算子：左辺値に、左辺値に含まれない右辺の「部位」追加したBuiExpの値を返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>左辺に、左辺に含まれない右辺の「部位」追加した新しいBuiExpのインスタンス</returns>
        /// <remarks>
        /// ただし、両方に含まれる「部位」は一つの「部位」にまとめられる。<br/>
        /// 　[左上 12345] | [上顎 54321|123] = [左上 54321|12345]<br/>
        /// 　[下顎 (5)43(2)1|123] + [下顎 (4)32|(1)(3)] = [下顎 (5)(4)3(2)21|1(1)23(3)]
        /// 右辺が空の場合は左辺。
        /// C++に動作を合わせるために左が空なら右側を返す。
        /// </remarks>
        public static BuiExp operator |(BuiExp left, BuiExp right)
        {
            if (right.IsEmpty)
            {
                return left;
            }
            else if (left.IsEmpty)
            {
                return right;
                // C++に合わせるために左が空なら右側を返す。
                // 結果rightに同一歯が複数本数ある場合に、leftが空の時だけrightの本数が保存される。
            }
            else
            {
                if (Mode.IsCppCompatible)
                {
                    if (!(left.IsShibangaiBui() && right.IsShibangaiBui())
                            && (left.IsShibangaiBui() || right.IsShibangaiBui())
                        )
                    {
                        // 旧C++互換モードで、どちらか一方の部位が歯番外部位の場合(歯番外部位とそうでない部位が混在してしまう場合)
                        // 歯番部位と歯番外部位が混在しないC++と互換の動作(元々混在している場合は除く)
                        if (left.IsShibangaiBui())
                        {
                            // 左辺が歯番外部位の場合は左辺を返す
                            return left;
                        }
                        else if (left.IsEmpty)
                        {
                            // 左辺が歯番外部位以外の場合、もし空なら右辺を返す
                            return right;
                        }
                        else
                        {
                            // 左辺が空でない限りは左辺を返す
                            return left;
                        }
                    }
                    else if (left.IsAllTooth && right.IsAllTooth)
                    {
                        // left,rightとも歯番部位のみ
                        // C++同様の動作を再現する

                        var result = new List<Bui>(left);

                        // C++と同様の動作：「resultに」ではなく、leftに元々無かったBuiはRightに何本あっても全部追加する
                        foreach (var bui in right)
                        {
                            if (!left.Contains(bui))
                            {
                                result.Add(bui);
                            }
                        }
                        return BuiExp.MakeByQuery(result);
                    }
                    else
                    {
                        // 両方歯番外部位
                        // または、rightとleftのどちらかまたは両方が、歯番と文字列部位が混在する部位の場合(混在部位がある場合は互換動作はできない)
                        return InnerOpratorOr(left, right);
                    }
                }
                else
                {
                    return InnerOpratorOr(left, right);
                }
            }
        }

        private static BuiExp InnerOpratorOr(BuiExp left, BuiExp right)
        {
            List<Bui> result = left.Contents.ToList();

            bool existTooth = left.Any(x => x is Tooth);
            bool existOther = left.Any(x => !(x is Tooth));

            foreach (Bui bui in right)
            {
                // 同じ部位が存在しなければ追加
                if (!result.Contains(bui))
                {
                    int insertPos = IndexOfToInsert(result, bui);
                    if (insertPos == -1)
                    {
                        result.Add(bui);
                    }
                    else
                    {
                        result.Insert(insertPos, bui);
                    }
                }
            }
            return MakeByQuery(result);
        }

        /// <summary>
        /// 排他的論理和演算子：左辺BuiExpと右辺のどちらか一方にのみに含まれるものを抽出した新しいBuiExpのインスタンスを返す
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>排他的論理和結果の新しいBuiExpインスタンス</returns>
        /// <remarks>
        /// 右辺か左辺のどちらか一方にのみ含まれる「もの」で構成された歯式を返す。<br/>
        /// 　[左上 12345] ^ [上顎 54321|123] = [上顎 54321|45]<br/>
        /// 　[下顎 (5)43(2)1|123] + [下顎 (4)32|(1)(3)] = [下顎 (5)4(4)(2)21|1(1)23(3)]
        /// </remarks>
        public static BuiExp operator ^(BuiExp left, BuiExp right)
        {
            if (left.IsEmpty && right.IsEmpty)
            {
                return Empty;
            }
            else if (left.IsEmpty)
            {
                return right;
            }
            else if (right.IsEmpty)
            {
                return left;
            }
            else
            {
                if (Mode.IsCppCompatible)
                {
                    BuiExp dbx1 = left | right;
                    BuiExp dbx2 = left & right;

                    return dbx1 - dbx2;
                }
                else
                {
                    // 排他後の和を返す
                    var query1 = from x in left where !right.Contains(x) select x;  // left内の部位の内、rightに含まれないもの
                    var query2 = from x in right where !left.Contains(x) select x;  // right内の部位の内、leftに含まれないもの
                    return BuiExp.MakeByQuery(query1.Concat(query2));
                }
            }
        }

        /// <summary>
        /// 部位の単位数を取得する
        /// </summary>
        /// <param name="bui">部位</param>
        /// <param name="unitType">部位単位</param>
        /// <returns>部位単位数</returns>
        public static int operator /(BuiExp bui, BuiUnitType unitType)
        {
            return bui.Split(unitType).Count();
        }

        // 定義済みインスタンスの定義
        // 定義済の空の部位 /////////////////////////////////////////

        /// <summary>空部位</summary>
        public static readonly BuiExp Empty;

        // 定義済みの連続した通常歯番のみの部位

        // 口腔全体 /////////////////////////////////////////

        // $パフォーマンス改善$
        /// <summary>全歯列</summary>
        private static BuiExp AllNormalValue;

        /// <summary>全永久歯列</summary>
        private static BuiExp AllNormalEikyushiValue;
        /// <summary>全乳歯列</summary>
        private static BuiExp AllNormalNyushiValue;


        // 上下顎単位 /////////////////////////////////////////

        /// <summary>上顎歯列</summary>
        private static BuiExp AllNormalUpperTeethValue;
        /// <summary>下顎歯列</summary>
        private static BuiExp AllNormalLowerTeethValue;

        /// <summary>上顎永久歯列</summary>
        private static BuiExp AllNormalUpperEikyushiValue;
        /// <summary>下顎永久歯列</summary>
        private static BuiExp AllNormalLowerEikyushiValue;

        // VerX.XXX 多数歯欠損対応 (2019/05/07 堤)
        /// <summary>上顎永久歯列 ※8番を除く</summary>
        private static BuiExp AllNormalUpperEikyushiExceptNo8Value;
        /// <summary>下顎永久歯列 ※8番を除く</summary>
        private static BuiExp AllNormalLowerEikyushiExceptNo8Value;

        /// <summary>上顎乳歯列</summary>
        private static BuiExp AllNormalUpperNyushiValue;
        /// <summary>下顎乳歯列</summary>
        private static BuiExp AllNormalLowerNyushiValue;


        // 顎右左側単位 /////////////////////////////////////////

        /// <summary>上顎右側歯列</summary>
        private static BuiExp AllNormalUpperRightTeethValue;
        /// <summary>上顎左側歯列</summary>
        private static BuiExp AllNormalUpperLeftTeethValue;

        /// <summary>下顎右側歯列</summary>
        private static BuiExp AllNormalLowerRightTeethValue;
        /// <summary>下顎左側歯列</summary>
        private static BuiExp AllNormalLowerLeftTeethValue;

        /// <summary>上顎右側永久歯列</summary>
        private static BuiExp AllNormalUpperRightEikyushiValue;
        /// <summary>上顎右側乳歯列</summary>
        private static BuiExp AllNormalUpperRightNyushiValue;

        /// <summary>上顎左側永久歯列</summary>
        private static BuiExp AllNormalUpperLeftEikyushiValue;
        /// <summary>上顎左側乳歯列</summary>
        private static BuiExp AllNormalUpperLeftNyushiValue;

        /// <summary>下顎右側永久歯列</summary>
        private static BuiExp AllNormalLowerRightEikyushiValue;
        /// <summary>下顎右側乳歯列</summary>
        private static BuiExp AllNormalLowerRightNyushiValue;

        /// <summary>下顎左側永久列</summary>
        private static BuiExp AllNormalLowerLeftEikyushiValue;
        /// <summary>下顎左側乳歯列</summary>
        private static BuiExp AllNormalLowerLeftNyushiValue;


        // 1/3顎単位 /////////////////////////////////////////

        /// <summary>上顎右側臼歯列</summary>
        private static BuiExp AllNormalUpperRightMolarsValue;
        /// <summary>上顎前歯列</summary>
        private static BuiExp AllNormalUpperFrontTeethValue;
        /// <summary>上顎左側臼歯列</summary>
        private static BuiExp AllNormalUpperLeftMolarsValue;

        /// <summary>下顎右側臼歯列</summary>
        private static BuiExp AllNormalLowerRightMolarsValue;
        /// <summary>下顎前歯列</summary>
        private static BuiExp AllNormalLowerFrontTeethValue;
        /// <summary>下顎左側臼歯列</summary>
        private static BuiExp AllNormalLowerLeftMolarsValue;

        /// <summary>上顎右側永久臼歯列</summary>
        private static BuiExp AllNormalUpperRightEikyushiMolarsValue;
        /// <summary>上顎永久前歯列</summary>
        private static BuiExp AllNormalUpperFrontEikyushiTeethValue;
        /// <summary>上顎左側永久臼歯列</summary>
        private static BuiExp AllNormalUpperLeftEikyushiMolarsValue;
        /// <summary>上顎右側乳臼歯列</summary>
        private static BuiExp AllNormalUpperRightNyushiMolarsValue;
        /// <summary>上顎乳前歯列</summary>
        private static BuiExp AllNormalUpperFrontNyushiTeethValue;
        /// <summary>上顎左側乳臼歯列</summary>
        private static BuiExp AllNormalUpperLeftNyushiMolarsValue;

        /// <summary>下顎右側永久臼歯列</summary>
        private static BuiExp AllNormalLowerRightEikyushiMolarsValue;
        /// <summary>下顎永久前歯列</summary>
        private static BuiExp AllNormalLowerFrontEikyushiTeethValue;
        /// <summary>下顎左側永久臼歯列</summary>
        private static BuiExp AllNormalLowerLeftEikyushiMolarsValue;
        /// <summary>下顎右側乳臼歯列</summary>
        private static BuiExp AllNormalLowerRightNyushiMolarsValue;
        /// <summary>下顎乳前歯列</summary>
        private static BuiExp AllNormalLowerFrontNyushiTeethValue;
        /// <summary>下顎左側乳臼歯列</summary>
        private static BuiExp AllNormalLowerLeftNyushiMolarsValue;


        /// <summary>上顎右側永久歯列</summary>
        public static BuiExp AllNormalUpperRightEikyushi
        {
            get
            {
                if (AllNormalUpperRightEikyushiValue == null)
                {
                    AllNormalUpperRightEikyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpper() && x.Position.IsRight() && x.IsEikyushi select x);
                }
                return AllNormalUpperRightEikyushiValue;
            }
        }

        /// <summary>上顎右側乳歯列</summary>
        public static BuiExp AllNormalUpperRightNyushi
        {
            get
            {
                if (AllNormalUpperRightNyushiValue == null)
                {
                    AllNormalUpperRightNyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpper() && x.Position.IsRight() && x.IsNyushi select x);
                }
                return AllNormalUpperRightNyushiValue;
            }
        }

        /// <summary>上顎左側永久歯列</summary>
        public static BuiExp AllNormalUpperLeftEikyushi
        {
            get
            {
                if (AllNormalUpperLeftEikyushiValue == null)
                {
                    AllNormalUpperLeftEikyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpper() && x.Position.IsLeft() && x.IsEikyushi select x);
                }
                return AllNormalUpperLeftEikyushiValue;
            }
        }

        /// <summary>上顎左側乳歯列</summary>
        public static BuiExp AllNormalUpperLeftNyushi
        {
            get
            {
                if (AllNormalUpperLeftNyushiValue == null)
                {
                    AllNormalUpperLeftNyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpper() && x.Position.IsLeft() && x.IsNyushi select x);
                }
                return AllNormalUpperLeftNyushiValue;
            }
        }


        /// <summary>下顎右側永久歯列</summary>
        public static BuiExp AllNormalLowerRightEikyushi
        {
            get
            {
                if (AllNormalLowerRightEikyushiValue == null)
                {
                    AllNormalLowerRightEikyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLower() && x.Position.IsRight() && x.IsEikyushi select x);
                }
                return AllNormalLowerRightEikyushiValue;
            }
        }

        /// <summary>下顎右側乳歯列</summary>
        public static BuiExp AllNormalLowerRightNyushi
        {
            get
            {
                if (AllNormalLowerRightNyushiValue == null)
                {
                    AllNormalLowerRightNyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLower() && x.Position.IsRight() && x.IsNyushi select x);
                }
                return AllNormalLowerRightNyushiValue;
            }
        }

        /// <summary>下顎左側永久歯列</summary>
        public static BuiExp AllNormalLowerLeftEikyushi
        {
            get
            {
                if (AllNormalLowerLeftEikyushiValue == null)
                {
                    AllNormalLowerLeftEikyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLower() && x.Position.IsLeft() && x.IsEikyushi select x);
                }
                return AllNormalLowerLeftEikyushiValue;
            }
        }

        /// <summary>下顎左側乳歯列</summary>
        public static BuiExp AllNormalLowerLeftNyushi
        {
            get
            {
                if (AllNormalLowerLeftNyushiValue == null)
                {
                    AllNormalLowerLeftNyushiValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLower() && x.Position.IsLeft() && x.IsNyushi select x);
                }
                return AllNormalLowerLeftNyushiValue;
            }
        }


        /// <summary>上顎右側歯列</summary>
        public static BuiExp AllNormalUpperRightTeeth
        {
            get
            {
                if (AllNormalUpperRightTeethValue == null)
                {
                    AllNormalUpperRightTeethValue = AllNormalUpperRightEikyushi + AllNormalUpperRightNyushi;
                }
                return AllNormalUpperRightTeethValue;
            }
        }

        /// <summary>上顎左側歯列</summary>
        public static BuiExp AllNormalUpperLeftTeeth
        {
            get
            {
                if (AllNormalUpperLeftTeethValue == null)
                {
                    AllNormalUpperLeftTeethValue = AllNormalUpperLeftEikyushi + AllNormalUpperLeftNyushi;
                }
                return AllNormalUpperLeftTeethValue;
            }
        }


        /// <summary>下顎左側歯列</summary>
        public static BuiExp AllNormalLowerLeftTeeth
        {
            get
            {
                if (AllNormalLowerLeftTeethValue == null)
                {
                    AllNormalLowerLeftTeethValue = AllNormalLowerLeftEikyushi + AllNormalLowerLeftNyushi;
                }
                return AllNormalLowerLeftTeethValue;
            }
        }

        /// <summary>下顎右側歯列</summary>
        public static BuiExp AllNormalLowerRightTeeth
        {
            get
            {
                if (AllNormalLowerRightTeethValue == null)
                {
                    AllNormalLowerRightTeethValue = AllNormalLowerRightEikyushi + AllNormalLowerRightNyushi;
                }
                return AllNormalLowerRightTeethValue;
            }
        }


        // 上下顎単位
        /// <summary>上顎永久歯列</summary>
        public static BuiExp AllNormalUpperEikyushi
        {
            get
            {
                if (AllNormalUpperEikyushiValue == null)
                {
                    AllNormalUpperEikyushiValue = AllNormalUpperRightEikyushi + AllNormalUpperLeftEikyushi;
                }
                return AllNormalUpperEikyushiValue;
            }
        }

        /// <summary>下顎永久歯列</summary>
        public static BuiExp AllNormalLowerEikyushi
        {
            get
            {
                if (AllNormalLowerEikyushiValue == null)
                {
                    AllNormalLowerEikyushiValue = AllNormalLowerRightEikyushi + AllNormalLowerLeftEikyushi;
                }
                return AllNormalLowerEikyushiValue;
            }
        }

        /// <summary>上顎乳歯列</summary>
        public static BuiExp AllNormalUpperNyushi
        {
            get
            {
                if (AllNormalUpperNyushiValue == null)
                {
                    AllNormalUpperNyushiValue = AllNormalUpperRightNyushi + AllNormalUpperLeftNyushi;
                }
                return AllNormalUpperNyushiValue;
            }
        }

        /// <summary>下顎乳歯列</summary>
        public static BuiExp AllNormalLowerNyushi
        {
            get
            {
                if (AllNormalLowerNyushiValue == null)
                {
                    AllNormalLowerNyushiValue = AllNormalLowerRightNyushi + AllNormalLowerLeftNyushi;
                }
                return AllNormalLowerNyushiValue;
            }
        }


        /// <summary>上顎歯列</summary>
        public static BuiExp AllNormalUpperTeeth
        {
            get
            {
                if (AllNormalUpperTeethValue == null)
                {
                    AllNormalUpperTeethValue = AllNormalUpperEikyushi + AllNormalUpperNyushi;
                }
                return AllNormalUpperTeethValue;
            }
        }

        /// <summary>下顎歯列</summary>
        public static BuiExp AllNormalLowerTeeth
        {
            get
            {
                if (AllNormalLowerTeethValue == null)
                {
                    AllNormalLowerTeethValue = AllNormalLowerEikyushi + AllNormalLowerNyushi;
                }
                return AllNormalLowerTeethValue;
            }
        }


        /// <summary>全永久歯列</summary>
        public static BuiExp AllNormalEikyushi
        {
            get
            {
                if (AllNormalEikyushiValue == null)
                {
                    AllNormalEikyushiValue = AllNormalUpperEikyushi + AllNormalLowerEikyushi;
                }
                return AllNormalEikyushiValue;
            }
        }

        /// <summary>全乳歯列</summary>
        public static BuiExp AllNormalNyushi
        {
            get
            {
                if (AllNormalNyushiValue == null)
                {
                    AllNormalNyushiValue = AllNormalUpperNyushi + AllNormalLowerNyushi;
                }
                return AllNormalNyushiValue;
            }
        }


        /// <summary>全歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormal
        {
            get
            {
                if (AllNormalValue == null)
                {
                    AllNormalValue = AllNormalEikyushi + AllNormalNyushi;
                }
                return AllNormalValue;
            }
        }


        // 1/3顎単位
        /// <summary>上顎右側永久臼歯列</summary>
        public static BuiExp AllNormalUpperRightEikyushiMolars
        {
            get
            {
                if (AllNormalUpperRightEikyushiMolarsValue == null)
                {
                    AllNormalUpperRightEikyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperRightMolar() && x.IsEikyushi select x);
                }
                return AllNormalUpperRightEikyushiMolarsValue;
            }
        }

        /// <summary>上顎永久前歯列</summary>
        public static BuiExp AllNormalUpperFrontEikyushiTeeth
        {
            get
            {
                if (AllNormalUpperFrontEikyushiTeethValue == null)
                {
                    AllNormalUpperFrontEikyushiTeethValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperFrontTooth() && x.IsEikyushi select x);
                }
                return AllNormalUpperFrontEikyushiTeethValue;
            }
        }

        /// <summary>上顎左側永久臼歯列</summary>
        public static BuiExp AllNormalUpperLeftEikyushiMolars
        {
            get
            {
                if (AllNormalUpperLeftEikyushiMolarsValue == null)
                {
                    AllNormalUpperLeftEikyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperLeftMolar() && x.IsEikyushi select x);
                }
                return AllNormalUpperLeftEikyushiMolarsValue;
            }
        }


        /// <summary>上顎右側乳臼歯列</summary>
        public static BuiExp AllNormalUpperRightNyushiMolars
        {
            get
            {
                if (AllNormalUpperRightNyushiMolarsValue == null)
                {
                    AllNormalUpperRightNyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperRightMolar() && x.IsNyushi select x);
                }
                return AllNormalUpperRightNyushiMolarsValue;
            }
        }

        /// <summary>上顎乳前歯列</summary>
        public static BuiExp AllNormalUpperFrontNyushiTeeth
        {
            get
            {
                if (AllNormalUpperFrontNyushiTeethValue == null)
                {
                    AllNormalUpperFrontNyushiTeethValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperFrontTooth() && x.IsNyushi select x);
                }
                return AllNormalUpperFrontNyushiTeethValue;
            }
        }

        /// <summary>上顎左側乳臼歯列</summary>
        public static BuiExp AllNormalUpperLeftNyushiMolars
        {
            get
            {
                if (AllNormalUpperLeftNyushiMolarsValue == null)
                {
                    AllNormalUpperLeftNyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpperLeftMolar() && x.IsNyushi select x);
                }
                return AllNormalUpperLeftNyushiMolarsValue;
            }
        }


        /// <summary>下顎右側永久臼歯列</summary>
        public static BuiExp AllNormalLowerRightEikyushiMolars
        {
            get
            {
                if (AllNormalLowerRightEikyushiMolarsValue == null)
                {
                    AllNormalLowerRightEikyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerRightMolar() && x.IsEikyushi select x);
                }
                return AllNormalLowerRightEikyushiMolarsValue;
            }
        }

        /// <summary>下顎永久前歯列</summary>
        public static BuiExp AllNormalLowerFrontEikyushiTeeth
        {
            get
            {
                if (AllNormalLowerFrontEikyushiTeethValue == null)
                {
                    AllNormalLowerFrontEikyushiTeethValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerFrontTooth() && x.IsEikyushi select x);
                }
                return AllNormalLowerFrontEikyushiTeethValue;
            }
        }

        /// <summary>下顎左側永久臼歯列</summary>
        public static BuiExp AllNormalLowerLeftEikyushiMolars
        {
            get
            {
                if (AllNormalLowerLeftEikyushiMolarsValue == null)
                {
                    AllNormalLowerLeftEikyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerLeftMolar() && x.IsEikyushi select x);
                }
                return AllNormalLowerLeftEikyushiMolarsValue;
            }
        }


        /// <summary>下顎右側乳臼歯列</summary>
        public static BuiExp AllNormalLowerRightNyushiMolars
        {
            get
            {
                if (AllNormalLowerRightNyushiMolarsValue == null)
                {
                    AllNormalLowerRightNyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerRightMolar() && x.IsNyushi select x);
                }
                return AllNormalLowerRightNyushiMolarsValue;
            }
        }

        /// <summary>下顎乳前歯列</summary>
        public static BuiExp AllNormalLowerFrontNyushiTeeth
        {
            get
            {
                if (AllNormalLowerFrontNyushiTeethValue == null)
                {
                    AllNormalLowerFrontNyushiTeethValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerFrontTooth() && x.IsNyushi select x);
                }
                return AllNormalLowerFrontNyushiTeethValue;
            }
        }

        /// <summary>下顎左側乳臼歯列</summary>
        public static BuiExp AllNormalLowerLeftNyushiMolars
        {
            get
            {
                if (AllNormalLowerLeftNyushiMolarsValue == null)
                {
                    AllNormalLowerLeftNyushiMolarsValue = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLowerLeftMolar() && x.IsNyushi select x);
                }
                return AllNormalLowerLeftNyushiMolarsValue;
            }
        }


        /// <summary>上顎右側臼歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalUpperRightMolars
        {
            get
            {
                if (AllNormalUpperRightMolarsValue == null)
                {
                    AllNormalUpperRightMolarsValue = AllNormalUpperRightEikyushiMolars + AllNormalUpperRightNyushiMolars;
                }
                return AllNormalUpperRightMolarsValue;
            }
        }

        /// <summary>上顎前歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalUpperFrontTeeth
        {
            get
            {
                if (AllNormalUpperFrontTeethValue == null)
                {
                    AllNormalUpperFrontTeethValue = AllNormalUpperFrontEikyushiTeeth + AllNormalUpperFrontNyushiTeeth;
                }
                return AllNormalUpperFrontTeethValue;
            }
        }

        /// <summary>上顎左側臼歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalUpperLeftMolars
        {
            get
            {
                if (AllNormalUpperLeftMolarsValue == null)
                {
                    AllNormalUpperLeftMolarsValue = AllNormalUpperLeftEikyushiMolars + AllNormalUpperLeftNyushiMolars;
                }
                return AllNormalUpperLeftMolarsValue;
            }
        }


        /// <summary>下顎右側臼歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalLowerRightMolars
        {
            get
            {
                if (AllNormalLowerRightMolarsValue == null)
                {
                    AllNormalLowerRightMolarsValue = AllNormalLowerRightEikyushiMolars + AllNormalLowerRightNyushiMolars;
                }
                return AllNormalLowerRightMolarsValue;
            }
        }

        /// <summary>下顎前歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalLowerFrontTeeth
        {
            get
            {
                if (AllNormalLowerFrontTeethValue == null)
                {
                    AllNormalLowerFrontTeethValue = AllNormalLowerFrontEikyushiTeeth + AllNormalLowerFrontNyushiTeeth;
                }
                return AllNormalLowerFrontTeethValue;
            }
        }

        /// <summary>下顎左側臼歯(永久歯+乳歯)列</summary>
        public static BuiExp AllNormalLowerLeftMolars
        {
            get
            {
                if (AllNormalLowerLeftMolarsValue == null)
                {
                    AllNormalLowerLeftMolarsValue = AllNormalLowerLeftEikyushiMolars + AllNormalLowerLeftNyushiMolars;
                }
                return AllNormalLowerLeftMolarsValue;
            }
        }


        // VerX.XXX 多数歯欠損対応 (2019/05/07 堤)
        /// <summary>上顎永久歯列 ※8番を除く</summary>
        public static BuiExp AllNormalUpperEikyushiExceptNo8
        {
            get
            {
                if(AllNormalUpperEikyushiExceptNo8Value == null)
                {
                    AllNormalUpperEikyushiExceptNo8Value = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsUpper() && x.IsEikyushi && x.Number != ToothNumber.UpperLeft8 && x.Number != ToothNumber.UpperRight8 select x);
                }
                return AllNormalUpperEikyushiExceptNo8Value;
            }
        }

        /// <summary>下顎永久歯列 ※8番を除く</summary>
        public static BuiExp AllNormalLowerEikyushiExceptNo8
        {
            get
            {
                if (AllNormalLowerEikyushiExceptNo8Value == null)
                {
                    AllNormalLowerEikyushiExceptNo8Value = MakeByQuery(from x in SimpleTooth.AllNormal where x.Position.IsLower() && x.IsEikyushi && x.Number != ToothNumber.LowerLeft8 && x.Number != ToothNumber.LowerRight8 select x);
                }
                return AllNormalLowerEikyushiExceptNo8Value;
            }
        }

    }
}

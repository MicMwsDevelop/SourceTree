//
// enum BuiUnitType - 部位単位種別
//
//   部位表現(BuiExp)に含まれる個々の歯番、文字列表現部位等を表す。
// 
// Copyright (C) MIC All Rights Reserved.
// 

namespace CommonLib.BuiData
{
	/// <summary>
	/// 部位単位種別
	/// (コメント追記)ここで定義されたEnumの値がそのままルールデータの保存値にならないものがあるため注意すること。
	/// BuiUnitTypeはC++とC#で種類および定義値が違うものがあるため、C#の定義値をそのまま保存すると、C++のルールデータとの辻褄が合わない。
	/// そのため、GetScriptControllerおよびSetScriptController内でDB⇔クラスに変換する際に、数値のコンバート処理が埋め込まれている。
	/// ここで定義されたenum値でDBのレコードを検索する際などに誤解が発生しやすいため、注意すること。
	/// </summary>
	public enum BuiUnitType
    {
        None = 0,

        /// <summary>歯式の中の一つの物毎に数える単位</summary>
        /// <remarks>つまり、"６⑥⑥"は都合３個と数える</remarks>
        Tooth,

        /// <summary>１／３顎単位</summary>
        OneThird,

        /// <summary>片側（かたそく）</summary>
        /// <remarks>右側と左側のいずれか一方のみにしか歯が無い顎のみが該当する。つまり、両側に該当する顎は無視。</remarks>
        OneSide,

        /// <summary> 両側（りょうそく）</summary>
        /// <remarks>左側と右側の両方に歯が含まれる顎のみ該当。つまり、片側に該当する顎は無視。</remarks>
        BothSide,

        /// <summary>顎(上顎、下顎)単位</summary>
        Jaw,

        /// <summary>口腔全体(部位全体)</summary>
        Mouth,

        /// <summary>装置毎</summary>
        /// <remarks>時系列検索時(チェック等)に全く同じ形の装置のみを該当させる設定。部位としての動作は「顎」と同じ。</remarks>
        Set,

        /// <summary>
        /// 穿洞面毎
        /// 算定回数出し時、面情報が与えられた場合に面数分倍加する指定。
        /// </summary>
        /// <remarks>現状部位自体には穿洞面を持てないので部位単体での動作は１歯単位と同じ</remarks>
        Kadomen,

        /// <summary>歯番毎</summary>
        /// <remarks>
        /// 同じ番号を持つもの全部を一つとして扱う。つまり、"６⑥⑥"は１つと数える。<br/>
        /// "１"と"Ａ"はそれぞれ別の歯番として扱う
        /// </remarks>
        ToothNumber,

        /// <summary>ToothPosition毎</summary>
        /// 同じ位置を占める全ての部位を一つとして扱う。つまり、"５⑤⑤Ｅ"は１つと数える。<br/>
        /// 同じ位置の永久歯と乳歯を区別しない
        ToothPosition,

        /// <summary>ひとつの文字列表現部位毎</summary>
        /// <remarks>BuiExpでは歯番を持つオブジェクトだけではなく文字列表現部位も扱える透過性を持つので、内部処理の一貫性を保つためにサポートした。</remarks>
        /// <remark>Unicorn1.0ではまだルール登録などでの使用を想定しているわけでは無い。</remark>
        StringBui,

        /// EachSide, 
        //   1997/02/12
        //     片両側？ よく分からないが、旧ﾁｪｯｸで両側と区別しているので、
        //     新ﾁｪｯｸのために一応追加
        //     動作仕様要調査(または使っていないなら削除) 2011/9/14 林
        //       「片両側」はU-BOXで、TakeOut関数内でインプリメントされておらず事実上使えない上、
        //       ルール登録でも設定できないようになっているため、Unicornではサポートしない。
    }
}

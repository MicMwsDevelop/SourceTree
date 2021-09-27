//
// enum ToothAttribute - 歯番の属性(通常歯番、支台歯、隙、過剰歯等)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// UBOX準拠の歯の属性
	/// </summary>
	[Flags]
    public enum ToothAttribute
    {
        None = 0,

        /// <summary>通常歯番</summary>
        NormalNumber            = 0x0001,
        /// <summary>支台歯[O]</summary>
        Shidaishi               = 0x0002,
        /// <summary>便宜抜髄支台歯(健全支台歯)[@]</summary>
        KenzenShidaishi         = 0x0004,
        /// <summary>隙(△)[^]</summary>
        Geki                    = 0x0008,
        /// <summary>過剰歯(▽)[V]</summary>
        Kajoshi                 = 0x0010,

        // 以下の属性についてはサポートするか検討が必要だが存在する物として仮インプリメント
        
        /// <summary>欠損歯(2歯欠損1歯ダミー等の場合の欠損歯)→数字の上に「×」[X]</summary>
        EtcKessonshi            = 0x0020,
        /// <summary>分割歯など→数字の上に「'」[']</summary>
        EtcBunkatsushi          = 0x0040,
        /// <summary>増歯→数字の上に黒丸[.]</summary>
        EtcZoshi                = 0x0080,
        /// <summary>鈎破損→数字の上(上顎)または下(下顎)に白丸[~]</summary>
        EtcKohason              = 0x0100,
        /// <summary>残根上のダミー(UBOXでは入力不可)[_]</summary>
        EtcDummyOnZankon        = 0x0200,

        All = NormalNumber | Shidaishi | KenzenShidaishi | Geki | Kajoshi | EtcKessonshi
                | EtcBunkatsushi | EtcZoshi | EtcKohason | EtcDummyOnZankon
    }
}

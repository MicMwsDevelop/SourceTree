//
// 都道府県 関連定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.Common;

namespace MwsLib.BaseFactory
{
	/// <summary>
	/// 都道府県 関連定義クラス
	/// </summary>
	public static class KenNumDef
    {
        /// <summary>
        /// 県番号
        /// </summary>
        public enum KenNumber
        {
            /// <summary>0 未定義</summary>
            None = 0,
            /// <summary>1 北海道</summary>
            Hokkaido = 1,
            /// <summary> 2 青森</summary>
            Aomori,
            /// <summary> 3 岩手</summary>
            Iwate,
            /// <summary>4 宮城</summary>
            Miyagi,
            /// <summary>5 秋田</summary>
            Akita,
            /// <summary>6 山形</summary>
            Yamagata,
            /// <summary>7 福島</summary>
            Fukushima,
            /// <summary>8 茨城</summary>
            Ibaraki,
            /// <summary>9 栃木</summary>
            Tochigi,
            /// <summary>10 群馬</summary>
            Gunma,
            /// <summary>11 埼玉</summary>
            Saitama,
            /// <summary>12 千葉</summary>
            Chiba,
            /// <summary>13 東京</summary>
            Tokyo,
            /// <summary>14 神奈川</summary>
            Kanagawa,
            /// <summary>15 新潟</summary>
            Niigata,
            /// <summary>16 富山</summary>
            Toyama,
            /// <summary>17 石川</summary>
            Ishikawa,
            /// <summary>18 福井</summary>
            Fukui,
            /// <summary>19 山梨</summary>
            Yamanashi,
            /// <summary>20 長野</summary>
            Nagano,
            /// <summary>21 岐阜</summary>
            Gifu,
            /// <summary>22 静岡</summary>
            Shizuoka,
            /// <summary>23 愛知</summary>
            Aichi,
            /// <summary>24 三重</summary>
            Mie,
            /// <summary> 25 滋賀</summary>
            Shiga,
            /// <summary>26 京都</summary>
            Kyoto,
            /// <summary>27 大阪</summary>
            Osaka,
            /// <summary>28 兵庫</summary>
            Hyogo,
            /// <summary>29 奈良</summary>
            Nara,
            /// <summary>30 和歌山</summary>
            Wakayama,
            /// <summary>31 鳥取</summary>
            Tottori,
            /// <summary>32 島根</summary>
            Shimane,
            /// <summary>33 岡山</summary>
            Okayama,
            /// <summary>34 広島</summary>
            Hiroshima,
            /// <summary>35 山口</summary>
            Yamaguchi,
            /// <summary>36 徳島</summary>
            Tokushima,
            /// <summary>37 香川</summary>
            Kagawa,
            /// <summary>38 愛媛</summary>
            Ehime,
            /// <summary>39 高知</summary>
            Kochi,
            /// <summary>40 福岡</summary>
            Fukuoka,
            /// <summary>41 佐賀</summary>
            Saga,
            /// <summary>42 長崎</summary>
            Nagasaki,
            /// <summary>43 熊本</summary>
            Kumamoto,
            /// <summary>44 大分</summary>
            Oita,
            /// <summary>45 宮崎</summary>
            Miyazaki,
            /// <summary>46 鹿児島</summary>
            Kagoshima,
            /// <summary>47 沖縄</summary>
            Okinawa,
        }

        /// <summary>
        /// 都道府県名文字列
        /// index[0]で都道府県、[1]は都府県省略
        /// </summary>
        public static readonly EnumDictionary<KenNumber, string[]> KenString = new EnumDictionary<KenNumber, string[]>() 
        {
            { KenNumber.None, new string[2] { "", "" }},
            { KenNumber.Hokkaido, new string[2] { "北海道", "北海道" }},
            { KenNumber.Aomori, new string[2] { "青森県", "青森" }},
            { KenNumber.Iwate, new string[2] { "岩手県", "岩手" }},
            { KenNumber.Miyagi, new string[2] { "宮城県", "宮城" }},
            { KenNumber.Akita, new string[2] { "秋田県", "秋田" }},
            { KenNumber.Yamagata, new string[2] { "山形県", "山形" }},
            { KenNumber.Fukushima, new string[2] { "福島県", "福島" }},
            { KenNumber.Ibaraki, new string[2] { "茨城県", "茨城" }},
            { KenNumber.Tochigi, new string[2] { "栃木県", "栃木" }},
            { KenNumber.Gunma, new string[2] { "群馬県", "群馬" }},
            { KenNumber.Saitama, new string[2] { "埼玉県", "埼玉" }},
            { KenNumber.Chiba, new string[2] { "千葉県", "千葉" }},
            { KenNumber.Tokyo, new string[2] { "東京都", "東京" }},
            { KenNumber.Kanagawa, new string[2] { "神奈川県", "神奈川" }},
            { KenNumber.Niigata, new string[2] { "新潟県", "新潟" }},
            { KenNumber.Toyama, new string[2] { "富山県", "富山" }},
            { KenNumber.Ishikawa, new string[2] { "石川県", "石川" }},
            { KenNumber.Fukui, new string[2] { "福井県", "福井" }},
            { KenNumber.Yamanashi, new string[2] { "山梨県", "山梨" }},
            { KenNumber.Nagano, new string[2] { "長野県", "長野" }},
            { KenNumber.Gifu, new string[2] { "岐阜県", "岐阜" }},
            { KenNumber.Shizuoka, new string[2] { "静岡県", "静岡" }},
            { KenNumber.Aichi, new string[2] { "愛知県", "愛知" }},
            { KenNumber.Mie, new string[2] { "三重県", "三重" }},
            { KenNumber.Shiga, new string[2] { "滋賀県", "滋賀" }},
            { KenNumber.Kyoto, new string[2] { "京都府", "京都" }},
            { KenNumber.Osaka, new string[2] { "大阪府", "大阪" }},
            { KenNumber.Hyogo, new string[2] { "兵庫県", "兵庫" }},
            { KenNumber.Nara, new string[2] { "奈良県", "奈良" }},
            { KenNumber.Wakayama, new string[2] { "和歌山県", "和歌山" }},
            { KenNumber.Tottori, new string[2] { "鳥取県", "鳥取" }},
            { KenNumber.Shimane, new string[2] { "島根県", "島根" }},
            { KenNumber.Okayama, new string[2] { "岡山県", "岡山" }},
            { KenNumber.Hiroshima, new string[2] { "広島県", "広島" }},
            { KenNumber.Yamaguchi, new string[2] { "山口県", "山口" }},
            { KenNumber.Tokushima, new string[2] { "徳島県", "徳島" }},
            { KenNumber.Kagawa, new string[2] { "香川県", "香川" }},
            { KenNumber.Ehime, new string[2] { "愛媛県", "愛媛" }},
            { KenNumber.Kochi, new string[2] { "高知県", "高知" }},
            { KenNumber.Fukuoka, new string[2] { "福岡県", "福岡" }},
            { KenNumber.Saga, new string[2] { "佐賀県", "佐賀" }},
            { KenNumber.Nagasaki, new string[2] { "長崎県", "長崎" }},
            { KenNumber.Kumamoto, new string[2] { "熊本県", "熊本" }},
            { KenNumber.Oita, new string[2] { "大分県", "大分" }},
            { KenNumber.Miyazaki, new string[2] { "宮崎県", "宮崎" }},
            { KenNumber.Kagoshima, new string[2] { "鹿児島県", "鹿児島" }},
            { KenNumber.Okinawa, new string[2] { "沖縄県", "沖縄" }},
        };

        // 指定県番号に対応した｢｣都｣｢道｣｢府｣｢県｣の文字を取得
        public static string GetKenKindString(KenNumber kenNumber)
        {
            switch (kenNumber)
            {
                case KenNumber.Hokkaido:
                    return "道";
                case KenNumber.Tokyo:
                    return "都";
                case KenNumber.Kyoto:
                case KenNumber.Osaka:
                    return "府";
                default:
                    return "県";
                case KenNumber.None:
                    return "";
            }
        }
    }
}

//
// GengoYear.cs
// 
// 「元号年」についての定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.001 新元号令和対応(2019/05/01 勝呂)
//
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Common
{
	/// <summary>
	/// 「元号年」についての定義クラス
	/// </summary>
	public static class GengoYear
    {
        private static readonly GengoInformation[] m_GengoInformations = new GengoInformation[] {
            new GengoInformation{ ID = GengoID.Meiji, Name = "明治", AlphaName = "M", Span = new Span(new Date(1868, 10, 23), new Date(1912, 7, 29))},
            new GengoInformation{ ID = GengoID.Taisho, Name = "大正", AlphaName = "T", Span = new Span(new Date(1912, 07, 30), new Date(1926, 12, 24))},
            new GengoInformation{ ID = GengoID.Showa, Name = "昭和", AlphaName = "S", Span = new Span(new Date(1926, 12, 25), new Date(1989, 1, 7))},
            new GengoInformation{ ID = GengoID.Heisei, Name = "平成", AlphaName = "H", Span = new Span(new Date(1989, 1, 8), new Date(2019, 4, 30))},

			// Ver1.001 新元号令和対応(2019/05/01 勝呂)
			new GengoInformation{ ID = GengoID.Reiwa, Name = "令和", AlphaName = "R", Span = new Span(new Date(2019, 5, 1), Date.MaxValue)},
		};

        /// <summary>全ての元号</summary>
        public static IEnumerable<GengoInformation> All
        {
            get
            {
                return m_GengoInformations;
            }
        }

        public static GengoInformation GetInformation(this GengoID id)
        {
            return m_GengoInformations.Where(x => x.ID == id).Single();
        }

        /// <summary>日付から元号を識別する</summary>
        /// <returns>元号識別子(明治より前の日付を指定した場合はGengoID.None)</returns>
        public static GengoID GetGengoID(this Date date)
        {
            var gengoInfo = m_GengoInformations.Where(x => x.Span.IsInside(date)).SingleOrDefault();
            if (gengoInfo == null)
            {
                // 元号情報が見つからないという事は明治より前の日付という事
                return GengoID.None;
            }
            else
            {
                return gengoInfo.ID;
            }
        }
        
        /// <summary>
        /// 日付から元号情報を取得
        /// </summary>
        /// <returns>
        /// 日付に該当する元号情報(該当する元号情報が無い場合はnull)
        /// </returns>
        public static GengoInformation GetGengoInformation(this Date date)
        {
            var id = GetGengoID(date);
            if (id != GengoID.None)
            {
                return GetInformation(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 日付に該当する元号文字列(漢字)
        /// </summary>
        /// <returns>
        /// 日付に該当する元号文字列(該当する元号情報が無い場合はnull)
        /// </returns>
        public static string GetGengoName(this Date date)
        {
            var gengo = date.GetGengoInformation();
            if (gengo != null)
            {
                return gengo.Name;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 日付に該当する元号文字列(英字省略形)
        /// </summary>
        /// <returns>
        /// 日付に該当する元号文字列(該当する元号情報が無い場合はnull)
        /// </returns>
        public static string GetGengoAlphaName(this Date date)
        {
            var gengo = date.GetGengoInformation();
            if (gengo != null)
            {
                return gengo.AlphaName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 日付に該当する元号年を取得する
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns>日付に該当する元号年（該当する元号情報がない場合は-1）</returns>
        public static int GetGengoYear(this Date date)
        {
            var gengo = date.GetGengoInformation();
            if (gengo != null)
            {
                return gengo.ToGengoYear(date.Year);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 元号年から西暦年への変換
        /// </summary>
        /// <param name="gengoID">元号を指定</param>
        /// <param name="gengoYear">元号年を指定(平成元年なら1, 平成１２年なら12)</param>
        /// <returns></returns>
        public static int ToADYear(this GengoID gengoID, int gengoYear)
        {
            var gengo = GetInformation(gengoID);
            return gengo.ToADYear(gengoYear);
        }
    }
}

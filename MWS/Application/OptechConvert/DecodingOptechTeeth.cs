//
// DecodingOptechTeeth.cs
//
// オプテック歯式クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using System.Collections.Generic;

namespace OptechConvert
{
	/// <summary>
	/// オプテック歯式→レセ電歯式変換クラス
	/// </summary>
	public class DecodingOptechTeeth
	{
		/// <summary>
		/// 歯式記号長
		/// </summary>
		public static int MarkLength = 5;

		/// <summary>
		/// 歯式開始記号
		/// </summary>
		public static readonly string MarkStart = "- - -";

		/// <summary>
		/// 部
		/// </summary>
		public static readonly string MarkPart = "a - -";

		/// <summary>
		/// 省略表現
		/// </summary>
		public static readonly string MarkOmit = "- o -";

		/// <summary>
		/// 分割近心
		/// </summary>
		public static readonly string SplitNear = "~1";

		/// <summary>
		/// 分割遠心
		/// </summary>
		public static readonly string SplitFar = "~4";

		/// <summary>
		/// 支台歯
		/// </summary>
		public static readonly string Bridge = "@";

		/// <summary>
		/// 便宜抜髄
		/// </summary>
		public static readonly string Kenzen = "*";

		/// <summary>
		/// 増歯
		/// </summary>
		public static readonly string Increase = "!";

		/// <summary>
		/// オプテック歯式を右上、左上、左下、右下に分割する
		/// </summary>
		/// <param name="optechTeeth">オプテック歯式</param>
		/// <param name="UR">右上</param>
		/// <param name="UL">左上</param>
		/// <param name="DL">左下</param>
		/// <param name="DR">右下</param>
		private static void Split(string optechTeeth, out List<string> UR, out List<string> UL, out List<string> DL, out List<string> DR)
		{
			UR = new List<string>();
			UL = new List<string>();
			DL = new List<string>();
			DR = new List<string>();

			string teeth = optechTeeth.Replace(MarkStart, "").Replace(MarkPart, "").Replace(MarkOmit, "").Trim();
			int direction = 0;
			bool start = false;
			for (int i = 0; i < teeth.Length; i++)
			{
				switch (teeth[i])
				{
					case '┘':
						direction = 1;
						start = true;
						break;
					case '└':
						direction = 2;
						start = true;
						break;
					case '┌': direction = 3;
						start = true;
						break;
					case '┐':
						direction = 4;
						start = true;
						break;
					case ' ': break;
					default:
						switch (direction)
						{
							case 1:
								if (start)
								{
									UR.Add(string.Format("┘{0}", teeth[i]));
									start = false;
								}
								else
								{
									UR[UR.Count - 1] += teeth[i];
								}
								break;
							case 2:
								if (start)
								{
									UL.Add(string.Format("└{0}", teeth[i]));
									start = false;
								}
								else
								{
									UL[UL.Count - 1] += teeth[i];
								}
								break;
							case 3:
								if (start)
								{
									DL.Add(string.Format("┌{0}", teeth[i]));
									start = false;
								}
								else
								{
									DL[DL.Count - 1] += teeth[i];
								}
								break;
							case 4:
								if (start)
								{
									DR.Add(string.Format("┐{0}", teeth[i]));
									start = false;
								}
								else
								{
									DR[DR.Count - 1] += teeth[i];
								}
								break;
						}
						break;
				}
			}
		}

		/// <summary>
		/// オプテック歯式をレセ電歯式に変換する
		/// </summary>
		/// <param name="optech">オプテック歯式</param>
		/// <param name="part">状態コードに部を設定する</param>
		/// <returns>レセ電歯式</returns>
		private static string Decoding(List<string> optech, bool part)
		{
			List<string> ret = new List<string>();
			foreach (string op in optech)
			{
				switch (op)
				{
					// ┘1△
					case "┘?1~1": ret.Add("101180"); break;
					// ┘2△
					case "┘?2~1": ret.Add("101280"); break;
					// ┘3△
					case "┘?3~1": ret.Add("101380"); break;
					// ┘4△
					case "┘?4~1": ret.Add("101480"); break;
					// ┘5△
					case "┘?5~1": ret.Add("101580"); break;
					// ┘6△
					case "┘?6~1": ret.Add("101680"); break;
					// ┘7△
					case "┘?7~1": ret.Add("101780"); break;
					// ┘8△
					case "┘?8~1": ret.Add("101880"); break;

					// └1△
					case "└?1~1": ret.Add("102180"); break;
					// └2△
					case "└?2~1": ret.Add("102280"); break;
					// └3△
					case "└?3~1": ret.Add("102380"); break;
					// └4△
					case "└?4~1": ret.Add("102480"); break;
					// └5△
					case "└?5~1": ret.Add("102580"); break;
					// └6△
					case "└?6~1": ret.Add("102680"); break;
					// └7△
					case "└?7~1": ret.Add("102780"); break;
					// └8△
					case "└?8~1": ret.Add("102880"); break;

					// ┌△1
					case "┌?1~1": ret.Add("103180"); break;
					// ┌△2
					case "┌?2~1": ret.Add("103280"); break;
					// ┌△3
					case "┌?3~1": ret.Add("103380"); break;
					// ┌△4
					case "┌?4~1": ret.Add("103480"); break;
					// ┌△5
					case "┌?5~1": ret.Add("103580"); break;
					// ┌△6
					case "┌?6~1": ret.Add("103680"); break;
					// ┌△7
					case "┌?7~1": ret.Add("103780"); break;
					// ┌△8
					case "┌?8~1": ret.Add("103880"); break;

					// ┐△1
					case "┐?1~1": ret.Add("104180"); break;
					// ┐△2
					case "┐?2~1": ret.Add("104280"); break;
					// ┐△3
					case "┐?3~1": ret.Add("104380"); break;
					// ┐△4
					case "┐?4~1": ret.Add("104480"); break;
					// ┐△5
					case "┐?5~1": ret.Add("104580"); break;
					// ┐△6
					case "┐?6~1": ret.Add("104680"); break;
					// ┐△7
					case "┐?7~1": ret.Add("104780"); break;
					// ┐△8
					case "┐?8~1": ret.Add("104880"); break;

					default:
						{
							string buf = string.Empty;
							string aaa = op.Substring(0, 3);
							switch (op.Substring(0, 3))
							{
								// ┘1
								case "┘#1": buf = "1011"; break;
								// ┘2
								case "┘#2": buf = "1012"; break;
								// ┘3
								case "┘#3": buf = "1013"; break;
								// ┘4
								case "┘#4": buf = "1014"; break;
								// ┘5
								case "┘#5": buf = "1015"; break;
								// ┘6
								case "┘#6": buf = "1016"; break;
								// ┘7
								case "┘#7": buf = "1017"; break;
								// ┘8
								case "┘#8": buf = "1018"; break;

								// └1
								case "└#1": buf = "1021"; break;
								// └2
								case "└#2": buf = "1022"; break;
								// └3
								case "└#3": buf = "1023"; break;
								// └4
								case "└#4": buf = "1024"; break;
								// └5
								case "└#5": buf = "1025"; break;
								// └6
								case "└#6": buf = "1026"; break;
								// └7
								case "└#7": buf = "1027"; break;
								// └8
								case "└#8": buf = "1028"; break;

								// ┌1
								case "┌#1": buf = "1031"; break;
								// ┌2
								case "┌#2": buf = "1032"; break;
								// ┌3
								case "┌#3": buf = "1033"; break;
								// ┌4
								case "┌#4": buf = "1034"; break;
								// ┌5
								case "┌#5": buf = "1035"; break;
								// ┌6
								case "┌#6": buf = "1036"; break;
								// ┌7
								case "┌#7": buf = "1037"; break;
								// ┌8
								case "┌#8": buf = "1038"; break;

								// ┐1
								case "┐#1": buf = "1041"; break;
								// ┐2
								case "┐#2": buf = "1042"; break;
								// ┐3
								case "┐#3": buf = "1043"; break;
								// ┐4
								case "┐#4": buf = "1044"; break;
								// ┐5
								case "┐#5": buf = "1045"; break;
								// ┐6
								case "┐#6": buf = "1046"; break;
								// ┐7
								case "┐#7": buf = "1047"; break;
								// ┐8
								case "┐#8": buf = "1048"; break;
							}
							if (3 == op.Length)
							{
								buf += (part) ? "10" : "00";
							}
							else
							{
								switch (op[3])
								{
									case '@': buf += "30"; break;
									case '*': buf += "50"; break;
									case '!': buf += "00"; break;
								}
							}
							ret.Add(buf);
						}
						break;
				}
			}
			return string.Join("", ret.ToArray());
		}

		/// <summary>
		/// オプテック歯式をレセ電歯式に変換する
		/// </summary>
		/// <param name="optechTeeth">オプテック歯式</param>
		/// <returns>レセ電歯式</returns>
		public static string ConvertRezeptComputeBui(string optechTeeth)
		{
			if (optechTeeth.Length <= MarkLength)
			{
				return string.Empty;
			}
			bool part = false;
			if (MarkPart == optechTeeth.Substring(0, MarkLength))
			{
				part = true;
			}
			List<string> inUR, inUL, inDL, inDR;
			Split(optechTeeth, out inUR, out inUL, out inDL, out inDR);

			string ret = string.Empty;
			if (0 < inUR.Count)
			{
				ret = Decoding(inUR, part);
			}
			if (0 < inUL.Count)
			{
				ret += Decoding(inUL, part);
			}
			if (0 < inDL.Count)
			{
				ret += Decoding(inDL, part);
			}
			if (0 < inDR.Count)
			{
				ret += Decoding(inDR, part);
			}
			return ret;
		}

		/// <summary>
		/// 通常歯式かどうか？
		/// </summary>
		/// <param name="optechTeeth"></param>
		/// <returns></returns>
		public static bool IsNormalTeeth(string optechTeeth)
		{
			if (MarkLength <= optechTeeth.Length)
			{
				return true;
			}
			return false;
		}
	}
}

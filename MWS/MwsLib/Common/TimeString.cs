//
// TimeString.cs
// 
// 時刻文字列の取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Text;

namespace MwsLib.Common
{
	public static class TimeString
    {
        /// <summary>
        /// 時刻を表す様々な形式の文字列の作成
        /// </summary>
        /// <param name="time">時刻</param>
        /// <param name="inJapanese">日本語形式(「午前」or「午後」,「時」「分」「秒」)か、"AM"or"PM"+HH:MM:SS形式か。※remarks参照のこと</param>
        /// <param name="separateHMS">時分秒の数字の間を記号で区切るかどうか※remarks参照のこと</param>
        /// <param name="hmsSeparator">時分秒の間を区切る記号</param>
        /// <param name="outputHour">「時」を出力するかどうか</param>
        /// <param name="outputDirectOverHour">true:「時」が0～23の間でない場合にそのまま出力。<br/>false:実際の時刻に換算して出力する。</param>
        /// <param name="outputAMPM">午前(AM),午後(PM)の区分を出力し「時」を１２時間表記で出力する</param>
        /// <param name="separateAfterAMPM">午前午後と「時」の間を区切る</param>
        /// <param name="afterAMPMSaperator">午前午後と「時」の間を区切る記号</param>
        /// <param name="paddingHMSChar">時分秒を桁揃えするときに数字の前に入れる文字</param>
        /// <param name="paddingHour">「時」を２桁(右詰)に揃える</param>
        /// <param name="outputMinute">「分」を出力するかどうか</param>
        /// <param name="paddingMinute">「分」を２桁(右詰)に揃える</param>
        /// <param name="outputSecond">「秒」を出力するかどうか</param>
        /// <param name="paddingSecond">「秒」を２桁(右詰)に揃える</param>
        /// <param name="paddingHourChar">時に対してのみ適用する桁揃え用の文字。未指定又はnullの時はpaddingHMSCharを適用する。</param>
        /// <param name="paddingMinuteChar">分に対してのみ適用する桁揃え用の文字。未指定又はnullの時はpaddingHMSCharを適用する。</param>
        /// <param name="paddingMinuteChar">秒に対してのみ適用する桁揃え用の文字。未指定又はnullの時はpaddingHMSCharを適用する。</param>
        /// <remarks>
        /// inJapaneseをtrueにする場合、separateHMSもfalseと明示的にしていする。しないと0時:1分:2秒という表現になってしまう。
        /// </remarks>
        /// <returns></returns>
        public static string GetString(
                                        this Time time,
                                        bool inJapanese = false,
                                        bool separateHMS = true,
                                        string hmsSeparator = ":",
                                        bool outputHour = true, 
                                        bool outputDirectOverHour = false,
                                        bool outputAMPM = false,
                                        bool separateAfterAMPM = false,
                                        string afterAMPMSaperator = " ",
                                        char paddingHMSChar = '0',
                                        bool paddingHour = true,
                                        bool outputMinute = true,
                                        bool paddingMinute = true,
                                        bool outputSecond = true,
                                        bool paddingSecond = true,
                                        char? paddingHourChar = null,
                                        char? paddingMinuteChar = null,
                                        char? paddingSecondChar = null
            )
        {
            StringBuilder result = new StringBuilder();

            if(outputHour){
                if (outputDirectOverHour && time.IsHourOverflowed){
                    result.Append(time.Hour);
                }else{
                    // 「時」を、実際の時刻に換算する
                    int hour = time.Hour;
                    if (hour < 0)
                    {
                        hour = (time.Hour % 24) + 24 ;
                        if (hour == 24)
                        {
                            hour = 0;
                        }
                    }
                    else if (hour >= 24)
                    {
                        hour = hour % 24;
                    }

                    string ampm = string.Empty;
                    if (outputAMPM)
                    {
                        if(hour < 12){
                            ampm = inJapanese ? "午前" : "AM";
                        }
                        else
                        {
                            ampm = inJapanese ? "午後" : "PM";
                            hour -= 12;
                        }
                        if (separateAfterAMPM)
                        {
                            ampm += afterAMPMSaperator;
                        }
                    }

                    string hourString = hour.ToString();
                    result.AppendFormat("{0}{1}", ampm, paddingHour ? hourString.PadLeft(2, paddingHourChar ?? paddingHMSChar) : hourString);

                }
                if(inJapanese){
                    result.Append("時");
                }
            }

            if(separateHMS && outputHour && outputMinute){
                result.Append(hmsSeparator);
            }

            if(outputMinute){
                string minuteString = time.Minute.ToString();
                result.Append(paddingMinute ? minuteString.PadLeft(2, paddingMinuteChar ?? paddingHMSChar) : minuteString);
                if(inJapanese){
                    result.Append("分");
                }
            }

            if(separateHMS && ((outputMinute && outputSecond) || (outputHour && !outputMinute && outputSecond))){
                result.Append(hmsSeparator);
            }

            if(outputSecond){
                string secondString = time.Second.ToString();
                result.Append(paddingSecond ? secondString.PadLeft(2, paddingSecondChar ?? paddingHMSChar) : secondString);
                if(inJapanese){
                    result.Append("秒");
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 時刻文字列作成 24h表現,時以外'0'で桁揃え　ex.9:05:03 
        /// </summary>
        public static string GetNormal1String(this Time time)
        {
            return time.GetString(paddingHour:false);
        }

        ///
        /// 時刻文字列作成 秒無し。24h表現,時以外'0'で桁揃え,秒無し　ex.9:05
        ///
        public static string GetNormal2String(this Time time)
        {
            return time.GetString(paddingHour:false, outputSecond:false);
        }

        ///
        /// 時刻文字列作成 秒無し。12hAM/PM表現,時はスペース、時以外は'0'で桁揃え ex.AM 9:05:03
        ///
        public static string GetUsa1String(this Time time)
        {
            return time.GetString(outputAMPM:true, paddingHourChar:' ');
        }

        ///
        /// 時刻文字列作成 秒無し。12hAM/PM表現,時はスペース、時以外は'0'で桁揃え,秒無し ex.AM 9:05
        /// 
        public static string GetUsa2String(this Time time)
        {
            return time.GetString(outputAMPM: true, paddingHourChar: ' ', outputSecond:false);
        }

        ///
        /// 時刻文字列作成 日本語表現、秒無し。12h午前/午後表現,午前・午後の後にスペース,時分秒の'0'で桁揃えなし ex.午前 9時5分3秒
        ///
        public static string GetJp1String(this Time time)
        {
            return time.GetString(outputAMPM: true, inJapanese: true, separateHMS:false, paddingHour: false, paddingMinute: false, paddingSecond: false, separateAfterAMPM: true);
        }

        ///
        /// 時刻文字列作成 日本語表現、秒無し。12h午前/午後表現,午前・午後の後にスペース,秒無し,時分秒の'0'で桁揃えなし ex.午前 9時5分3秒
        ///
        public static string GetJp2String(this Time time)
        {
            return time.GetString(outputAMPM: true, inJapanese: true, separateHMS: false, outputSecond: false, paddingHour: false, paddingMinute: false, separateAfterAMPM: true);
        }

        // 午前午後のみ日本語の表現は非対応(C++で使っていなかった)
        // eTSE_JP_AS1  = 6,	// ex.午前 9:05:03
	    // eTSE_JP_AS2  = 7,	// ex.午前 9:05

        ///
        /// 時刻文字列作成 日本語表現、秒無し。12h午前/午後表現,時は桁揃え無し、時以外は'0'で桁揃え、秒無し ex.午前9時5分
        ///
        public static string GetJp3String(this Time time)
        {
            return time.GetString(outputAMPM: true, inJapanese: true, separateHMS: false, outputSecond: false, paddingHour: false, paddingMinute:false);
        }
    }
}

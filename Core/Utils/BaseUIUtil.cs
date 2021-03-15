using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CYM
{
    public enum KMGType
    {
        OneK,
        TenK,
    }
    public class BaseUIUtil
    {
        const string Yes = "Yes";
        const string No = "No";
        #region Base
        public static string Decorate(string numberStr, string color) => string.Format("<color={0}>{1}</color>", color, numberStr);
        // 根据sign是否大于0决定一些东西
        // positiveSign:是否给正数写加号
        static string DecorateStr(string numberStr, float? sign, bool positiveSign, bool reverseColor)
        {
            if (sign == null)
                return "";
            return string.Format("<color={0}>{1}{2}</color>", reverseColor ? GetColor(-sign.Value) : GetColor(sign.Value), positiveSign && sign >= 0 ? "+" : "", numberStr);
        }

        static string GetSign(float number)
        {
            if (number >= 0) return "+";
            else if (number < 0) return "";
            else return "";
        }

        static string GetColor(float number, bool isReverseCol = false)
        {
            if (number > 0)
            {
                if (!isReverseCol) return SysConst.COL_Green;
                else return SysConst.COL_Red;
            }
            else if (number < 0)
            {
                if (!isReverseCol) return SysConst.COL_Red;
                else return SysConst.COL_Green;
            }
            else return SysConst.COL_Yellow;
        }

        static string ValidDigit(float f, int digit)
        {
            if (digit <= 0) throw new ArgumentOutOfRangeException();
            string e = string.Format("{0:e" + (digit - 1) + "}", f);
            float fd = float.Parse(e);
            return fd.ToString();
        }
        public static string Floor(float f) => Mathf.FloorToInt(f).ToString();
        public static string Ceil(float f) => Mathf.CeilToInt(f).ToString();
        public static string Round(float f) => Mathf.RoundToInt(f).ToString();
        public static string RoundSign(float f) => GetSign(f) + Round(Mathf.Abs(f));
        public static string RoundColor(float f) => DecorateStr(Round(f), f, false, false);
        public static string Plain(int i) => i.ToString();
        public static string Plain(float f) => f.ToString();
        public static string ColSign(int number) => DecorateStr(number.ToString(), number, true, false);
        public static string ColSign(float number, bool reverseColor = false) => DecorateStr(number.ToString(), number, true, reverseColor);
        public static string Bool(float val)
        {
            if (val <= 0.0f) return No;
            else return Yes;
        }
        public static string Bool(bool b)
        {
            if (!b) return No;
            else return Yes;
        }
        public static string Sign(float val) => val >= 0 ? "+" + val.ToString() : val.ToString();
        public static string Sign(float val, string str) => val >= 0 ? "+" + str : str;
        public static string Sign(string str) => !str.StartsWith("-") ? "+" + str : str;
        #endregion

        #region Digital
        public static string OneD(float? f, bool isOption = false) => OneDFormat(f, isOption);
        public static string TwoD(float f, bool isOption = false) => TwoDFormat(f, isOption);

        public static string TwoDS(float f)
        {
            string sign = GetSign(f);
            return string.Format("{1}{0:0.00}", f, sign);
        }
        public static string TwoDC(float f, bool isReverseCol = false)
        {
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1:0.00}</color>", color, f);
        }
        public static string TwoDCS(float f, bool isReverseCol = false)
        {
            string sign = GetSign(f);
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1}{2:0.00}</color>", color, sign, f);
        }

        public static string OneDS(float f)
        {
            string sign = GetSign(f);
            return string.Format("{1}{0:0.0}", f, sign);
        }
        public static string OneDC(float f, bool isReverseCol = false)
        {
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1:0.0}</color>", color, f);
        }
        public static string OneDCS(float f, bool isReverseCol = false)
        {
            string sign = GetSign(f);
            string color = GetColor(f, isReverseCol);
            return string.Format("<color={0}>{1}{2:0.0}</color>", color, sign, f);
        }

        // 小于1的时候显示1位小数,否则返回整数
        public static string RoundD(float f)
        {
            if (f == 0.0f)
                return Round(f);
            if (f < 1.0f && f > -1.0f)
                return string.Format("{0:0.0}", f);
            else
                return Round(f);
        }
        static string OneDFormat(float? f, bool isOption)
        {
            return isOption ? string.Format("{0:0.#}", f.HasValue ? f : 0) : string.Format("{0:0.0}", f);
        }
        static string TwoDFormat(float f, bool isOption)
        {
            return isOption ? string.Format("{0:0.##}", f) : string.Format("{0:0.00}", f);
        }
        #endregion

        #region KMG
        public static string KMG(float? number, KMGType type = KMGType.TenK)
        {
            if (number == null) return "";
            float f = GetNumber((int)number);
            return ValidDigit(f, 2) + GetSuffix((int)number);

            float GetNumber(int num)
            {
                int abs = Mathf.Abs(num);

                if (type == KMGType.TenK)
                {
                    if (abs >= 10000000)
                    {
                        return (num / 1000000);
                    }
                    else if (abs >= 10000)
                    {
                        return (num / 1000.0f);
                    }
                }
                else if (type == KMGType.OneK)
                {
                    if (abs >= 1000000)
                    {
                        return (num / 1000000);
                    }
                    else if (abs >= 1000)
                    {
                        return (num / 1000.0f);
                    }
                }
                return num;
            }
            string GetSuffix(int num)
            {
                int abs = Mathf.Abs(num);
                if (type == KMGType.TenK)
                {
                    if (abs >= 10000000)
                    {
                        return "M";
                    }
                    else if (abs >= 10000)
                    {
                        return "K";
                    }
                }
                else if (type == KMGType.OneK)
                {
                    if (abs >= 1000000)
                    {
                        return "M";
                    }
                    else if (abs >= 1000)
                    {
                        return "K";
                    }
                }
                return "";
            }
        }
        public static string KMGC(float? number, bool reverseColor = false, KMGType type = KMGType.TenK)
        {
            return DecorateStr(KMG((int)number, type), number, false, reverseColor);
        }
        public static string KMGCS(float? number, bool reverseColor = false, KMGType type = KMGType.TenK)
        {
            return DecorateStr(KMG((int)number, type), number, true, reverseColor);
        }
        #endregion

        #region 百分比
        // 裁剪小数部分
        public static string AddPerSign(int? percent)
        {
            if (percent == null) return "";
            return string.Format("{0}%", percent.Value);
        }
        //百分号
        public static string PerToInt(float? percent, bool isHaveSignal = true)
        {
            if (percent == null) return "";
            return string.Format("{0}", (int)(percent.Value * 100)) + (isHaveSignal ? "%" : "");
        }
        public static string PerToIntCol(float? percent, bool isHaveSignal = true)
        {
            if (percent == null) return "";
            return DecorateStr(string.Format("{0}%", (int)(percent.Value * 100)), percent.Value, isHaveSignal, false);
        }
        // 保留小数部分
        public static string Per(float? percent)
        {
            if (percent == null) return "";
            return string.Format("{0}%", OneD(percent.Value * 100));
        }
        public static string PerSign(float? percent)
        {
            if (percent == null) return "";
            return percent >= 0 ? "+" + Per(percent) : Per(percent);
        }
        public static string PerC(float? percent, bool reverseColor = false)
        {
            if (percent == null) return "";
            return DecorateStr(Per(percent), percent, false, reverseColor);
        }
        public static string PerCS(float? percent, bool reverseColor = false)
        {
            if (percent == null) return "";
            return DecorateStr(Per(percent), percent, true, reverseColor);
        }
        // 天枰颜色
        public static string PerApparently(float? percent, string colorLeft = SysConst.COL_Yellow, string colorRight = SysConst.COL_Green)
        {
            if (percent == null) return "";
            return string.Format("<color={0}>{1}</color>", percent.Value <= 0.0f ? colorLeft : colorRight, Per(Mathf.Abs(percent.Value)));
        }
        #endregion

        #region UI Special
        // 1-100数值越大,颜色越红
        public static string RiseColInt(float num, bool isReverseColor = false, int min = 0, int max = 100)
        {
            int round = Mathf.RoundToInt(num);
            int small = Mathf.RoundToInt((max - min) * 0.3f + min);
            int big = Mathf.RoundToInt((max - min) * 0.7f + min);
            float colorSign = GetRiseColSign(round, small, big);
            if (isReverseColor)
            {
                colorSign = -colorSign;
            }

            return Decorate(round.ToString(), GetColor(colorSign));
        }
        // 1-100数值越小,颜色越红
        public static string DeriseColInt(float num, int min = 0, int max = 100)
        {
            return RiseColInt(num, true, min, max);
        }
        // 0-1数值越小,颜色越红
        public static string PerRiseCol(float num, bool isReverseColor = false, bool isHaveSignal = true)
        {
            string percentStr = PerToInt(num, isHaveSignal);
            if (!isReverseColor)
            {
                if (num > 0.7) return Green(percentStr);
                if (num <= 0.7f && num > 0.3f) return Yellow(percentStr);
                if (num <= 0.3f) return Red(percentStr);
            }
            else
            {
                if (num > 0.7) return Red(percentStr);
                if (num <= 0.7f && num > 0.3f) return Yellow(percentStr);
                if (num <= 0.3f) return Green(percentStr);
            }
            return percentStr;
        }
        public static float GetRiseColSign(int num, int small, int big)
        {
            return num < small ? -1 : (big < 70 ? 0 : 1);
        }
        public static string CDStyle(float f)
        {
            if (f > 1.0f)
                return Round(f);
            return string.Format("{0:0.0}", f);
        }
        #endregion

        #region UIColor
        public static string Nation(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Yellow);
        public static string Castle(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Green);

        public static string Yellow(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Yellow);
        public static string Yellow(float number) => Yellow(number.ToString());

        public static string Red(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Red);
        public static string Red(float number) => Red(number.ToString());

        public static string Green(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Green);
        public static string Green(float number) => Green(number);

        public static string Grey(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Grey);
        public static string Grey(float number) => Grey(number);

        public static string Religion(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Yellow);
        public static string TradeRes(string name) => string.Format("<color={1}>{0}</color>", name, SysConst.COL_Grey);

        public static Color FromHex(string str)
        {
            Color color = new Color();
            ColorUtility.TryParseHtmlString(str, out color);
            return color;
        }
        public static string ToHex(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }
        #endregion

        #region Misc
        public static string Indent(string str)
        {
            return SysConst.STR_Indent+str;
        }
        #endregion

        #region presenter
        public static void ResetRectTransform(RectTransform rectTransform)
        {
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        #endregion   
    }
}

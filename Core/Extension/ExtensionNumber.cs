using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CYM
{
    public static class ExtensionNumber
    {
        #region color
        public static string Red(this float number) => BaseUIUtil.Red(number);
        public static string Red(this string name) => BaseUIUtil.Red(name);
        public static string Yellow(this float number) => BaseUIUtil.Yellow(number);
        public static string Yellow(this string name) => BaseUIUtil.Yellow(name);
        public static string Green(this float number) => BaseUIUtil.Green(number);
        public static string Green(this string name) => BaseUIUtil.Green(name);
        public static string Grey(this float number) => BaseUIUtil.Grey(number);
        public static string Grey(this string name) => BaseUIUtil.Grey(name);
        #endregion

        #region str col
        public static Color ToColor(this string str) => BaseUIUtil.FromHex(str);
        public static string ToHex(this Color color) => BaseUIUtil.ToHex(color);
        public static string Indent(this string str) => BaseUIUtil.Indent(str);
        #endregion

        #region ceil floor
        public static string Floor(this float f) => BaseUIUtil.Floor(f);
        public static string Ceil(this float f) => BaseUIUtil.Ceil(f);
        #endregion

        #region per
        public static string PerSign(this int percent) => BaseUIUtil.AddPercentSign(percent);
        public static string Per(this float percent) => BaseUIUtil.Per(percent);
        public static string PerC(this float percent, bool reverseColor = false) => BaseUIUtil.PerC(percent, reverseColor);
        public static string PerCS(this float percent, bool reverseColor = false) => BaseUIUtil.PerCS(percent, reverseColor);
        public static string PerRiseCol(this float num, bool isReverseColor = false, bool isHaveSignal = true) => BaseUIUtil.PerRiseCol(num, isReverseColor, isHaveSignal);
        public static string PerSign(this float percent) => BaseUIUtil.PerSign(percent);
        public static string PerToInt(this float percent, bool isHaveSignal = true) => BaseUIUtil.PerToInt(percent, isHaveSignal);
        public static string PerToIntCol(this float percent, bool isHaveSignal = true) => BaseUIUtil.PerToIntCol(percent, isHaveSignal);
        #endregion

        #region kmg
        public static string KMG(this int number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number, type);
        public static string KMGC(this int number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number, reverseColor, type);
        public static string KMGCS(this int number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number, reverseColor, type);

        public static string KMG(this float number, KMGType type = KMGType.TenK) => BaseUIUtil.KMG(number, type);
        public static string KMGC(this float number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGC(number, reverseColor, type);
        public static string KMGCS(this float number, bool reverseColor = false, KMGType type = KMGType.TenK) => BaseUIUtil.KMGCS(number, reverseColor, type);
        #endregion

        #region col
        public static string ColSign(this float number, bool reverseColor = false) => BaseUIUtil.ColSign(number, reverseColor);
        public static string ColSign(this int number) => BaseUIUtil.ColSign(number);
        #endregion

        #region D
        public static string OneD(this float f, bool isOption = false) => BaseUIUtil.OneD(f, isOption);
        public static string OneDC(this float f, bool isReverseCol = false) => BaseUIUtil.OneDC(f, isReverseCol);
        public static string OneDCS(this float f, bool isReverseCol = false) => BaseUIUtil.OneDCS(f, isReverseCol);
        public static string OneDS(this float f) => BaseUIUtil.OneDS(f);
        public static string TwoD(this float f, bool isOption = false) => BaseUIUtil.TwoD(f, isOption);
        public static string TwoDC(this float f, bool isReverseCol = false) => BaseUIUtil.TwoDC(f, isReverseCol);
        public static string TwoDCS(this float f, bool isReverseCol = false) => BaseUIUtil.TwoDCS(f, isReverseCol);
        public static string TwoDS(this float f) => BaseUIUtil.TwoDS(f);
        #endregion

        #region sign
        public static string Sign(this string str) => BaseUIUtil.Sign(str);
        public static string Sign(this float val, string str) => BaseUIUtil.Sign(val, str);
        public static string Sign(this float val) => BaseUIUtil.Sign(val);
        #endregion

        #region round
        public static string Round(this float f) => BaseUIUtil.Round(f);
        public static string RoundColor(this float f) => BaseUIUtil.RoundColor(f);
        public static string RoundD(this float f) => BaseUIUtil.RoundD(f);
        public static string RoundSign(this float f) => BaseUIUtil.RoundSign(f);
        #endregion

        #region other
        public static string Bool(this float val) => BaseUIUtil.Bool(val);
        public static string Bool(this bool val) => BaseUIUtil.Bool(val);
        public static string CDStyle(this float f) => BaseUIUtil.CDStyle(f);
        #endregion
    }
}

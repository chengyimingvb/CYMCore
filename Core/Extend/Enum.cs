using System;
using System.Linq.Expressions;
//**********************************************
// Discription	：Base Core Calss .All the Mono will inherit this class
// Author	：CYM
// Team		：MoBaGame
// Date		：2020-7-16
// Copyright ©1995 [CYMCmmon] Powered By [CYM] Version 1.0.0 
// Desc     ：此代码由陈宜明于2020年编写,版权归陈宜明所有
// Copyright (c) 2020 陈宜明 All rights reserved.
namespace CYM
{
    [Serializable, Unobfus]
    public static class Enum<T> where T : Enum
    {
        #region prop
        static readonly Func<T, int> _wrapper;
        static readonly Func<int, T> _wrapperInvert;
        #endregion

        #region set
        public static T Start()
        {
            return (T)Enum.GetValues(typeof(T)).GetValue(0);
        }
        public static T End()
        {
            var data = Enum.GetValues(typeof(T));
            return (T)data.GetValue(data.Length-1);
        }
        public static int Length()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
        public static int Int(T enu)
        {
            return _wrapper(enu);
        }
        public static T Invert(int val)
        {
            return _wrapperInvert(val);
        }
        public static T Parse(string str)
        {
            var ret = Enum.Parse(typeof(T), str);
            return (T)ret;
        }

        public static void For(Action<T> callback)
        {
            for (var type = 0; type < Enum.GetValues(typeof(T)).Length; ++type)
            {
                callback(Invert(type));
            }
        }

        public static void ForIndex(Action<int> callback)
        {
            for (var type = 0; type < Enum.GetValues(typeof(T)).Length; ++type)
            {
                callback(type);
            }
        }
        #endregion

        #region life
        static Enum()
        {
            var p = Expression.Parameter(typeof(T), null);
            var c = Expression.ConvertChecked(p, typeof(int));
            _wrapper = Expression.Lambda<Func<T, int>>(c, p).Compile();
            //逆向
            var p2 = Expression.Parameter(typeof(int), null);
            var c2 = Expression.ConvertChecked(p2, typeof(T));
            _wrapperInvert = Expression.Lambda<Func<int, T>>(c2, p2).Compile();
        }
        #endregion
    }
}

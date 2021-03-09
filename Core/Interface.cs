using System.Collections;

//**********************************************
// Discription	：Base Core Calss .All the Mono will inherit this class
// Author	：CYM
// Team		：MoBaGame
// Date		：2020-7-16
// Copyright ©1995 [CYMCmmon] Powered By [CYM] Version 1.0.0 
// Desc     ：此代码由陈宜明于2020年编写,版权归陈宜明所有
// Copyright (c) 2020 陈宜明 All rights reserved.
//**********************************************

namespace CYM
{
    #region Callback
    public delegate void Callback();
    public delegate void Callback<T>(T arg1);
    public delegate void Callback<T, U>(T arg1, U arg2);
    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
    public delegate void Callback<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);
    public delegate void Callback<T, U, V, W, Z>(T arg1, U arg2, V arg3, W arg4, Z arg5);
    #endregion

    #region enum
    public enum LevelType
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
    }
    public enum GameDiffType
    {
        Simple,//简单
        Ordinary,//普通
        Difficulty,//困难
        Extremely,//变态
    }
    public enum GamePropType
    {
        Low,//简单
        Middle,//普通
        Hight,//困难
    }
    #endregion

    #region other
    public interface IBase
    {
        long ID { get; set; }
        string TDID { get; set; }
    }
    public interface ILoader
    {
        IEnumerator Load();
        string GetLoadInfo();
    }
    public interface IResRegister<T2>
    {
        T2 this[string name] { get; }
        void Add(T2 c);
        void Add(string name, T2 c);
        void Remove(T2 c);
        void Remove(string name);
        T2 Data(string name);
        bool ContainsKey(string name);
        void Clear();
    }
    #endregion
}

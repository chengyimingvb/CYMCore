﻿//using System;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CYM
{
    [Unobfus]
    public class BinData<T> where T :class
    {
        public T Data { get; private set; }
        protected string File = "BinData";
        public BinData(string fileName)
        {
            File = fileName;
            var bin = Resources.Load<TextAsset>(File);
            if (bin == null)
            {
                Debug.LogError("没有这个文件:"+File);
                return;
            }
            Data = BaseFileUtil.BytesToObject(bin.bytes) as T;
        }
    }
}

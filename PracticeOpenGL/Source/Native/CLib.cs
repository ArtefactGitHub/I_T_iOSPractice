﻿using System;
using System.Runtime.InteropServices;

//namespace Repro
//{
public static class CFunctions
{
    // extern int32_t clib_add_internal (int32_t left, int32_t right);
    [DllImport("__Internal")]
    //static extern int clib_add_internal (int left, int right);
    public static extern int clib_add_internal(int left, int right);
}
//}

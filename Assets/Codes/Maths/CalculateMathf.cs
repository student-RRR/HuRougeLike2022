using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculateMathf
{
    // ²Ö¥[
    public static int Accumulate(int num)
    {
        if (num == 0)
            return 0;

        return num + Accumulate(num - 1);
    }

    // ¨úµ´¹ï­È
    public static int AbsoluteValue(int num)
    {
        if (num > 0)
            return num;
        return -num;
    }
}

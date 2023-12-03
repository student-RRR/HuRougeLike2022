using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculateMathf
{
    // �֥[
    public static int Accumulate(int num)
    {
        if (num == 0)
            return 0;

        return num + Accumulate(num - 1);
    }

    // �������
    public static int AbsoluteValue(int num)
    {
        if (num > 0)
            return num;
        return -num;
    }
}

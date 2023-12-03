using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RangeMathf
{
    // 取得周圍8格座標
    public static Position2D[] Get8MazeRange(Position2D _pos)
    {
        Position2D[] position2Ds = new Position2D[8];
        int top = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                position2Ds[top++] = new Position2D(_pos.x + i, _pos.y + j);
            }
        }
        
        return position2Ds;
    }

    // 取得周圍附近n程度的座標
    public static Position2D[] GetScopeRange(Position2D _pos, int scope)
    {
        // 計算總格數
        int slotNum = CalculateMathf.Accumulate(scope) * 4;
        LogServise.Log("slotNum:" + slotNum);

        Position2D[] position2Ds = new Position2D[slotNum]; // 減1是因為不算中心
        int top = 0;

        

        for (int i = -scope; i <= scope; i++)
        {
            for (int j = -scope; j <= scope; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                if (CalculateMathf.AbsoluteValue(i) + CalculateMathf.AbsoluteValue(j) > scope)
                    continue;

                position2Ds[top++] = new Position2D(_pos.x + i, _pos.y + j);
            }
        }

        return position2Ds;
    }

}

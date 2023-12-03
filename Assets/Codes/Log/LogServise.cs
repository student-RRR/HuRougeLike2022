using System.Collections;
using System.Collections.Generic;

public static class LogServise
{
    private static Queue<string> logText = new Queue<string>();

    private static string tempString = null;
    // �g�JLOG
    public static void Log(string input)
    {
        logText.Enqueue(input);
    }

    public static string PlayLog()
    {
        if (logText.TryDequeue(out tempString))
        {
            return tempString;
        }

        return null;
        
    }

    // ���X01�}�C�a��
    public static string CreateArrayMapString(bool[,] room)
    {
        if (room == null)
            return null;

        var msg = "";

        for (int j = 0; j < room.GetLength(1); j++)
        {
            for (int i = 0; i < room.GetLength(0); i++)
            {
                if (room[i, j] == true)
                    msg += 1;
                else
                    msg += 0;
            }
            msg += "\n";
        }

        return msg;
    }


}

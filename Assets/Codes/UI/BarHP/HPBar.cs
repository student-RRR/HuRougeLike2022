using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar
{
    public GameObject barObj;
    private float initBarLength;

    // 條Rate
    private float barRate;
    public float BarRate
    {
        get
        {
            return barRate;
        }
        set
        {
            barRate = value;
            BarLengthp = initBarLength * barRate;
        }
    }

    private float barLengthp;
    public float BarLengthp
    {
        get 
        {
            return barLengthp;
        }
        set 
        {
            if (value < 0)
            {
                value = 0;
            }

            barLengthp = value;
            this.barObj.transform.localScale = new Vector3( barLengthp, 
                                                            this.barObj.transform.localScale.y, 
                                                            this.barObj.transform.localScale.z);
        }
    }

    public HPBar()
    {
        barObj = HuRougeLike2022Factory.GetAssetFactory().LoadUIModel(ENUM_UI.BarHP);
        barObj = (UnityEngine.Object.Instantiate(barObj) as GameObject);

        // 初始Bar長度
        initBarLength = barObj.transform.lossyScale.y;
    }




}

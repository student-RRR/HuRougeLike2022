using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureElf : ICreature
{
    // 預設資料
    private new string name = "無名精靈";
    //name 成員則是透過公用唯讀屬性存取
    public string Name {
        get { return name; }
        set { name = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] // Makes the class serializable so it can be saved out to a file
public class CreatureManager
{
    public List<ICharacter> creatureList { get; set; }

}

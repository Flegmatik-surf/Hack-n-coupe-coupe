using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Global Characters Data", menuName = "Global Character Data")]
public class GlobalCharacterData : ScriptableObject
{
    public CharacterData warrior;
    public CharacterData bowman;
    public CharacterData samourai;
}

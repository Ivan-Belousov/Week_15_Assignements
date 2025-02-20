using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;          // <-- Added this

public class CharacterEvents
{
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;
}
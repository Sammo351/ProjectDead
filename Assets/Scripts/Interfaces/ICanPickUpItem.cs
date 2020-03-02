using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanPickUpItem {
    bool OnPickUpItem (Drop drop);
}
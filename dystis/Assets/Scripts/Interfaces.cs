using UnityEngine;
using System.Collections;

public interface ITalkable {
    void TalkWith();
}

public interface IDamageable {
    void DamageIt(float damageAmount);
}
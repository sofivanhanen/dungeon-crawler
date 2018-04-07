using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Attack_test 
{

    [UnityTest]
    public IEnumerator AttackStartsSwingAnimation()
    {
        // Not yet tested
        var sword = new GameObject();
        var controller = sword.AddComponent<SwordController>();
        var animator = sword.AddComponent<Sword_anim_controller>();
        sword.Attack();
        yield return null; // Skip a frame
        Assert.True(GetCurrentAnimatorStateInfo(0).IsName("Sword_swing"));
    }
}
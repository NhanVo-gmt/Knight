using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : SingletonObject<Test>
{
    protected override void Awake()
    {
        base.Awake();
    }
}

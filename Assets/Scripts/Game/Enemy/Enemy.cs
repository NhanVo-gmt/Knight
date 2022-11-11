using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data; // todo set private (for unity editor to see)

    AttackData touchAttackData;

    Core core;
    Health health;
    Combat combat;
    VFXController vfx;
    public BehaviourTreeComponent treeComponent {get; private set;}

    Collider2D col;

    #region Set up
    
    void Awake() 
    {
        col = GetComponent<Collider2D>();
        core = GetComponentInChildren<Core>();
        touchAttackData = FindObjectOfType<GameSettings>().TouchAttackSettings;
        
        data = Instantiate(data);

        SetUpBehaviourTree();
    }

    void SetUpBehaviourTree()
    {
        InitializeTreeComponent();

        CloneTree();
    }

    void InitializeTreeComponent() 
    {
        treeComponent = BehaviourTreeComponent.CreateTreeComponentFromGameObject(gameObject, data);
    }

    void CloneTree() 
    {
        data.tree = data.tree.Clone();
    }

    void Start() 
    {
        GetCoreComponent();

        InitializeTreeNodeComponent();
    }

    private void GetCoreComponent()
    {
        health = core.GetCoreComponent<Health>();
        combat = core.GetCoreComponent<Combat>();
        vfx = core.GetCoreComponent<VFXController>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpDamagerType(IDamageable.DamagerType.Enemy);
        health.SetHealth(data.healthData);
    }

    void InitializeTreeNodeComponent()
    {
        Player player = FindObjectOfType<Player>();
        
        data.tree.Traverse(data.tree.rootNode, (n) =>
        {
            n.treeComponent = treeComponent;
            n.player = player;
        });
    }

    #endregion

    #region Unity Call back

    void Update() 
    {
        data.tree.Update();
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(touchAttackData, IDamageable.DamagerType.Enemy);
        }
    }
}

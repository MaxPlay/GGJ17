using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Ability
{

    [SerializeField]
    protected float cooldown;
    protected float usedCooldown;

    public float Cooldown
    {
        get { return cooldown; }
    }

    private bool used;

    public bool Used
    {
        get { return used; }
    }

    public bool Usable
    {
        get { return usedCooldown <= 0; }
    }

    public float CooldownPercentage
    {
        get
        {
            if (cooldown == 0)
                return 1;
            return 1 - (usedCooldown / cooldown);
        }
    }

    public void Reset()
    {
        used = false;
    }

    public void Use()
    {
        usedCooldown = cooldown;
        used = true;
    }

    public virtual void Update()
    {
        if (usedCooldown > 0)
            usedCooldown -= Time.deltaTime;
    }
}
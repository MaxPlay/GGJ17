﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaves : MonoBehaviour
{
    public float Sin;
    public float Orb;
    void Start()
    {
        morphID = Animator.StringToHash("Morph");
        movespeedID = Animator.StringToHash("Movespeed");
        movementID = Animator.StringToHash("Moving");
        attackingID = Animator.StringToHash("Attacking");
        jumpID = Animator.StringToHash("Jump");
        deathID = Animator.StringToHash("Dead");
        landingAttackID = Animator.StringToHash("LandingAttack");
        pushAttackID = Animator.StringToHash("PushAttack");
        steppedID = Animator.StringToHash("Stepped");



        animator = GetComponent<Animator>();
        material = GetComponentInChildren<Renderer>().material;
        skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        emissionID = Shader.PropertyToID("_EmissionColor");
        albedoID = Shader.PropertyToID("_Color");
    }

    void Update()
    {
        FeedAnimator();
        ChangeMaterial();
        LerpChibby();
    }

    private const int runningLayerID = 1;
    private const int chibbyLayerID = 2;

    private int morphID = Animator.StringToHash("Morph");
    private int movespeedID;
    private int movementID;
    private int attackingID;
    private int jumpID;
    private int deathID;


    private int landingAttackID;
    private int pushAttackID;
    private int steppedID;

    public float Movespeed;
    public bool Attacking;
    public bool Jump;
    public bool Dead;

    public void OnLandingAttack()
    {
        animator.SetTrigger(landingAttackID);
    }

    public void OnPushAttack()
    {
        animator.SetTrigger(pushAttackID);
    }

    public void OnDamage()
    {
        animator.SetTrigger(steppedID);
    }

    void FeedAnimator()
    {
        animator.SetLayerWeight(chibbyLayerID, Sin);
        animator.SetFloat(morphID, 1 - Sin);
        animator.SetBool(attackingID, Attacking);
        animator.SetBool(jumpID, Jump);
        animator.SetBool(deathID, Dead);

        animator.SetBool(deathID, Dead);


        animator.SetFloat(movespeedID, Movespeed);
        animator.SetLayerWeight(runningLayerID, Movespeed);
        if (Movespeed > 0.0f)
        {
            animator.SetBool(movementID, true);
        }
        else
        {
            animator.SetBool(movementID, false);
        }
    }


    public Color BigColor;
    public Color SmallColor;
    public Color CurrentColor;
    public float SmallMultiplier;
    private Animator animator;
    private SkinnedMeshRenderer skinnedRenderer;
    private Material material;
    private int emissionID;
    private int albedoID;
    void ChangeMaterial()
    {
        CurrentColor = Color.Lerp(BigColor, SmallColor, Sin);
        material.SetColor(albedoID, CurrentColor);
        material.SetColor(emissionID, CurrentColor);
        float scaleValue = 1 - SmallMultiplier * Sin;
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    void LerpChibby()
    {
        skinnedRenderer.SetBlendShapeWeight(0, Sin * 100);
    }

    void DeathOrb()
    {
        skinnedRenderer.SetBlendShapeWeight(1, Orb);
    }
}

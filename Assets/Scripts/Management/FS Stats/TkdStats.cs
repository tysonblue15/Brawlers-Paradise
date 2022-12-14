using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TkdStats : MonoBehaviour
{
    public float firstAttackTime = 1;
    public float firstAttackDistance = 9;
    public float secondAAttackTime = 1.12f;
    public float secondAAttackDistance = 7;
    public float secondBAttackTime = 1.12f;
    public float secondBAttackDistance = 7;
    public float thirdAttackTime = 1.12f;
    public float thirdAttackV1Distance = 7;
    public float thirdAttackV2Distance = 7;
    public float thirdAttackV3Distance = 7;
    public float thirdAttackV4Distance = 7;

    [Header("Fight Type Stats")]
    public int[] tkdFightTypeStats = new int[5];

    [Header("Combo")]
    public float comboTime = 2.5f;
    public int comboDamage = 30;

    [Header("Ai Combo Usage")]
    [Range(0, 10)] public int aiComboFrequency;


    #region Animation
    /* 0 - Boxing Combat Idle
     * 2 - Run
     * 3 - TKD Basic Attack 1
     * 4 - TKD Basic Attack 2V1
     * 5 - TKD Basic Attack 2V2
     * 6V1 - TKD Basic Attack 3V1
     * 6V2 - TKD Basic Attack 3V2
     * 7V1 - TKD Basic Attack 3V3
     * 7V2 - TKD Basic Attack 3V4
     * 
     * 10 - TKD Combo
     * 
     * 15 - TKD Counter Attack
     * 
     * 20 - TKD Parry
     * 
     * 25 - Backwards Dodge
     * 26 - Forward Dodge
     *
     * 40 - Throw Idle
     * 41 - Throw Aim
     * 
     * 52 - Grabbed
     * 
     * 100 - Flinch 1
     * 101 - Flinch 2
     * 
     * 105 - Stunned
     * 
     * 107 - Ground Flinch 1
     * 
     * 110 - Knockback
     * 
     * 115 - Knockdown
     * 
     * 118 - Recovery
     * 
     * 120 - Parried
     */

    #endregion
}

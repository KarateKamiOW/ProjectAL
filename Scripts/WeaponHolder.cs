using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public static WeaponHolder instance;


    [SerializeField] Weapon testwep1;
    Weapon[] equippedWeps = new Weapon[1];

    public Weapon[] EquippedWeps 
    { get { return equippedWeps; } }

    //List<Weapon> equippedWeps;

    private void Awake()
    {
        instance = this;
        equippedWeps[0] = testwep1;
        
    }
    private void Start()
    {
        SetWeaponInfo();
    }
    void SetWeaponInfo() 
    {
        var equippedWep = equippedWeps[0];
        var shootInfo = Shooting.instance;

        shootInfo.bullet = equippedWep.Bullet;
        shootInfo.bulletLeft = equippedWep.BulletLeft;
        shootInfo.bulletSpeed = equippedWep.BulletSpeed;
        shootInfo.anim.runtimeAnimatorController = equippedWep.WepContr;
        shootInfo.firerate = equippedWep.FireRate;
        shootInfo.range = equippedWep.Range;
        shootInfo.wepSprite.sprite = equippedWep.WepSprite;
        shootInfo.TheWepType = equippedWep.WepType;
        shootInfo.BurstShots = equippedWep.BurstShots;

        //Set The Variables
        var shootingPointOffsetX = shootInfo.spX;
        var shootingPointOffsetY = shootInfo.spY;
        var eqpWepSPointX = equippedWep.ShootPoint.x;
        var eqpWepSPointY = equippedWep.ShootPoint.y;
        

        //Equal the variable with eachother
        shootingPointOffsetX = eqpWepSPointX;
        shootingPointOffsetY = eqpWepSPointY;
        //Set The ShootingPoint Positions
        shootInfo.spX = shootingPointOffsetX;
        shootInfo.spY = shootingPointOffsetY;
        

        shootInfo.OffsetShootingPosition();

    }

}

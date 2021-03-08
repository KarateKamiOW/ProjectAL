using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName = "Inventory System/Weapon")]
public class Weapon : ItemObject
{
    [Header("Weapon Info and Stats")]
    [SerializeField] WeaponType wepType;
    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField] Sprite wepSprite;
    [SerializeField] Vector3 shootPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletLeft;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] int magSize;
    [SerializeField] float reloadSpeed;

    [SerializeField] RuntimeAnimatorController wepContr;

    [Header("isBurst? How Many Shots")]
    [SerializeField] int burstShots;

    public enum WeaponType 
    { 
        None,
        Melee,
        Burst,
        SemiAuto,
        Blade,
        Automatic
    }

    public enum Personality 
    { 
        None,
        Enthusiastic,
        Brave,
        Witty,
        Compassionate,
        Rude,
        Artistic,
        Aggressive,
        Amoral,
        Embarrassed,
        Grumpy

    }

    public string Name 
    { get { return name; } }

    public WeaponType WepType 
    { get { return wepType; } }

    public int Damage 
    { get { return damage; } }

    public float Range 
    { get { return range; } }

    public Sprite WepSprite 
    { get { return wepSprite; } }

    public Vector3 ShootPoint 
    { get { return shootPoint; } }

    public GameObject Bullet 
    { get { return bullet; } }

    public GameObject BulletLeft
    { get { return bulletLeft; } }

    public float BulletSpeed 
    { get { return bulletSpeed; } }

    public float FireRate
    { get { return fireRate; } }

    public int MagSize 
    { get { return magSize; } }

    public RuntimeAnimatorController WepContr 
    { get { return wepContr; } }

    public int BurstShots 
    { get { return burstShots; } }


}

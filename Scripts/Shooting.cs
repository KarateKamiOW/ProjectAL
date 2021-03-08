using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance;

    public Transform gun;
    public Transform shootPoint;    //Attach shootPoint as a child of shotPointHolder
    public GameObject shootPointHolder;     //We will move the gameobjects transform instead of the shootpoint
                                            //We do so to offset different shooting positions for diff. guns.
                                            //Moving a parent gameobject is easier to handle and understand.
    Vector2 direction;
    public GameObject bullet 
    { get; set; }
    public GameObject bulletLeft 
    { get; set; }       //Bullet needs a left sprite to instatiate facing the proper way
    public float bulletSpeed 
    { get; set; }
    public float range 
    { get; set; }     //Currently, range is determined by 'time until bullet is destroyed'  Very Short Ranges destroy the bullet sooner
    public Animator anim;
    public SpriteRenderer wepSprite;

    public float firerate 
    { get; set; }
    float readyForNextShot = 1;
    float timeBtwnShots = 0;
    //{ get; set; }
    float tuckGun = 4;
    CharacterController2D contr;
    bool burstStillShooting = false;
    

    public Weapon.WeaponType TheWepType
    { get; set; }
    public int BurstShots 
    { get; set; }

    
  

    
    public bool FacingRight 
    { get; set; }
    public float spX    //spx or ShootingPointX is used as a temp variable
    { get; set; }
    public float spY    //spY or ShootingPointY is used similarly
    { get; set; }
    Vector3 offsetPos;  //Temp used for shootPointHolder position.

    private void Awake()
    {
        instance = this;
        FacingRight = true;

        offsetPos = shootPointHolder.transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {
        
        tuckGun -= Time.deltaTime;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)gun.position;
        FaceMouse();

        if (TheWepType == Weapon.WeaponType.Automatic)
            ShotConfigurationAutomatic();
        else if (TheWepType == Weapon.WeaponType.SemiAuto || TheWepType == Weapon.WeaponType.Burst)
            ShotConfigurationSemiAuto();


        /*if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time > readyForNextShot) 
            {
                gun.gameObject.SetActive(true);
                readyForNextShot = Time.time + 1 / firerate;
                Shoot();
                tuckGun = 4;
            }
            
        }*/

        /*if (timeBtwnShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                gun.gameObject.SetActive(true);
                Shoot();
                tuckGun = 4;
                timeBtwnShots = 1 / firerate;
                //timeBtwnShots = readyForNextShot;
            }
        }
        else 
        {
            timeBtwnShots -= Time.deltaTime;
        }*/
        if (tuckGun <= 0) 
        {
            gun.gameObject.SetActive(false);
        }
    }
    void Shoot() 
    {
        GameObject bulletIns;
        var theBullet = bullet;
        var bulletRot = bullet.transform.rotation.z;
        Vector3 shootingPoint = shootPoint.position; 
        
        if (FacingRight)
        {
            bulletRot = 1;
            bulletIns = Instantiate(bullet, shootingPoint, shootPoint.rotation);
        }
        else 
        {
            bulletRot = -1;
            bulletIns = Instantiate(bulletLeft, shootingPoint, shootPoint.rotation);
            
        }

        if(FacingRight)
            bulletIns.GetComponent<Rigidbody2D>().AddForce(bulletIns.transform.right * bulletSpeed);
        else
            bulletIns.GetComponent<Rigidbody2D>().AddForce((bulletIns.transform.right * -1) * bulletSpeed);

        anim.SetTrigger("Shoot");
        

        Destroy(bulletIns, range);   
    }

    void FaceMouse() 
    {
        if (FacingRight)
        {
            gun.transform.right = direction;   
        }
        else 
        { 
            gun.transform.right = direction * -1;
        }
    }

    public void OffsetShootingPosition() 
    {
        Vector3 theOffset = new Vector3(spX, spY, 0);
        shootPointHolder.transform.position = offsetPos + theOffset;
    }

    void ShotConfigurationAutomatic() 
    {
        if (timeBtwnShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                gun.gameObject.SetActive(true);
                Shoot();
                tuckGun = 4;
                timeBtwnShots = 1 / firerate;
            }
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }

    void ShotConfigurationSemiAuto() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            gun.gameObject.SetActive(true);
            if (Time.time > readyForNextShot) 
            {
                readyForNextShot = Time.time + 1 / firerate;
                if (TheWepType == Weapon.WeaponType.Burst) 
                {
                    for (int i = 0; i < BurstShots; i++) 
                    {
                        Shoot();
                        StartCoroutine(BurstConfiguration());
                    }
                    
                }
                else
                    Shoot();

                tuckGun = 4;
            }           
        }
    }

    IEnumerator BurstConfiguration() 
    {
        for (int i = 0; i < BurstShots; i++) 
        {
            Shoot();
            yield return new WaitForSeconds(.15f);
        }
        yield break;
    }
}

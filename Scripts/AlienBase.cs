using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBase : MonoBehaviour
{
    public static AlienBase instance;

    public InventoryObject inventory;

    [SerializeField] string name;

    //Gender gender;
    //Race race;

    Sprite sprite;
    private void Awake()
    {
        instance = this;
    }

    public enum Gender
    {
        None,
        Male,
        Female,
        Neutral
    }

    public enum Race
    {
        None,
        Neafum,
        Norps,
        Tiggle,
        Quip,
        Crilla,
        Mip,
    }

    public string Name
    { get; set; }
    public Sprite AlienSprite
    { get { return sprite; } }

    public Gender AlienGender { get; set; }
    public Race AlienRace { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if (item) 
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

}

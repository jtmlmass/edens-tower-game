using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum Type
    {
        // Types are:
        Currency,       // 0
        Quest,          // 1
        Weapon,         // 2
        Consumables     // 3

    }
    public int id;
    public string title;
    public Type type;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public Item(int id, string title, Type type, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.type = type;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.stats = item.stats;
    }
    
    public string toString()
    {
        return (title + " " + id.ToString());
    }

}

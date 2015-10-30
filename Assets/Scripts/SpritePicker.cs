using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SpritePicker : MonoBehaviour
{

    public string sheetname;
    private Sprite[] sprites;
    private SpriteRenderer sr;
    private Sprite sprite;
    private string[] names;

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/WeaponSprites");
        sr = GetComponent<SpriteRenderer>();
        names = new string[sprites.Length];

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = sprites[i].name;
        }
    }

    public void ChangeSprite(int index)
    {
        Sprite sprite = sprites[index];
         sr.sprite = sprite;
    }

    public void ChangeSpriteByName(string name)
    {
        Sprite sprite = sprites[Array.IndexOf(names, name)];
        sr.sprite = sprite;
    }

    public Sprite getSpriteByName(string name)
    {
        Sprite sprite = sprites[Array.IndexOf(names, name)];
        return sprite;
    }
    public void setSpriteByName(string name)
    {
        SpriteRenderer item = this.GetComponentInChildren<SpriteRenderer>();
        Debug.Log("Printing item type: "+ item.GetType());
        item.sprite = sprites[Array.IndexOf(names, name)];
    }
    public void setSpriteByIndex(int index)
    {
        SpriteRenderer item = this.GetComponentInChildren<SpriteRenderer>();
        Debug.Log("Printing item type: "+item.GetType());
        item.sprite = sprites[index];
    }
    public void printAllSprites()
    {
        foreach(Sprite s in sprites)
        {
            Debug.Log(s.name);
        }
    }
}
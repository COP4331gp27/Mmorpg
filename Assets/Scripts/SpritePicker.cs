using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SpritePicker : MonoBehaviour
{

    public string sheetname;
    public Sprite[] sprites;
    private SpriteRenderer sr;
    private Sprite sprite;
    private string[] names;

    void Awake()
    {
        //sprites = Resources.LoadAll<Sprite>("Sprites/WeaponSprites.png");
        
        sr = GetComponentInChildren<SpriteRenderer>();
        names = new string[sprites.Length];

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = sprites[i].name;
        }
        //printAllSprites();
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
        //SpriteRenderer item = this.GetComponentInChildren<SpriteRenderer>();
        Debug.Log("Printing item type: "+ sr.GetType());
        sr.sprite = sprites[Array.IndexOf(names, name)];
    }
    public void setSpriteByIndex(int index)
    {
        //Debug.Log("Index: " + index+" Sprite name: "+sprites[index+1].ToString());
        //Debug.Log("Sprite is located in: " + this.transform.ToString());
        //Debug.Log("Printing item type: "+sr.GetType());
        //Debug.Log("Name of " + index + " sprite is: " + sprites[index].ToString());
        //sprite = sprites[index];
        this.GetComponentInChildren<SpriteRenderer>().sprite = sprites[index];
        //Debug.Log("Random sprite: " + sprites[32]);
    }
    public void printAllSprites()
    {
        foreach(Sprite s in sprites)
        {
            Debug.Log(s.name);
        }
    }
}
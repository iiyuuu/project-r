using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    public GameObject ammoPreFab;
    public PlayerStats _playerStats;
    List<Bullet> bullets = new List<Bullet>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating Bullets");
        DrawBullets();
    }

    public void DrawBullets()
    {
        ClearBullets();

        float maxAmmo = _playerStats.maxAmmo;//checks how many half hearts to add to the end
        for(int i = 0; i < maxAmmo; i++)//create empty heart shell depending on hp
        {
            Debug.Log("Created" + i);
            CreateBullet();
        }
    }

    public void CreateBullet()
    {
        GameObject newBullet = Instantiate(ammoPreFab);//instantiate prefab
        newBullet.transform.SetParent(transform);//set transform parent

        Bullet newBulletComp = newBullet.GetComponent<Bullet>();//telling component to be empty and update sprite and list accordingly
        bullets.Add(newBulletComp);
    }

    public void ClearBullets()//destroys everything under the parent object
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        bullets = new List<Bullet>();
    }

}

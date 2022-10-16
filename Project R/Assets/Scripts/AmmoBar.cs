using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    public GameObject ammoPreFab;
    public PlayerStats _playerStats;
    List<Bullet> bullets = new List<Bullet>();
    int Ammo;
    int help = 0;
    // Start is called before the first frame update
    private void OnLevelWasLoaded()
    {
        DrawBullets();
    }
    void Start()
    {
        
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Ammo = _playerStats.maxAmmo;
        DrawBullets();
    }

    private void FixedUpdate()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (_playerStats.currentAmmo < Ammo)
        {
            removeBullet();
            Ammo -= 1;
        }
        else if (_playerStats.currentAmmo > Ammo)
        {
            CreateBullet();
            Ammo += 1;
        }
    }

    public void DrawBullets()
    {
        ClearBullets();

        float maxAmmo = _playerStats.maxAmmo;//checks how many half bullets to add to the end
        for(int i = 0; i < maxAmmo; i++)//create empty bullet shell depending on ammo
        {
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

    public void removeBullet()
    {
        int size = bullets.Count;
        bullets.Remove(bullets[size - 1]);
        foreach (Transform t in transform)
        {
            help += 1;
            if(_playerStats.currentAmmo < help)
            {
                Destroy(t.gameObject);
            }
        }
        help = 0;

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

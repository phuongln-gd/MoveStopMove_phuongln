using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Transform tf;
    public MeshRenderer rend;
    public List<Material> materials;

    private bool flag = false;
    
    private void Update()
    {
        if (flag)
        {
            Vector3 posPlayer = LevelManager.Ins.player.tf.position;
            if(Vector3.Distance(tf.position,posPlayer) > LevelManager.Ins.player.attackRange)
            {
                ChangeMaterial(0);
                flag = false;
            }
        }
    }
    public async void ChangeMaterial(int i)
    {
        flag = true;
        rend.material = materials[i];
    }
}

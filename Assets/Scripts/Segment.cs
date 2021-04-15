using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Segment : MonoBehaviour
{
    public int SegId { set; get; }
    public bool transition;

    public int length;
    public int beginZ1, beginZ2, beginZ3;
    public int endZ1, endZ2, endZ3;

    private PieceSpawner[] pieces;

    private void Awake()
    {
        pieces = gameObject.GetComponentsInChildren<PieceSpawner>();
        for (int i = 0; i < pieces.Length; i++)
            foreach (MeshRenderer mr in pieces[i].GetComponentsInChildren<MeshRenderer>())
                mr.enabled = LevelMenagger.Instance.showCollider;
    
    }

    public void Spawn()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < pieces.Length; i++)
            pieces[i].Spawn();
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < pieces.Length; i++)
            pieces[i].Despawn();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;
    public void Spawn()
    {
        int amtObj = 0;
        switch(type)
        {
            case PieceType.jump:
                amtObj = LevelMenagger.Instance.jumps.Count;
                break;
            case PieceType.slide:
                amtObj = LevelMenagger.Instance.slides.Count;
                break;
            case PieceType.skip:
                amtObj = LevelMenagger.Instance.skips.Count;
                break;
            case PieceType.ramp:
                amtObj = LevelMenagger.Instance.ramps.Count;
                break;
            case PieceType.longblock:
                amtObj = LevelMenagger.Instance.longblocks.Count;
                break;
        }
        currentPiece = LevelMenagger.Instance.GetPiece(type, Random.Range(0, amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }
    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}

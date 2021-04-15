
using UnityEngine;

public enum PieceType
{
    none = - 1,
    ramp = 0,
    longblock = 1,
    jump = 2,
    slide = 3,
    skip = 4,
}
public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    public PieceType type;
    public int visualIndex;
}

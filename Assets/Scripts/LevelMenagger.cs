using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelMenagger : MonoBehaviour
{
    public static LevelMenagger Instance { set; get; }

    private const float distanceBeforeSpawn = 200.0f;
    private const int initialSegments = 15;
    private const int initialTransitionSegments = 2;
    private const int maxSegmentsOnScreen = 25;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnX;
    private int currentLevel;
    private int z1, z2, z3;
    public bool showCollider = true;

    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> skips = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();

    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();


    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnX = 0;
        currentLevel = 0;
    }

    private void Start()
    {
        for (int i = 0; i < initialSegments; i++)
            if (i < initialTransitionSegments)
                SpawnTransition();
            else
            GenerateSegment();

    }

    private void Update()
    {
        if (currentSpawnX - cameraContainer.position.x < distanceBeforeSpawn)
            GenerateSegment();

        if(amountOfActiveSegments >= maxSegmentsOnScreen)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
    }

    private void GenerateSegment()
    {
        SpawnSegment();
        if (Random.Range(0f, 1f) < (continiousSegments * 0.25f))
        {
            continiousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continiousSegments++;
        }

    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginZ1 == z1 || x.beginZ2 == z2 || x.beginZ3 == z3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        z1 = s.endZ1;
        z2 = s.endZ2;
        z3 = s.endZ3;

        s.transform.SetParent(transform);
        s.transform.localPosition = new Vector3(1, 0, 0) * currentSpawnX;

        currentSpawnX += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginZ1 == z1 || x.beginZ2 == z2 || x.beginZ3 == z3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        z1 = s.endZ1;
        z2 = s.endZ2;
        z3 = s.endZ3;

        s.transform.SetParent(transform);
        s.transform.localPosition = new Vector3(1,0,0) * currentSpawnX;

        currentSpawnX += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }


    public Segment GetSegment (int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);
        if(s == null)
        {
            GameObject go = Instantiate((transition)?availableTransitions[id].gameObject:availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegId = id;
            s.transition = transition;
            segments.Insert(0, s);

        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }
        return s;
    }
    public Piece GetPiece (PieceType pt, int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if (p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
                go = ramps[visualIndex].gameObject;
            else if (pt == PieceType.longblock)
                go = longblocks[visualIndex].gameObject;
            else if (pt == PieceType.jump)
                go = jumps[visualIndex].gameObject;
            else if (pt == PieceType.ramp)
                go = ramps[visualIndex].gameObject;
            else if (pt == PieceType.slide)
                go = slides[visualIndex].gameObject;
            else if (pt == PieceType.skip)
                go = skips[visualIndex].gameObject;

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
    }
}

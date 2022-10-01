using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    #region Singleton Init
    private static DungeonGenerator _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static DungeonGenerator Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<DungeonGenerator>();
        _instance.Initialize();
    }
    #endregion

    public int spareSpaceRows, spareSpaceColumns;
    public int dungRows, dungColumns;
    public int minRoomSize, maxRoomSize;

    public GameObject[,] dungTiles;
    public TileData[,] dungTilesData;
    public WanderPoint wanderPoint;

    [HideInInspector] public List<TileData> sWalls = new List<TileData>();
    [HideInInspector] public List<TileData> grounds = new List<TileData>();

    [Header("Prefabs")]
    public GameObject p_groundTile;
    public GameObject p_wallTile;

    private int roomCounter;
    private int corridorsCounter;

    private GameObject roomsParent;
    private GameObject corridorsParent;

    void Initialize()
    {
        Generate();
    }

    public void Generate()
    {
        roomCounter = 0;
        corridorsCounter = 0;
        spareSpaceRows = Mathf.RoundToInt(dungRows * 1.4f);
        spareSpaceColumns = Mathf.RoundToInt(dungColumns * 1.4f);
        sWalls.Clear();
        grounds.Clear();

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        dungTiles = new GameObject[spareSpaceRows, spareSpaceColumns];
        dungTilesData = new TileData[spareSpaceRows, spareSpaceColumns];

        InitParents();

        SubDungeon rootSubDungeon = new SubDungeon(new Rect(spareSpaceRows / 2 - (dungRows / 2), spareSpaceColumns / 2 - (dungColumns / 2), dungRows, dungColumns));
        CreateBSP(rootSubDungeon);
        rootSubDungeon.CreateRoom();
        DrawRooms(rootSubDungeon); // swaped \>
        DrawCorridors(rootSubDungeon); //    <\

        DungeonSpaceFill();
        SetNeighborsForTiles();
        GetComponent<DungeonFiller>()?.Fill(this);
        if (StorageManager.Instance != null)
            StorageManager.Instance.Init(this);
        SetNeighborsForTiles();
        SetTypeForTiles();
        GetComponent<TilesSpritesManager>()?.SetSpritesForTiles(this);
        GetComponent<DungeonResourcesSpawner>()?.Spawn(this, 12, 8, 4);
        GetComponent<DungeonResourcesSpawner>()?.SetSprites();
        GetComponent<FogOfWar>()?.FogOfWarOn(this);

        if (BuildingsManager.Instance != null)
            BuildingsManager.Instance.InitBuildPlaces(grounds);
    }

    private void InitParents()
    {
        roomsParent = new GameObject();
        roomsParent.transform.SetParent(transform);
        roomsParent.name = $"Rooms";

        corridorsParent = new GameObject();
        corridorsParent.transform.SetParent(transform);
        corridorsParent.name = $"Corridors";
    }

    private void SetNeighborsForTiles()
    {
        for (int i = 1; i < spareSpaceRows - 1; i++)
            for (int j = 1; j < spareSpaceColumns - 1; j++)
                dungTilesData[i, j].SetNeighbors(this, i, j);
    }

    private void SetTypeForTiles()
    {
        for (int i = 1; i < spareSpaceRows - 1; i++)
            for (int j = 1; j < spareSpaceColumns - 1; j++)
            {
                if (i == 1 && j == 1)
                    wanderPoint = dungTilesData[i, j].gameObject.AddComponent<WanderPoint>();
                if (dungTilesData[i, j].tileType == TileData.TileType.Wall)
                    if (TileDataTool.ContainsInNeighbors(dungTilesData[i, j], TileData.TileType.Ground, false))
                    {
                        dungTilesData[i, j].tileType = TileData.TileType.SWall;
                        sWalls.Add(dungTilesData[i, j]);
                        dungTilesData[i, j].GetComponent<BoxCollider2D>().enabled = true;
                    }
            }
                
    }

    private void DungeonSpaceFill()
    {
        GameObject wallParent = new GameObject();
        wallParent.transform.SetParent(transform);
        wallParent.name = $"Walls";

        for (int i = 0; i < spareSpaceRows; i++)
            for (int j = 0; j < spareSpaceColumns; j++)
                if (dungTiles[i, j] == null)
                {
                    GameObject instance = Instantiate(p_wallTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(wallParent.transform);
                    dungTiles[i, j] = instance;

                    dungTilesData[i, j] = instance.GetComponent<TileData>();
                    dungTilesData[i, j].tileType = TileData.TileType.Wall;
                    dungTilesData[i, j].pos = new Vector2(i, j);
                }
    }

    public class SubDungeon
    {
        public SubDungeon left, right;
        public Rect rect;
        public List<Rect> corridors = new List<Rect>();

        public Rect room = new Rect(-1, -1, 0, 0); // i.e null
        public int debugId;

        private static int debugCounter = 0;

        public SubDungeon(Rect mrect)
        {
            rect = mrect;
            debugId = debugCounter;
            debugCounter++;
        }

        public bool IAmLeaf()
        {
            return left == null && right == null;
        }

        public bool Split(int minRoomSize, int maxRoomSize)
        {
            if (!IAmLeaf()) return false;

            bool splitH;
            if (rect.width / rect.height >= 1.25f)
                splitH = false;
            else if (rect.height / rect.width >= 1.25f)
                splitH = true;
            else
                splitH = Random.Range(0.0f, 1.0f) > 0.5f;

            if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize)
                return false;

            if (splitH)
            {
                int split = Random.Range(minRoomSize, (int)(rect.width - minRoomSize));
                left = new SubDungeon(new Rect(rect.x, rect.y, rect.width, split));
                right = new SubDungeon(new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
            }
            else
            {
                int split = Random.Range(minRoomSize, (int)(rect.height - minRoomSize));
                left = new SubDungeon(new Rect(rect.x, rect.y, split, rect.height));
                right = new SubDungeon(new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
            }
            return true;
        }

        public void CreateRoom()
        {
            if (left != null)
                left.CreateRoom();
            if (right != null)
                right.CreateRoom();

            if (left != null && right != null)
                CreateCorridorBetween(left, right);

            if (IAmLeaf())
            {
                int roomWidth = (int)Random.Range(rect.width / 2, rect.width - 2);
                int roomHeight = (int)Random.Range(rect.height / 2, rect.height - 2);
                int roomX = (int)Random.Range(1, rect.width - roomWidth - 1);
                int roomY = (int)Random.Range(1, rect.height - roomHeight - 1);

                room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
            }
        }

        public Rect GetRoom()
        {
            if (IAmLeaf())
                return room;
            if (left != null)
            {
                Rect lroom = left.GetRoom();
                if (lroom.x != -1)
                    return lroom;
            }
            if (right != null)
            {
                Rect rroom = right.GetRoom();
                if (rroom.x != -1)
                    return rroom;
            }

            return new Rect(-1, -1, 0, 0);
        }

        public void CreateCorridorBetween(SubDungeon left, SubDungeon right)
        {
            Rect lroom = left.GetRoom();
            Rect rroom = right.GetRoom();

            Vector2 lpoint = new Vector2((int)Random.Range(lroom.x + 1, lroom.xMax - 1), (int)Random.Range(lroom.y + 1, lroom.yMax - 1));
            Vector2 rpoint = new Vector2((int)Random.Range(rroom.x + 1, rroom.xMax - 1), (int)Random.Range(rroom.y + 1, rroom.yMax - 1));

            if (lpoint.x > rpoint.x)
            {
                Vector2 temp = lpoint;
                lpoint = rpoint;
                rpoint = temp;
            }

            int w = (int)(lpoint.x - rpoint.x);
            int h = (int)(lpoint.y - rpoint.y);

            if (w != 0)
            {
                if (Random.Range(0, 1) > 2)
                {
                    corridors.Add(new Rect(lpoint.x, lpoint.y, Mathf.Abs(w) + 1, 1));

                    if (h < 0)
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    else
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, -Mathf.Abs(h)));
                }
                else
                {
                    if (h < 0)
                        corridors.Add(new Rect(lpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    else
                        corridors.Add(new Rect(lpoint.x, rpoint.y, 1, Mathf.Abs(h)));

                    corridors.Add(new Rect(lpoint.x, rpoint.y, Mathf.Abs(w) + 1, 1));
                }
            }
            else
            {
                if (h < 0)
                    corridors.Add(new Rect((int)lpoint.x, (int)lpoint.y, 1, Mathf.Abs(h)));
                else
                    corridors.Add(new Rect((int)rpoint.x, (int)rpoint.y, 1, Mathf.Abs(h)));
            }
        }
    }

    private void CreateBSP(SubDungeon subDungeon)
    {
        if (subDungeon.IAmLeaf())
            if (subDungeon.rect.width > maxRoomSize || subDungeon.rect.height > maxRoomSize || Random.Range(0.0f, 1.0f) > 0.25)
                if (subDungeon.Split(minRoomSize, maxRoomSize))
                {
                    CreateBSP(subDungeon.left);
                    CreateBSP(subDungeon.right);
                }
    }

    private void DrawRooms(SubDungeon subDungeon)
    {
        if (subDungeon == null) return;

        if (subDungeon.IAmLeaf())
        {
            roomCounter++;
            GameObject roomParent = new GameObject();
            roomParent.transform.SetParent(roomsParent.transform);
            roomParent.name = $"Room_{roomCounter}";

            for (int i = (int)subDungeon.room.x; i < subDungeon.room.xMax; i++)
            {
                for (int j = (int)subDungeon.room.y; j < subDungeon.room.yMax; j++)
                {
                    GameObject instance = Instantiate(p_groundTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(roomParent.transform);
                    dungTiles[i, j] = instance;

                    dungTilesData[i, j] = instance.GetComponent<TileData>();
                    dungTilesData[i, j].SetTileType(TileData.TileType.Ground);
                    dungTilesData[i, j].pos = new Vector2(i, j);
                    grounds.Add(dungTilesData[i, j]);
                }
            }
        }
        else
        {
            DrawRooms(subDungeon.left);
            DrawRooms(subDungeon.right);
        }
    }

    private void DrawCorridors(SubDungeon subDungeon)
    {
        if (subDungeon == null) return;

        DrawCorridors(subDungeon.left);
        DrawCorridors(subDungeon.right);

        foreach (Rect corridor in subDungeon.corridors)
        {
            corridorsCounter++;
            GameObject corridorParent = new GameObject();
            corridorParent.transform.SetParent(corridorsParent.transform);
            corridorParent.name = $"Corridor_{corridorsCounter}";

            for (int i = (int)corridor.x; i < corridor.xMax; i++)
            {
                for (int j = (int)corridor.y; j < corridor.yMax; j++)
                {
                    if (dungTiles[i, j] == null)
                    {
                        GameObject instance = Instantiate(p_groundTile, new Vector3(i, j, 0f), Quaternion.identity, corridorParent.transform) as GameObject;
                        dungTiles[i, j] = instance;

                        dungTilesData[i, j] = instance.GetComponent<TileData>();
                        dungTilesData[i, j].SetTileType(TileData.TileType.Ground);
                        dungTilesData[i, j].pos = new Vector2(i, j);
                        grounds.Add(dungTilesData[i, j]);
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

//Script realizat de Craciun Claudiu-Viorel
public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private int roomWidth;
    [SerializeField]
    private int roomHeight;
    [SerializeField]
    Vector2Int roomNumberRange;


    [SerializeField]
    private int minNrEnemies = 4;
    [SerializeField]
    private int maxNrEnemies = 9;
    [SerializeField]
    private GameObject enemy1;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField]
    private GameObject lootObject;
    [SerializeField]
    private GameObject portal;

    [SerializeField]
    private Tilemap ground;

    [SerializeField]
    private Tilemap walls;

    [SerializeField]
    private Tile groundTile;

    [SerializeField]
    private Tile frontWallTile;
    [SerializeField]
    private Tile backWallTile;
    [SerializeField]
    private Tile leftWallTile;
    [SerializeField]
    private Tile rightWallTile;
    [SerializeField]
    private Tile upLeftWallTile;
    [SerializeField]
    private Tile upRightWallTile;
    [SerializeField]
    private Tile downLeftWallTile;
    [SerializeField]
    private Tile downRightWallTile;
    [SerializeField]
    private Tile wallTile;
    

    HashSet<Vector2Int> roomPos;

    private int roomNr;



    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector2(roomWidth/2,roomHeight/2);//Muta playerul in centrul camerei 0,0
        roomNr = Random.Range(roomNumberRange.x, roomNumberRange.y); //Se genereaza un numar random de camere intre 2 valori setate din editorul unity
        roomPos = GenerateRoomPositions(roomNr);//Se genereaza pozitii random pentru camere, pornindu-se de la camera 0,0
        GenerateMap();
    }

    void GenerateMap()
    {
        HashSet<Vector2Int> floorPositions = GenerateGround();
        PaintFloorTiles(floorPositions);
        HashSet<Vector2Int> wallPositions = GenerateWalls(floorPositions);
        PaintWallTiles(wallPositions,floorPositions);
    }

    HashSet<Vector2Int> GenerateGround()//Se parcurg pe rand pozitiile camerelor generate in roomPos
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        foreach(var pos in roomPos)
        {
            GenerateRoom(floorPos, pos, 0, new Vector2Int(pos.x*roomWidth, pos.y*roomHeight), new Vector2Int(pos.x * roomWidth + roomWidth, pos.y * roomHeight + roomHeight));
        }
        return floorPos;
    }

    HashSet<Vector2Int> GenerateWalls(HashSet<Vector2Int> floorPos)//Se foloseste de podeaua generata anterior pentru a genera peretii
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach(var pos in floorPos)
        {
            foreach(var direction in Direction2DandDiagonals.cardinalDirectionsList)
            {
                var neighbourPosition = pos + direction;//Pentru fiecare pozitie memorata in floorPos, se verifica vecinii, iar daca nu exista podea pe vreunul din ei se pune perete
                if (floorPos.Contains(neighbourPosition) == false)
                    wallPos.Add(neighbourPosition);//Se adauga pozitia lipsa din jurul pozitiei din floorPos la wallPos
            }
        }
        return wallPos;
    }

    void PaintFloorTiles(HashSet<Vector2Int> floorPos)//Pentru fiecare pozitie din floorPos se "Coloreaza" pozitia aferenta in tilemap-ul podelei
    {
        foreach (var position in floorPos) {
            PaintSingleGroundTile(position);
        }
    }

    void PaintSingleGroundTile(Vector2Int position)//Incarca o textura a unui tile la pozitia transmisa in Tilemap-ul ground
    {
        var tilePosition = ground.WorldToCell((Vector3Int)position);
        ground.SetTile(tilePosition, groundTile);
    }

    void PaintWallTiles(HashSet<Vector2Int> wallPos, HashSet<Vector2Int> floorPos)//Se parcurg toate pozitiile peretilor si se incarca textura
    {
        foreach (var position in wallPos)
        {
            PaintSingleWallTile(position, floorPos);
        }
    }

    void PaintSingleWallTile(Vector2Int position, HashSet<Vector2Int> floorPos)//Se verifica pozitiile ce inconjoara peretele curent astfel incat sa se determine ce textura se foloseste
    {
        Tile currentTile = wallTile;//Textura default care se incarca in momentul in care niciunul dintre cazurile verificate mai jos nu sunt adevarate
        if (floorPos.Contains(new Vector2Int(position.x + 1, position.y + 1)))
        {
            currentTile = downLeftWallTile;//Se verifica daca pozitia curenta este pozitie de colt stanga jos
        }
        if (floorPos.Contains(new Vector2Int(position.x - 1, position.y + 1)))
        {
            currentTile = downRightWallTile;//Se verifica daca pozitia curenta este pozitie de colt dreapta jos
        }
        if(floorPos.Contains(new Vector2Int(position.x + 1, position.y - 1)))
        {
            currentTile = upLeftWallTile;//Se verifica daca pozitia curenta este pozitie de colt stanga sus
        }
        if(floorPos.Contains(new Vector2Int(position.x - 1, position.y - 1)))
        {
            currentTile = upRightWallTile;//Se verifica daca pozitia curenta este pozitie de colt dreapta sus
        }
        if(floorPos.Contains(new Vector2Int(position.x + 1, position.y)))
        {
            currentTile = leftWallTile;//Se verifica daca pozitia curenta este pozitie de perete stanga
        }
        if(floorPos.Contains(new Vector2Int(position.x - 1, position.y)))
        {
            currentTile = rightWallTile;//Se verifica daca pozitia curenta este pozitie de perete dreapta
        }
        if ((floorPos.Contains(new Vector2Int(position.x, position.y-1)) && floorPos.Contains(new Vector2Int(position.x, position.y + 1))) || floorPos.Contains(new Vector2Int(position.x, position.y - 1)))
        {
            currentTile = backWallTile;//Se verifica daca pozitia curenta este pozitie de perete spate sau perete dintre camere
        }
        else if(floorPos.Contains(new Vector2Int(position.x, position.y + 1)))
        {
            currentTile = frontWallTile;//Se verifica daca pozitia curenta este pozitie de perete din fata
        }


        var tilePosition = walls.WorldToCell((Vector3Int)position);
        walls.SetTile(tilePosition, currentTile);//Se incarca textura aferenta pe pozitie
    }

    HashSet<Vector2Int> GenerateRoomPositions(int roomNr)//Genereaza pozitii pentru camere pornind de la pozitia 0,0 si mergant sus,jos,stanga si dreapta in mod aleator
    {
        HashSet<Vector2Int> pos = new HashSet<Vector2Int>();
        pos.Add(new Vector2Int(0, 0));
        var lastPos = new Vector2Int(0, 0);
        for(int i = 0; i < roomNr; i++)
        {
            int count = 0;
            var newPosition = lastPos + Direction2D.GetRandomCardinalDirection();
            while (pos.Contains(newPosition))
            {
                count++;
                newPosition = lastPos + Direction2D.GetRandomCardinalDirection();
                if(count == 4)
                {
                    break;
                }
            }
            pos.Add(newPosition);
            lastPos = newPosition;
        }
        return pos;
    }
    void GenerateRoom(HashSet<Vector2Int> floorPositions, Vector2Int currentRoomPos,int roomType, Vector2Int topLeftCorner, Vector2Int downRightCorner) {
        /*Aceasta functie este folosita pentru a genera un anumit tip de camera la o pozitie anume din roomPos*/
        if (roomPos.Contains(new Vector2Int(currentRoomPos.x, (currentRoomPos.y-1))))
        {
            floorPositions.Add(new Vector2Int(topLeftCorner.x + roomWidth/2+1, topLeftCorner.y));
            if(roomWidth%2 == 0)
            {
                floorPositions.Add(new Vector2Int(topLeftCorner.x + roomWidth / 2, topLeftCorner.y));
            }
        }
        if(roomPos.Contains(new Vector2Int(currentRoomPos.x-1, (currentRoomPos.y))))
        {
            floorPositions.Add(new Vector2Int(topLeftCorner.x, topLeftCorner.y + roomHeight/2 + 1));
            if (roomHeight % 2 == 0)
            {
                floorPositions.Add(new Vector2Int(topLeftCorner.x, topLeftCorner.y + roomHeight / 2));
            }
        }
        if (roomPos.Contains(new Vector2Int(currentRoomPos.x+1, (currentRoomPos.y))))
        {
            floorPositions.Add(new Vector2Int(downRightCorner.x+1, topLeftCorner.y + roomHeight / 2 + 1));
            if (roomHeight % 2 == 0)
            {
                floorPositions.Add(new Vector2Int(downRightCorner.x + 1, topLeftCorner.y + roomHeight / 2));
            }
        }
        //Cele 3 if-uri de mai sus se asigura ca intre oricare doua camere alaturate exista o portita de trecere. In cazul in care pentru latimea/lungimea
        //camerei se folosesc valori pare, portita va fi de latime 2, altfel va fi de latime 1.(pentru simetricitate)


        //Se verifica tipul de camera si se incarca lay-out-ul de camera corespunzator
        switch (roomType)
        {
            case 0:
                //codul care genereaza un numar aleator de enemy1 si enemy2 care sa nu se suprapuna si care sa nu se genereze in camera de inceput
                HashSet<Vector2> enemyPos = new HashSet<Vector2>();
                if (currentRoomPos != Vector2Int.zero)
                {
                    int nrEnemies = Random.Range(minNrEnemies, maxNrEnemies);
                    int nrEnemy1 = Random.Range(0, nrEnemies + 1);
                    int nrEnemy2 = nrEnemies - nrEnemy1;
                    for(int i = 0; i < nrEnemy1; i++)//In acest for se genereaza pe pozitii random in fiecare camera inamicii de tip enemy1
                    {
                        int x1 = topLeftCorner.x+2;
                        int x2 = downRightCorner.x;
                        int y1 = topLeftCorner.y+1;
                        int y2 = downRightCorner.y;
                        Vector2 pos = new Vector2(Random.Range(x1,x2), Random.Range(y1,y2));
                        int count = 0;
                        while (enemyPos.Contains(pos))
                        {
                            x1 = topLeftCorner.x + 2;
                            x2 = downRightCorner.x;
                            y1 = topLeftCorner.y + 1;
                            y2 = downRightCorner.y;
                            pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                            count++;
                            if(count == 4)
                            {
                               break;
                            }
                        }
                        enemyPos.Add(pos);
                        pos.x += 0.5f;
                        pos.y += 0.5f;
                        Instantiate(enemy1, (Vector3)pos, Quaternion.identity);
                    }
                    for (int i = 0; i < nrEnemy2; i++)//In acest for se genereaza pe pozitii random in fiecare camera inamicii de tip enemy2
                    {
                        int x1 = topLeftCorner.x + 2;
                        int x2 = downRightCorner.x;
                        int y1 = topLeftCorner.y + 1;
                        int y2 = downRightCorner.y;
                        Vector2 pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                        int count = 0;
                        while (enemyPos.Contains(pos))
                        {
                            x1 = topLeftCorner.x + 2;
                            x2 = downRightCorner.x;
                            y1 = topLeftCorner.y + 1;
                            y2 = downRightCorner.y;
                            pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                            count++;
                            if (count == 4)
                            {
                                break;
                            }
                        }
                        enemyPos.Add(pos);
                        pos.x += 0.5f;
                        pos.y += 0.5f;
                        Instantiate(enemy2, (Vector3)pos, Quaternion.identity);
                    }
                }
                else//In else intra camera 0,0 in care se asaza portalul de intoarcere
                {
                    int x1 = topLeftCorner.x + 2;
                    int x2 = downRightCorner.x;
                    int y1 = topLeftCorner.y + 1;
                    int y2 = downRightCorner.y;
                    Vector2 pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                    int count = 0;
                    while(pos.x == roomWidth/2 || pos.y == roomHeight / 2)
                    {
                        count++;
                        x1 = topLeftCorner.x + 2;
                        x2 = downRightCorner.x;
                        y1 = topLeftCorner.y + 1;
                        y2 = downRightCorner.y;
                        pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                        if(count == 4)
                        {
                            break;
                        }
                    }
                    Instantiate(portal, (Vector3)pos, Quaternion.identity);
                }

                //codul care genereaza un numar aleator de breakable objects
                {
                    int nrObjects = Random.Range(7, 13);
                    int x1 = topLeftCorner.x + 2;
                    int x2 = downRightCorner.x;
                    int y1 = topLeftCorner.y + 1;
                    int y2 = downRightCorner.y;
                    Vector2 pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                    int count = 0;
                    while (enemyPos.Contains(pos))
                    {
                        x1 = topLeftCorner.x + 2;
                        x2 = downRightCorner.x;
                        y1 = topLeftCorner.y + 1;
                        y2 = downRightCorner.y;
                        pos = new Vector2(Random.Range(x1, x2), Random.Range(y1, y2));
                        count++;
                        if (count == 4)
                        {
                            break;
                        }
                    }
                    enemyPos.Add(pos);
                    pos.x += 0.5f;
                    pos.y += 0.5f;
                    Instantiate(lootObject, (Vector3)pos, Quaternion.identity);
                }
                
                for(int i = (topLeftCorner.x)+2; i < downRightCorner.x; i++)
                {
                    for(int j = (topLeftCorner.y)+1; j < downRightCorner.y; j++)
                    {
                        floorPositions.Add(new Vector2Int(i, j));
                    }
                }
                break;
            case 1:
                for (int i = (topLeftCorner.x) + 2; i < downRightCorner.x; i++)
                {
                    for (int j = (topLeftCorner.y) + 1; j < downRightCorner.y; j++)
                    {
                        floorPositions.Add(new Vector2Int(i, j));
                    }
                }
                break;
            case 2:
                for (int i = (topLeftCorner.x) + 2; i < downRightCorner.x; i++)
                {
                    for (int j = (topLeftCorner.y) + 1; j < downRightCorner.y; j++)
                    {
                        floorPositions.Add(new Vector2Int(i, j));
                    }
                }
                    break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//UP
        new Vector2Int(1,0),//RIGHT
        new Vector2Int(0,-1),//DOWN
        new Vector2Int(-1,0)//LEFT
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}

public static class Direction2DandDiagonals
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//UP
        new Vector2Int(1,0),//RIGHT
        new Vector2Int(0,-1),//DOWN
        new Vector2Int(-1,0),//LEFT
        new Vector2Int(1,1),//UP-Right
        new Vector2Int(-1,1),//UP-Left
        new Vector2Int(-1,-1),//Down-Left
        new Vector2Int(1,-1)//DownRight
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] enemyPrefabs;
    public GameObject[] lines;
    List<GameObject> spawnLines;
    public int initFood = 100;
    private bool isWin = false;
    private bool m_isGameRunning = true;
    public bool isGameRunning
    {
        get
        {
            return m_isGameRunning;
        }

        set
        {
            if(value == false && m_isGameRunning == true)
            {
                CancelInvoke("SpawnEnemy");

                // all human stop working
                foreach(GameObject line in lines)
                {
                    // all monster stop working
                    Monster[] monsters = line.GetComponentsInChildren<Monster>();
                    foreach(Monster monster in monsters)
                    {
                        monster.StopWorking();
                        if(!isWin)
                        {
                            monster.Celebrate();
                        }
                    }

                    Human[] humans = line.GetComponentsInChildren<Human>();
                    foreach(Human human in humans)
                    {
                        human.StopWorking();
                        if(isWin)
                        {
                            human.Celebrate();
                        }
                    }
                }


                
            }
            m_isGameRunning = value;
        }
    }
    
    [SerializeField] float spawnInterval = 5.0f;
    private GameObject currentSelectGrid;
    private int m_food;
    public int food
    {
        get
        {
            return m_food;
        }
        set
        {
            if (value < 0)
            {
                Debug.Log("Food cannot be set to negative!");
            }
            else
            {
                m_food = value;
            }
        }
    }
    private GameObject m_currentSelectPrefab = null;
    public GameObject currentSelectPrefab
    {
        get
        {
            return m_currentSelectPrefab;
        }

        set
        {
            if(value)
            {
                if(((GameObject)value).GetComponent<Human>() != null)
                {
                    m_currentSelectPrefab = value;
                }
                else
                {
                    Debug.Log("Set failed: Not Human.");
                }
            }
            else
            {
                Debug.Log("No selected prefab");
                m_currentSelectPrefab = null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_food = initFood;
        spawnLines = new List<GameObject>(lines);
        InvokeRepeating("SpawnEnemy", 1.0f, spawnInterval);
        //Time.timeScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameRunning)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(IsPointAtEmptyGrid())
                {
                    if(m_currentSelectPrefab != null)
                    {
                        SpawnHuman();
                    }
                }
                else
                {
                    if(m_currentSelectPrefab == null)
                    {
                        RemoveHuman();
                    }
                }
            }
            CheckLine();
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    void SpawnEnemy()
    {
        if(spawnLines.Count > 0)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject line = spawnLines[Random.Range(0, spawnLines.Count)];
            LineManage lineManage = line.GetComponent<LineManage>();
            
            if (lineManage)
            {
                Vector3 pos = lineManage.GetSpawnPos();
                //Vector3 pos = new Vector3(spawnPosX, 0, line.transform.position.z);

                Instantiate(enemy, pos, Quaternion.AngleAxis(270f, Vector3.up), line.transform);
            }
            else
            {
                Debug.LogError(line.ToString() + " has no component LineMange!");
            }

        }
        else
        {
            Debug.Log("All Lines Defended");
        }
    }

    void SpawnHuman()
    {

        //Debug.Log("SpawnHuman");
        int cost = m_currentSelectPrefab.GetComponent<Human>().cost;
        if (null != m_currentSelectPrefab && m_food >= cost)
        {
            m_food -= cost;
            GameObject human = Instantiate(m_currentSelectPrefab,currentSelectGrid.transform.position, Quaternion.AngleAxis(90f,Vector3.up),currentSelectGrid.transform);
            human.GetComponent<Human>().locatedGrid = currentSelectGrid;
            currentSelectGrid = null;
        }
    }

    void RemoveHuman()
    
    {
        if(currentSelectGrid)
        {
            Human human = currentSelectGrid.GetComponentInChildren<Human>();
            if (human)
            {
                Destroy(human.gameObject);
            }
            else
            {
                Debug.Log(currentSelectGrid.ToString() + "grid has no human");
            }
        }
        else
        {
            Debug.Log("no selected grid");
        }
    }

    bool IsPointAtEmptyGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 60f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 50, 1 << 3))
        {
            GameObject selectedGameobj = hit.collider.gameObject;
            if (selectedGameobj.CompareTag("Grid"))
            {
                //Debug.Log("hit the " + selectedGameobj.name);
                currentSelectGrid = selectedGameobj;
                if(!selectedGameobj.GetComponent<Grid>().isOccupied)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void CheckLine()
    {
        spawnLines.RemoveAll(n => n.GetComponent<LineManage>().GetDefendState());

        if(spawnLines.Count == 0 && lines.Length != 0)
        {
            Win();
        }
    }

    void Win()
    {
        // all the farmer celebrate
        /*
        foreach(GameObject line in lines)
        {
            Farmer[] farmers = line.GetComponentsInChildren<Farmer>();
            if(farmers.Length > 0)
            {
                foreach(Farmer farmer in farmers)
                {
                    farmer.Celebrate();
                }
            }
        }*/

        Debug.Log("Victory!");
        isWin = true;
        isGameRunning = false;
        //Time.timeScale = 0;
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        isWin = false;
        isGameRunning = false;
        //Time.timeScale = 0;
    }
}

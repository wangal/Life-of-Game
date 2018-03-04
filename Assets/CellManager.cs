using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour {

    // Dimensions of the Cell Table
    [Range(3, 100)]
    [Tooltip("Width of the Cells")]
    public int m_width;

    [Range(3, 100)]
    [Tooltip("Height of the Cells")]
    public int m_height;

    [Range(1, 100)]
    [Tooltip("Depth of the Cells")]
    public int m_depth;

    // Time to spawn next generation
    [Range(0.1f, 1)]
    [Tooltip("Time to Spawn Next Generation")]
    public float m_NextGenerationTime;
    
    private Cell[,,] m_cells; // Table of Cells
    private List<Neighbor> m_neighbors; // all neighbor offsets

    public Text m_generateMessage;
    public Text m_is3DMessage;
    public bool m_generate = false; // controls whether to generate cell or not

    // struct for storing the offsets of neighbor
    struct Neighbor {
        public Neighbor(int x, int y, int z) {
            xOff = x;
            yOff = y;
            zOff = z;
        }
        public int xOff;
        public int yOff;
        public int zOff;
    }

    private void Awake()
    {
        m_cells = new Cell[m_width, m_height, m_depth]; // allocate cells for (width x height x depth)
        m_neighbors = new List<Neighbor>(); // list of offsets in xyz coordinates

        int []offset = { 0, 1, -1 }; // offset for xyz

        // Adds all possible neighbor offsets
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                int maxLimit = (m_depth == 1) ? 1 : 3; // check for 2D or 3D neighbors BASED ON DEPTH
                for(int k = 0; k < maxLimit; ++k)
                {
                    if (offset[i] != 0 || offset[j] != 0 || offset[k] != 0) // if offsets are not all zeroes -> add as neighbor
                        m_neighbors.Add(new Neighbor(offset[i], offset[j], offset[k]));
                }
            }
        }

        m_generateMessage.text = m_generate ? "Generation: ON" : "Generation: OFF";
        m_is3DMessage.text = (m_depth == 1) ? "2D Enabled" : "3D Enabled";
    }

    void Start () {
        // generate the board of cells (width x height x depth)
        float dist = 1.2f;
        for (int y = 0; y < m_height; ++y)
        {
            for (int x = 0; x < m_width; ++x)
            {
                for (int z = 0; z < m_depth; ++z)
                {
                    GameObject go = Instantiate(Resources.Load("Cell"), new Vector3(dist * x - (dist * m_width / 2), dist * y - (dist * m_height / 2), dist * z - (dist * m_depth / 2)), Quaternion.identity) as GameObject; // Instantiate cell
                    go.name = "Cell: (x: " + x + ", y: " + y + ", z:" + z + ")"; // Name of cell
                    m_cells[x, y, z] = go.GetComponent<Cell>(); // add the cell to the multi-array

                    go.transform.parent = transform; // set cell to under "game of life"
                }
            }
        }

        FindCellsNeighbors(); // find the number of neighbors for each cell
        InvokeRepeating("UpdateCells", 0.0f, m_NextGenerationTime); // calls "UpdateCells" at specified time intervals
    }

    // Update is called once per frame
    void Update () {
        // Controls cell generation using "space"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_generate = !m_generate;
            m_generateMessage.text = m_generate ? "Generation: ON" : "Generation: OFF";
        }
    }

    void UpdateCells()
    {
        if (!m_generate) return; // controls whether

        FindCellsNeighbors(); // find the number of neighbors for each cell
        if (m_depth != 1)
            GenerateCells3D(); // generate the next generation for 3D
        else GenerateCells2D(); // generate the next generation for 2D (normal "Life of Game" logic)
    }

    void FindCellsNeighbors()
    {
        for (int y = 0; y < m_width; ++y)
        {
            for (int x = 0; x < m_height; ++x)
            {
                for (int z = 0; z < m_depth; ++z)
                {
                    int sum = 0;
                    for (int n = 0; n < m_neighbors.Count; ++n)
                    {
                        // finds neighbor of (x, y, z)
                        int i = x + m_neighbors[n].xOff;
                        int j = y + m_neighbors[n].yOff;
                        int k = z + m_neighbors[n].zOff;

                        // if neighbor is alive -> add to sum
                        if( 0 <= i && i < m_width && 
                            0 <= j && j < m_height &&
                            0 <= k && k < m_depth &&
                            m_cells[i, j, k].m_alive)
                            ++sum;
                    }

                    // stores number of neighbors alive in Cell.cs
                    m_cells[x, y, z].neighborsAlive = sum;
                }
            }
        }
    }

    void GenerateCells2D()
    {
        for (int y = 0; y < m_width; ++y)
        {
            for (int x = 0; x < m_height; ++x)
            {
                for (int z = 0; z < m_depth; ++z)
                {
                    Cell c = m_cells[x, y, z]; // take a cell
                    c.m_alive = ( // assign cell state
                        (!c.m_alive && c.neighborsAlive == 3) || // dead cell logic
                        (c.m_alive && (c.neighborsAlive == 2 || c.neighborsAlive == 3))); // alive cell logic
                        
                    c.ChangeColor(); // change color
                }
            }
        }
    }

    void GenerateCells3D()
    {
        for (int y = 0; y < m_width; ++y)
        {
            for (int x = 0; x < m_height; ++x)
            {
                for (int z = 0; z < m_depth; ++z)
                {
                    Cell c = m_cells[x, y, z]; // take a cell
                    c.m_alive = // assign cell state
                        ((!c.m_alive && c.neighborsAlive == 4) || // dead cell logic: exactly 4
                        (c.m_alive && (c.neighborsAlive >= 4 && c.neighborsAlive <= 9))); // alive cell logic: 4-9
                    c.ChangeColor();
                }
            }
        }
    }
}

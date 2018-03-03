Controls: 

TO handle this problem, I have defined two classes:

Cell.cs
- Stores the state of the cell as dead (black) or alive (white).
- Each cell contains the number of neighbors at current generation.
- Mouse click on the cell will change the state.

CellManager.cs
- This class is used manage the state of each cell when it is time for a new generation.
This is for better organization and preventing any synchronization issues.
- Upon "Awake" and "Start", the manager will allocate the number of grids based upon width, height, and depth.
- 
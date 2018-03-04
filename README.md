# Life-of-Game
Conway's Life of Game in 2D and 3D

Controls: 
- WASD - Move camera forward, left, backward, and right
- QE - Move camera up and down
- IJKL - Rotate the camera
- SPACEBAR - Pauses and start simulation of Life of Game
- MOUSE LEFT CLICK - Toggles cell's state to dead/alive

To handle this problem, I have defined two classes:

Cell.cs
- Stores the state of the cell as dead (black) or alive (white).
- Each cell contains the number of neighbors at current generation.
- Mouse click on the cell will change the state.

CellManager.cs
- This class is used manage the state of each cell when it is time for a new generation. This includes updating the number of neighbors alive for every "Cell.cs" from this script. I did this for better organization and preventing any synchronization/timing issues.
- Upon "Awake" and "Start", the manager will allocate the number of grids based upon width, height, and depth. "2D" logic is applied when the DEPTH = 1, "3D" logic when DEPTH is >= 2.
- "Generation Time" controls the time to generate next generation UPON STARTUP.
- For 3D logic, I have chosen to spawn live cell if the cell is alive with 4 to 9 neighbors, or if the cell is dead with excatly 4 neighbors.

In addition, I chose to space the cubes outwards and included CameraController.cs, so that users can alter the cell state in 3D. Finally, I provided the UI messages to show whether the game is 2D/3D and creating the next generations. All of this is to help visualize the algorithm and simplify the controls for tweaking setup of the cell table.

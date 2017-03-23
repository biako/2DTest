using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IRecyle should be implemented by other comonent scripts that do the restart and shutdown work for a specific type of GameObject
public interface IRecycle  {
    void Restart();
    void Shutdown();	
}

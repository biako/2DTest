﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestartShutDown : MonoBehaviour, IRecycle {
    void IRecycle.Restart() {
        GetComponent<InputState>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<Actions>().enabled = true;
        GetComponent<InputState>().lastInputRight = true;
        gameObject.layer = 9; // set it to the player layer

    }

    void IRecycle.Shutdown() {


    }
}

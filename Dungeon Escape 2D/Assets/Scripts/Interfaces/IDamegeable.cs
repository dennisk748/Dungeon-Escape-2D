using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCode
{
    public interface IDamegeable
    {
        int health { get; set; }

        void Damage();
    }

}
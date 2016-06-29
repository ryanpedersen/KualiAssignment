using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevators
{
    class Elevator
    {
        public event EventHandler atThisFloor;
        public event EventHandler DoorOpen;
        public event EventHandler DoorClosed;


        public int maxFloors { get; set; }
        public int moveRateSec { get; set; }
        private int currentFloor { get; set; }
        private int newFloor { get; set; }

        public void MoveToFloor(int newFloor)
        {

        }
    }
}

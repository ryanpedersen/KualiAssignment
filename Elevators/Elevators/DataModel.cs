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


        private int maxFloors { get; set; }
        private int currentFloor { get; set; }
        private int newFloor { get; set; }
        private int moveRateSec { get; set; }


        public Elevator(int maxFloors, int moveRateSec)
        {
            this.maxFloors = maxFloors;
            this.moveRateSec = moveRateSec;

        }

        public void MoveToFloor(int newFloor)
        {

        }

         
    }
}

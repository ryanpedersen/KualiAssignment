using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Elevators
{
    class Elevator
    {
        public event EventHandler atThisFloor;
        public event EventHandler doorOpen;
        public event EventHandler doorClosed;

        public int myNumber { get; set; }
        public int maxFloors { get; set; }
        public int moveRateSec { get; set; }
        private int currentFloor { get; set; }
        private int newFloor { get; set; }
        private bool? moveUp { get; set; }

        private int TotalFloorsMoved { get; set; }
        private int TotalTripsMade { get; set; }

        public bool inServiceMode { get; set; }

        private DispatcherTimer TimeForNewFloorTimer;

        public void Init()
        {
            inServiceMode = false;

            TimeForNewFloorTimer = new System.Windows.Threading.DispatcherTimer();
            TimeForNewFloorTimer.Tick += TimeForNewFloorTimer_Tick;
            TimeForNewFloorTimer.Interval = new TimeSpan(0, 0, 1);

        }

        public void MoveToFloor(int moveToFloor)
        {
            // close doors?
            if (doorClosed != null)
                doorClosed(this, new EventArgs());

            if (currentFloor == moveToFloor)
            {
                moveUp = null;
                return;
            }

            //direction
            moveUp = true;
            if (moveToFloor < currentFloor)
                moveUp = false;

            newFloor = moveToFloor;

            TimeForNewFloorTimer.Start();
        }

        private void TimeForNewFloorTimer_Tick(object sender, EventArgs e)
        {
            if (moveUp.Value)
                currentFloor++;
            else
                currentFloor--;

            TotalFloorsMoved++;

            if (atThisFloor != null)
                atThisFloor(this, new EventArgs());

            Console.WriteLine(myNumber + " " + currentFloor );

            // notify/report when a
            if (currentFloor == newFloor)
            {
                TimeForNewFloorTimer.Stop();

                moveUp = null;

                // keep doors open?
                if (doorOpen != null)
                    doorOpen(this, new EventArgs());

                TotalTripsMade++;

                if ((TotalTripsMade % 100) == 0)
                    inServiceMode = true;
            }
        }

        public int HowCloseToThisFloor(int ReqestingFloor)
        {
            if(inServiceMode)
                throw new Exception("In service Mode");

            // elevator is stopped
            if(moveUp == null)
            {
                return Math.Abs(currentFloor - ReqestingFloor);
            }

            // elevator has moved passed elevator
            if ((moveUp.Value && (currentFloor > ReqestingFloor)) ||
                (moveUp.Value == false) && (currentFloor < ReqestingFloor))
                throw new Exception("Elevator passed this floor");


            return Math.Abs(currentFloor - ReqestingFloor);
        }

        public bool InServiceMode()
        {
            return inServiceMode;
        }

        public void ExitServiceMode()
        {
            inServiceMode = false;
        }
    }
}

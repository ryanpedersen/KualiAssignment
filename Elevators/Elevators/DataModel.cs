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
        private bool moveUp { get; set; }

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
            if (moveUp)
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

                // keep doors open?
                if (doorOpen != null)
                    doorOpen(this, new EventArgs());

                TotalTripsMade++;

                if (TotalTripsMade mod 100)
                    inServiceMode = true;
            }
        }


        public void ExitServiceMode()
        {
            inServiceMode = false;
        }
    }
}

using System;
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

        // this needs to change to an array, so it can stop at multiple floors
        private int newFloor { get; set; }
        private bool? moveUp { get; set; }

        private int TotalFloorsMoved { get; set; }
        private int TotalTripsMade { get; set; }

        public bool inServiceMode { get; set; }

        // this needs to change to an array, so it can pick up mutiple people
        private bool StopAndPickUp { get; set; }

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
            if (moveToFloor > maxFloors)
                throw new Exception("Can't go above top floor");

            if(moveToFloor < 1)
                throw new Exception("Can't go below bottom floor");

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

        public void StopAndAddFloor(int fromPauseFloor, int toAddFloor)
        {
            // stop the elevator when it's at the fromPauseFloor... set a flag so the TimeForNewFloor timer knows to stop
            // add it to the array
            bool StopAndPickUp = true;
            // StopAndPickUp.Add(fromPaulseFloor)

            // add this to the list of floors to stop
            // newFloor.Add(toAddFloor)

        }


        // this needs to be split up.. one for picking up, one for dropping off
        private void TimeForNewFloorTimer_Tick(object sender, EventArgs e)
        {
            if (moveUp.Value)
                currentFloor++;
            else
                currentFloor--;

            TotalFloorsMoved++;

            // when it's an array, stop at the correct floors
            if(StopAndPickUp)
            {
                // open doors
                // close doors
                // keep going
                // if StopAndPick up array is empty,... no need to stop and pick up
            }

            // log
            if (atThisFloor != null)
                atThisFloor(this, new EventArgs());

            Console.WriteLine(myNumber + " " + currentFloor );

            // notify/report when a
            // newFloor will be an array, if current floor is one of the newFloors, open doors, close doors, keep going
            // remove this floor from the list
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

            // when the newFloor list is empty .. stop timer.
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

            if (moveUp.Value)
            {
                if (currentFloor > ReqestingFloor)
                    throw new Exception("Elevator passed this floor");

                if (ReqestingFloor > newFloor)
                    throw new Exception("Elevator not going that far up");

            }

            if (moveUp.Value == false)
            {
                if (currentFloor < ReqestingFloor)
                    throw new Exception("Elevator passed this floor");

                if (ReqestingFloor < newFloor)
                    throw new Exception("Elevator not going down that far");
            }

            // what about the logic that even though we are not passing through, it's still the closest to the pick up?

            return Math.Abs(currentFloor - ReqestingFloor);
        }

        public bool IsMoving()
        {
            if (moveUp == null)
                return false;
            else
                return true;
        }

        public bool InServiceMode()
        {
            return inServiceMode;
        }

        public void ExitServiceMode()
        {
            inServiceMode = false;
        }

        // Add a method to show more status of elevator
    }
}

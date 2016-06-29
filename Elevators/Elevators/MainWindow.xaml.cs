using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Elevators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            runButton.Click += RunButton_Click;

            requestButton.Click += RequestButton_Click;

            mElevators = new ObservableCollection<Elevator>();
        }



        ObservableCollection<Elevator> mElevators;

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            // add error checking and exception handling - verify input text is a number
            for (int i = 0; i < Convert.ToInt16(numOfElevators.Text); i++)
            {
                var ele = new Elevator { maxFloors = Convert.ToInt16(numberOfFloors.Text), moveRateSec = 1, myNumber = i, inServiceMode = false };

                ele.Init();

                ele.atThisFloor += Ele_atThisFloor;
                ele.doorOpen += Ele_doorOpen;
                ele.doorClosed += Ele_doorClosed;

                mElevators.Add(ele);
            }
        }

        // pass in what elevator and floor it's at in the event handler
        private void Ele_doorClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator door closed");
        }

        // pass in what elevator and floor it's at in the event handler
        private void Ele_doorOpen(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator door open");
        }

        // pass in what elevator and floor it's at in the event handler
        private void Ele_atThisFloor(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator at floor");
        }

        // create a button to put an elevator in service mode

        // create a UI to show all elevator status.

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            int fromFloor = Convert.ToInt16(requestFromVal.Text);
            int toFloor = Convert.ToInt16(requestToVal.Text);

            int closestElevatorNumber = -1;
            int closestElevatorDistance = 1000000;


            // add error checking and exception handling - verify input text is a number

            // what elevator is closes?
            foreach (var ele in mElevators)
            {
                try
                {
                    int thisEledistance = ele.HowCloseToThisFloor(fromFloor);

                    if (thisEledistance < closestElevatorDistance)
                    {
                        closestElevatorDistance = thisEledistance;
                        closestElevatorNumber = ele.myNumber;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("this elevator is not an option");
                }
            }

            // no elevator is in service
            if (closestElevatorNumber == -1)
            {
                Console.WriteLine("all elevators must be out of service");
                return;
            }

            // request the closest elevator to stop
            foreach (var ele in mElevators)
            {
                try
                {
                    if (ele.myNumber == closestElevatorNumber)
                    {
                        // what about the person in the elevator already?
                        if (ele.IsMoving())
                        {
                            ele.StopAndAddFloor(fromFloor, toFloor);
                        }
                        else
                        {
                            ele.MoveToFloor(toFloor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

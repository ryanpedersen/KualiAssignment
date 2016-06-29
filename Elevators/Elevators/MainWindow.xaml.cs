using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }



        ObservableCollection<Elevator> mElevators;

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void Ele_doorClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator door closed");
        }

        private void Ele_doorOpen(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator door open");
        }

        private void Ele_atThisFloor(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator at floor");
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            int fromFloor = Convert.ToInt16(requestFromVal.Text);
            int toFloor = Convert.ToInt16(requestToVal.Text);

            int closestElevatorNumber = -1;
            int closestElevatorDistance = 1000000;

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
                if (ele.myNumber == closestElevatorNumber)
                {
                    ele.MoveToFloor(toFloor);
                }
            }
        }
    }
}

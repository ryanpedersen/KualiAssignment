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
                ele.DoorOpen += Ele_DoorOpen;
                ele.DoorClosed += Ele_DoorClosed;

                mElevators.Add(ele);
            }

        }

        private void Ele_DoorClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Elevator door closed");
        }

        private void Ele_DoorOpen(object sender, EventArgs e)
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

            foreach(var ele in mElevators)
            {

            }
        }

    }
}

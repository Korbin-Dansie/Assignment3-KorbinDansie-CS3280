using System;
using System.Collections.Generic;
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

namespace Assignment3_KorbinDansie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Attributes

        /// <summary>
        /// Number of students
        /// </summary>
        int inumberOfStudents;

        /// <summary>
        /// Number of assignments
        /// </summary>
        int inumberOfAssignments;

        /// <summary>
        /// Currently selected Student
        /// </summary>
        int iSelectedStudentIndex;

        /// <summary>
        /// List of all student names
        /// </summary>
        string[] saStudentNames;

        /// <summary>
        /// Assignments grades. First row is student index. Second is grade
        /// </summary>
        int[,] iaStudentScores;

        #endregion Attributes
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnResetScores_Click(object sender, RoutedEventArgs e)
        {
            // Reset Window
            // Enable top
        }

        private void btnSubmitCounts_Click(object sender, RoutedEventArgs e)
        {
            // Validate user input - display error lables

            // If valid enable bottom

            // Create arrays
        }

        private void btnFirstStudent_Click(object sender, RoutedEventArgs e)
        {
            // iSelectedIndex = 0
            // Display Student name
        }

        private void btnPrevStudent_Click(object sender, RoutedEventArgs e)
        {
            // Move student index - 1. Dont go below zero.
            // Display Student name

        }

        private void btnNextStudent_Click(object sender, RoutedEventArgs e)
        {
            // Move student index - 1. Dont go below zero.
            // Display Student name

        }

        private void btnLastStudent_Click(object sender, RoutedEventArgs e)
        {
            // iSelectedIndex = last index
            // Display Student name
        }

        private void btnStudentInfoSaveName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            // Validate data
            // Save the students core to the array iaStudentScore - 1
        }

        private void btnDisplayScores_Click(object sender, RoutedEventArgs e)
        {
            // Generate header, number of assignments

            // Outer for loop through the students

            //Inner loop through the grades

            // Add up then cal avg

            // Calculate letter grade
        }


    }
}

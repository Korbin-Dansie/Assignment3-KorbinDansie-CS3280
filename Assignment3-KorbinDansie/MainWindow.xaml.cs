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


// TODO: Reset the text in the top text boxes to nothing

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


        const int NUMBER_OF_TABS_AFTER_NAME = 8;
        const int NUMBER_OF_TABS_BETWEEN_COLLUMS = 2;


        #endregion Attributes
        public MainWindow()
        {
            InitializeComponent();
            enableBottomUI(false);
        }

        #region Counts

        /// <summary>
        /// When focused select all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbNumberOfStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbNumberOfStudents.SelectAll();
        }

        /// <summary>
        /// When focused select all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbNumberOfAssignments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbNumberOfAssignments.SelectAll();
        }

        /// <summary>
        /// Reset the window when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetScores_Click(object sender, RoutedEventArgs e)
        {
            // Reset Window
            // Enable top
            enableTopUI(true);
            enableBottomUI(false);
        }

        /// <summary>
        /// Validate the user inputed the correct number or students and assignments, then enable the bottom UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitCounts_Click(object sender, RoutedEventArgs e)
        {
            // Validate user input - display error lables
            int studentInput;
            int assignmentInput;

            //Hide previous error messages
            tbErrorStudents.Visibility = Visibility.Hidden;
            tbErrorAssignments.Visibility = Visibility.Hidden;


            // Try parsing number of students
            if (!int.TryParse(tbNumberOfStudents.Text, out studentInput))
            {
                // Display error message
                tbErrorStudents.Visibility = Visibility;
                return;
            }
            else if(studentInput < 1 || studentInput > 10)
            {
                tbErrorStudents.Visibility = Visibility;
                return;
            }


            // Try parsing number of assignments
            if (!int.TryParse(tbNumberOfAssignments.Text, out assignmentInput))
            {
                tbErrorAssignments.Visibility = Visibility;
                return;
            }
            else if (assignmentInput < 1 || assignmentInput > 99)
            {
                tbErrorStudents.Visibility = Visibility;
                return;
            }

            // Set the global variables
            inumberOfStudents = studentInput;
            inumberOfAssignments = assignmentInput;

            // If valid enable bottom
            enableBottomUI(true);
            enableTopUI(false);

            // Create arrays
            saStudentNames = new string[inumberOfStudents];
            iaStudentScores = new int[inumberOfStudents, inumberOfAssignments];

            // Default vales for the arrays
            for (int i = 0; i < saStudentNames.Length; i++)
            {
                saStudentNames[i] = "Student #" + (i+1);
            }

            Random random = new Random();
            int testIndex = 0;
            for (int i = 0; i < iaStudentScores.GetLength(0); i++)
            {
                //For each assignment set it to zero
                for (int j = 0; j < iaStudentScores.GetLength(1); j++)
                {
                    iaStudentScores[i, j] = random.Next(50, 100);
                }
            }

            // Update the currently selected student
            updatetblockStudentInfoName();
        }

        #endregion Counts

        #region EnableUI

        /// <summary>
        /// Enable the top UI
        /// </summary>
        /// <param name="value">enabled = true</param>
        private void enableTopUI(bool value)
        {
            tbNumberOfAssignments.IsEnabled = value;
            tbNumberOfStudents.IsEnabled = value;

            btnSubmitCounts.IsEnabled = value;
        }

        /// <summary>
        /// Enable the bottom UI
        /// </summary>
        /// <param name="value">enabled = true</param>
        private void enableBottomUI(bool value)
        {
            btnFirstStudent.IsEnabled = value;
            btnPrevStudent.IsEnabled = value;
            btnNextStudent.IsEnabled = value;  
            btnLastStudent.IsEnabled = value;

            tbStudentInfoName.IsEnabled = value;
            btnStudentInfoSaveName.IsEnabled = value;

            tbAssignmentNumber.IsEnabled = value;
            tbAssignmentScore.IsEnabled = value;

            btnSaveScore.IsEnabled = value;

            btnDisplayScores.IsEnabled = value;
        }

        #endregion EnableUI

        #region Naviage
        /// <summary>
        /// Go to the first (0) student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstStudent_Click(object sender, RoutedEventArgs e)
        {
            // iSelectedIndex = 0
            iSelectedStudentIndex = 0;

            // Display Student name
            updatetblockStudentInfoName();
        }

        /// <summary>
        /// Go to the previous student ( iSelectedStudentIndex - 1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevStudent_Click(object sender, RoutedEventArgs e)
        {
            // Move student index - 1. Dont go below zero.
            if(iSelectedStudentIndex > 0)
            {
                iSelectedStudentIndex--;
            }
            // Display Student name
            updatetblockStudentInfoName();
        }

        /// <summary>
        /// Go to the next student ( iSelectedStudentIndex + 1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextStudent_Click(object sender, RoutedEventArgs e)
        {
            // Move student index + 1. Dont go above limit.
            if (iSelectedStudentIndex < saStudentNames.Length - 1)
            {
                iSelectedStudentIndex++;
            }
            // Display Student name
            updatetblockStudentInfoName();
        }

        /// <summary>
        /// Go to the last student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastStudent_Click(object sender, RoutedEventArgs e)
        {
            // iSelectedIndex = last index
            iSelectedStudentIndex = saStudentNames.Length - 1;

            // Display Student name
            updatetblockStudentInfoName();
        }

        #endregion Naviage

        #region Student Info Top
        /// <summary>
        /// Update the name of the student at the current index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStudentInfoSaveName_Click(object sender, RoutedEventArgs e)
        {
            saStudentNames[iSelectedStudentIndex] = tbStudentInfoName.Text;
            tbStudentInfoName.Text = String.Empty;

            updatetblockStudentInfoName();
        }

        /// <summary>
        /// Update the tblockStudentInfoName with the current name at iSelectedStudentIndex
        /// </summary>
        private void updatetblockStudentInfoName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(saStudentNames[iSelectedStudentIndex]);

            // Set the selected students name
            tblockStudentInfoName.Text = sb.ToString();

        }
        #endregion Student Info Top

        #region Student Info Bottom
        /// <summary>
        /// Validate user input, then update the selected grade for the current student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            // Validate data
            // Save the students core to the array iaStudentScore - 1
        }

        #endregion Student Info Bottom

        #region Display Scores

        /// <summary>
        /// Display the current grades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayScores_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument myFlowDoc = new FlowDocument();

            double length;
            myFlowDoc.Blocks.Add(createNewHeaders(out length));

            //Set lenght of rtb so no overfill
            myFlowDoc.PageWidth = length;

            // Add paragraphs to the FlowDocument.
            for (int i = 0; i < saStudentNames.Length; i++)
            {
                myFlowDoc.Blocks.Add(createNewStudentParagraph(i));
            }

            // Add initial content to the RichTextBox.
            rtbDisplayScores.Document = myFlowDoc;
        }

        /// <summary>
        /// Create a new paragraph for the headers
        /// </summary>
        /// <param name="length">Returns lenght of header in px</param>
        /// <returns></returns>
        private Paragraph createNewHeaders(out double length)
        {
            StringBuilder sb = new StringBuilder();

            // Add the First header
            sb.Append("Student");

            // Add spaces between columns
            for (int sbTabLength = sb.Length; sbTabLength < NUMBER_OF_TABS_AFTER_NAME * 4; sbTabLength++)
            {
                sb.Append(" ");
            }

            // Add the assignments headers
            StringBuilder sbAssignments = new StringBuilder();
            for (int i = 0; i < iaStudentScores.GetLength(1); i++)
            {
                // # number
                // If on assignments 1 - 9 add one else add two spaces
                if ((i + 1) < 10)
                {
                    sbAssignments.Append(" ");
                }

                sbAssignments.Append("#" + (i + 1));
                // Add spaces between columns
                for (int sbTabLength = sbAssignments.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
                {
                    sbAssignments.Append(" ");
                }

                // Add to main sb then clearn
                sb.Append(sbAssignments);
                sbAssignments.Clear();
            }

            // Add avg
            sbAssignments.Append("AVG");

            for (int sbTabLength = sbAssignments.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
            {
                sbAssignments.Append(" ");
            }
            sb.Append(sbAssignments);
            sbAssignments.Clear();

            // Add grade
            sbAssignments.Append("GRADE");

            for (int sbTabLength = sbAssignments.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
            {
                sbAssignments.Append(" ");
            }
            sb.Append(sbAssignments);
            sbAssignments.Clear();

            // Return the new paragraph
            Paragraph paragraph = new Paragraph(new Run(sb.ToString()));

            length = sb.ToString().Length * 7.3; // No idea why its this number. Its based on px meaurements

            //// Might look into formated text latter
            //Typeface tf = new Typeface(rtbDisplayScores.FontFamily, rtbDisplayScores.FontStyle, rtbDisplayScores.FontWeight, rtbDisplayScores.FontStretch);
            //FormattedText formattedText = new 
            //    FormattedText
            //    (
            //    sb.ToString(),
            //    System.Globalization.CultureInfo.CurrentCulture,
            //    rtbDisplayScores.FlowDirection,
            //    tf,
            //    rtbDisplayScores.size
            //    rtbDisplayScores.FontSize,
            //    rtbDisplayScores.Foreground
            //    )

            // OR something with a bitmap?

            return paragraph;

        }

        /// <summary>
        /// Create a new paragraph display the students info
        /// </summary>
        /// <param name="iStudent">Current student index</param>
        /// <param name="numberOfTabsForName">Number to tabs between name and first assignment</param>
        /// <param name="numberOfTabsBetweenRows">Number of tabs between assignments columns</param>
        /// <returns></returns>
        private Paragraph createNewStudentParagraph(int iStudent)
        {
            double average = 0;

            StringBuilder sb = new StringBuilder();


            // Add the name
            string name = saStudentNames[iStudent];
            sb.Append(name);

            // Add tabs between columns
            for (int sbTabLength = sb.Length; sbTabLength < NUMBER_OF_TABS_AFTER_NAME * 4; sbTabLength++)
            {
                sb.Append(" ");
            }


            // Add the grades
            StringBuilder sbGrade = new StringBuilder();
            for (int i = 0; i < iaStudentScores.GetLength(1); i++)
            {
                int currentAssignmentScore = iaStudentScores[iStudent, i];

                // Total the grades together
                average += currentAssignmentScore;
                
                // Add spaces based on how the length of the grade
                if(currentAssignmentScore > 99)
                {
                    ; // No spaces needed
                }
                else if (currentAssignmentScore > 9)
                {
                    sbGrade.Append(" ");
                }
                else
                {
                    sbGrade.Append("  ");
                }

                sbGrade.Append(currentAssignmentScore);

                // Add tabs between columns
                for (int sbTabLength = sbGrade.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
                {
                    sbGrade.Append(" ");
                }

                // Add to string then clear
                sb.Append(sbGrade);
                sbGrade.Clear();
            }


            // Add the avg
            average = average / iaStudentScores.GetLength(1);

            // add spaces before avg
            if((int)average > 99)
            {
                ;
            }
            else if(((int)average) > 9)
            {
                sbGrade.Append(" ");
            }
            else
            {
                sbGrade.Append("  ");
            }

            sbGrade.Append(String.Format("{0:0.00}", average));

            // Add tabs between columns
            for (int sbTabLength = sbGrade.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
            {
                sbGrade.Append(" ");
            }

            sb.Append(sbGrade);
            sbGrade.Clear();


            // Add the letter grade
            if(average >= 93)
            {
                sbGrade.Append("A");
            }
            else if(average >= 90)
            {
                sbGrade.Append("A-");
            }
            else if (average >= 87)
            {
                sbGrade.Append("B+");
            }
            else if (average >= 83)
            {
                sbGrade.Append("B");
            }
            else if (average >= 80)
            {
                sbGrade.Append("B-");
            }
            else if (average >= 77)
            {
                sbGrade.Append("C+");
            }
            else if (average >= 73)
            {
                sbGrade.Append("C");
            }
            else if (average >= 70)
            {
                sbGrade.Append("C-");
            }
            else if (average >= 67)
            {
                sbGrade.Append("D+");
            }
            else if (average >= 63)
            {
                sbGrade.Append("D");
            }
            else if (average >= 60)
            {
                sbGrade.Append("D-");
            }
            else
            {
                sbGrade.Append("E");
            }

            // Add tabs between columns
            for (int sbTabLength = sbGrade.Length; sbTabLength < NUMBER_OF_TABS_BETWEEN_COLLUMS * 4; sbTabLength++)
            {
                sbGrade.Append(" ");
            }

            sb.Append(sbGrade);
            sbGrade.Clear();


            // Return the new paragraph
            Paragraph paragraph = new Paragraph(new Run(sb.ToString()));
            return paragraph;
        }

        #endregion Display Scores


    } // End of partial class
}

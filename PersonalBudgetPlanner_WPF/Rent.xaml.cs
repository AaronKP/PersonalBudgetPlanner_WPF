using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
/*
 *
 * Author: Aaron Padiachy (ST10090758)
 * Purpose of program:
 *  The purpose of this program is serve as a personal budget planning application.
 * The program now makes use of Windows presentation foundation (WPF) to afford more user friendliness to the application. 
 *The program accepts the users gross monthly income and various expenses.
 *A calculation is performed to aquire the funds available after all the deductions.
 *The program also calculates the monthly home loan repayment as part of their expense if the user chooses to purchase a home 
 *The program also takes into consideration whether the user wishes to purchase a vehicle. It calculates the monthly repayments for purchasing a car.
 *The Program now also allows the user to select whether or not they want to save up for something and requests a few details from the user to calculate how much the user should save up
 *each month to reach their goal amount by a certain number of years specified by the user.
 *The program makes use of a List of objects to store the expenses.The attributes of each expense objecy contain properties for the expense name and the expense value. 
 *Lamda expressions are used to define how an operation is performed on the List.
 * BIBLIOGRAPHY AND REFERENCES: 
 * [1]      Youtube.com. 2022. [online] Available at: <https://www.youtube.com/watch?v=wxznTygnRfQ> [Accessed 13 May 2022].
 * 
 * [2]      V, A. and Harrison, R., 2022. How to calculate the sum of all values in a dictionary excluding the first item's value?.
 *          [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/8128477/how-to-calculate-the-sum-of-all-values-in-a-dictionary-excluding-the-first-item> [Accessed 3 June 2022].
 *          
 * [3]      Hadfield, P., 2022. How to pass Dictionary<string, string> object in some method. 
 *          [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/4734280/how-to-pass-dictionarystring-string-object-in-some-method> [Accessed 3 June 2022].
 *          
 * [4]      C-sharpcorner.com. 2022. Sort a Dictionary by Value in C#. 
 *          [online] Available at: <https://www.c-sharpcorner.com/UploadFile/mahesh/sort-a-dictionary-by-value-in-C-Sharp/> [Accessed 3 June 2022].
 *
 * [5]      object, H., 2022. How to Sort a List<T> by a property in the object. [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object> [Accessed 30 June 2022].
 *
 * [6]      U., J., LE, A. and Quinn, G., 2022. C# List of objects, how do I get the sum of a property. [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/4351876/c-sharp-list-of-objects-how-do-i-get-the-sum-of-a-property> [Accessed 30 June 2022].
 *
 * [7]      L., M., 2022. How do I make a textblock's visibility 'Visible' when text is entered in a textbox and the Enter key is pressed?. [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/45042196/how-do-i-make-a-textblocks-visibility-visible-when-text-is-entered-in-a-textb> [Accessed 30 June 2022].
 * 
 * [8]      Var, B., Brunscheon, J. and Berni, T., 2022. How to navigate between windows in WPF?. [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/21706226/how-to-navigate-between-windows-in-wpf#:~:text=NavigationService%20is%20for%20browser%20navigation,%2F%2Fcreate%20your%20new%20form.> [Accessed 30 June 2022].
 * 
 * [9]      Popovich, A., 2022. Define a List of Objects in C#. [online] Stack Overflow. Available at: <https://stackoverflow.com/questions/27448455/define-a-list-of-objects-in-c-sharp> [Accessed 30 June 2022].
 * 
 */
namespace PersonalBudgetPlanner_WPF
{
    /// <summary>
    /// Interaction logic for Rent.xaml
    /// </summary>
    public partial class Rent : Window
    {
        public static double rentAmount;//stores the users rent. Will be used to update the list of expenses
        public Rent()
        {
            InitializeComponent();
            btnNextRent.Visibility = Visibility.Collapsed;//hide the Next button when the rent wondow pops up [7]
        }

        private void btnNextRent_Click(object sender, RoutedEventArgs e)
        {
           //https://www.c-sharpcorner.com/blogs/sending-any-value-from-one-form-to-another-form-in-wpf1
            if (Expenses.carChoice == 0)//open the car window if the user wishes to purchase a car
            {
                Car carObj =new Car();
                carObj.Show();
                this.Hide();
            }
            else if (Expenses.carChoice == 1)//open the summary window if the user does not wish to purchase a car
            {
                new Savings().Show();//[8]
                this.Hide();
            }
        }

        private void btnBackRent_Click(object sender, RoutedEventArgs e)
        {
            new Expenses().Show();//navigate to previous window
            this.Hide();
        }

        private void btnValidateRent_Click(object sender, RoutedEventArgs e)
        {
            bool validRent = false;//used to control whether the Next button is visible or not. The Next button is visible if the data entered by the user is valid
            try
            {
                rentAmount = Convert.ToDouble(txtbxRentAmount.Text);
                MessageBox.Show($"INPUT VALID.\nData successfully captured!\nClick Next to proceed.", "Validation Success", MessageBoxButton.OK, MessageBoxImage.Information);//prompt to show valid input has been captured
                validRent = true;
            }
            catch (Exception exception)//error handling with message box pop up to notify user.
            {
                MessageBox.Show($"Invalid Input. Please re-enter value(s) in the correct format.\nError: {exception.Message}", "Validation failed", MessageBoxButton.OK, MessageBoxImage.Error);
                validRent = false;
            }
            //
            if (validRent == true)
            {
                btnNextRent.Visibility = Visibility.Visible;//make Next button visible
                btnValidateRent.Visibility = Visibility.Collapsed;//make validate button invisible to avoid duplicate values being entered into the list
                
                //add the monthly rent to the List of expenditures once it has been validated
                Expenses.expenditureObj.Add(new Expenditure
                {
                    expenseName = "Rent",
                    expenseValue = Convert.ToDouble(rentAmount)
                }
                );
            }
        }
    }
}

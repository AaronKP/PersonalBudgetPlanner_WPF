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
    /// Interaction logic for Homeloan.xaml
    /// </summary>
    public partial class Homeloan : Window
    {
        //fields to store user input. These fields, once assigned will be used to calculate the monthly home loan repayment
        public static double propertyPurchasePrice;
        public static double depositPercentage;
        public static double interestRatePercentage;
        public static double monthsToRepay;
        public static double homeLoanRepayment;
        public static String hlPossibility;//stores a string which will serve as a message to the user about the possibility of a homeloan being successful
        public Homeloan()
        {
            InitializeComponent();
            btnNextHL.Visibility = Visibility.Collapsed;//[7]
        }

        private void btnNextHL_Click(object sender, RoutedEventArgs e)
        {
           
           
            if (Expenses.carChoice == 0)
            {
                new Car().Show();
                this.Hide();
            }
            else if (Expenses.carChoice == 1)
            {
                new Summary().Show();//[8]
                this.Hide();
            }
        }

        private void btnBackHL_Click(object sender, RoutedEventArgs e)
        {
            new Expenses().Show();
            this.Hide();
            Expenses.expenditureObj.Clear();//clear the list if the user wishes to return to the window for expense input. Prevents duplicate values.
        }

        private void btnCalcMonthlyHL_Click(object sender, RoutedEventArgs e)// this button is for calculation and validation purposes.
        {
            bool validHl=false;
            try
            {
                //convert user input into a doubleand assign to fields
                propertyPurchasePrice=Convert.ToDouble(txtbxPropertyPurchasePrice.Text);
                depositPercentage = Convert.ToDouble(txtbxDepositHL.Text); 
                interestRatePercentage = Convert.ToDouble(txtbxInterestHL.Text); 
                monthsToRepay = Convert.ToDouble(txtbxMonthsRepay.Text);
                //notifies user that their data was captured successfully
                MessageBox.Show($"INPUT VALID.\nData successfully captured!\nClick Next to proceed.", "Validation Success", MessageBoxButton.OK, MessageBoxImage.Information);//prompt to show valid input has been captured
                validHl = true;
            }
            catch (Exception exception)//[1]
            {
                MessageBox.Show($"Invalid Input. Please re-enter value(s) in the correct format.\nError: {exception.Message}", "Validation failed", MessageBoxButton.OK, MessageBoxImage.Error);
                validHl = false;
            }

            if (validHl==true)
            {
                btnNextHL.Visibility = Visibility.Visible;
                btnCalcMonthlyHL.Visibility = Visibility.Collapsed;//hide calculate button omce Next button is visible to prevent user from causing an error when populating the List of expenditures
                HomeLoanClass home = new HomeLoanClass();//instantiate home loan class to use method to calculate monthly repayment of home loan
                //populate the List with the monthly homeloan repayment 
                Expenses.expenditureObj.Add(new Expenditure
                {
                    expenseName = "Home loan Repayment",
                    expenseValue = home.calcMonthlyRepayment(Income.grossIncome)
                }
                );
                homeLoanRepayment = home.calcMonthlyRepayment(Income.grossIncome);
                txtblkMonthlyHLRepaymenyt.Text ="R "+ Convert.ToString(home.calcMonthlyRepayment(Income.grossIncome));//sets the text in the window to 
                //display the monthly homeloan repayment
                
            }
            
        }
        //method that displays a warning if the home loan repayment is greater than 33% of users income.
        public static string displayHLwarning()
        {
            
            if (homeLoanRepayment > (Income.grossIncome * 0.33333333333333))// [1]
            {
                hlPossibility="WARNING!!!\nAPPROVAL OF HOMELOAN:\nUNLIKELY!!!!!";
            }
            else
            {
                hlPossibility="HOME LOAN APPROVAL:\nLIKELY \n MONTHLY REPAYMENT IS: \nR "+homeLoanRepayment;//String interpolation
            }
            return hlPossibility;
        }
       
    }
}

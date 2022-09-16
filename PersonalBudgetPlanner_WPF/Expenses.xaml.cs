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
using System.Linq;//import to use Sum method for lists
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
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Window
    {
        public static int carChoice;//stores the user's choice w.r.t car purchase since the data gets lost when the window closes
        public static int accommodationChoice;// stores users choice w.r.t accommodation preference
        public static double HLmonthlyRepayment;//stores the monthly repayment for a Home loanwhich will be added to the List of expenditures
        public static double carMonthlyRepayment;//stores the monthly car repayment which will be added to the List of expenditures
        public static double totalExpense;//stores total cost of all expenses.
        public static double netIncome;//stores user's income after expense deductions
        //list for Expenditure objects which will have each object's properties (names and values) usuing the .Add() method 
        public static List<Expenditure> expenditureObj = new List<Expenditure>();//[9]

        
        public Expenses()
        {
            InitializeComponent();
            //this code is placed here so that the implementation occurs as soon as the window pops up, i.e., visibilty of the next button and validate button being invisible.
            btnNextExpense.Visibility = Visibility.Collapsed;// set next button invisible to prevent user from moving to the next window while entering invalid input
            btnCalcExpenditure.Visibility = Visibility.Collapsed;// to prevent errors in populating the expense list
        }

        private void txtbxGroceries_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnBackExpense_Click(object sender, RoutedEventArgs e)//hide current window and go back to previous window
        {
            new Income().Show();
            this.Hide();
            expenditureObj.Clear();//clear the list to prevent duplicate values from being entered.
        }

        //Event handling for next button
        private void btnNextExpense_Click(object sender, RoutedEventArgs e)
        {
            if (cboAccommodation.SelectedIndex == 0)// display homeloan window if the user selects home loan 
            {
                accommodationChoice = cboAccommodation.SelectedIndex;// this stores the index of the combo box corresponding to home loan. This will be used help navigate back to the homeloan page if the user decides to
                new Homeloan().Show();
                this.Hide();
               
               
            }
            else if (cboAccommodation.SelectedIndex == 1)// display rent window if user selects rent
            {
                accommodationChoice = cboAccommodation.SelectedIndex;// this stores the index of the combo box corresponding to rent. This will be used help navigate back to the rent page if the user decides to
                new Rent().Show();
                this.Hide();
            
            
            }
            // this stores the index of the combo box corresponding to whether the user wishes to buy a car. This will be used help navigate to the car page if the user decides to purchase a car
            if (cboVehicle.SelectedIndex == 0)
            {
                carChoice = 0;
            }
            else
            {
                carChoice = 1;
            }
        }

        private void btnCalcExpenditure_Click(object sender, RoutedEventArgs e)
        {
            bool validExpenditures = false;//variable to control whether or not the Next button is displayed.
            //populate the list with objects of each expense and have the attributes of each object we assigned a name for the name property and a value for the expense value property
            try
            {
                //[9]
                expenditureObj.Add(
                new Expenditure { expenseName ="Groceries",
                                  expenseValue= Convert.ToDouble(txtbxGroceries.Text)  
                                }
                );
                expenditureObj.Add(
                new Expenditure
                {
                    expenseName = "Water+Lights",
                    expenseValue = Convert.ToDouble(txtbxWaterLights.Text)
                }
                );
                expenditureObj.Add(
                new Expenditure
                {
                    expenseName = "Travel Cost",
                    expenseValue = Convert.ToDouble(txtbxTravel.Text)
                }
                );
                expenditureObj.Add(
                new Expenditure
                {
                    expenseName = "Cell/Telephone",
                    expenseValue = Convert.ToDouble(txtbxPhone.Text)
                }
                );
                expenditureObj.Add(
                new Expenditure
                {
                    expenseName = "Other Expenses",
                    expenseValue = Convert.ToDouble(txtbxOtherExpenses.Text)
                }
                );
                //notifies user that capturing the data was successful
                MessageBox.Show($"INPUT VALID.\nData successfully captured!\nClick Next to proceed.", "Validation Success", MessageBoxButton.OK, MessageBoxImage.Information);//prompt to show valid input has been captured
                validExpenditures = true;
               
            }
            catch(Exception exception)//expetion handling
            {
                MessageBox.Show($"Invalid Input. Please re-enter value(s) in the correct format.\nError: {exception.Message}", "Validation failed", MessageBoxButton.OK,MessageBoxImage.Error);
                //clears the list to avoid duplicate values appearing in the data grid and to avoid errors in calculations
                if (expenditureObj.Count > 0)// >0 to avoid a null error when trying to clear the list
                {
                    expenditureObj.Clear();//clear the list to prepare for storing new values
                }
            }

            if (validExpenditures == true && cboAccommodation.SelectedIndex>-1 && cboVehicle.SelectedIndex>-1)// Execute if statemnet implementation if and only if all data is valid abd all fields of window are populated
            {
                btnNextExpense.Visibility = Visibility.Visible;// set next button visible to allow user to move on to next window
                btnCalcExpenditure.Visibility = Visibility.Collapsed;//hide validate button to prevent duplicate values from being entered into the list.
            }

            
        }

        // this code is to set visibility to the validate button if and only if the index for the combo boxes of both homeloan and car repayment is >-1
        //This is to prevent errorsa in data capturing.
        private void cboAccommodation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAccommodation.SelectedIndex > -1 && cboVehicle.SelectedIndex>-1)
            {
                btnCalcExpenditure.Visibility = Visibility.Visible;
            }
        }
        // this code is to set visibility to the validate button if and only if the index for the combo boxes of both homeloan and car repayment is >-1
        //This is to prevent errorsa in data capturing.
        private void cboVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAccommodation.SelectedIndex > -1 && cboVehicle.SelectedIndex > -1)
            {
                btnCalcExpenditure.Visibility = Visibility.Visible;
            }
        }

        

    }
}

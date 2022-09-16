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
    /// Interaction logic for Car.xaml
    /// </summary>
    public partial class Car : Window
    {
        //fields used to store user input. Will be used to calculate monthly car repayment.
        public static String carModelAndMake;
        public static double carPurchasePrice;
        public static double carTotalDeposit;
        public static double carInterestRate;
        public static double carInsurancePremium;
        public Car()
        {
            InitializeComponent();
            btnNextCar.Visibility = Visibility.Collapsed;//hide next button when window pops up. Done to allow for data validation
        }

        private void btnBackCar_Click(object sender, RoutedEventArgs e)
        {
           //depending on the users choice of accomodation, the back button will navigate to the correct accomodation window
            if (Expenses.accommodationChoice == 0)// homeloan choice
            {
                new Homeloan().Show();
                this.Hide();
            }
            else if (Expenses.accommodationChoice == 1)//rent choice
            {
                new Rent().Show();
                this.Hide();
            }
            Expenses.expenditureObj.RemoveAt(Expenses.expenditureObj.Count - 1);//if the user wishes to go back to the previous
                                                                                //window then delete the last entered item in the
                                                                                //list to prevent duplicate values
        }

        private void btnNextCar_Click(object sender, RoutedEventArgs e)// Next button event handling
        {
            new Savings().Show();//show summary wondow when next button is clicked
            this.Close();//close current window
        }

        private void btnCalcMonthlyCar_Click(object sender, RoutedEventArgs e)
        {
            bool validCarInfo = false;//used to control the visibilty of the Next button and used to control whether or not monthly repayment is added to the list
            try//[1]
            {
                // string to double conversions to be able to store the user input in the respective data field.
                carModelAndMake = txtbxCarModelMake.Text;
                carPurchasePrice = Convert.ToDouble(txtbxCarPurchasePrice.Text);
                carTotalDeposit = Convert.ToDouble(txtbxDeposit.Text);
                carInterestRate = Convert.ToDouble(txtbxInterestCar.Text);
                carInsurancePremium = Convert.ToDouble(txtbxInsurancePremium.Text);

                MessageBox.Show($"INPUT VALID.\nData successfully captured!\nClick Next to proceed.", "Validation Success", MessageBoxButton.OK, MessageBoxImage.Information);//prompt to show valid input has been captured
                validCarInfo = true;
            }
            catch (Exception exception)// exception handling. Notify user of invalid input
            {
                MessageBox.Show($"Invalid Input. Please re-enter value(s) in the correct format.\nError: {exception.Message}", "Validation failed", MessageBoxButton.OK, MessageBoxImage.Error);
                validCarInfo = false;
            }

            if (validCarInfo == true)
            {
                btnNextCar.Visibility = Visibility.Visible;//show next button is all data entered is valid
                btnCalcMonthlyCar.Visibility = Visibility.Collapsed;//hides calculate button to prevent duplicate values from being entered into the expense list
                //instantiate CarClass object to get the monthly repayment for the car.
                CarClass cars = new CarClass();
                //add monthly car repayment to the list of expenses
                Expenses.expenditureObj.Add(new Expenditure
                {
                    expenseName = "Car repayment of:" + carModelAndMake,//specifies the car model and make 
                    expenseValue = cars.calcMonthlyRepayment(Income.grossIncome)//invoke the ethod to return the monthly repayment.
                });
                txtblkMonthlyCarRepaymenyt.Text ="R " + Convert.ToString(cars.calcMonthlyRepayment(Income.grossIncome));
            }
        }
    }
}

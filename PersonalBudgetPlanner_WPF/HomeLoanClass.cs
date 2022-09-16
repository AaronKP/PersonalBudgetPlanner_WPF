using System;
using System.Collections.Generic;
using System.Text;
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
    class HomeLoanClass : Expense//[1] HomeLoanClass extends the Expense class to inherit its fields and methods which will be useful in calculations
    {
        public override double calcMonthlyRepayment(double grossIncome)//overriden method for calculating the monthly repayment for a homeloan
        {
            monthlyRepayment = 0;//set to 0 to get rid of garbage values
            double newOpeningBalance = (Homeloan.propertyPurchasePrice- (Homeloan.propertyPurchasePrice * (Homeloan.depositPercentage / 100)));//since there is a deposit to be paid a new opening balance is to be calculated.
                                                                                                           // Deposit property is divided by 100 to convert it to a decimal of the percentage. The new balance to be paid after the deposit is the original purchase price minus the deposit amount.

            double yearsToPay = Homeloan.monthsToRepay / 12.0;// calculation for monthly repayment requires the nr of month to repay loan to be converted into nr of years

            monthlyRepayment = (newOpeningBalance * (1 + (Homeloan.interestRatePercentage / 100) * yearsToPay)) / Homeloan.monthsToRepay;// formula to calculate monthly home loan repayment
            return monthlyRepayment;
        }
    }
}

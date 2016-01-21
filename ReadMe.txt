MYOB - Employee Monthly Payslip
===============================

Description ---------------------------------------------------------------------------------------

When I input the employee's details: first name, last name, annual salary(positive integer) and 
super rate(0% - 50% inclusive), payment start date, 

the program should generate payslip information with name, pay period,  gross income, income tax, 
net income and super.

The calculation details will be the following:
• pay period = per calendar month
• gross income = annual salary / 12 months
• income tax = based on the tax table provide below
• net income = gross income - income tax
• super = gross income x super rate

Notes: All calculation results should be rounded to the whole dollar. If >= 50 cents round up to 
the next dollar increment, otherwise round down. The following rates for 2012-13 apply from 
1 July 2012.

Taxable income   Tax on this income
0 - $18,200     Nil
$18,201 - $37,000       19c for each $1 over $18,200
$37,001 - $80,000       $3,572 plus 32.5c for each $1 over $37,000
$80,001 - $180,000      $17,547 plus 37c for each $1 over $80,000
$180,001 and over       $54,547 plus 45c for each $1 over $180,000
The tax table is from ATO: http://www.ato.gov.au/content/12333.htm

Example Data

Employee annual salary is 60,050, super rate is 9%, how much will this employee be paid for the 
month of March ?

• pay period = Month of March (01 March to 31 March)
• gross income = 60,050 / 12 = 5,004.16666667 (round down) = 5,004
• income tax = (3,572 + (60,050 - 37,000) x 0.325) / 12  = 921.9375 (round up) = 922
• net income = 5,004 - 922 = 4,082
• super = 5,004 x 9% = 450.36 (round down) = 450

Here is the csv input and output format we provide. (But feel free to use any format you want)
Input (first name, last name, annual salary, super rate (%), payment start date):
David,Rudd,60050,9%,01 March – 31 March
Ryan,Chen,120000,10%,01 March – 31 March
Output (name, pay period, gross income, income tax, net income, super):
David Rudd,01 March – 31 March,5004,922,4082,450
Ryan Chen,01 March – 31 March,10000,2696,7304,1000

---------------------------------------------------------------------------------------------------

MYOB.PayslipConsoleApp ----------------------------------------------------------------------------

One possible solution for the problem stated above is implemented in the software 
MYOB.PayslipConsoleApp. This software was implemented in C# using MS Visual Studio 2012 and the 
.Net Framework 4.5 (the software targets the MS Windows OSs, more specifically the MS Windows 7.0). 
The software (MYOB.PayslipConsoleApp.exe) is available in the folder \MYOB\MYOB.Application, as 
well as, the data source file (BasicReferenceData.csv). 

Note: The software can be recreated at any time by opening the MYOB.sln file (Visual Studio 
Solution file) and rebuilding the solution. The final product will be available under the 
MYOB.PayslipConsoleApp project folder (bin\Debug or bin\Release, depending on the configuration 
settings chosen).

The MYOB.PayslipConsoleApp is very straightforward console application. The main goal of the 
MYOB.PayslipConsoleApp is to allow the user to calculate the payslip information for a specific 
employee or for all the employees available in the data source. In the current implementation, the 
“data source” is obtained from a CSV file that resides in the same folder as the application. This 
CSV file (BasicReferenceData.csv) contains the payment information about all the employees 
available in the “virtual” company. The payslip information calculated is shown on the terminal, 
as well as, saved in a CSV file (PayslipData.csv).

---------------------------------------------------------------------------------------------------

Comments ------------------------------------------------------------------------------------------

Few comments about the implementation of the MYOB.PayslipConsoleApp:

1. The supporting framework of the application was designed to be easily expandable and adaptable. 
It is fairly easy to incorporate additional features and, as well as, to add different types of 
data sources (e.g., proper databases) and user interaction (e.g., WinForms, WPF Forms or Web 
interfaces).

2. The numerical calculations are performed using the C# decimal data type (although, the final 
financial values are represented as positive integer numbers).

3. In the payslip information, the pay period is represented as “dd MMM – dd MMM” corresponding to 
the first and last days of the target month. Note: The target month information is obtained from 
the “payment start date” from the employee’s record.

4. The reference data source is stored in a CSV file named BasicReferenceData.csv. All the 
employees’ records used by the MYOB.PayslipConsoleApp come from this file. 

5. In the process of loading the BasicReferenceData.csv data source, the application filters out 
the employees’ records that are not valid.

6. The payslip information (once requested) is displayed on the terminal and saved in the CSV 
file PayslipData.csv.

---------------------------------------------------------------------------------------------------

Distribution --------------------------------------------------------------------------------------

All the files necessary to run and build the MYOB.PayslipConsoleApp are distributed in the ZIP file
MYOB.zip. Once you unzip this file, you will obtain the following folder structure:

+ MYOB -+
		|
		+ MYOB.Application
		|
		+ MYOB.Controllers
		|
		+ MYOB.Core
		|
		+ MYOB.Data
		|
		+ MYOB.Data.Tests
		|
		+ MYOB.Models
		|
		+ MYOB.PayslipConsoleApp
		|
		+ MYOB.Views
		|
		+ packages
		|
		+ TestResults

The application can be found in the MYOB.Application folder (as well as, the CSV data source). The
MS Visual Studio Solution (MYOB.sln) is at the root (\MYOB) of the folder structure.

---------------------------------------------------------------------------------------------------

Contact -------------------------------------------------------------------------------------------

Any questions or comments, just contact:

Nick Lehtola (DRNL)

E nick.lehtola@gmail.com | M 027 381 9337 | L https://www.linkedin.com/in/nick-lehtola-12b79796

---------------------------------------------------------------------------------------------------

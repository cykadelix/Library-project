Team 11 README File:
Account Logins:
	Student accounts:
Username: sarah@gmail.com // this account has an overdue balance to test the overdue trigger
Password: 123
	
Username: hatem@gmail.com
Password: 123

	Admin account:
Username: abe@gmail.com
Password: 123

	Employee Account:
Username: jake@gmail.com
Password: 123

Controllers: 
These are used to control the views that take on the logic of the application. In the controllers are functions that correspond with the views with the same name as the function and in the folder that shares a name with the controller. The views are what the actual user sees. These views are filled out using ViewModels that are the objects sent to and from the view. The controller  fills out these view models for the views to use. 
	
How to check out items:
Login or register
Go to the explore page after logging in
Press the “checkout” button 
	
      // You can try to check out more than 5 items to test the trigger.

How to return items:
go to Dashboard
Go to checkouts
Press the unchecked box next to red items to return them

How to put items on hold:
Login as a user
Go to the explore page
Find an item that has the “hold item” button
Press that button



How to checkout from hold:
Login as user
Go to my holds in the dashboard
Press the unchecked box . If the box is checked then that item is unavailable

How to add an employee:
Login as a manager
Go to dashboard 
Go to Manage employees
Fill out the form at the top and hit submit

To edit an employee
Login as manager and go to dashboard > manage employees
Press the edit button in the table of employees 
The form will fill out their user info
Change the information and hit submit

How to generate reports:
Login as an admin
Go to dashboard
Go to Report Generator
Select either Checkouts, Average Rating, or Student Checkouts
If Checkouts, specify a time frame and click generate report
If Average Rating, enter an item id and click generate report
If Student Checkouts, enter a student’s library card number and click generate report
To go to another report, click the dropdown menu in the top left and click a different report.
 
How to manage a student: 
Login as an admin
Go to dashboard
Click Manage Students
From this page you can either create a new student, delete a current student, or edit a current student's information. 
To create a new student, fill out the form with the new student’s info and click submit
To edit a current student, navigate to the student’s record in the data table below the create student form and click the pen icon at the end of the student’s record. Edit the student’s information and click save to save the changes to the student.
To delete a current student, navigate to the student’s record in the data table below the create student form and click the trash can icon to delete the student. 

How to manage employee:
Login as admin
 Go to dashboard 
 click Manage employees 
 From this page you can create, edit or delete employees.


How to add media:
Login as employee
Go to inventory tab in the dashboard
Select the type you would like to create from the box
Fill out the form and hit submit
How to add reviews
Login as a user
Go to the dashboard
Go to add review
Fill out the form and hit submit

How to edit admin information:
Login as admin
Go to dashboard
Go to settings

How to pay balance:
Login as user
Go to dashboard
Click on balance
Enter amount to pay and click submit
Note: This page does not show how much the overdue balance is, so the overdue balance is defaulted to 5. Therefore, enter 5 and click submit, then you should be able to check out items. Test the trigger first before paying the overdue balance


Installation Steps:

Clone the repository into Visual Studio or any other IDE that supports .sln files
Navigate to the master-2 branch and build the project.
Browse the master-2 branch to view our project. 

	




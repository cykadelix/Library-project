# Team 11 README File

## Account Logins:
### Student accounts:
- Username: sarah@gmail.com  (this account has an overdue balance to test the overdue trigger)
- Password: 123

- Username: hatem@gmail.com
- Password: 123

### Admin account:
- Username: abe@gmail.com
- Password: 123

### Employee Account:
- Username: jake@gmail.com
- Password: 123

## Controllers
These are used to control the views that take on the logic of the application. In the controllers are functions that correspond with the views with the same name as the function and in the folder that shares a name with the controller. The views are what the actual user sees. These views are filled out using ViewModels that are the objects sent to and from the view. The controller fills out these view models for the views to use.

## How to check out items:
1. Login or register
2. Go to the explore page after logging in
3. Press the “checkout” button

Note: You can try to check out more than 5 items to test the trigger.

## How to return items:
1. Go to Dashboard
2. Go to checkouts
3. Press the unchecked box next to red items to return them

## How to put items on hold:
1. Login as a user
2. Go to the explore page
3. Find an item that has the “hold item” button
4. Press that button

## How to checkout from hold:
1. Login as user
2. Go to my holds in the dashboard
3. Press the unchecked box.

Note: If the box is checked then that item is unavailable.

## How to add an employee:
1. Login as a manager
2. Go to dashboard
3. Go to Manage employees
4. Fill out the form at the top and hit submit

## How to edit/delete an employee:
1. Login as manager and go to dashboard. Click manage employees
2. Press the edit button in the table of employees represented as a pen icon
3. The form will fill out their user info
4. Change the information and hit submit
5. To delete an employee, click the trash icon instead of the pen icon and they will become inactive. 

## How to generate reports:
1. Login as an admin
2. Go to dashboard
3. Go to Report Generator
4. Select either Checkouts, Average Rating, or Student Checkouts
   - If Checkouts, specify a time frame and click generate report
   - If Average Rating, enter an item id and click generate report
   - If Student Checkouts, enter a student’s library card number and click generate report
5. To go to another report, click the dropdown menu in the top left and click a different report.

## How to manage a student:
1. Login as an admin
2. Go to dashboard
3. Click Manage Students
4. From this page you can either create a new student, delete a current student, or edit a current student's information.
   - To create a new student, fill out the form with the new student’s info and click submit
   - To edit a current student, navigate to the student’s record in the data table below the create student form and click the pen icon at the end of the student’s record. Edit the student’s information and click save to save the changes to the student.
   - To delete a current student, navigate to the student’s record in the data table below the create student form and click the trash can icon to delete the student.

## How to add media:
1. Login as employee
2. Go to inventory tab in the dashboard
3. Select the type you would like to create from the box
4. Fill out the form and hit submit

##How to add reviews
1. Login as a user
2. Go to the dashboard
3. Go to add review
4. Fill out the form and hit submit

##How to edit admin information:
1. Login as admin
2. Go to dashboard
3. Go to settings

##How to pay balance:
1. Login as user
2. Go to dashboard
3. Click on balance
4. Enter amount to pay and click submit
Note: This page does not show how much the overdue balance is, so the overdue balance is defaulted to 5. Therefore, enter 5 and click submit, then you should be able to check out items. Test the trigger first before paying the overdue balance


##Installation Steps:
1. Clone the repository into Visual Studio or any other IDE that supports .sln files
2. Navigate to the master-2 branch and build the project.
3. Browse the master-2 branch to view our project. 


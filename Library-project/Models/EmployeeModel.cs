using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EmployeeModel
{
    //Employee Name
    public string fName { get; set; }
    public string mName { get; set; }
    public string lName { get; set; }

    //Employee Data
    [Key] public int employeeID { get; set; }
    public int supervisorID { get; set; }
    public string position { get; set; }
    public float salary { get; set; }           //float or int?
    public short age { get; set; }

    //Personal Info
    public string eMail { get; set; }
    public string password { get; set; }
    public string homeAddress { get; set; }
    public int phoneNumber { get; set; }        //should this be an int?



    //Constructor
    public EmployeeModel() { }

}
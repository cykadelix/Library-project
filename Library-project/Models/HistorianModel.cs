using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HistorianModel{
    //Historian Name
    public string fName { get; set; }
    public string mName { get; set; }
    public string lName { get; set; }
    
    //Historian Data
    [Key] public int historianID { get; set; }
    public string expertise { get; set; }
    public string education { get; set; }
    public short age { get; set; }

    /*
    //Historian Availability
    public string[] availableDaysOfWeek { get; set; }       //how are we structuring this?
    public string[] availableTimesOfDays { get; set; }      //how are we structuring this?
    */

    //Constructor
    public HistorianModel() { }

}
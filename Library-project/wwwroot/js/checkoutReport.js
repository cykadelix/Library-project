$("#report-type").on("change", function () {
    let selectedType = $(this).val();

    if (selectedType == "Checkouts") {
        window.location.href = '../Reports/CheckoutsByDateIndex';
    } else if (selectedType == "Employees") {
        window.location.href = '../Reports/EmployeesByDateIndex';
    } else if (selectedType == "Students") {
        window.location.href = '../Reports/StudentsByDateIndex';
    }
});


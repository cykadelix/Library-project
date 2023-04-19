$("#report-type").on("change", function () {
    let selectedType = $(this).val();

    if (selectedType == "Checkouts") {
        window.location.href = '../Reports/CheckoutsByDateIndex';
    } else if (selectedType == "Ratings") {
        window.location.href = '../Reports/AverageRatingIndex';
    } else if (selectedType == "Students") {
        window.location.href = '../Reports/StudentsByDateIndex';
    }
});


$("#media-type").on("change", function () {
    let selectedType = $(this).val();

    if (selectedType == "Computer") {
        mediaType = "Computer";
        window.location.href = '../AddMedia/AddComputer';
    } else if (selectedType == "Camera") {
        mediaType = "Camera";
        window.location.href = '../AddMedia/AddCamera';
    } else if (selectedType == "Projector") {
        mediaType = "Projector";
        window.location.href = '../AddMedia/AddProjector';
    } else if (selectedType == "Book") {
        mediaType = "Book";
        window.location.href = '../AddMedia/AddBook';
    } else if (selectedType == "Journal") {
        $('.media-entry-form').not('#journal-entry-form').hide();
        $('#journal-entry-form').show();

    } else if (selectedType == "Movie") {
        $('.media-entry-form').not('#movie-entry-form').hide();
        $('#movie-entry-form').show();

    } else if (selectedType == "Audiobook") {
        $('.media-entry-form').not('#audiobook-entry-form').hide();
        $('#audiobook-entry-form').show();

    }
});

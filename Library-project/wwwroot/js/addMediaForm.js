$("#media-type").on("change", function () {
    let selectedType = $(this).val();

    if (selectedType == "Computer") {
        $('.media-entry-form').not('#computer-entry-form').hide();
        $('#computer-entry-form').show();
    } else if (selectedType == "Camera") {
        $('.media-entry-form').not('#camera-entry-form').hide();
        $('#camera-entry-form').show();

    } else if (selectedType == "Projector") {
        $('.media-entry-form').not('#projector-entry-form').hide();
        $('#projector-entry-form').show();

    } else if (selectedType == "Book") {
        $('.media-entry-form').not('#book-entry-form').hide();
        $('#book-entry-form').show();

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

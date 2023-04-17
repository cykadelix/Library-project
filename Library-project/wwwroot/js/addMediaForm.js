$("#media-type").on("change", function () {
    let selectedType = $(this).val();

    if (selectedType == "Computer") {
        window.location.href = '../AddMedia/AddComputer';
    } else if (selectedType == "Camera") {
        window.location.href = '../AddMedia/AddCamera';
    } else if (selectedType == "Projector") {
        window.location.href = '../AddMedia/AddProjector';
    } else if (selectedType == "Book") {
        window.location.href = '../AddMedia/AddBook';
    } else if (selectedType == "Journal") {
        window.location.href = '../AddMedia/AddJournal';
    } else if (selectedType == "Movie") {
        window.location.href = '../AddMedia/AddMovie';
    } else if (selectedType == "Audiobook") {
        window.location.href = '../AddMedia/AddAudiobook';
    }
});


let mediaType = "book";

$('#explore-navbar div ul li a').on('click', function () {
    $('#explore-navbar div ul li').removeClass('active-b');
    $(this).parent().addClass('active-b');
});

$('#camera-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetCameras',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "camera";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

$('#computer-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetComputers',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "computer";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
})

$('#projector-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetProjectors',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "projector";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

$('#book-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetBooks',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "book";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

$('#journal-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetJournals',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "journal";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

$('#movie-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetMovies',
        asyc: false,
        success: function (result) {
            $('#media-partial-placeholder').html(result);
            mediaType = "movie";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

$('#audiobook-explore-btn').on('click', function () {
    $('.media-display-container').height('auto');
    $.ajax({
        type: 'GET',
        url: '/Explore/GetAudiobooks',
        asyc: false,
        success: function (result) {
            console.log(result);
            $('#media-partial-placeholder').html(result);
            mediaType = "audiobook";

            loadRandomImages();
            resizeExploreContainer();
        }
    });
});

function resizeExploreContainer() {
    let $images = $('.media-item-img');
    let loadedImagesCount = 0;

    $images.on('load', (function () {
        loadedImagesCount++;
        if (loadedImagesCount >= 3 && loadedImagesCount == $images.length) {
            //Set fixed height for explore container
            let h = $('.media-display-container').height();
            $('.media-display-container').height(h * (3/5));
        }
    }));
}

function loadRandomImages() {
    let i = 0;
    document.querySelectorAll('.media-item-img').forEach(function (displayItem) {
        displayItem.src = "/images/" + mediaType + "/" + mediaType + ((i++ % 5) + 1) + ".jpg";
    })
}
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

            loadImages();
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

            loadImages();
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

            loadImages();
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

            loadImages();
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

            loadImages();
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

            loadImages();
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
            $('#media-partial-placeholder').html(result);
            mediaType = "audiobook";

            loadImages();
            resizeExploreContainer();
        }
    });
});

$(document).on('click', '.media-checkout-btn', function () {
    let dataToSend = {"studentid": 13, "mediaid": 53};
    let elementClicked = $(this);

    let result;
    $.ajax({
        type: 'POST',
        url: '/Checkout/CreateCheckout',
        asyc: false,
        data: dataToSend,
        success: function (data) {
            result = data;
            if (result == "") {
                showSuccess(elementClicked);
            } else {
                showError(elementClicked, result);
            }
        }
    });
});

function showSuccess(element) {
    element.find('.checkout-success-text').show();
    element.closest('.media-display-item').delay(3000).slideUp(1000);

}

function showError(element, message) {
    element.find('.checkout-fail-text').text("Checkout failed" + message.substring(5));
    element.find('.checkout-fail-text').show();
    element.find('.checkout-fail-text').effect("shake", { distance: 3 });
}

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

function loadImages() {
    let i = 0;
    document.querySelectorAll('.media-item-img').forEach(function (displayItem) {
        displayItem.src = "images/media/" + mediaType + $(displayItem).data('id') + ".jpg";
    })
}

$(document).on('click', '.media-display-item', function (e) {
    if (!$(e.target).is("a") && !$(e.target).is("h3")) {
        clickToExapndCards($(this));
    }
});


function clickToExapndCards($obj) {
    let clickedElement = $obj;
    if (clickedElement.hasClass('expanded')) {
        clickedElement.find('.description-placeholder').slideUp(250);
        clickedElement.find('.media-description').slideDown(500);
        clickedElement.removeClass('expanded');
    } else {
        clickedElement.find('.media-description').slideUp(500);
        clickedElement.find('.description-placeholder').slideDown(250);
        clickedElement.addClass('expanded');
    }

};

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

            
            resizeExploreContainer();
        }
    });
});

$(document).on('click', '.media-checkout-btn', function () {
    let dataToSend = {"studentid": 13};
    let elementClicked = $(this);
    dataToSend.mediaid = elementClicked.parent().siblings('.media-item-img').data('id');

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

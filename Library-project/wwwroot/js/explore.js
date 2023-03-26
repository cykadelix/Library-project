$('#explore-navbar div ul li a').on('click', function () {
    $('#explore-navbar div ul li').removeClass('active-b');
    $(this).parent().addClass('active-b');
});

$('#camera-explore-btn').on('click', function () {
    $.ajax({
        type: 'GET',
        url: '/Explore/GetCameras',
        success: function (result) {
            $('#partial-placeholder').html(result);
        }
    });
})
$('#explore-navbar div ul li a').on('click', function () {
    $('#explore-navbar div ul li').removeClass('active-b');
    $(this).parent().addClass('active-b');
});
$(function () {
    loginValidate();
    initMenu();
    toggleAdminInfo();
    initEditor();
    initAvatarBox();
    initResponsive();
    initVideoPreview();

    if ($.isFunction('owlCarousel')) {
        $(".owl-carousel").owlCarousel({
            items: 1,
            loop: true,
            nav: true,
            dots: true,
            autoplay: true,
            autoplayTimeout: 3000,
            autoplayHoverPause: true,
            animateIn: "fadeIn",
            animateOut: "fadeOut",
            responsive: {
                0: {
                    items: 1,
                    nav: false
                },
                992: {
                    items: 2,
                    nav: true
                },
                1200: {
                    items: 3,
                    nav: true
                }
            }
        });
    }

    $(window).resize(function () {
        initResponsive();
    });

    $(".img-upload").change(function () {
        readURL(this);
    });

    $(".dasboard-list a").blinklink({
        colorThree: "#FB1FFB",
        colorLast: ""
    });
});

function loginValidate() {
    $(".login-form-box .a-login-btn").click(function () {
        var usernameInput = $(".login-form-box .input-username");
        var passwordInput = $(".login-form-box .input-password");
        var alertBox = $(".login-form-box .div-alert");

        if (usernameInput.val() == "") {
            alertBox.find(".message").html("Hãy nhập username");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            usernameInput.focus();
            return false;
        }

        if (passwordInput.val() == "") {
            alertBox.find(".message").html("Hãy nhập mật khẩu");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            passwordInput.focus();
            return false;
        }

        return true;
    });
}

function initMenu() {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar, #content').toggleClass('active');
        $(this).toggleClass('active');

        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
}

function toggleAdminInfo() {
    $("#sidebar .btn-collapse").click(function (e) {
        $("#sidebar .content").slideToggle(500);
        e.preventDefault();
    });
}

function initEditor() {
    tinymce.init({
        selector: '.editor',
        menubar: false,
        statusbar: false,
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | preview save print | insertfile image media template link anchor codesample | ltr rtl',
        plugins: 'print preview fullpage paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap emoticons autoresize',
    });
}

function readURL(input) {
    var container = $(input).closest(".avatar-box");
    container.find('.img-preview').fadeOut();
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            container.find('.img-preview').attr('src', e.target.result);
            container.find('.img-data').val(e.target.result);
            container.find('.img-preview').closest("a").attr('href', e.target.result);
            container.find('.img-preview').fadeIn(500);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function initAvatarBox() {
    var container = $(".avatar-container");
    var defaultUrl = "/Content/images/no-image.png";

    var linkBox = container.find(".link-box");
    var avatarBox = container.find(".avatar-box");
    avatarBox.find(".close").click(function () {
        $(this).closest(".avatar-box").find("img").attr("src", defaultUrl);
        $(this).closest(".avatar-box").find("img").closest("a").attr("href", defaultUrl);
        $(this).closest(".avatar-box").find("input").val("");
        linkBox.find("input").val("");
    });

    linkBox.find("button").click(function () {
        avatarBox.find("img").attr("src", defaultUrl);
        avatarBox.find("input").val("");
        linkBox.find("input").val("");
    });
    linkBox.find("input").on("input", function () {
        var value = $(this).val();
        avatarBox.find("img").attr("src", value);
        avatarBox.find("input").val("");
    });

    avatarBox.find("img").on("error", function () {
        $(this).attr("src", defaultUrl);
        return true;
    })
}

function initResponsive() {
    var width = $(window).width();

    if (width < 975) {
        $(".select-category-main").attr("size", "1");
    }
    else {
        $(".select-category-main").attr("size", "20");
    }
}

function getYoutubeID(url) {
    url = url.split(/(vi\/|v=|\/v\/|youtu\.be\/|\/embed\/)/);
    return (url[2] !== undefined) ? url[2].split(/[^0-9a-z_\-]/i)[0] : url[0];
}

function initVideoPreview() {
    var input = $(".video-url");
    var iframe = $(".video-preview");
    var img = $(".video-avatar");

    input.on("input", function () {
        var url = $(this).val();
        var id = getYoutubeID(url);
        var newUrl = "http://www.youtube.com/embed/" + id;
        var avatarUrl = "https://i.ytimg.com/vi/" + id + "/0.jpg";
        iframe.attr("src", newUrl);
        img.attr("src", avatarUrl);
    });

    input.keyup(function () {
        var url = $(this).val();
        var id = getYoutubeID(url);
        if (id == "" || id == url)
            return;
        var newUrl = "http://www.youtube.com/embed/" + id;
        input.val(newUrl);
    });
}
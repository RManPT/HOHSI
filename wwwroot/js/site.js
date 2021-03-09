// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// you can use app's unique identifier here


//shows the body only when all elements are rendered
/*$(window).load(function () {
    $("body").fadeIn("slow");
});*/



//adds a loading spinner until body fully loaded
$('body').show();
$(window).on('load', function () {
    $("#spinner").fadeOut(100, function () {
        $("#spinner").remove(); //Remove the loading div to make page more lightweight
    });
});

//dropdown menu
const $dropdown = $(".dropdown");
const $dropdownToggle = $(".dropdown-toggle");
const $dropdownMenu = $(".dropdown-menu");
const showClass = "show";

$(window).on("load resize", function () {
    if (this.matchMedia("(min-width: 768px)").matches) {
        $dropdown.hover(
            function () {
                const $this = $(this);
                $this.addClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "true");
                $this.find($dropdownMenu).addClass(showClass);
            },
            function () {
                const $this = $(this);
                $this.removeClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "false");
                $this.find($dropdownMenu).removeClass(showClass);
            }
        );
    } else {
        $dropdown.off("mouseenter mouseleave");
    }
});

//datepicker

$(function () {
    $('#datetimepicker').datetimepicker({
       //format: 'LT'  //time only
        //format: 'L'//date only
        locale: 'pt',
        format: "DD/MM/YYYY hh:mm",
        inline: true,
        sideBySide: true
    });
});

//auto search box
var searchValue = $('#searchString').val();
$(function () {
    setTimeout(checkSearchChanged, 0.1);
});

function checkSearchChanged() {
    var currentValue = $('#searchString').val();
    if ((currentValue) && currentValue != searchValue && currentValue != '') {
        searchValue = $('#searchString').val();
        $('#searchSubmit').click();
    }
    else {
        setTimeout(checkSearchChanged, 0.1);
    }
}
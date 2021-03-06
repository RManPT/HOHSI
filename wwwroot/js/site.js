﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// you can use app's unique identifier here

$("#theme-toggler").click(function () {
    toggleTheme();
});

const LOCAL_STORAGE_KEY = "toggle-bootstrap-theme";
const LOCAL_META_DATA = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY));
// you can change this url as needed
const DARK_THEME_PATH = "/css/dark-theme.css";
const DARK_STYLE_LINK = document.getElementById("dark-theme-style");
const THEME_TOGGLER = document.getElementById("theme-toggler");
let isDark = LOCAL_META_DATA && LOCAL_META_DATA.isDark;

// check if user has already selected dark theme earlier
if (isDark) {
    enableDarkTheme();
} else {
    disableDarkTheme();
}


/**
 * Apart from toggling themes, this will also store user's theme preference in local storage.
 * So when user visits next time, we can load the same theme.
 *
 */
function toggleTheme() {
    isDark = !isDark;
    if (isDark) {
        enableDarkTheme();
    } else {
        disableDarkTheme();
    }
    const META = { isDark };
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(META));
}

function enableDarkTheme() {
    DARK_STYLE_LINK.setAttribute("href", DARK_THEME_PATH);
    THEME_TOGGLER.innerHTML = "Theme B";
}

function disableDarkTheme() {
    DARK_STYLE_LINK.setAttribute("href", "/lib/bootstrap/dist/css/bootstrap.css");
    THEME_TOGGLER.innerHTML = "Theme A";
}


/*$(window).load(function () {
    $("body").fadeIn("slow");
});*/

//shows the body only when all elements are rendered
$(document).ready(function () {
    $('body').show();
});

/*

//toggles navbar buttons


// Close the dropdown if the user clicks outside of it
$(document).click(function (e) {
    console.log($("#myDropdown"))
    $(".myDropdown").each(function () {
        if ($(this)[0].classList.contains('show')) {
            console.log("did");
            $(this)[0].classList.remove('show');

        }
    });
    console.log("click");
});

$('.dropbtn').click(function () {
    $(this).parent().find('div')[0].classList.toggle("show");
});
*/

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
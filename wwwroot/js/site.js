// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.switch').click(() => {
    toggleTheme()
    })


//toggles theme and stores user preference
const LOCAL_STORAGE_KEY = "toggle-bootstrap-theme"; const LOCAL_META_DATA = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY));
const DARK_THEME_PATH = "/css/dark-theme.css"; const DARK_STYLE_LINK = document.getElementById("dark-theme-style");
const THEME_TOGGLER = document.getElementById("theme-toggler"); let isDark = LOCAL_META_DATA && LOCAL_META_DATA.isDark;

if (isDark) {
    enableDarkTheme();
} else {
    disableDarkTheme();
}

function toggleTheme() {
    isDark = !isDark;
    if (isDark) {
        enableDarkTheme();
    } else {
        disableDarkTheme();
    }
    const META = { isDark };
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(META));
} function enableDarkTheme() {
    DARK_STYLE_LINK.setAttribute("href", DARK_THEME_PATH);
    THEME_TOGGLER.innerHTML = "Theme A";
} function disableDarkTheme() {
    DARK_STYLE_LINK.setAttribute("href", "/lib/bootstrap/dist/css/bootstrap.min.css");
    THEME_TOGGLER.innerHTML = "Theme B";
}

$(function () {
    $('body').show();
}); // end ready
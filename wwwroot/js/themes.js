$("#theme-toggler").click(function () {
    toggleTheme();
});

//allows direct manipulation to document localstorage object
const LOCAL_STORAGE_KEY = "toggle-bootstrap-theme";
const LOCAL_META_DATA = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY));
// theme toggler definitions
const ALT_THEME_PATH = "/css/alt-theme.css";
const ALT_STYLE_LINK = document.getElementById("alt-theme-style");
const THEME_TOGGLER = document.getElementById("theme-toggler");
let isAlt = LOCAL_META_DATA && LOCAL_META_DATA.isAlt;

// check if user has already selected alternate theme earlier
if (isAlt) {
    enableAltTheme();
} else {
    disableAltTheme();
}


/**
 * Apart from toggling themes, this will also store user's theme preference in local storage.
 * So when user visits next time, we can load the same theme.
 *
 */
function toggleTheme() {
    isAlt = !isAlt;
    if (isAlt) {
        enableAltTheme();
    } else {
        disableAltTheme();
    }
    const META = { isAlt };
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(META));
}

function enableAltTheme() {
    ALT_STYLE_LINK.setAttribute("href", ALT_THEME_PATH);
    THEME_TOGGLER.innerHTML = "Theme B";
}

function disableAltTheme() {
    ALT_STYLE_LINK.setAttribute("href", "/lib/bootstrap/dist/css/bootstrap.css");
    THEME_TOGGLER.innerHTML = "Theme A";
}

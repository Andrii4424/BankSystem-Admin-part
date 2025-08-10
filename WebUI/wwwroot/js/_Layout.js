const openMenu = document.getElementById("open-menu");
const closeMenu = document.getElementById("close-menu");
const menu = document.getElementById("menu");
const languageName = document.getElementById("language-name");
const choseLanguageMenu = document.getElementById("chose-language");


openMenu.addEventListener("click", () => {
    openMenu.classList.add("clicked");
    closeMenu.classList.remove("clicked");
    menu.classList.add("opened");
});

closeMenu.addEventListener("click", () => {
    openMenu.classList.remove("clicked");
    closeMenu.classList.add("clicked");
    menu.classList.remove("opened");
});

//Language
languageName.addEventListener("click", () => {
    choseLanguageMenu.classList.toggle("opened");
});

document.body.addEventListener("click", (event) => {
    if (!event.target.closest("#language")) {
        choseLanguageMenu.classList.remove("opened");
    }
});

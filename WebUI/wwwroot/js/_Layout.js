const openMenu = document.getElementById("open-menu");
const closeMenu = document.getElementById("close-menu");
const menu = document.getElementById("menu");



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
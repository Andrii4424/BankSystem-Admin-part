const menu = document.getElementById("menu");
const openMenu = document.getElementById("open-menu");
const closeMenu = document.getElementById("close-menu");

openMenu.addEventListener("click", () => {
    menu.classList.add("opened");
    openMenu.classList.add("clicked");
    closeMenu.classList.remove("clicked");
});


closeMenu.addEventListener("click", () => {
    menu.classList.remove("opened");
    openMenu.classList.remove("clicked");
    closeMenu.classList.add("clicked");
});
const openMenuIcon = document.getElementById("open-menu");
const openMenuBlock = document.getElementById("open-menu-block");
const closeMenuIcon = document.getElementById("close-menu");
const menuBackground = document.getElementById("open-menu-background");
const openMenuIcons = document.getElementById("open-menu-page");
const iconBlock = document.getElementById("icon-block");
const menuContent = document.getElementById("menu-content");




iconBlock.addEventListener("click", OpenMenu);


function OpenMenu() {
    openMenuIcon.classList.toggle("clicked");
    closeMenuIcon.classList.toggle("clicked");
    menuBackground.classList.toggle("opened");
    openMenuIcons.classList.toggle("opened");
    menuContent.classList.toggle("opened");
    /*openMenuIcon.style.display = "none";
    openMenuBlock.style.display = "none";

    closeMenuIcon.style.display = "inline"*/
}

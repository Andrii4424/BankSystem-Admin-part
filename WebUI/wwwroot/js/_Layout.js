const openMenuIcon = document.getElementById("open-menu");
const openMenuBlock = document.getElementById("open-menu-block");
const closeMenuIcon = document.getElementById("close-menu");

openMenuBlock.addEventListener("click", OpenMenu);

function OpenMenu() {
    openMenuIcon.classList.toggle("clicked");
    closeMenuIcon.classList.toggle("clicked");
    /*openMenuIcon.style.display = "none";
    openMenuBlock.style.display = "none";

    closeMenuIcon.style.display = "inline"*/
}
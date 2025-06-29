const openMenuIcon = document.getElementById("open-menu");
const openMenuBlock = document.getElementById("open-menu-block");
const closeMenuIcon = document.getElementById("close-menu");


openMenuIcon.addEventListener("click", OpenMenu);
openMenuBlock.addEventListener("click", OpenMenu);

function OpenMenu() {
    openMenuIcon.style.display = "none";
    openMenuBlock.style.display = "none";

    closeMenuIcon.style.display = "inline"
}
const oneColumn = document.getElementById("one-column");
const twoColumns = document.getElementById("two-colums");

oneColumn.addEventListener("click", () => {
    oneColumn.classList.add("chosen");
    twoColumns.classList.remove("chosen");
});

twoColumns.addEventListener("click", () => {
    twoColumns.classList.add("chosen");
    oneColumn.classList.remove("chosen");
});
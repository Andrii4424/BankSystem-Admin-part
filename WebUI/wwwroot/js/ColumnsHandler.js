const oneColumn = document.getElementById("one-column");
const twoColumns = document.getElementById("two-columns");
const tableBlock = document.getElementById("elements-block");


oneColumn.addEventListener("click", () => {
    oneColumn.classList.add("chosen");
    twoColumns.classList.remove("chosen");
    tableBlock.classList.remove("two-columns");
});

twoColumns.addEventListener("click", () => {
    twoColumns.classList.add("chosen");
    oneColumn.classList.remove("chosen");
    tableBlock.classList.add("two-columns");
});


import { GeneralListMethods } from "./GeneralListMethods.js";

const sortIcon = document.getElementById("sort-icon");
const filterIcon = document.getElementById("filter-icon");
const sortList = document.getElementById("banks-sort-list");
const filterSettings = GeneralListMethods.filterList.querySelectorAll('input[type="checkbox"][name="filter"]');

//Default chekings when DOM update
GeneralListMethods.checkAndApplyColumns();

filterSettings.forEach(button => {
    const inputValue = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${button.value}"]`);
    if (inputValue !== null && button.checked) {
        inputValue.disabled = false;
    }
});

//Event listeners
GeneralListMethods.oneColumn.addEventListener("click", () => {
    GeneralListMethods.oneColumn.classList.add("chosen");
    GeneralListMethods.twoColumns.classList.remove("chosen");
    GeneralListMethods.elementsListBlock.classList.remove("two-columns");
});

GeneralListMethods.twoColumns.addEventListener("click", () => {
    GeneralListMethods.twoColumns.classList.add("chosen");
    GeneralListMethods.oneColumn.classList.remove("chosen");
    GeneralListMethods.elementsListBlock.classList.add("two-columns");
    GeneralListMethods.CheckLastColumn();
});

filterIcon.addEventListener("click", () => {
    GeneralListMethods.filterList.classList.toggle("opened");
    sortList.classList.remove("opened");
});

sortIcon.addEventListener("click", () => {
    sortList.classList.toggle("opened");
    GeneralListMethods.filterList.classList.remove("opened");
});

filterSettings.forEach(button => {
    button.addEventListener("change", () => {
        const inputValue = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${button.value}"]`);
        if (inputValue !== null) {
            inputValue.value = null;
            inputValue.disabled = !button.checked;
        }
    });
});

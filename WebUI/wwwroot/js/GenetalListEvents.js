import { GeneralListMethods } from "./GeneralListMethods.js";

const sortIcon = document.getElementById("sort-icon");
const filterIcon = document.getElementById("filter-icon");
const sortList = document.getElementById("banks-sort-list");
const filterSettings = GeneralListMethods.filterList.querySelectorAll('input[type="checkbox"][name="filter"]');

//Event listeners
GeneralListMethods.EmptyListTitleChecker();

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


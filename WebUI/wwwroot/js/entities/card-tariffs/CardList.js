import { GeneralListMethods } from "../../GeneralListMethods.js";

const submitFilters = document.querySelectorAll(".filter-submit-button");
const sortValue = document.querySelectorAll('input[name="SortValue"]');
const form = document.getElementById("search");
const loadMoreButton = document.querySelector(".load-more");
const firstElementFilter = document.querySelector('input[name="FirstElement"]');
const elementsToLoadFilter = document.querySelector('input[name="ElementsToLoad"]');
const searchByBankCheckbox = document.querySelector(".search-by-bank");
const searchByBankInput = document.querySelector(".search-by-bank-input");

elementsToLoadFilter.value = 12;

loadButtonChecker();
CheckAndChangeTextColor();
GeneralListMethods.EmptyListTitleChecker("Card");

searchByBankCheckbox.addEventListener("change", () => {
    searchByBankInput.disabled = !searchByBankCheckbox.checked;
    if (!searchByBankCheckbox.checked) {
        searchByBankInput.value = null;
    }
});

submitFilters.forEach(button => {
    button.addEventListener("click", async () => {
        await GetCards();
    });
});
sortValue.forEach(orderBy => {
    orderBy.addEventListener("change", async () => {
        await GetCards();
    });
});

async function GetCards() {
    firstElementFilter.value = 0;
    const formData = new FormData(form)
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    const response = await fetch("/get-card-tariffs", {
        method: "POST",
        body: formData
    })
    await GeneralListMethods.AddElementsToEnd(response);
    GeneralListMethods.EmptyListTitleChecker("Card");
    loadButtonChecker();
    CheckAndChangeTextColor();
}

loadMoreButton.addEventListener("click", async () => {
    firstElementFilter.value = GeneralListMethods.GetElementsCount();
    elementsToLoadFilter.value = 12;
    const formData = new FormData(form)
    const response = await fetch(`/get-card-tariffs`, {
        method: "POST",
        body: formData
    });

    await GeneralListMethods.AddElementsToEnd(response);
    elementsToLoadFilter.value = GeneralListMethods.GetElementsCount();
    loadButtonChecker();
    CheckAndChangeTextColor();
})


function loadButtonChecker() {
    const listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
    if (GeneralListMethods.GetElementsCount() >= listCount) {
        loadMoreButton.style.display = "none";
    }
    else {
        loadMoreButton.style.display = "block";
    }
}


function CheckAndChangeTextColor() {
    document.querySelectorAll(".element-block").forEach(el => {
        const style = window.getComputedStyle(el);
        const bg = style.backgroundColor;

        const rgbMatch = bg.match(/\d+/g);
        if (!rgbMatch || rgbMatch.length < 3) return;

        const r = parseInt(rgbMatch[0]).toString(16).padStart(2, '0');
        const g = parseInt(rgbMatch[1]).toString(16).padStart(2, '0');
        const b = parseInt(rgbMatch[2]).toString(16).padStart(2, '0');
        const hex = `${r}${g}${b}`;

        if (isDarkColor(hex)) {
            el.style.color = "white";
        } else {
            el.style.color = "black";
        }
    });
}

//For changing text color when background is dark
function isDarkColor(hexColor) {
    if (!hexColor) return false;

    hexColor = hexColor.replace("#", "");
    if (hexColor.length === 3) {
        hexColor = hexColor.split("").map(c => c + c).join("");
    }

    if (hexColor.length !== 6) return false;

    const r = parseInt(hexColor.substring(0, 2), 16);
    const g = parseInt(hexColor.substring(2, 4), 16);
    const b = parseInt(hexColor.substring(4, 6), 16);

    const brightness = 0.299 * r + 0.587 * g + 0.114 * b;

    return brightness < 128;
}


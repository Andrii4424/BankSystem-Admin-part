import { GeneralListMethods } from "../../GeneralListMethods.js";

const submitFilters = document.querySelectorAll(".filter-submit-button");
const sortValue = document.querySelectorAll('input[name="SortValue"]');
const form = document.getElementById("search");
const loadMoreButton = document.querySelector(".load-more");
const firstElementFilter = document.querySelector('input[name="FirstElement"]');
const elementsToLoadFilter = document.querySelector('input[name="ElementsToLoad"]');
const searchByBankCheckbox = document.querySelector(".search-by-bank");
const searchByBankInput = document.querySelector(".search-by-bank-input");
const deleteButtons = document.querySelectorAll(".delete-element");
let listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;


elementsToLoadFilter.value = 12;

loadButtonChecker();
CheckAndChangeTextColor();
GeneralListMethods.EmptyListTitleChecker("Card");
//Filtration and pagination
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
    listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
}

loadMoreButton.addEventListener("click", async () => {
    firstElementFilter.value = GeneralListMethods.GetElementsCount();
    elementsToLoadFilter.value = 12;
    listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
    const response = await loadItems();

    await GeneralListMethods.AddElementsToEnd(response);
    elementsToLoadFilter.value = GeneralListMethods.GetElementsCount();
    loadButtonChecker();
    CheckAndChangeTextColor();
})
async function loadItems() {
    const formData = new FormData(form)
    const response = await fetch(`/get-card-tariffs`, {
        method: "POST",
        body: formData
    });

    return response;
}



function loadButtonChecker() {
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
//Когда элемент удаляется, новая кнопка удаления не появляется в списке, надо решить проблему и доделать удаление

//Deleting
GeneralListMethods.elementsListBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        const id = event.target.dataset.elementId;
        await deleteItem(id);
        event.target.closest(".element-block").remove();

        //Replace deleting item
        firstElementFilter.value = GeneralListMethods.GetElementsCount();
        elementsToLoadFilter.value = 1;
        listCount--;
        const itemToReplace = await loadItems();
        await GeneralListMethods.AddElementsToEnd(itemToReplace);

        //Checkers after delete
        elementsToLoadFilter.value = GeneralListMethods.GetElementsCount();
        loadButtonChecker();
        GeneralListMethods.EmptyListTitleChecker("Card");
        GeneralListMethods.checkAndApplyColumns();
    }
});


async function deleteItem(id) {
    const result = await fetch(`/DeleteCard/${id}`, {
        method: "DELETE"
    });

    return result.ok;
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


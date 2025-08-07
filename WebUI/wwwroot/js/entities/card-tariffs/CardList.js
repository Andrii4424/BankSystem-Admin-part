import { GeneralListMethods } from "../../GeneralListMethods.js";

const submitFilters = document.querySelectorAll(".filter-submit-button");
const sortValue = document.querySelectorAll('input[name="SortValue"]');
const form = document.getElementById("search");
const loadMoreButton = document.querySelector(".load-more");
const firstElementFilter = document.querySelector('input[name="FirstElement"]');
const elementsToLoadFilter = document.querySelector('input[name="ElementsToLoad"]');
const searchByBankCheckbox = document.querySelector(".search-by-bank");
const searchByBankInput = document.querySelector(".search-by-bank-input");
let listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
let chosenDeleteButton;

elementsToLoadFilter.value = 12;

loadButtonChecker();
GeneralListMethods.CheckAndChangeTextColor();
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
    GeneralListMethods.CheckAndChangeTextColor();
    listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
    loadButtonChecker();
    GeneralListMethods.checkAndApplyColumns();
}

loadMoreButton.addEventListener("click", async () => {
    firstElementFilter.value = GeneralListMethods.GetElementsCount();
    elementsToLoadFilter.value = 12;
    listCount = document.querySelectorAll(".elements-count")[document.querySelectorAll(".elements-count").length - 1].dataset.elementsListCount;
    const response = await loadItems();

    await GeneralListMethods.AddElementsToEnd(response);
    elementsToLoadFilter.value = GeneralListMethods.GetElementsCount();
    loadButtonChecker();
    GeneralListMethods.CheckAndChangeTextColor();
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

//Deleting
//Open confirmation window
GeneralListMethods.elementsListBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        chosenDeleteButton = event.target;
        GeneralListMethods.OpenConfirmDeleteWindow(`${chosenDeleteButton.dataset.elementName} card`);
    }
});

GeneralListMethods.deleteWindow.addEventListener("click", async (event) => {
    if (event.target.matches(".confirm-delete-button")) {
        const id = chosenDeleteButton.dataset.elementId;
        await deleteItem(id);
        chosenDeleteButton.closest(".element-block").remove();

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

        GeneralListMethods.CloseConfirmDeleteWindow();
    }
    else if (!event.target.closest("#delete-window")) {         //Close delete confitmation window when user click on another part of the screen
        GeneralListMethods.CloseConfirmDeleteWindow();
    }
    else if (event.target.matches(".cancel-delete-button")) {
        GeneralListMethods.CloseConfirmDeleteWindow();
        chosenDeleteButton = null;
    }
});


async function deleteItem(id) {
    const result = await fetch(`/DeleteCard/${id}`, {
        method: "DELETE"
    });

    return result.ok;
}
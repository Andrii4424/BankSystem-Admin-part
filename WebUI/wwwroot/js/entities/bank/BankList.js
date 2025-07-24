import { GeneralListMethods } from "../../GeneralListMethods.js";

//General elements
let deletedElementsCount=0;

//Load elements
const loadBanks = document.getElementById("load-banks");
const addBank = document.querySelector(".add-element");
const searchButton = document.getElementById("search-button");
const submitFilters = document.getElementById("submit-filters");

//Sort elements
let oldOrderMethod = GeneralListMethods.elementsListBlock.dataset.orderMethod;
let oldSearchValue = GeneralListMethods.elementsListBlock.dataset.searchValue;
let oldFilters = JSON.parse(GeneralListMethods.elementsListBlock.dataset.filters);

//Filter elements
const inputRating = document.querySelector('.rating');
const clientsCount = document.querySelector('.clients-count');
const capitalization = document.querySelector('.capitalization');


//Delete elements
let button; //Last clicked delete button

//Input settings
inputRating.addEventListener("input", () => {
    if (inputRating.value > 5) inputRating.value = 5;
    if (inputRating.value < 1) inputRating.value = null;
    if (inputRating.value.length > 3) {
        inputRating.value = inputRating.value.slice(0, 3);
    }
});
clientsCount.addEventListener("input", () => {
    if (clientsCount.value < 0) clientsCount.value = null;
});
capitalization.addEventListener("input", () => {
    if (capitalization.value < 0) capitalization.value = null;
});

//General methods
LoadButtonChecker();

async function LoadItems(firstElement, itemsToLoad) {
    const url = `/load-banks/${firstElement}/${itemsToLoad}/${GeneralListMethods.GetSearchUrl()}/${GeneralListMethods.GetSortUrl()}${getBankFiltersUrl()}`
    const response = await fetch(url, {
        method: "GET"
    });
    await GeneralListMethods.AddElementsToEnd(response);
    LoadButtonChecker();
    GeneralListMethods.checkAndApplyColumns();
    GeneralListMethods.EmptyListTitleChecker("Bank");
}

//Filters sort and search handlers
async function UpdateItems(firstElement, itemsToLoad) {
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    await LoadItems(firstElement, itemsToLoad);
}

GeneralListMethods.radioInputs.forEach(async function (button) {
    button.addEventListener("change", async () => {
        await UpdateItems(0, GeneralListMethods.GetElementsAddCount());
    });
});

submitFilters.addEventListener("click", async () => {
    await UpdateItems(0, GeneralListMethods.GetElementsAddCount());
});

searchButton.addEventListener("click", async () => {
    await UpdateItems(0, GeneralListMethods.GetElementsAddCount());
});


function getBankFiltersUrl() {
    const filters = GeneralListMethods.filterList.querySelectorAll(`input[type="checkbox"][name="filter"]`);
    let url = "";
    GeneralListMethods.filterList.querySelectorAll(`input[type="checkbox"][class="value-filter"]`).forEach(filter => {
        if (filter.checked) {
            let filterValue = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value;
            filter.checked = (filterValue !== null && filterValue!=="");
        }
    });

    filters.forEach(filter => {
        switch (filter.value) {
            case "license-required":
                url += "/" + filter.checked;
                break;
            case "site-required":
                url += "/" + filter.checked;
                break;
            case "rating":
                filter.checked ? url += "/" + GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "clients-count":
                filter.checked ? url += "/" + GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "capitalization":
                filter.checked ? url += "/" + GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            default:
                break;
        }
    })
    return url;
}

//Pagination methods
function LoadButtonChecker() {
    //This method choses last partial view block with count of all list elements, if partial view doesnt exist it 
    //take basic view elements value with startup value(without filters)
    const partialBlockLength = document.querySelectorAll(".partial-counter")[document.querySelectorAll(".partial-counter").length-1];
    let allItemsCount;
    if (partialBlockLength !== undefined) allItemsCount = parseInt(partialBlockLength.dataset.elementsCount);
    else allItemsCount = parseInt(GeneralListMethods.elementsListBlock.dataset.elementsListCount)-deletedElementsCount;
    const elementsListCount = GeneralListMethods.GetElementsCount();
    if (allItemsCount === elementsListCount) {
        loadBanks.style.display = "none";
    } else if (elementsListCount === 0) {
        loadBanks.style.display = "none";
    } else {
        loadBanks.style.display = "block";
    }
    GeneralListMethods.CheckLastColumn();
}

//Loads old filters when user back from update/add element page
if (oldOrderMethod !== null && oldOrderMethod !== undefined && oldOrderMethod !=="") {
    GeneralListMethods.radioInputs.forEach(button => {
        if (button.value === oldOrderMethod) {
            button.checked = true;
        }
    });
}
if (oldSearchValue !== null && oldSearchValue != "" && oldSearchValue!="0") {
    GeneralListMethods.searchInput.value = oldSearchValue;
    GeneralListMethods.lastSearch = oldSearchValue;
    LoadButtonChecker();
}

if (oldFilters !== null && oldFilters !== undefined) {
    document.querySelector(`input[type="checkbox"][name="filter"][value="license-required"]`).checked = GeneralListMethods.ToBoolean(oldFilters["LicenseFilter"]);
    document.querySelector(`input[type="checkbox"][name="filter"][value="site-required"]`).checked = GeneralListMethods.ToBoolean(oldFilters["SiteFilter"]);
    if (GeneralListMethods.ToBoolean(oldFilters["RatingFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="rating"]`).checked = true; 
        let ratingValue = oldFilters["RatingFilter"].length > 1 ? oldFilters["RatingFilter"] / 10: oldFilters["RatingFilter"];
        document.querySelector(`input[type="number"][name="filter"][class="rating"]`).value = ratingValue;
    }
    if (GeneralListMethods.ToBoolean(oldFilters["ClientsCountFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="clients-count"]`).checked = true;
        document.querySelector(`input[type="number"][name="filter"][class="clients-count"]`).value = oldFilters["ClientsCountFilter"];
    }
    if (GeneralListMethods.ToBoolean(oldFilters["CapitalizationFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="capitalization"]`).checked = true;
        document.querySelector(`input[type="number"][name="filter"][class="capitalization"]`).value = oldFilters["CapitalizationFilter"];
    }
    LoadButtonChecker();
}
//Pagination
loadBanks.addEventListener("click", async () => {
    await LoadItems(GeneralListMethods.GetElementsCount(), 6);
    LoadButtonChecker();
});

//Filter saving when user back from add bank page 
addBank.addEventListener("click", () => {
    const countHiddenInput = document.getElementById("add-bank-input");
    const searchHiddenInput = document.getElementById("add-bank-input-search");
    const orderHiddenInput = document.getElementById("add-bank-input-method");
    
    const licenseHiddenInput = document.getElementById("add-bank-input-license-filter");
    const siteHiddenInput = document.getElementById("add-bank-input-site-filter");
    const ratingHiddenInput = document.getElementById("add-bank-input-rating-filter");
    const clientsCountHiddenInput = document.getElementById("add-bank-input-clients-filter");
    const capitalizationHiddenInput = document.getElementById("add-bank-input-capitalization-filter");

    setHiddenBankFiltersInputValues(countHiddenInput, searchHiddenInput, orderHiddenInput, licenseHiddenInput, siteHiddenInput,
        ratingHiddenInput, clientsCountHiddenInput, capitalizationHiddenInput);
});

//Filter saving when user back from update bank page
GeneralListMethods.elementsListBlock.addEventListener("click", (event) => {
    if (event.target.matches(".update-element")) {
        const countHiddenInput = event.target.closest("form").querySelector(".update-bank-input");
        const searchHiddenInput = event.target.closest("form").querySelector(".update-bank-input-search");
        const orderHiddenInput = event.target.closest("form").querySelector(".update-bank-input-method");

        const licenseHiddenInput = event.target.closest("form").querySelector(".update-bank-input-license-filter");
        const siteHiddenInput = event.target.closest("form").querySelector(".update-bank-input-site-filter");
        const ratingHiddenInput = event.target.closest("form").querySelector(".update-bank-input-rating-filter");
        const clientsCountHiddenInput = event.target.closest("form").querySelector(".update-bank-input-clients-filter");
        const capitalizationHiddenInput = event.target.closest("form").querySelector(".update-bank-input-capitalization-filter");

        setHiddenBankFiltersInputValues(countHiddenInput, searchHiddenInput, orderHiddenInput, licenseHiddenInput, siteHiddenInput,
            ratingHiddenInput, clientsCountHiddenInput, capitalizationHiddenInput);
    }

})

function setHiddenBankFiltersInputValues(countHiddenInput, searchHiddenInput, orderHiddenInput, licenseHiddenInput, siteHiddenInput,
    ratingHiddenInput, clientsCountHiddenInput, capitalizationHiddenInput) {

    countHiddenInput.value = GeneralListMethods.GetElementsCount() >= 6 ? GeneralListMethods.GetElementsCount(): 6;
    searchHiddenInput.value = GeneralListMethods.lastSearch !== ""? GeneralListMethods.GetLastSearch(): "0";
    orderHiddenInput.value = GeneralListMethods.GetSortUrl();

    const filters = GeneralListMethods.filterList.querySelectorAll(`input[type="checkbox"][name="filter"]`);

    filters.forEach(filter => {
        switch (filter.value) {
            case "license-required":
                licenseHiddenInput.value =  filter.checked;
                break;
            case "site-required":
                siteHiddenInput.value = filter.checked;
                break;
            case "rating":
                filter.checked ? ratingHiddenInput.value = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : ratingHiddenInput.value = "0";
                break;
            case "clients-count":
                filter.checked ? clientsCountHiddenInput.value = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : clientsCountHiddenInput.value = "0";
                break;
            case "capitalization":
                filter.checked ? capitalizationHiddenInput.value = GeneralListMethods.filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : capitalizationHiddenInput.value = "0";
                break;
            default:
                break;
        }
    })
}

//Deleting elements
//Decraese when element is deleting
GeneralListMethods.elementsListBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        button = event.target;
        console.log(GeneralListMethods);

        GeneralListMethods.OpenConfirmDeleteWindow(button.dataset.elementName);
    }
});

GeneralListMethods.deleteWindow.addEventListener("click", async (event) => {
    if (event.target.matches(".confirm-delete-button")) {
        const id = button.dataset.elementId;

        deletedElementsCount++;
        await DeleteItem(`/delete-bank/bank-id/${id}/first-element/${GeneralListMethods.GetElementsCount()}/${GeneralListMethods.GetSearchUrl()}
        /${GeneralListMethods.GetSortUrl()}${getBankFiltersUrl()}`);
        button.closest(".element-block").remove();

        LoadButtonChecker();
        GeneralListMethods.EmptyListTitleChecker("Bank");
        GeneralListMethods.checkAndApplyColumns();

        GeneralListMethods.CloseConfirmDeleteWindow();
    }
    else if (!event.target.closest("#delete-window")) {         //Close delete confitmation window when user click on another part of the screen
        GeneralListMethods.CloseConfirmDeleteWindow();
    }
    else if (event.target.matches(".cancel-delete-button")) {
        GeneralListMethods.CloseConfirmDeleteWindow();
        button = null;
    }
});

async function DeleteItem(url) {
    const replacedElement = await fetch(url, {
        method: "POST"
    });

    await GeneralListMethods.AddElementsToEnd(replacedElement);
}

GeneralListMethods.EmptyListTitleChecker("Bank");

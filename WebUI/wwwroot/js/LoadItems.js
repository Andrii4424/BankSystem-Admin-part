//General elements
let deletedElementsCount=0;

//Load elements
//General DOM elements
const elementsListBlock = document.getElementById("elements-block");
const sortButton = document.getElementById("sort-icon");
const filterButton = document.getElementById("filter-icon");
const submitFilters = document.getElementById("submit-filters");
const searchInput = document.getElementById("search-input");
const searchButton = document.getElementById("search-button");
const emptyListTitle = document.getElementById("empty-list-title");
//Bank general elements
const loadBanks = document.getElementById("load-banks");
const addBank = document.querySelector(".add-element");
let count = document.querySelectorAll(".element-table").length;
let elementsListCount = parseInt(elementsListBlock.dataset.elementsListCount);

//Sort elements
//Bank elements
const sortList = document.getElementById("banks-sort-list");
const radioInputs = document.querySelectorAll('input[name="sort"]');
let oldOrderMethod = elementsListBlock.dataset.orderMethod;
let oldSearchValue = elementsListBlock.dataset.searchValue;
let oldFilters = JSON.parse(elementsListBlock.dataset.filters);

//Filter elements
//Bank filters
const filterList = document.getElementById("banks-filter-list");
const filterSettings = filterList.querySelectorAll('input[type="checkbox"][name="filter"]');
const inputRating = document.querySelector('.rating');

//Input settings
//Bank inputs
inputRating.addEventListener('input', () => {
    if (inputRating.value > 5) inputRating.value = 5;
    if (inputRating.value < 1) inputRating.value = null;
    if (inputRating.value.length > 3) {
        inputRating.value = inputRating.value.slice(0, 3);
    }
});

//Icons handlers
//Sort and filrer icons
filterButton.addEventListener("click", () => {
    filterList.classList.toggle("opened");
    sortList.classList.remove("opened");
});

sortButton.addEventListener("click", () => {
    sortList.classList.toggle("opened");
    filterList.classList.remove("opened");
});


//General methods
EmptyListTitleChecker();
function EmptyListTitleChecker() {
    count = document.querySelectorAll(".element-table").length;
    if (count === 0) { emptyListTitle.style.display = "block" }
    else {emptyListTitle.style.display="none" }
}

async function addElementsToEnd(response) {
    elementsListBlock.insertAdjacentHTML("beforeend", await response.text());
}
function GetElementsCount() {
    return (document.querySelectorAll(".element-table").length) > 6 ? document.querySelectorAll(".element-table").length: 6;
}

//Filters sort and search handlers
function getSortUrl() {
    return document.querySelector('input[name="sort"]:checked').value;
}
function getSearchUrl() {
    return (searchInput.value !== "" && searchInput.value !== null) ? searchInput.value : "0";
}

//Bank handlers
async function UpdateItems(firstElement, itemsToLoad) {
    document.querySelectorAll(".element-block").forEach(element => element.remove());
    await LoadItems(firstElement, itemsToLoad);
}
//Request handlers
radioInputs.forEach(async function (button) {
    button.addEventListener("change", async () => {
        await UpdateItems(0, GetElementsCount());
    });
});

submitFilters.addEventListener("click", async () => {
    await UpdateItems(0, GetElementsCount());
});

searchButton.addEventListener("click", async () => {
    await UpdateItems(0, GetElementsCount());
});


//Additional bank filtration handler methods
filterSettings.forEach(button => {
    button.addEventListener("change", () => {
        const inputValue = filterList.querySelector(`input[type="number"][class="${button.value}"]`);
        if (inputValue !== null) {
            inputValue.value = null;
            inputValue.disabled = !button.checked;
        }
    });
});


function getBankFiltersUrl() {
    const filters = filterList.querySelectorAll(`input[type="checkbox"][name="filter"]`);
    let url = "";
    filterList.querySelectorAll(`input[type="checkbox"][class="value-filter"]`).forEach(filter => {
        if (filter.checked) {
            let filterValue = filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value;
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
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "clients-count":
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            case "capitalization":
                filter.checked ? url += "/" + filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : url += "/0";
                break;
            default:
                break;
        }
    })
    return url;
}


//Pagination methods
//Bank methods
if (elementsListCount == count) {
    loadBanks.style.display = "none";
}

async function LoadItems(firstElement, itemsToLoad) {
    const url = `/load-banks/${firstElement}/${itemsToLoad}/${getSearchUrl()}/${getSortUrl()}${getBankFiltersUrl()}`
    const response = await fetch(url, {
        method: "GET"
    });
    await addElementsToEnd(response);
    LoadButtonChecker();
    EmptyListTitleChecker();
}


function LoadButtonChecker() {
    //This method choses last partial view block with count of all list elements, if partial view doesnt exist it 
    //take basic view elements value with startup value(without filters)
    const partialBlockLength = document.querySelectorAll(".partial-counter")[document.querySelectorAll(".partial-counter").length-1];
    let allItemsCount;
    if (partialBlockLength !== undefined) allItemsCount = parseInt(partialBlockLength.dataset.elementsCount);
    else allItemsCount = parseInt(elementsListBlock.dataset.elementsListCount)-deletedElementsCount;
    const elementsListCount = document.querySelectorAll(".element-table").length;
    if (allItemsCount === elementsListCount) {
        loadBanks.style.display = "none";
    } else if (elementsListCount === 0) {
        loadBanks.style.display = "none";
    } else {
        loadBanks.style.display = "block";
    }
}


//Loads old filters when user back from update/add element page
if (oldOrderMethod !== null && oldOrderMethod !== undefined && oldOrderMethod !=="") {
    radioInputs.forEach(button => {
        if (button.value === oldOrderMethod) {
            button.checked = true;
        }
    });
}
if (oldSearchValue !== null && oldSearchValue != "" && oldSearchValue!="0") {
    searchInput.value = oldSearchValue;
    LoadButtonChecker();
}

if (oldFilters !== null && oldFilters !== undefined) {
    document.querySelector(`input[type="checkbox"][name="filter"][value="license-required"]`).checked = ToBoolean(oldFilters["LicenseFilter"]);
    document.querySelector(`input[type="checkbox"][name="filter"][value="site-required"]`).checked = ToBoolean(oldFilters["SiteFilter"]);
    if (ToBoolean(oldFilters["RatingFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="rating"]`).checked = true; 
        let ratingValue = oldFilters["RatingFilter"].length > 1 ? oldFilters["RatingFilter"] / 10: oldFilters["RatingFilter"];
        document.querySelector(`input[type="number"][name="filter"][class="rating"]`).value = ratingValue;
    }
    if (ToBoolean(oldFilters["ClientsCountFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="clients-count"]`).checked = true;
        document.querySelector(`input[type="number"][name="filter"][class="clients-count"]`).value = oldFilters["ClientsCountFilter"];
    }
    if (ToBoolean(oldFilters["CapitalizationFilter"])) {
        document.querySelector(`input[type="checkbox"][name="filter"][value="capitalization"]`).checked = true;
        document.querySelector(`input[type="number"][name="filter"][class="capitalization"]`).value = oldFilters["CapitalizationFilter"];
    }
    LoadButtonChecker();
}

function ToBoolean(value) {
    if (value === "true" || value === "True" || value === "TRUE") {
        return true;
    }
    else if (parseInt(value)>0) {
        return true;
    }
    else {
        return false;
    }

}

loadBanks.addEventListener("click", async () => {
    count = document.querySelectorAll(".element-table").length;
    await LoadItems(count, 6);
    LoadButtonChecker();
});


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

function setHiddenBankFiltersInputValues(countHiddenInput, searchHiddenInput, orderHiddenInput, licenseHiddenInput, siteHiddenInput,
    ratingHiddenInput, clientsCountHiddenInput, capitalizationHiddenInput) {

    let count = document.querySelectorAll(".element-table").length;
    countHiddenInput.value = count;
    searchHiddenInput.value = getSearchUrl();
    orderHiddenInput.value = getSortUrl();

    const filters = filterList.querySelectorAll(`input[type="checkbox"][name="filter"]`);

    filters.forEach(filter => {
        switch (filter.value) {
            case "license-required":
                licenseHiddenInput.value =  filter.checked;
                break;
            case "site-required":
                siteHiddenInput.value = filter.checked;
                break;
            case "rating":
                filter.checked ? ratingHiddenInput.value = filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : ratingHiddenInput.value = "0";
                break;
            case "clients-count":
                filter.checked ? clientsCountHiddenInput.value = filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : clientsCountHiddenInput.value = "0";
                break;
            case "capitalization":
                filter.checked ? capitalizationHiddenInput.value = filterList.querySelector(`input[type="number"][class="${filter.value}"]`).value : capitalizationHiddenInput.value = "0";
                break;
            default:
                break;
        }
    })
}

elementsListBlock.addEventListener("click", (event) => {
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
//Deleting elements
//Decraese when element is deleting
elementsListBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        const button = event.target;
        const id = button.dataset.elementId;
        const count = document.querySelectorAll(".element-table").length;
        const sortMethod = document.querySelector('input[name="sort"]:checked').value;
        deletedElementsCount++;
        button.closest(".element-block").remove();
        await DeleteItem(`/delete-bank/bank-id/${id}/first-element/${count}/${getSearchUrl()}/${sortMethod}${getBankFiltersUrl()}`);
    }
});

async function DeleteItem(url) {
    const replacedElement = await fetch(url, {
        method: "POST"
    });

    await addElementsToEnd(replacedElement);
    LoadButtonChecker();
    EmptyListTitleChecker();
}


